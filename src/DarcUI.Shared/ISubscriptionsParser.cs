// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DarcUI
{
    public interface ISubscriptionsParser
    {
        List<Subscription> Parse(string darcOutput);
    }
}
