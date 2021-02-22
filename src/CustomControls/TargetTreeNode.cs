// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Windows.Forms;

namespace DarcUI
{
    internal class TargetTreeNode : TreeNode
    {
        public TargetTreeNode(string text) : base(text)
        {
            ImageIndex = SelectedImageIndex = 1;
        }
    }
}
