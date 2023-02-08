// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DarcUI.CustomControls;

public partial class CreateSubscription : Form
{
    public CreateSubscription()
    {
        InitializeComponent();
    }

    public void SetContext(Subscription? subscription)
    {
        propertyGrid1.SelectedObject = subscription;
        btnOk.Enabled = subscription is not null;
    }
}
