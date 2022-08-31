// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarcUI.Pages
{
    partial class Index
    {
        private static readonly SubscriptionsParser s_subscriptionsParser = new();
        private static readonly SubscriptionRetriever s_subscriptionsRetriever = new();
        private static readonly SubscriptionManager s_subscriptionManager = new();
        private static List<Subscription>? s_subscriptions;

        protected override async Task OnInitializedAsync()
        {
            await BindSubscriptionsAsync(false);

            await base.OnInitializedAsync();
        }

        private async Task BindSubscriptionsAsync(bool forceReload)
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
        }
    }
}
