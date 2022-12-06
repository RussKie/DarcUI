// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace DarcUI
{
    public class SubscriptionManager
    {
        private static readonly Executable s_darc = new Executable("darc");

        public Task<ExecutionResult> CreateSubscriptionAsync(Subscription subscription)
        {
            string? notifications = null;
            if (!string.IsNullOrWhiteSpace(subscription.TokenFailureNotificationTags))
            {
                notifications = $"--failure-notification-tags '{subscription.TokenFailureNotificationTags.Trim()}'";
            }

            string batchable = subscription.Batchable ? "--batchable" : string.Empty;
            string trigger = subscription.UpdateFrequency != UpdateFrequency.None ? "--trigger" : string.Empty;
            string command = $"add-subscription --channel '{subscription.SourceChannel}' --source-repo '{subscription.Source}' --target-repo '{subscription.Target}' --target-branch '{subscription.TargetBranch}' --update-frequency {subscription.UpdateFrequency} {notifications} {batchable} {trigger} --verbose --quiet";

            return s_darc.GetOutputAsync(command);
        }

        public Task<ExecutionResult> DeleteSubscriptionAsync(string subscriptionId)
        {
            return s_darc.GetOutputAsync($"delete-subscriptions --id {subscriptionId} --quiet");
        }

        public Task<ExecutionResult?> UpdateSubscriptionAsync(Subscription subscription, string? propertyName)
        {
            return propertyName switch
            {
                nameof(Subscription.Enabled) => ToggleSubscriptionAsync(subscription),
                nameof(Subscription.UpdateFrequency) => UpdateUpdateFrequency(subscription),
                nameof(Subscription.TokenFailureNotificationTags) => UpdateFailureNotificationTagsAsync(subscription),
                _ => Task.FromResult((ExecutionResult?)null),
            };
        }

        private async Task<ExecutionResult?> ToggleSubscriptionAsync(Subscription subscription)
        {
            string status = subscription.Enabled ? "--enable" : "--disable";
            ExecutionResult output = await s_darc.GetOutputAsync($"subscription-status --id {subscription.Id} {status} --quiet");
            return output;
        }

        private async Task<ExecutionResult?> UpdateFailureNotificationTagsAsync(Subscription subscription)
        {
            ExecutionResult output = await s_darc.GetOutputAsync($"update-subscription --id {subscription.Id} --failure-notification-tags '{subscription.TokenFailureNotificationTags}'");
            return output;
        }

        private async Task<ExecutionResult?> UpdateUpdateFrequency(Subscription subscription)
        {
            ExecutionResult output = await s_darc.GetOutputAsync($"update-subscription --id {subscription.Id} --update-frequency {subscription.UpdateFrequency}");
            return output;
        }
    }
}
