// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// Copyright (c) Git Extensions. All rights reserved.
// Borrowed from https://github.com/gitextensions/gitextensions

using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace DarcUI
{
    public sealed class Executable
    {
        private readonly string _workingDir;
        private readonly Func<string> _fileNameProvider;

        public Executable(string fileName, string workingDir = "")
            : this(() => fileName, workingDir)
        {
        }

        public Executable(Func<string> fileNameProvider, string workingDir = "")
        {
            _workingDir = workingDir;
            _fileNameProvider = fileNameProvider;
        }

        /// <inheritdoc />
        public IProcess Start(string? arguments = default, bool createWindow = false, bool redirectInput = false, bool redirectOutput = false)
        {
            var fileName = _fileNameProvider();

            return new ProcessWrapper(fileName, arguments, _workingDir, createWindow, redirectInput, redirectOutput);
        }

        public string GetOutput(string arguments)
        {
            return Program.JoinableTaskFactory.Run(
                async () =>
                {
                    using (var process = Start(
                        arguments,
                        createWindow: false,
                        redirectInput: false,
                        redirectOutput: true))
                    {
                        var outputBuffer = new MemoryStream();
                        var errorBuffer = new MemoryStream();
                        var outputTask = process.StandardOutput.BaseStream.CopyToAsync(outputBuffer);
                        var errorTask = process.StandardError.BaseStream.CopyToAsync(errorBuffer);
                        var exitTask = process.WaitForExitAsync();

                        await Task.WhenAll(outputTask, errorTask, exitTask);

                        var output = outputBuffer.ToArray();
                        var error = errorBuffer.ToArray();

                        return DecodeString(output, error);
                    }
                });
        }

        private static string DecodeString(byte[] output, byte[] error)
        {
            string outputString = "";
            if (output != null && output.Length > 0)
            {
                Stream? ms = null;
                try
                {
                    ms = new MemoryStream(output);
                    using (var reader = new StreamReader(ms))
                    {
                        ms = null;
                        reader.Peek();
                        outputString = reader.ReadToEnd();
                        if (error == null || error.Length == 0)
                        {
                            return outputString;
                        }
                    }
                }
                finally
                {
                    ms?.Dispose();
                }

                outputString = outputString + Environment.NewLine;
            }

            string? errorString = null;
            if (error != null && error.Length > 0)
            {
                Stream? ms = null;
                try
                {
                    ms = new MemoryStream(error);
                    using (var reader = new StreamReader(ms))
                    {
                        ms = null;
                        reader.Peek();

                        errorString = reader.ReadToEnd();
                        if (output == null || output.Length == 0)
                        {
                            return errorString;
                        }
                    }
                }
                finally
                {
                    ms?.Dispose();
                }
            }

            return outputString + errorString;
        }

        #region ProcessWrapper

        /// <summary>
        /// Manages the lifetime of a process. The <see cref="System.Diagnostics.Process"/> object has many members
        /// that throw at different times in the lifecycle of the process, such as after it is disposed. This class
        /// provides a simplified API that meets the need of this application via the <see cref="IProcess"/> interface.
        /// </summary>
        private sealed class ProcessWrapper : IProcess
        {
            // TODO should this use TaskCreationOptions.RunContinuationsAsynchronously
            private readonly TaskCompletionSource<int> _exitTaskCompletionSource = new TaskCompletionSource<int>();

            private readonly object _syncRoot = new object();
            private readonly Process _process;
            private readonly bool _redirectInput;
            private readonly bool _redirectOutput;

            private bool _disposed;

            public ProcessWrapper(string fileName, string? arguments, string workDir, bool createWindow, bool redirectInput, bool redirectOutput)
            {
                _redirectInput = redirectInput;
                _redirectOutput = redirectOutput;

                _process = new Process
                {
                    EnableRaisingEvents = true,
                    StartInfo =
                    {
                        UseShellExecute = false,
                        ErrorDialog = false,
                        CreateNoWindow = !createWindow,
                        RedirectStandardInput = redirectInput,
                        RedirectStandardOutput = redirectOutput,
                        RedirectStandardError = redirectOutput,
                        FileName = fileName,
                        Arguments = arguments ?? string.Empty,
                        WorkingDirectory = workDir
                    }
                };

                _process.Exited += OnProcessExit;

                _process.Start();
            }

            private void OnProcessExit(object? sender, EventArgs eventArgs)
            {
                lock (_syncRoot)
                {
                    // The Exited event can be raised after the process is disposed, however
                    // if the Process is disposed then reading ExitCode will throw.
                    if (!_disposed)
                    {
                        var exitCode = _process.ExitCode;
                        _exitTaskCompletionSource.TrySetResult(exitCode);
                    }
                }
            }

            /// <inheritdoc />
            public StreamWriter StandardInput
            {
                get
                {
                    if (!_redirectInput)
                    {
                        throw new InvalidOperationException("Process was not created with redirected input.");
                    }

                    return _process.StandardInput;
                }
            }

            /// <inheritdoc />
            public StreamReader StandardOutput
            {
                get
                {
                    if (!_redirectOutput)
                    {
                        throw new InvalidOperationException("Process was not created with redirected output.");
                    }

                    return _process.StandardOutput;
                }
            }

            /// <inheritdoc />
            public StreamReader StandardError
            {
                get
                {
                    if (!_redirectOutput)
                    {
                        throw new InvalidOperationException("Process was not created with redirected output.");
                    }

                    return _process.StandardError;
                }
            }

            /// <inheritdoc />
            public void WaitForInputIdle() => _process.WaitForInputIdle();

            /// <inheritdoc />
            public Task<int> WaitForExitAsync() => _exitTaskCompletionSource.Task;

            /// <inheritdoc />
            public int WaitForExit()
            {
                return Program.JoinableTaskFactory.Run(() => WaitForExitAsync());
            }

            /// <inheritdoc />
            public void Dispose()
            {
                lock (_syncRoot)
                {
                    if (_disposed)
                    {
                        return;
                    }

                    _disposed = true;
                }

                _process.Exited -= OnProcessExit;

                _exitTaskCompletionSource.TrySetCanceled();

                _process.Dispose();
            }
        }

        #endregion
    }

}
