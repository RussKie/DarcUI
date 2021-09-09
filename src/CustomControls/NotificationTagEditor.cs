﻿// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System;
using System.Windows.Forms;

namespace DarcUI.CustomControls
{
    public class NotificationTagEditor : UITypeEditor
    {
        public const string User32 = "user32.dll";
        [DllImport(User32, ExactSpelling = true)]
        private static extern IntPtr GetFocus();
        [DllImport(User32, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context.Instance is not Subscription subscription)
            {
                return value;
            }

            using ManageFailureNotifications dialog = new();
            dialog.SetContext(subscription);

            IntPtr hwndFocus = GetFocus();
            try
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    subscription.TokenFailureNotificationTags = dialog.TokenFailureNotificationTags;
                    value = dialog.TokenFailureNotificationTags;
                }
            }
            finally
            {
                if (hwndFocus != IntPtr.Zero)
                {
                    SetFocus(hwndFocus);
                }
            }

            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) => UITypeEditorEditStyle.Modal;
    }
}
