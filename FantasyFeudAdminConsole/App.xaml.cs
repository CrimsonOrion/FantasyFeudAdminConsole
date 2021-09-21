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

namespace FantasyFeudAdminConsole
{
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
                .Build();

            GlobalConfig.DatabaseSettings = new()
            {
                DataSource = configuration.GetSection("Database Settings").GetSection("Data Source").Value,
                InitialCatalog = configuration.GetSection("Database Settings").GetSection("Initial Catalog").Value,
                UserId = configuration.GetSection("Database Settings").GetSection("User ID").Value,
                Password = configuration.GetSection("Database Settings").GetSection("Password").Value,
                PersistSecurityInfo = bool.TryParse(configuration.GetSection("Database Settings").GetSection("Persist Security Info").Value, out var b) && b
            };

            GlobalConfig.WebServerSettings = new()
            {
                EventServer = new Uri(configuration.GetSection("Web Server Settings").GetSection("Event Server").Value),
                EventServerTest = new Uri(configuration.GetSection("Web Server Settings").GetSection("Event Server Test").Value)
            };

            MsSqlConnectionString.SetConnectionString(GlobalConfig.DatabaseSettings.DataSource, GlobalConfig.DatabaseSettings.InitialCatalog, GlobalConfig.DatabaseSettings.UserId, GlobalConfig.DatabaseSettings.Password, GlobalConfig.DatabaseSettings.PersistSecurityInfo);
            SQLiteConnectionString.SetConnectionString(GlobalConfig.DatabaseSettings.DataSource);

            _ = containerRegistry

                .RegisterInstance<IDialogCoordinator>(new DialogCoordinator())

                .RegisterScoped<IDataAccess, MsSqlDataAccess>()
                .RegisterScoped<IDataProcessor, DataProcessorMsSql>()
                .RegisterScoped<IWebProcessor, WebProcessor>()
                ;
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //_ = moduleCatalog
            //    .AddModule<Module>()
        }
    }
}