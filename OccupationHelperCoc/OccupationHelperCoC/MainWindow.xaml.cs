using Occupations.Interfaces;
using ReactiveUI;
using Splat;
using System.Reactive.Linq;
using System.Windows;

namespace OccupationHelperCoC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IViewFor<MainWindowViewModel>
    {
        public MainWindow()
        {
            ViewModel = new MainWindowViewModel(Locator.Current.GetService<IOccupationParser>());
            InitializeComponent();
            this.WhenActivated(async disposables =>
            {
                await ViewModel.Initialize.Execute();
            });
            this.OneWayBind(ViewModel, vm => vm.SelectedItem, v => v.OccupationInfoView.ViewModel);
            this.OneWayBind(ViewModel, vm => vm.SelectedItem, v => v.OccupationInfoView.Visibility, item => item != null ? Visibility.Visible : Visibility.Collapsed);
            this.OneWayBind(ViewModel, vm => vm.OccupationTypes, v => v.OccupationTypeListBox.ItemsSource);
            this.OneWayBind(ViewModel, vm => vm.Occupations, v => v.OccupationsListBox.ItemsSource);
            this.Bind(ViewModel, vm => vm.SelectedItem, v => v.OccupationsListBox.SelectedItem);
            this.OneWayBind(ViewModel, vm => vm.Occupations.Count, v => v.NumberToGetIntegerUpDown.Maximum);
            this.Bind(ViewModel, vm => vm.SelectedNumber, v => v.NumberToGetIntegerUpDown.Value);
            this.BindCommand(ViewModel, vm => vm.GetItemWithSerialNumber, v => v.GetButton, x => x.SelectedNumber);
            this.BindCommand(ViewModel, vm => vm.Random, v => v.RandomButton);
        }

        public MainWindowViewModel ViewModel
        {
            get => (MainWindowViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (MainWindowViewModel)value; }

        public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(MainWindowViewModel), typeof(MainWindow),
                                    new PropertyMetadata(default(MainWindowViewModel)));
    }
}
