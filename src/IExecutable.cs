// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// Copyright (c) Git Extensions. All rights reserved.
// Borrowed from https://github.com/gitextensions/gitextensions

namespace DarcUI
{
    /// <summary>
    /// Defines an executable that can be launched to create processes.
    /// </summary>
    public interface IExecutable
    {
        /// <summary>
        /// Starts a process of this executable.
        /// </summary>
        /// <remarks>
        /// This is a low level means of starting a process. Most code will want to use one of the extension methods
        /// provided by <c>ExecutableExtensions</c>.
        /// </remarks>
        /// <param name="arguments">Any command line arguments to be passed to the executable when it is started.</param>
        /// <param name="createWindow">Whether to create a window for the process or not.</param>
        /// <param name="redirectInput">Whether the standard input stream of the process will be written to.</param>
        /// <param name="redirectOutput">Whether the standard output stream of the process will be read from.</param>
        /// <returns>The started process.</returns>
        IProcess Start(string? arguments = default, bool createWindow = false, bool redirectInput = false, bool redirectOutput = false);

        /// <summary>
        /// Launches a process for the executable and returns its output.
        /// </summary>
        /// <param name="arguments">The arguments to pass to the executable</param>
        /// <returns>The concatenation of standard output and standard error.</returns>
        string GetOutput(string arguments);
    }

}
