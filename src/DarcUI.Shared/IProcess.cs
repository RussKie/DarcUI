﻿// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// Copyright (c) Git Extensions. All rights reserved.
// Borrowed from https://github.com/gitextensions/gitextensions

using System.Diagnostics;

namespace DarcUI;

/// <summary>
/// Defines a process instance.
/// </summary>
/// <remarks>
/// This process will either be running or exited.
/// </remarks>
public interface IProcess : IDisposable
{
    /// <summary>
    /// Gets an object that facilitates writing to the process's standard input stream.
    /// </summary>
    /// <remarks>
    /// To access the underlying <see cref="Stream"/>, dereference <see cref="StreamWriter.BaseStream"/>.
    /// </remarks>
    /// <exception cref="InvalidOperationException">This process's input was not redirected
    /// when calling <see cref="IExecutable.Start"/>.</exception>
    StreamWriter StandardInput { get; }

    /// <summary>
    /// Gets an object that facilitates writing to the process's standard output stream.
    /// </summary>
    /// <remarks>
    /// To access the underlying <see cref="Stream"/>, dereference <see cref="StreamWriter.BaseStream"/>.
    /// </remarks>
    /// <exception cref="InvalidOperationException">This process's output was not redirected
    /// when calling <see cref="IExecutable.Start"/>.</exception>
    StreamReader StandardOutput { get; }

    /// <summary>
    /// Gets an object that facilitates writing to the process's standard error stream.
    /// </summary>
    /// <remarks>
    /// To access the underlying <see cref="Stream"/>, dereference <see cref="StreamWriter.BaseStream"/>.
    /// </remarks>
    /// <exception cref="InvalidOperationException">This process's output was not redirected
    /// when calling <see cref="IExecutable.Start"/>.</exception>
    StreamReader StandardError { get; }

    /// <summary>
    ///  Immediately stops the associated process, and optionally its child/descendent processes.
    /// </summary>
    public void Kill();

    /// <summary>
    /// Returns a task that completes when the process exits, or when this object is disposed.
    /// </summary>
    /// <returns>A task that yields the process's exit code, or <c>null</c> if this object was disposed before the process exited.</returns>
    Task<int> WaitForExitAsync();

    /// <summary>
    /// Waits for the process to reach an idle state.
    /// </summary>
    /// <see cref="Process.WaitForInputIdle()"/>
    void WaitForInputIdle();
}
