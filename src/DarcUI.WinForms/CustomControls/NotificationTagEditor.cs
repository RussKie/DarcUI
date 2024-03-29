﻿// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace DarcUI.CustomControls;

public class NotificationTagEditor : UITypeEditor
{
    public const string User32 = "user32.dll";
    [DllImport(User32, ExactSpelling = true)]
    private static extern IntPtr GetFocus();
    [DllImport(User32, ExactSpelling = true, SetLastError = true)]
    public static extern IntPtr SetFocus(IntPtr hWnd);

    public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
    {
        SubscriptionProxy? subscription = context.Instance as SubscriptionProxy;
        if (subscription is null)
        {
            subscription = (context.Instance as ReadOnlySubscriptionProxy)?.WrappedObject as SubscriptionProxy;
        }

        if (subscription is null)
        {
            return value;
        }

        using ManageFailureNotifications dialog = new();
        dialog.SetContext(subscription);
        dialog.StartPosition = FormStartPosition.CenterParent;

        IntPtr hwndFocus = GetFocus();
        try
        {
            if (dialog.ShowDialog(new Win32WindowWrapper(hwndFocus)) == DialogResult.OK)
            {
                subscription.TokenFailureNotificationTags = dialog.TokenFailureNotificationTags.Trim().Trim('\'');
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

    private struct Win32WindowWrapper : IWin32Window
    {
        public Win32WindowWrapper(IntPtr hwnd)
        {
            Handle = hwnd;
        }

        public IntPtr Handle { get; }
    }
}
