using CoCHelpers.Interfaces;
using CoCHelpers.Parsers;
using ReactiveUI;
using Splat;
using System.Windows;

namespace OccupationHelperCoC
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Locator.CurrentMutable.RegisterLazySingleton<IOccupationParser>(() => new OccupationParser());
            Locator.CurrentMutable.Register(() => new MainWindow(), typeof(IViewFor<MainWindowViewModel>));
            Locator.CurrentMutable.Register(() => new OccupationView(), typeof(IViewFor<OccupationViewModel>));
        }
    }
}
