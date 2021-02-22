// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.Threading;

namespace DarcUI
{
    public partial class MainForm : Form
    {
        private static readonly SubscriptionsParser s_subscriptionsParser = new SubscriptionsParser();
        private static readonly SubscriptionRetriever s_subscriptionsRetriever = new SubscriptionRetriever();
        private static readonly SubscriptionUpdater s_subscriptionUpdater = new SubscriptionUpdater();
        private static List<Subscription>? s_subscriptions;
        private GroupByOption _groupByOption = default;
        private readonly ImageList _imageList = new ImageList();
        private readonly WaitSpinner _waitSpinner;

        public MainForm()
        {
            InitializeComponent();

            _imageList.Images.Add(new Bitmap(1, 1)); // default
            _imageList.Images.Add(Properties.Resources.arrow_join); // target
            _imageList.Images.Add(Properties.Resources.arrow_split); // source
            _imageList.Images.Add(Properties.Resources.branch_document); // branch
            _imageList.Images.Add(Properties.Resources.block); // channel

            treeView1.ImageList = _imageList;

            _waitSpinner = new WaitSpinner
            {
                BackColor = SystemColors.Window,
                //Visible = false,
                Size = new Size(50, 50) // DpiUtil.Scale(new Size(50, 50))
            };

            BindSubscriptions(forceReload: false);
        }

        private void BindSubscriptions(bool forceReload)
        {
            tsbtnRefresh.Enabled = false;
            treeView1.Nodes.Clear();
            propertyGrid1.SelectedObject = null;
            ShowSpinner(visible: true, hostControl: treeView1);

            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                try
                {
                    await TaskScheduler.Default;

                    var output = await s_subscriptionsRetriever.GetSubscriptionsAsync(forceReload);
                    if (string.IsNullOrWhiteSpace(output))
                    {
                        s_subscriptions = null;
                    }
                    else
                    {
                        s_subscriptions = s_subscriptionsParser.Parse(output);
                    }

                    await this.SwitchToMainThreadAsync();

                    treeView1.BeginUpdate();
                    BindSubscriptions(treeView1, s_subscriptions, _groupByOption);
                }
                finally
                {
                    await this.SwitchToMainThreadAsync();

                    ShowSpinner(visible: false);
                    treeView1.EndUpdate();
                    tsbtnRefresh.Enabled = true;
                }
            }).FileAndForget();
        }

        private void BindSubscriptions(TreeView treeView, List<Subscription>? subscriptions, GroupByOption option)
        {
            if (subscriptions is null)
            {
                return;
            }

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

        private void ShowSpinner(bool visible, Control? hostControl = null)
        {
            if (!visible)
            {
                Controls.Remove(_waitSpinner);
                return;
            }

            // If no specific control provided, show the spinner in the middle of the form
            hostControl ??= this;

            _waitSpinner.Location = new Point(hostControl.Left + (hostControl.Width - _waitSpinner.Width) / 2,
                                              hostControl.Top + (hostControl.Height - _waitSpinner.Height) / 2);
            Controls.Add(_waitSpinner);
            _waitSpinner.BringToFront();
        }

        private void groupByOption1_Click(object sender, EventArgs e)
        {
            if (_groupByOption == GroupByOption.RepoBranchChannelSource)
            {
                return;
            }

            _groupByOption = GroupByOption.RepoBranchChannelSource;
            BindSubscriptions(forceReload: false);

            groupByOption1.Checked = _groupByOption == GroupByOption.RepoBranchChannelSource;
            groupByOption2.Checked = _groupByOption == GroupByOption.ChannelSourceRepoBranch;
        }

        private void groupByOption2_Click(object sender, EventArgs e)
        {
            if (_groupByOption == GroupByOption.ChannelSourceRepoBranch)
            {
                return;
            }

            _groupByOption = GroupByOption.ChannelSourceRepoBranch;
            BindSubscriptions(forceReload: false);

            groupByOption1.Checked = _groupByOption == GroupByOption.RepoBranchChannelSource;
            groupByOption2.Checked = _groupByOption == GroupByOption.ChannelSourceRepoBranch;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Subscription? subscription = propertyGrid1.SelectedObject as Subscription;
            if (subscription is null)
            {
                return;
            }

            tsbtnRefresh.Enabled = false;
            tabControl.Enabled = false;
            ShowSpinner(visible: true, hostControl: propertyGrid1);

            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                try
                {
                    await TaskScheduler.Default;

                    await s_subscriptionUpdater.UpdateSubscriptionAsync(subscription, e.ChangedItem.Label);
                }
                finally
                {
                    await this.SwitchToMainThreadAsync();

                    ShowSpinner(visible: false);
                    tabControl.Enabled = true;
                    tsbtnRefresh.Enabled = true;
                }
            }).FileAndForget();
        }

        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            BindSubscriptions(forceReload: true);
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
            RepoBranchChannelSource,
            ChannelSourceRepoBranch,
        }
    }
}
