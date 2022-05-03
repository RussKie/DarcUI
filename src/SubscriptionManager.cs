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
            return Task.Run(() =>
            {
                string output = s_darc.GetOutput($"delete-subscriptions --id {subscriptionId} --quiet");
                return output;
            });
        }

        public Task<string?> UpdateSubscriptionAsync(Subscription subscription, string? propertyName)
        {
            return Task.Run(() =>
            {
                return propertyName switch
                {
                    nameof(Subscription.Enabled) => ToggleSubscription(subscription),
                    nameof(Subscription.TokenFailureNotificationTags) => UpdateFailureNotificationTags(subscription),
                    _ => null,
                };
            });
        }

        private string ToggleSubscription(Subscription subscription)
        {
            string status = subscription.Enabled ? "--enable" : "--disable";
            string output = s_darc.GetOutput($"subscription-status --id {subscription.Id} {status} --quiet");
            return output;
        }

        private string UpdateFailureNotificationTags(Subscription subscription)
        {
            string output = s_darc.GetOutput($"update-subscription --id {subscription.Id} --failure-notification-tags '{subscription.TokenFailureNotificationTags}'");
            return output;
        }
    }
}
