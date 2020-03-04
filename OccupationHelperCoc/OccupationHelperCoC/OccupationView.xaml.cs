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
            this.OneWayBind(ViewModel, vmProperty => vmProperty.Name, v => v.NameTextBlock.Text);
            this.OneWayBind(ViewModel, vmProperty => vmProperty.Description, v => v.DescriptionTextBlock.Text);
            this.OneWayBind(ViewModel, vmProperty => vmProperty.OccupationSkillPoints, v => v.OccupationSkillPointsTextBlock.Text);
            this.OneWayBind(ViewModel, vmProperty => vmProperty.MinCredit, v => v.MinCreditTextBlock.Text);
            this.OneWayBind(ViewModel, vmProperty => vmProperty.MaxCredit, v => v.MaxCreditTextBlock.Text);
            this.OneWayBind(ViewModel, vmProperty => vmProperty.Skills, v => v.SkillsTextBlock.Text);
            this.OneWayBind(ViewModel, vmProperty => vmProperty.OccupationType, v => v.OccupationTypeTextBlock.Text);
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
