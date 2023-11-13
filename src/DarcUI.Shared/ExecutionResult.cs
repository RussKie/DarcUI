// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;
using System.Text;

namespace DarcUI;

public sealed class ExecutionResult
{
    private Queue<ExecutionResult>? _chainedResults;

    public ExecutionResult(int exitCode, string input, string output, string error)
    {
        ExitCode = exitCode;
        Input = input;
        Output = output;
        Error = error;
    }

    public int ExitCode { get; }
    public string Input { get; }
    public string Output { get; }
    public string Error { get; }

    public void ChainWith(ExecutionResult result2)
    {
        _chainedResults ??= new();
        _chainedResults.Enqueue(result2);
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        Append(sb, this);

        if (_chainedResults is not null)
        {
            foreach (var result in _chainedResults)
            {
                sb.AppendLine("----");
                Append(sb, result);
            }
        }

        return sb.ToString();

        static void Append(StringBuilder builder, ExecutionResult result)
        {
            builder.AppendLine($"[Input] {result.Input}");

            if (!string.IsNullOrEmpty(result.Output))
            {
                builder.AppendLine($"[Output] {result.Output}");
            }

            if (!string.IsNullOrEmpty(result.Error))
            {
                builder.AppendLine($"[Error] {result.Error}");
            }
        }
    }
}
