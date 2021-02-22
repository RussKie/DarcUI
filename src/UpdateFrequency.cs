// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.Serialization;

namespace DarcUI
{
    // https://github.com/dotnet/arcade-services/blob/970d23b31b4d8542a9b39d089f6c2a8a701d2e91/src/Maestro/Client/src/Generated/Models/UpdateFrequency.cs
    public enum UpdateFrequency
    {
        [EnumMember(Value = "none")]
        None,
        [EnumMember(Value = "everyDay")]
        EveryDay,
        [EnumMember(Value = "everyBuild")]
        EveryBuild,
        [EnumMember(Value = "twiceDaily")]
        TwiceDaily,
        [EnumMember(Value = "everyWeek")]
        EveryWeek,
    }
}
