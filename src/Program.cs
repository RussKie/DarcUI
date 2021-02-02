// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualStudio.Threading;

namespace DarcUI
{
    public static class Program
    {
        internal static readonly JoinableTaskFactory JoinableTaskFactory = new JoinableTaskContext().Factory;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // This form created to obtain UI synchronization context only
            using (new Form())
            {
                // Store the shared JoinableTaskContext
                ThreadHelper.JoinableTaskContext = new JoinableTaskContext();
            }

            if (!Debugger.IsAttached)
            {
                AppDomain.CurrentDomain.UnhandledException += (s, e) => Report((Exception)e.ExceptionObject, e.IsTerminating);
                Application.ThreadException += (s, e) => Report(e.Exception, isTerminating: false);
            }

            Application.Run(new Form1());
        }

        private static void Report(Exception exception, bool isTerminating)
        {
            TaskDialogPage page = new()
            {
                AllowCancel = false,
                AllowMinimize = false,
                Caption = "Error",
                Icon = TaskDialogIcon.Error,
                SizeToContent = true,
                Text = exception.Message,
                Expander = new TaskDialogExpander
                {
                    Text = exception.Demystify().StackTrace,
                    CollapsedButtonText = "Show stack trace",
                    ExpandedButtonText = "Hide stack trace"
                }
            };

            if (isTerminating)
            {
                page.Buttons.Add("Terminate");
            }
            else
            {
                page.Buttons.Add(TaskDialogButton.OK);
            }

            Form owner = Application.OpenForms[0];
            if (TaskDialog.ShowDialog(owner, page) != TaskDialogButton.OK)
            {
                Environment.Exit(-1);
            }
        }
    }
}
