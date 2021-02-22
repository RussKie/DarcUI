// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.Packaging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.Threading;

namespace DarcUI
{
    public partial class MainForm : Form
    {
        private static readonly SubscriptionsParser s_subscriptionsParser = new();
        private static readonly SubscriptionRetriever s_subscriptionsRetriever = new();
        private static readonly SubscriptionManager s_subscriptionManager = new();
        private static List<Subscription>? s_subscriptions;
        private GroupByOption _groupByOption = default;
        private readonly ImageList _imageList = new ImageList();
        private readonly WaitSpinner _waitSpinner;
        private bool _operationInProgress;

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
            treeView1.Nodes.Clear();

            propertyGrid1.SelectedObject = null;
            propertyGrid1.AllowCreate = propertyGrid1.AllowDelete = false;

            InvokeAsync(hostControl: treeView1,
                asyncMethod: async () =>
                {
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
                },
                onCompleteMethod: () =>
                {
                    treeView1.EndUpdate();
                });
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
                    TargetTreeNode targetNode = new(target.Key);
                    treeView.Nodes.Add(targetNode);

                    foreach (IGrouping<string, Subscription> targetBranch in target.GroupBy(s => s.TargetBranch).OrderBy(s => s.Key))
                    {
                        TargetBranchNode targetBranchNode = new(targetBranch.Key);
                        targetNode.Nodes.Add(targetBranchNode);

                        foreach (IGrouping<string, Subscription> channel in targetBranch.GroupBy(s => s.SourceChannel).OrderBy(s => s.Key))
                        {
                            ChannelTreeNode channelNode = new(channel.Key);
                            targetBranchNode.Nodes.Add(channelNode);

                            foreach (Subscription subscription in channel.OrderBy(s => s.Source))
                            {
                                channelNode.Nodes.Add(new SourceTreeNode(subscription));
                            }
                        }
                    }
                }
            }

            void GroupByChannelSourceRepoBranch()
            {
                foreach (IGrouping<string, Subscription> channel in subscriptions.GroupBy(s => s.SourceChannel).OrderBy(s => s.Key))
                {
                    ChannelTreeNode channelNode = new(channel.Key);
                    treeView.Nodes.Add(channelNode);

                    foreach (IGrouping<string, Subscription> source in channel.GroupBy(s => s.Source).OrderBy(s => s.Key))
                    {
                        SourceTreeNode sourceNode = new(source.Key);
                        channelNode.Nodes.Add(sourceNode);

                        foreach (IGrouping<string, Subscription> target in source.GroupBy(s => s.Target).OrderBy(s => s.Key))
                        {
                            TargetTreeNode targetNode = new(target.Key);
                            sourceNode.Nodes.Add(targetNode);

                            foreach (Subscription subscription in target.OrderBy(s => s.TargetBranch))
                            {
                                targetNode.Nodes.Add(new TargetBranchNode(subscription));
                            }
                        }
                    }
                }
            }
        }

        private void InvokeAsync(Control hostControl, Func<Task> asyncMethod, Action? onCompleteMethod = null)
        {
            if (!_operationInProgress)
            {
                Debug.Assert(!_operationInProgress);
            }

            BeginOperation();

            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                try
                {
                    await asyncMethod();
                }
                finally
                {
                    await this.SwitchToMainThreadAsync();
                    EndOperation();
                }

                onCompleteMethod?.Invoke();
            }).FileAndForget();

            void BeginOperation()
            {
                _operationInProgress = true;
                tsbtnRefresh.Enabled = false;
                tabControl.Enabled = false;
                ShowSpinner(visible: true, hostControl);
            }

            void EndOperation()
            {
                _operationInProgress = false;
                ShowSpinner(visible: false);
                tabControl.Enabled = true;
                tsbtnRefresh.Enabled = true;
            }

            void ShowSpinner(bool visible, Control? hostControl = null)
            {
                if (!visible)
                {
                    _waitSpinner.Host = null;
                    Controls.Remove(_waitSpinner);
                    return;
                }

                Controls.Add(_waitSpinner);
                _waitSpinner.Host = hostControl ?? this;
                _waitSpinner.BringToFront();
            }
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

            InvokeAsync(hostControl: propertyGrid1,
                asyncMethod: () => s_subscriptionManager.UpdateSubscriptionAsync(subscription, e.ChangedItem.Label));
        }

        private void propertyGrid1_DeleteClicked(object sender, EventArgs e)
        {
            if (propertyGrid1.SelectedObject is not Subscription subscription)
            {
                Debug.Fail("How did we get here?");
                return;
            }

            TaskDialogVerificationCheckBox verificationCheckBox = new("Yes, proceed!");

            var buttonYes = TaskDialogButton.Yes;
            buttonYes.Enabled = false;
            verificationCheckBox.CheckedChanged += (s, e) => buttonYes.Enabled = verificationCheckBox.Checked;

            TaskDialogPage page = new()
            {
                AllowCancel = false,
                AllowMinimize = false,
                Caption = "Are you sure?",
                Icon = TaskDialogIcon.Warning,
                SizeToContent = true,
                Heading = "This subscription will be deleted. Proceed?",
                Verification = verificationCheckBox,
            };

            page.Buttons.Add(buttonYes);
            page.Buttons.Add(TaskDialogButton.No);

            Form owner = Application.OpenForms[0];
            if (TaskDialog.ShowDialog(owner, page) == TaskDialogButton.Yes)
            {
                InvokeAsync(hostControl: propertyGrid1,
                    asyncMethod: () => s_subscriptionManager.DeleteSubscriptionAsync(subscription.Id),
                    onCompleteMethod: () => BindSubscriptions(forceReload: true));
            }
        }

        private void propertyGrid1_NewClicked(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode is ChannelTreeNode channelNode &&
                channelNode.FirstNode is SourceTreeNode sourceNode &&
                sourceNode.Tag is Subscription subscription)
            {
                // create a new subscription
                // using form = new CreateSubscription();
                // form.Context = subscription;
                // form.ShowDialog(this);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is not null)
            {
                propertyGrid1.SelectedObject = e.Node.Tag;

                propertyGrid1.AllowCreate =
                    propertyGrid1.AllowDelete = true;

                return;
            }

            propertyGrid1.SelectedObject = null;
            propertyGrid1.AllowCreate =
                propertyGrid1.AllowDelete = false;

            propertyGrid1.AllowCreate = e.Node is ChannelTreeNode && _groupByOption == GroupByOption.RepoBranchChannelSource;
        }

        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            BindSubscriptions(forceReload: true);
        }

        private enum GroupByOption
        {
            RepoBranchChannelSource,
            ChannelSourceRepoBranch,
        }
    }
}
