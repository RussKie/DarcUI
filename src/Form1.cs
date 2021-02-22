// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DarcUI
{
    public partial class Form1 : Form
    {
        private static readonly SubscriptionsParser s_subscriptionsParser = new SubscriptionsParser();
        private static readonly SubscriptionRetriever s_subscriptionsRetriever = new SubscriptionRetriever();
        private static List<Subscription> s_subscriptions;
        private GroupByOption _groupByOption = default;
        private readonly ImageList _imageList = new ImageList();

        public Form1()
        {
            InitializeComponent();

            _imageList.Images.Add(new Bitmap(1, 1)); // default
            _imageList.Images.Add(Properties.Resources.arrow_join); // target
            _imageList.Images.Add(Properties.Resources.arrow_split); // source
            _imageList.Images.Add(Properties.Resources.branch_document); // branch
            _imageList.Images.Add(Properties.Resources.block); // channel

            treeView1.ImageList = _imageList;

            BindSubscriptions();
        }

        private void ResetControls()
        {
            treeView1.Nodes.Clear();
        }

        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            BindSubscriptions();
        }

        private void BindSubscriptions()
        {
            try
            {
                treeView1.BeginUpdate();
                ResetControls();

                var output = ThreadHelper.JoinableTaskFactory.Run(s_subscriptionsRetriever.GetSubscriptions);
                if (string.IsNullOrWhiteSpace(output))
                {
                    return;
                }

                s_subscriptions = s_subscriptionsParser.Parse(output);
                BindSubscriptions(treeView1, s_subscriptions, _groupByOption);
            }
            finally
            {
                treeView1.EndUpdate();
            }
        }

        private void BindSubscriptions(TreeView treeView, List<Subscription> subscriptions, GroupByOption option)
        {
            switch (option)
            {
                case GroupByOption.RepoBranchChannelSource:
                    {
                        GroupByRepoBranchChannelSource();
                        return;
                    }

                case GroupByOption.ChannelSourceRepoBranch:
                default:
                    {
                        GroupByChannelSourceRepoBranch();
                        return;
                    }
            }

            void GroupByRepoBranchChannelSource()
            {
                foreach (IGrouping<string, Subscription> target in subscriptions.GroupBy(s => s.Target).OrderBy(s => s.Key))
                {
                    TreeNode targetNode = treeView.Nodes.Add(target.Key);
                    targetNode.ImageIndex = targetNode.SelectedImageIndex = 1;

                    foreach (IGrouping<string, Subscription> targetBranch in target.GroupBy(s => s.TargetBranch).OrderBy(s => s.Key))
                    {
                        TreeNode targetBranchNode = targetNode.Nodes.Add(targetBranch.Key);
                        targetBranchNode.ImageIndex = targetBranchNode.SelectedImageIndex = 3;

                        foreach (IGrouping<string, Subscription> channel in targetBranch.GroupBy(s => s.SourceChannel).OrderBy(s => s.Key))
                        {
                            TreeNode channelNode = targetBranchNode.Nodes.Add(channel.Key);
                            channelNode.ImageIndex = channelNode.SelectedImageIndex = 4;

                            foreach (Subscription subscription in channel.OrderBy(s => s.Source))
                            {
                                TreeNode sourceNode = channelNode.Nodes.Add(subscription.Source);
                                if (!subscription.Enabled)
                                {
                                    sourceNode.ForeColor = SystemColors.GrayText;
                                }

                                sourceNode.ImageIndex = sourceNode.SelectedImageIndex = 2;
                                sourceNode.Tag = subscription;
                            }
                        }
                    }
                }
            }

            void GroupByChannelSourceRepoBranch()
            {
                foreach (IGrouping<string, Subscription> channel in subscriptions.GroupBy(s => s.SourceChannel).OrderBy(s => s.Key))
                {
                    TreeNode channelNode = treeView.Nodes.Add(channel.Key);
                    channelNode.ImageIndex = channelNode.SelectedImageIndex = 4;

                    foreach (IGrouping<string, Subscription> source in channel.GroupBy(s => s.Source).OrderBy(s => s.Key))
                    {
                        TreeNode sourceNode = channelNode.Nodes.Add(source.Key);
                        sourceNode.ImageIndex = sourceNode.SelectedImageIndex = 2;

                        foreach (IGrouping<string, Subscription> target in source.GroupBy(s => s.Target).OrderBy(s => s.Key))
                        {
                            TreeNode targetNode = sourceNode.Nodes.Add(target.Key);
                            targetNode.ImageIndex = targetNode.SelectedImageIndex = 1;

                            foreach (Subscription subscription in target.OrderBy(s => s.TargetBranch))
                            {
                                TreeNode targetBranchNode = targetNode.Nodes.Add(subscription.TargetBranch);
                                if (!subscription.Enabled)
                                {
                                    targetBranchNode.ForeColor = SystemColors.GrayText;
                                }
                                targetBranchNode.ImageIndex = targetBranchNode.SelectedImageIndex = 3;
                                targetBranchNode.Tag = subscription;
                            }
                        }
                    }
                }
            }
        }

        private void groupByOption1_Click(object sender, EventArgs e)
        {
            if (_groupByOption == GroupByOption.ChannelSourceRepoBranch)
            {
                return;
            }

            _groupByOption = GroupByOption.ChannelSourceRepoBranch;
            BindSubscriptions();

            // TODO:
            groupByOption1.Checked = _groupByOption == GroupByOption.ChannelSourceRepoBranch;
            groupByOption2.Checked = _groupByOption == GroupByOption.RepoBranchChannelSource;
        }

        private void groupByOption2_Click(object sender, EventArgs e)
        {
            if (_groupByOption == GroupByOption.RepoBranchChannelSource)
            {
                return;
            }

            BindSubscriptions();
            _groupByOption = GroupByOption.RepoBranchChannelSource;

            // TODO:
            groupByOption1.Checked = _groupByOption == GroupByOption.ChannelSourceRepoBranch;
            groupByOption2.Checked = _groupByOption == GroupByOption.RepoBranchChannelSource;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag == null)
            {
                return;
            }

            propertyGrid1.SelectedObject = e.Node.Tag;
        }

        private enum GroupByOption
        {
            ChannelSourceRepoBranch,
            RepoBranchChannelSource
        }
    }
}
