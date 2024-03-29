﻿// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DarcUI.Pages;

partial class Index
{
    private static readonly ISubscriptionsParser s_subscriptionsParser = new SubscriptionsTextParser();
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
        ExecutionResult result = await s_subscriptionsRetriever.GetSubscriptionsAsync(forceReload);
        if (string.IsNullOrWhiteSpace(result.Output))
        {
            s_subscriptions = null;
        }
        else
        {
            s_subscriptions = s_subscriptionsParser.Parse(result.Output);
        }
    }
}
