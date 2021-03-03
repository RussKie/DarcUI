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

            _ = BindSubscriptionsAsync(forceReload: false);
        }

        private async Task BindSubscriptionsAsync(bool forceReload)
        {
            string? selectedNodePath = null;
            if (treeView1.Nodes.Count > 0)
            {
                selectedNodePath = treeView1.SelectedNode?.FullPath;
            }

            treeView1.Nodes.Clear();

            propertyGrid1.SelectedObject = null;
            propertyGrid1.AllowCreate = propertyGrid1.AllowDelete = false;

            await InvokeAsync(hostControl: treeView1,
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
                },
                onCompleteMethod: () =>
                {
                    treeView1.BeginUpdate();
                    BindSubscriptions(treeView1, s_subscriptions, _groupByOption);
                    treeView1.EndUpdate();

                    if (selectedNodePath is not null)
                    {
                        foreach (TreeNode node in treeView1.Nodes)
                        {
                            if (GetNodeFromPath(node, selectedNodePath) is TreeNode selectedNode)
                            {
                                treeView1.SelectedNode = selectedNode;
                                treeView1.SelectedNode.Expand();
                                treeView1.SelectedNode.EnsureVisible();
                                break;
                            }
                        }
                    }
                    treeView1.EndUpdate();

                    if (selectedNodePath is not null)
                    {
                        foreach (TreeNode node in treeView1.Nodes)
                        {
                            if (GetNodeFromPath(node, selectedNodePath) is TreeNode selectedNode)
                            {
                                treeView1.SelectedNode = selectedNode;
                                treeView1.SelectedNode.Expand();
                                treeView1.SelectedNode.EnsureVisible();
                                break;
                            }
                        }
                    }
                });

            static TreeNode? GetNodeFromPath(TreeNode node, string fullPath)
            {
                TreeNode? foundNode = null;
                foreach (TreeNode tn in node.Nodes)
                {
                    if (tn.FullPath == fullPath)
                    {
                        return tn;
                    }
                    else if (tn.Nodes.Count > 0)
                    {
                        foundNode = GetNodeFromPath(tn, fullPath);
                    }
                    if (foundNode is not null)
                        return foundNode;
                }
                return null;
            }
        }

        private static void BindSubscriptions(TreeView treeView, List<Subscription>? subscriptions, GroupByOption option)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostControl">The control to have the "loading" overlay for.</param>
        /// <param name="asyncMethod">
        ///  A method that does heavy-lifting work.
        ///  This method is run on a background thread.
        /// </param>
        /// <param name="onCompleteMethod">
        ///  A method to execute once the <paramref name="asyncMethod"/> has completed.
        ///  This method is run on the UI thread.
        /// </param>
        /// <returns>An awaitable.</returns>
        private async Task InvokeAsync(Control hostControl, Func<Task> asyncMethod, Action? onCompleteMethod = null)
        {
            if (!_operationInProgress)
            {
                Debug.Assert(!_operationInProgress);
            }

            Debug.Assert(!hostControl.InvokeRequired);
            BeginUIOperation();

            await Task.Run(async () =>
            {
                try
                {
                    await asyncMethod();
                }
                finally
                {
                    hostControl.Invoke((Action)(() =>
                    {
                        onCompleteMethod?.Invoke();
                        EndUIOperation();
                    }));
                }
            });

            void BeginUIOperation()
            {
                _operationInProgress = true;
                tsbtnRefresh.Enabled = false;
                tabControl.Enabled = false;
                ShowSpinner(visible: true, hostControl);
            }

            void EndUIOperation()
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

        private async void groupByOption1_Click(object sender, EventArgs e)
        {
            if (_groupByOption == GroupByOption.RepoBranchChannelSource)
            {
                return;
            }

            _groupByOption = GroupByOption.RepoBranchChannelSource;
            await BindSubscriptionsAsync(forceReload: false);

            groupByOption1.Checked = _groupByOption == GroupByOption.RepoBranchChannelSource;
            groupByOption2.Checked = _groupByOption == GroupByOption.ChannelSourceRepoBranch;
        }

        private async void groupByOption2_Click(object sender, EventArgs e)
        {
            if (_groupByOption == GroupByOption.ChannelSourceRepoBranch)
            {
                return;
            }

            _groupByOption = GroupByOption.ChannelSourceRepoBranch;
            await BindSubscriptionsAsync(forceReload: false);

            groupByOption1.Checked = _groupByOption == GroupByOption.RepoBranchChannelSource;
            groupByOption2.Checked = _groupByOption == GroupByOption.ChannelSourceRepoBranch;
        }

        private async void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Subscription? subscription = propertyGrid1.SelectedObject as Subscription;
            if (subscription is null)
            {
                return;
            }

            await InvokeAsync(hostControl: propertyGrid1,
                asyncMethod: () => s_subscriptionManager.UpdateSubscriptionAsync(subscription, e.ChangedItem.Label));
        }

        private async void propertyGrid1_DeleteClicked(object sender, EventArgs e)
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
                await InvokeAsync(hostControl: propertyGrid1,
                    asyncMethod: () => s_subscriptionManager.DeleteSubscriptionAsync(subscription.Id));
                await BindSubscriptionsAsync(forceReload: true);
            }
        }

        private void propertyGrid1_NewClicked(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode is ChannelTreeNode channelNode &&
                channelNode.FirstNode is SourceTreeNode sourceNode &&
                sourceNode.Tag is Subscription subscription)
            {
                // create a new subscription
                //
                // darc help add-subscription
                // darc add-subscription --target-repo https://github.com/dotnet/winforms --target-branch main --source-repo https://github.com/dotnet/roslyn-analyzers  --channel ".NET Eng - Latest" --update-frequency everyDay --trigger --verbose --quiet
                //
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

        private async void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            await BindSubscriptionsAsync(forceReload: true);
        }

        private enum GroupByOption
        {
            RepoBranchChannelSource,
            ChannelSourceRepoBranch,
        }
    }
}
