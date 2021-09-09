// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarcUI.CustomControls
{
    public partial class CreateSubscription : Form
    {
        public CreateSubscription()
        {
            InitializeComponent();
        }

        public void SetContext(Subscription? subscription)
        {
            propertyGrid1.SelectedObject = subscription;
            btnOk.Enabled = subscription is null;
        }
    }
}
