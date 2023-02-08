// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DarcUI;

internal class ChannelTreeNode : TreeNode
{
    public ChannelTreeNode(string channel) :
        base(channel)
    {
        ImageIndex =
            SelectedImageIndex = 4;
    }
}
