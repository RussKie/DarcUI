// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Drawing.Design;
using DarcUI.CustomControls;

namespace DarcUI
{
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class SubscriptionProxy
    {
        private const string CategoryDetails = "Details";
        private const string CategoryNotifications = "Notifications";
        private const string CategoryStatus = "Status";

        [Category(CategoryDetails)]
        [ReadOnly(true)]
        public string Source { get; set; }

        [Category(CategoryDetails)]
        [ReadOnly(true)]
        public string SourceChannel { get; set; }

        [Category(CategoryDetails)]
        [ReadOnly(true)]
        public string Target { get; set; }

        [Category(CategoryDetails)]
        [ReadOnly(true)]
        public string TargetBranch { get; set; }

        [Category(CategoryDetails)]
        [ReadOnly(true)]
        public string Id { get; set; }

        [Category(CategoryNotifications)]
        [Editor(typeof(NotificationTagEditor), typeof(UITypeEditor))]
        //[ReadOnly(true)]
        public string TokenFailureNotificationTags { get; set; }

        [Category(CategoryStatus)]
        public UpdateFrequency UpdateFrequency { get; set; }

        [Category(CategoryStatus)]
        public bool Enabled { get; set; }

        [Category(CategoryStatus)]
        public bool Batchable { get; set; }

        // TODO
        //[Category(CategoryStatus)]
        //public string[] MergePolicies { get; set; }

        public static implicit operator SubscriptionProxy?(Subscription? subscription)
        {
            if (subscription is null)
            {
                return null;
            }

            return new()
            {
                Source = subscription.Source,
                SourceChannel = subscription.SourceChannel,
                Target = subscription.Target,
                TargetBranch = subscription.TargetBranch,
                Id = subscription.Id,
                TokenFailureNotificationTags = subscription.TokenFailureNotificationTags,
                UpdateFrequency = subscription.UpdateFrequency,
                Enabled = subscription.Enabled,
                Batchable = subscription.Batchable,
            };
        }

        public static implicit operator Subscription?(SubscriptionProxy? subscriptionProxy)
        {
            if (subscriptionProxy is null)
            {
                return null;
            }

            return new()
            {
                Source = subscriptionProxy.Source,
                SourceChannel = subscriptionProxy.SourceChannel,
                Target = subscriptionProxy.Target,
                TargetBranch = subscriptionProxy.TargetBranch,
                Id = subscriptionProxy.Id,
                TokenFailureNotificationTags = subscriptionProxy.TokenFailureNotificationTags,
                UpdateFrequency = subscriptionProxy.UpdateFrequency,
                Enabled = subscriptionProxy.Enabled,
                Batchable = subscriptionProxy.Batchable,
            };
        }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore CS8601 // Possible null reference assignment.
}
