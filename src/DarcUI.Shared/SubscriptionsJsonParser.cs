// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json;

namespace DarcUI
{
    public class SubscriptionsJsonParser : ISubscriptionsParser
    {
        public List<Subscription> Parse(string darcOutput)
        {
            if (string.IsNullOrWhiteSpace(darcOutput))
            {
                return new List<Subscription>();
            }

            JsonSerializerOptions options = new()
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.WriteAsString
            };
            var list = JsonSerializer.Deserialize<List<Subscription>>(darcOutput, options);
            return list!;
        }
    }
}
