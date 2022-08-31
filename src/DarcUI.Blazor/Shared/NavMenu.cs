// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DarcUI.Shared
{
    partial class NavMenu
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        [Inject]
        private IJSRuntime JSRuntime { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private bool _collapseNavMenu = true;

        private string ApplicationName => Application.ProductName;

        private string? NavMenuCssClass => _collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            _collapseNavMenu = !_collapseNavMenu;
        }

        private async Task RefreshAsync(object sender)
        {
            bool confirm = await JSRuntime.InvokeAsync<bool>("confirm", "Refresh data?");
            if (!confirm)
            {
                return;
            }
        }
    }
}
