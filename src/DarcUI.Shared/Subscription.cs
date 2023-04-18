// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace DarcUI;

public record Subscription
{
    [AllowNull]
    [JsonPropertyName("sourceRepository")]
    public string Source { get; set; }

    [AllowNull]
    [JsonPropertyName("channel")]
    public SourceChannel SourceChannel { get; set; }

    [AllowNull]
    [JsonPropertyName("targetRepository")]
    public string Target { get; set; }

    [AllowNull]
    public string TargetBranch { get; set; }

    [AllowNull]
    public string Id { get; set; }

    [AllowNull]
    [JsonPropertyName("pullRequestFailureNotificationTags")]
    public string TokenFailureNotificationTags { get; set; }

    public bool Enabled { get; set; }

    [AllowNull]
    [JsonPropertyName("policy")]
    public Policy MergePolicy { get; set; }
}

public record SourceChannel
{
    // NOTE: this property isn't available when sourced as Text
    public int? Id { get; set; }

    [AllowNull]
    public string Name { get; set; }

    // NOTE: this property isn't available when sourced as Text
    public string? Classification { get; set; }

}

public record Policy
{
    public bool Batchable { get; set; }

    public UpdateFrequency UpdateFrequency { get; set; }

    // NOTE: this property isn't available when sourced as Text
    public List<MergePolicy>? MergePolicies { get; set; }
}

public record MergePolicy
{
    [AllowNull]
    public string Name { get; set; }

    public MergePolicyProperties? Properties { get; set; }
}


public record MergePolicyProperties
{
    public string[]? IgnoreChecks { get; set; }
}

