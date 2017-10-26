using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UltimateTicTacToe.View;
using Unity;

namespace UltimateTicTacToe
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IUnityContainer container = new UnityContainer();
            // Register services in IoC container (Dependency Injection)

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}
