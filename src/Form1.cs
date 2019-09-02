// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Windows.Forms;

namespace DarcUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSubscriptionRefresh_Click(object sender, EventArgs e)
        {
            var executable = new Executable("darc");
            var output = executable.GetOutput("get-subscriptions");
        }

    }

}
