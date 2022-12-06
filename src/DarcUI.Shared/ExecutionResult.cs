// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text;

namespace DarcUI
{
    public sealed class ExecutionResult
    {
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

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"[Input] {Input}");

            if (string.IsNullOrEmpty(Output))
            {
                sb.AppendLine($"[Output] {Output}");
            }

            if (string.IsNullOrEmpty(Error))
            {
                sb.AppendLine($"[Error] {Error}");
            }

            return sb.ToString();
        }
    }
}
