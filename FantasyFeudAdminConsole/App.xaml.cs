using ControlzEx.Theming;

using FantasyFeudAdminConsole.Core.Configuration;
using FantasyFeudAdminConsole.Core.DataAccess;
using FantasyFeudAdminConsole.Core.Processors;

using MahApps.Metro.Controls.Dialogs;

using Microsoft.Extensions.Configuration;

using Prism.Ioc;
using Prism.Modularity;

using System;
using System.Windows;

namespace FantasyFeudAdminConsole;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncAll;
        ThemeManager.Current.SyncTheme();
    }

    protected override Window CreateShell() => Container.Resolve<MainWindow>();
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json", false, true)
            .Build()
            ?? throw new Exception("appsettings.json not found");

        GlobalConfig.DatabaseSettings = new()
        {
            DataSource = configuration.GetSection("Database Settings").GetSection("Data Source").Value,
            PersistSecurityInfo = bool.TryParse(configuration.GetSection("Database Settings").GetSection("Persist Security Info").Value, out var b) && b
        };

        GlobalConfig.WebServerSettings = new()
        {
            EventServer = new Uri(configuration.GetSection("Web Server Settings").GetSection("Event Server").Value!),
            EventServerTest = new Uri(configuration.GetSection("Web Server Settings").GetSection("Event Server Test").Value!)
        };

        SQLiteConnectionString.SetConnectionString(GlobalConfig.DatabaseSettings.DataSource
            ?? throw new("Datasource not found"));

        _ = containerRegistry

            .RegisterInstance<IDialogCoordinator>(new DialogCoordinator())

            .RegisterScoped<IDataAccess, SQLiteDataAccess>()
            .RegisterScoped<IDataProcessor, DataProcessorSQLite>()
            .RegisterScoped<IWebProcessor, WebProcessor>()
            ;
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        //_ = moduleCatalog
        //    .AddModule<Module>()
    }
}