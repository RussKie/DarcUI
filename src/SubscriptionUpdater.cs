// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.Threading.Tasks;

namespace DarcUI
{
    public class SubscriptionUpdater
    {
        public Task<string?> UpdateSubscriptionAsync(Subscription subscription, string propertyName)
        {
            return Task.Run(() =>
            {
                if (propertyName == nameof(Subscription.Enabled))
                {
                    return ToggleSubscription(subscription);
                }

                return null;
            });
        }

        private string ToggleSubscription(Subscription subscription)
        {
            string output;

            string status = subscription.Enabled ? "--enable" : "--disable";

            var executable = new Executable("darc");
            output = executable.GetOutput($"subscription-status --id {subscription.Id} {status} --quiet");

            return output;
        }
    }
}
