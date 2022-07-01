// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Windows.Forms;

namespace DarcUI
{
    internal class SourceTreeNode : TreeNode
    {
        public SourceTreeNode(Subscription subscription)
            : this(subscription.Source)
        {
            Tag = subscription;

            if (!subscription.Enabled)
            {
                ForeColor = SystemColors.GrayText;
            }
        }

        public SourceTreeNode(string text) : base(text)
        {
            ImageIndex =
                SelectedImageIndex = 2;
        }
    }
}
