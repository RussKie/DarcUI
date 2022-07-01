// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Windows.Forms;

namespace DarcUI
{
    internal class TargetBranchNode : TreeNode
    {
        public TargetBranchNode(Subscription subscription)
          : this(subscription.Source)
        {
            Tag = (SubscriptionProxy?)subscription;

            if (!subscription.Enabled)
            {
                ForeColor = SystemColors.GrayText;
            }
        }

        public TargetBranchNode(string? text) : base(text)
        {
            ImageIndex = 
                SelectedImageIndex = 3;
        }
    }
}
