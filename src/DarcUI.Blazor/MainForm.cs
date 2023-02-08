// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;

namespace DarcUI;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();

        var services = new ServiceCollection();
        services.AddWindowsFormsBlazorWebView();

        blazorWebView1.HostPage = "wwwroot\\index.html";
        blazorWebView1.Services = services.BuildServiceProvider();
        blazorWebView1.RootComponents.Add<Main>("#app");

        //blazorWebView1.WebView.CoreWebView2InitializationCompleted += (s, e) =>
        //{
        //    blazorWebView1.WebView.CoreWebView2.OpenDevToolsWindow();
        //};
    }
}
