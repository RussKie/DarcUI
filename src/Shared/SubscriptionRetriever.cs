// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DarcUI
{
    public class SubscriptionRetriever
    {
        public async Task<string> GetSubscriptionsAsync(bool forceReload)
        {
            string output;

            var path = Path.Combine(Path.GetFullPath("."), "darc-get-subscriptions.cache");
            if (!forceReload && File.Exists(path))
            {
                output = File.ReadAllText(path);
                if (!string.IsNullOrWhiteSpace(output))
                {
                    return output;
                }
            }

            var executable = new Executable("darc");
            output = await executable.GetOutputAsync("get-subscriptions");
            if (!string.IsNullOrWhiteSpace(output))
            {
                File.WriteAllText(path, output);
            }

            //var output = File.ReadAllText(@"C:\Development\DarcUI\examples\get-subscriptions.txt");

            //            var output = @"
            //https://dev.azure.com/dnceng/internal/_git/aspnet-websdk (.NET Core SDK 3.0.1xx Internal) ==> 'https://dev.azure.com/dnceng/internal/_git/dotnet-toolset' ('internal/release/3.0.1xx')
            //  - Id: 5b0dff86-0fdb-4ff1-c5c7-08d76fa9c820
            //  - Update Frequency: EveryBuild
            //  - Enabled: False
            //  - Batchable: False
            //  - Merge Policies:
            //    Standard
            //https://dev.azure.com/dnceng/internal/_git/aspnet-websdk (.NET Core SDK 3.1.1xx Internal) ==> 'https://dev.azure.com/dnceng/internal/_git/dotnet-toolset' ('internal/release/3.1.1xx')
            //  - Id: b5e87ff7-7f99-48b7-e8d5-08d76e1cf1e0
            //  - Update Frequency: EveryBuild
            //  - Enabled: False
            //  - Batchable: False
            //  - Merge Policies:
            //    Standard
            //https://dev.azure.com/dnceng/internal/_git/aspnet-websdk (.NET Core SDK 3.1.2xx Internal) ==> 'https://dev.azure.com/dnceng/internal/_git/dotnet-toolset' ('internal/release/3.1.2xx')
            //  - Id: 62bd055f-8a1d-4ecb-e8d6-08d76e1cf1e0
            //  - Update Frequency: EveryBuild
            //  - Enabled: False
            //  - Batchable: False
            //  - Merge Policies:
            //    Standard
            //https://dev.azure.com/dnceng/internal/_git/dotnet-aspnetcore (.NET Core 3.0 Internal Servicing) ==> 'https://dev.azure.com/dnceng/internal/_git/aspnet-websdk' ('internal/release/3.0.1xx')
            //  - Id: 82098cfe-a77f-42a0-e8ce-08d76e1cf1e0
            //  - Update Frequency: EveryBuild
            //  - Enabled: False
            //  - Batchable: False
            //  - Merge Policies:
            //    Standard
            //https://dev.azure.com/dnceng/internal/_git/dotnet-aspnetcore (.NET Core 3.1 Internal Servicing) ==> 'https://dev.azure.com/dnceng/internal/_git/aspnet-websdk' ('internal/release/3.1.1xx')
            //  - Id: 77ebb8c3-d4b0-4928-575a-08d76e1d56cb";

            return output;
        }
    }
}
