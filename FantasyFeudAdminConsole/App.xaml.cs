using System.Windows;

using ControlzEx.Theming;

using FantasyFeudAdminConsole.Core.DataAccess;
using FantasyFeudAdminConsole.Core.Processors;

using MahApps.Metro.Controls.Dialogs;

using Prism.Ioc;
using Prism.Modularity;

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
            _ = containerRegistry
                .RegisterInstance<IDialogCoordinator>(new DialogCoordinator())

                .RegisterScoped<ISqlDataAccess, MsSqlDataAccess>()
                .RegisterScoped<IDataProcessor, DataProcessor>()
                ;
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //_ = moduleCatalog
            //    .AddModule<CutoffScreenModule>()
        }
    }
}