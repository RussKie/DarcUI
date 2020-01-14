// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace DarcUI
{
    public partial class Form1 : Form
    {
        private static readonly SubscriptionsParser s_subscriptionsParser = new SubscriptionsParser();
        private static List<Subscription> s_subscriptions;


        public Form1()
        {
            InitializeComponent();

            cboSubscriptionFrom.SelectedIndexChanged += cboSubscriptionFrom_SelectedIndexChanged;
        }

        private void ResetControls()
        {
            cboSubscriptionFrom.DataSource = null;
            cboSubscriptionTo.DataSource = null;
            //cboSubscriptionFrom.Items.Clear();
            //cboSubscriptionTo.Items.Clear();
            tlpnlSubscriptions.Enabled = false;
        }

        private void btnSubscriptionRefresh_Click(object sender, EventArgs e)
        {
            ResetControls();

            var executable = new Executable("darc");
            var output = executable.GetOutput("get-subscriptions");

            //            var output = @"
            //https://dev.azure.com/dnceng/internal/_git/aspnet-websdk (.NET Core SDK 3.0.1xx Internal) ==> 'https://dev.azure.com/dnceng/internal/_git/dotnet-toolset' ('internal/release/3.0.1xx')
            //  - Id: 5b0dff86-0fdb-4ff1-c5c7-08d76fa9c820
            //  - Update Frequency: EveryBuild
            //  - Enabled: False
            //  - Batchable: False
            //  - Merge Policies:
            //    Standard
            //https://dev.azure.com/dnceng/internal/_git/aspnet-websdk (.NET Core SDK 3.1.1xx Internal) ==> 'https://dev.azure.com/dnceng/internal/_git/dotnet-toolset' ('internal/release/3.1.1xx')
            //  - Id: b5e87ff7-7f99-48b7-e8d5-08d76e1cf1e0
            //  - Update Frequency: EveryBuild
            //  - Enabled: False
            //  - Batchable: False
            //  - Merge Policies:
            //    Standard
            //https://dev.azure.com/dnceng/internal/_git/aspnet-websdk (.NET Core SDK 3.1.2xx Internal) ==> 'https://dev.azure.com/dnceng/internal/_git/dotnet-toolset' ('internal/release/3.1.2xx')
            //  - Id: 62bd055f-8a1d-4ecb-e8d6-08d76e1cf1e0
            //  - Update Frequency: EveryBuild
            //  - Enabled: False
            //  - Batchable: False
            //  - Merge Policies:
            //    Standard
            //https://dev.azure.com/dnceng/internal/_git/dotnet-aspnetcore (.NET Core 3.0 Internal Servicing) ==> 'https://dev.azure.com/dnceng/internal/_git/aspnet-websdk' ('internal/release/3.0.1xx')
            //  - Id: 82098cfe-a77f-42a0-e8ce-08d76e1cf1e0
            //  - Update Frequency: EveryBuild
            //  - Enabled: False
            //  - Batchable: False
            //  - Merge Policies:
            //    Standard
            //https://dev.azure.com/dnceng/internal/_git/dotnet-aspnetcore (.NET Core 3.1 Internal Servicing) ==> 'https://dev.azure.com/dnceng/internal/_git/aspnet-websdk' ('internal/release/3.1.1xx')
            //  - Id: 77ebb8c3-d4b0-4928-575a-08d76e1d56cb";

            if (string.IsNullOrWhiteSpace(output))
            {
                return;
            }

            s_subscriptions = s_subscriptionsParser.Parse(output);

            cboSubscriptionFrom.DataSource = s_subscriptions.Select(s => s.Source).Distinct().OrderBy(s => s).ToArray();
            cboSubscriptionTo.DataSource = s_subscriptions.Select(s => s.Target).Distinct().OrderBy(s => s).ToArray();

            tlpnlSubscriptions.Enabled = true;
        }

        private void cboSubscriptionFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSubscriptionFrom.SelectedIndex < 0)
            {
                cboSubscriptionTo.DataSource = null;
                return;
            }

            cboSubscriptionTo.DataSource = s_subscriptions
                                            .Where(s => s.Source == (string)cboSubscriptionFrom.SelectedItem)
                                            .Select(s => s.SourceChannel)
                                            .Distinct()
                                            .OrderBy(s => s)
                                            .ToArray();
        }

    }

    public class Subscription
    {
        public string Source { get; set; }
        public string SourceChannel { get; set; }
        public string Target { get; set; }
        public string TargetBranch { get; set; }
        public string Id { get; set; }
        public string UpdateFrequency { get; set; }
        public bool Enabled { get; set; }
        public bool Batchable { get; set; }
        public string[] MergePolicies { get; set; }
    }

    public class SubscriptionsParser
    {
        private static readonly char[] s_TokenHttps = "https".ToCharArray();
        private static readonly char[] s_TokenArrow = " ==> ".ToCharArray();

        public unsafe List<Subscription> Parse(string darcOutput)
        {
            if (string.IsNullOrWhiteSpace(darcOutput))
            {
                return new List<Subscription>();
            }

            var list = new List<Subscription>();
            int length = darcOutput.Length;
            fixed (char* pOutput = darcOutput)
            {
                var span = new ReadOnlySpan<char>(pOutput, length);
                int start = 0;
                Subscription subscription;
                while (start < length)
                {
                    int offset = 0;
                    var line1 = ReadLine(span, start, length, out offset);

                    if (line1.StartsWith(s_TokenHttps))
                    {
                        Debug.WriteLine(line1.ToString());

                        subscription = new Subscription();
                        list.Add(subscription);

                        int start1 = 0;
                        int end1 = line1.IndexOf(' ');
                        subscription.Source = line1.Slice(start1, end1).ToString();

                        start1 = end1 + 2;          // ' ('
                        end1 = line1.IndexOf(')');
                        subscription.SourceChannel = line1.Slice(start1, end1 - start1).ToString();

                        start1 = end1 + s_TokenArrow.Length + 2;    // ') ==> ''
                        end1 = line1.LastIndexOf(' ');              // ' '
                        subscription.Target = line1.Slice(start1, end1 - start1 - 1).ToString();

                        start1 = end1 + 2;          // ' ('
                        end1 = line1.Length;        // last ')'
                        subscription.TargetBranch = line1.Slice(start1, end1 - start1 - 1).ToString();
                    }

                    if (offset < 0)
                    {
                        break;
                    }

                    start += offset;
                }
            }

            return list;

            ReadOnlySpan<char> ReadLine(in ReadOnlySpan<char> globalSpan, int startPosition, int totalLength, out int offset)
            {
                offset = 0;
                bool hasMoreChars;
                while ((hasMoreChars = ((startPosition + offset) < totalLength)) && globalSpan[startPosition + offset] != '\n')
                {
                    offset++;
                }

                // trim trailing EoL
                var lineLength = offset - (hasMoreChars ? 0 : 1);
                while (lineLength > 0 && (globalSpan[startPosition + lineLength] == '\n' || globalSpan[startPosition + lineLength] == '\r'))
                {
                    lineLength--;
                }

                if (hasMoreChars)
                {
                    // the next line starts here
                    offset++;
                }
                else
                {
                    offset = -1;
                }

                return globalSpan.Slice(startPosition, lineLength);
            }
        }
    }
}
