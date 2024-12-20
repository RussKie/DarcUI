﻿// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DarcUI;

public class SubscriptionRetriever
{
    public async Task<ExecutionResult> GetSubscriptionsAsync(bool forceReload)
    {
        string output;

        var path = Path.Combine(Path.GetFullPath("."), "darc-get-subscriptions.cache.json");
        if (!forceReload && CanUse(path))
        {
            output = File.ReadAllText(path);
            if (!string.IsNullOrWhiteSpace(output))
            {
                return new(0, input: string.Empty, output, error: string.Empty);
            }
        }

        var executable = new Executable("darc");
        var result = await executable.GetOutputAsync("get-subscriptions --output-format json", cancellationToken: default);
        if (!string.IsNullOrWhiteSpace(result.Output))
        {
            File.WriteAllText(path, result.Output);
        }

        return result;
    }

    private static bool CanUse(string path)
    {
        if (!File.Exists(path))
        {
            return false;
        }

        if (new FileInfo(path).LastWriteTime.AddDays(1) < DateTime.Today)
        {
            return false;
        }

        return true;
    }
}
