﻿// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DarcUI;

public class SubscriptionManager
{
    private static readonly Executable s_darc = new Executable("darc");

    public Task<ExecutionResult> CreateSubscriptionAsync(Subscription subscription)
    {
        string? notifications = null;
        if (!string.IsNullOrWhiteSpace(subscription.TokenFailureNotificationTags))
        {
            notifications = $"--failure-notification-tags \"{subscription.TokenFailureNotificationTags.Trim()}\"";
        }

        string batchable = subscription.MergePolicy.Batchable ? "--batchable" : string.Empty;
        string trigger = subscription.Enabled && subscription.MergePolicy.UpdateFrequency != UpdateFrequency.None ? "--trigger" : string.Empty;
        string command = $"add-subscription --channel \"{subscription.SourceChannel.Name}\" --source-repo \"{subscription.Source}\" --target-repo \"{subscription.Target}\" --target-branch \"{subscription.TargetBranch}\" --update-frequency {subscription.MergePolicy.UpdateFrequency} {notifications} {batchable} {trigger} --quiet --verbose";

        return s_darc.GetOutputAsync(command);
    }

    public Task<ExecutionResult> DeleteSubscriptionAsync(string subscriptionId)
    {
        return s_darc.GetOutputAsync($"delete-subscriptions --id {subscriptionId} --quiet --verbose --debug");
    }

    public Task<ExecutionResult> TriggerSubscriptionAsync(string subscriptionId)
    {
        return s_darc.GetOutputAsync($"trigger-subscriptions --id {subscriptionId} --quiet --verbose --debug");
    }

    public Task<ExecutionResult?> UpdateSubscriptionAsync(Subscription subscription, string? propertyName)
    {
        return propertyName switch
        {
            nameof(Subscription.Enabled) => ToggleSubscriptionAsync(subscription),
            nameof(Policy.UpdateFrequency) => UpdateUpdateFrequency(subscription),
            nameof(Subscription.TokenFailureNotificationTags) => UpdateFailureNotificationTagsAsync(subscription),
            _ => Task.FromResult((ExecutionResult?)null),
        };
    }

    public Task<ExecutionResult> ViewDefaultChannelsAsync(Subscription subscription)
    {
        string command = $"get-default-channels --channel \"{subscription.SourceChannel.Name}\" --source-repo \"{subscription.Target}\" --branch \"{subscription.TargetBranch}\" --verbose";

        return s_darc.GetOutputAsync(command);
    }

    private async Task<ExecutionResult?> ToggleSubscriptionAsync(Subscription subscription)
    {
        string status = subscription.Enabled ? "--enable" : "--disable";
        ExecutionResult output = await s_darc.GetOutputAsync($"subscription-status --id {subscription.Id} {status} --quiet --verbose --debug");
        return output;
    }

    private async Task<ExecutionResult?> UpdateFailureNotificationTagsAsync(Subscription subscription)
    {
        ExecutionResult output = await s_darc.GetOutputAsync($"update-subscription --id {subscription.Id} --failure-notification-tags \"{subscription.TokenFailureNotificationTags}\" --verbose --debug");
        return output;
    }

    private async Task<ExecutionResult?> UpdateUpdateFrequency(Subscription subscription)
    {
        ExecutionResult output = await s_darc.GetOutputAsync($"update-subscription --id {subscription.Id} --update-frequency {subscription.MergePolicy.UpdateFrequency} --verbose --debug");
        return output;
    }
}
