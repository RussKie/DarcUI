﻿// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace DarcUI
{
    public class SubscriptionsParser
    {
        private static readonly char[] s_TokenHttps = "https".ToCharArray();
        private static readonly char[] s_TokenArrow = " ==> ".ToCharArray();
        private static readonly char[] s_TokenId = "  - Id: ".ToCharArray();
        private static readonly char[] s_TokenUpdateFrequency = "  - Update Frequency: ".ToCharArray();
        private static readonly char[] s_TokenEnabled = "  - Enabled: ".ToCharArray();
        private static readonly char[] s_TokenBatchable = "  - Batchable: ".ToCharArray();

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
                Subscription subscription = null;
                while (start < length)
                {
                    int offset = 0;
                    ReadOnlySpan<char> line = ReadLine(span, start, length, out offset);

                    //Debug.WriteLine(line.ToString());

                    if (line.StartsWith(s_TokenHttps))
                    {
                        subscription = new Subscription();
                        list.Add(subscription);

                        int start1 = 0;
                        int end1 = line.IndexOf(' ');
                        subscription.Source = line.Slice(start1, end1).ToString();

                        start1 = end1 + 2;                          // ' ('
                        end1 = line.IndexOf(')');
                        subscription.SourceChannel = line.Slice(start1, end1 - start1).ToString();

                        start1 = end1 + s_TokenArrow.Length + 2;    // ') ==> ''
                        end1 = line.LastIndexOf(' ');               // ' '
                        subscription.Target = line.Slice(start1, end1 - start1 - 1).ToString();

                        start1 = end1 + 3;                          // ' (''
                        end1 = line.Length;                         // last '')'
                        subscription.TargetBranch = line.Slice(start1, end1 - start1 - 2).ToString();
                    }
                    else if (line.StartsWith(s_TokenId))
                    {
                        if (subscription == null)
                        {
                            // TODO: provide a better exception
                            throw new NullReferenceException("Subscription is null");
                        }

                        subscription.Id = line.Slice(s_TokenId.Length).ToString();
                    }
                    else if (line.StartsWith(s_TokenEnabled))
                    {
                        subscription.Enabled = GetBool(subscription, line, s_TokenEnabled.Length);
                    }
                    else if (line.StartsWith(s_TokenBatchable))
                    {
                        subscription.Batchable = GetBool(subscription, line, s_TokenBatchable.Length);
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

                return globalSpan.Slice(startPosition, lineLength + 1);
            }

            bool GetBool(Subscription currentSubscription, in ReadOnlySpan<char> globalSpan, int startPosition)
            {
                if (currentSubscription == null)
                {
                    // TODO: provide a better exception
                    throw new NullReferenceException("Subscription is null");
                }

                return bool.Parse(globalSpan.Slice(startPosition).ToString());
            }
        }
    }
}
