// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace DarcUI
{
    public class SubscriptionManager
    {
        private static readonly Executable s_darc = new Executable("darc");

        public Task<string> DeleteSubscriptionAsync(string subscriptionId)
        {
            return s_darc.GetOutputAsync($"delete-subscriptions --id {subscriptionId} --quiet");
        }

        public Task<string?> UpdateSubscriptionAsync(Subscription subscription, string? propertyName)
        {
            return propertyName switch
            {
                nameof(Subscription.Enabled) => ToggleSubscriptionAsync(subscription),
                nameof(Subscription.UpdateFrequency) => UpdateUpdateFrequency(subscription),
                nameof(Subscription.TokenFailureNotificationTags) => UpdateFailureNotificationTagsAsync(subscription),
                _ => Task.FromResult((string?)null),
            };
        }

        private async Task<string?> ToggleSubscriptionAsync(Subscription subscription)
        {
            string status = subscription.Enabled ? "--enable" : "--disable";
            string output = await s_darc.GetOutputAsync($"subscription-status --id {subscription.Id} {status} --quiet");
            return output;
        }

        private async Task<string?> UpdateFailureNotificationTagsAsync(Subscription subscription)
        {
            string output = await s_darc.GetOutputAsync($"update-subscription --id {subscription.Id} --failure-notification-tags '{subscription.TokenFailureNotificationTags}'");
            return output;
        }

        private async Task<string?> UpdateUpdateFrequency(Subscription subscription)
        {
            string output = await s_darc.GetOutputAsync($"update-subscription --id {subscription.Id} --update-frequency {subscription.UpdateFrequency}");
            return output;
        }
    }
}
