// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DarcUI.CustomControls;

public partial class ManageFailureNotifications : Form
{
    public ManageFailureNotifications()
    {
        InitializeComponent();
    }

    public string TokenFailureNotificationTags => txtTags.Text;

    public void SetContext(SubscriptionProxy subscription)
    {
        txtTags.Text = subscription.TokenFailureNotificationTags;
    }
}
