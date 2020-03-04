using Occupations.Interfaces;
using Occupations.Parsers;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
