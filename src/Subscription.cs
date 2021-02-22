// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;

namespace DarcUI
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class Subscription
    {
        private const string CategoryDetails = "Details";
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

        [Category(CategoryStatus)]
        public UpdateFrequency UpdateFrequency { get; set; }

        [Category(CategoryStatus)]
        public bool Enabled { get; set; }

        [Category(CategoryStatus)]
        public bool Batchable { get; set; }

        // TODO
        //[Category(CategoryStatus)]
        //public string[] MergePolicies { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
