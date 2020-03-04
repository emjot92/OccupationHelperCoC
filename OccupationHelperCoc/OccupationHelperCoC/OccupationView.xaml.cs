using ReactiveUI;
using System.Windows;
using System.Windows.Controls;

namespace OccupationHelperCoC
{
    /// <summary>
    /// Interaction logic for OccupationView.xaml
    /// </summary>
    public partial class OccupationView : UserControl, IViewFor<OccupationViewModel>
    {
        public OccupationView()
        {
            InitializeComponent();
        }

        public OccupationViewModel ViewModel
        {
            get => (OccupationViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (OccupationViewModel)value; }

        public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(OccupationViewModel), typeof(OccupationView),
                                    new PropertyMetadata(default(OccupationViewModel)));
    }
}
