// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DarcUI;

public record Subscription
{
    public string? Source { get; set; }
    public string? SourceChannel { get; set; }
    public string? Target { get; set; }
    public string? TargetBranch { get; set; }
    public string? Id { get; set; }
    public string? TokenFailureNotificationTags { get; set; }
    public UpdateFrequency UpdateFrequency { get; set; }
    public bool Enabled { get; set; }
    public bool Batchable { get; set; }

    // TODO
    //public string[] MergePolicies { get; set; }
}
