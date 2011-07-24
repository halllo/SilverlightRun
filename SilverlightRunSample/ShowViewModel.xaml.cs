using Microsoft.Phone.Controls;
using System;

namespace SilverlightRunSample
{
    public partial class ShowViewModel : PhoneApplicationPage
    {
        public ShowViewModel()
        {
            InitializeComponent();
            Loaded += new System.Windows.RoutedEventHandler(ShowViewModel_Loaded);
        }

        void ShowViewModel_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            tb_VMS.Text = @"namespace SilverlightRunSample.VM
{
    public class MainPage : SilverlightRun.ViewModel.ColdViewModel<MainPage>
    {
        [SilverlightRun.Tombstoning.SurvivesTombstoning]
        public string Word1 { get; set; }

        [SilverlightRun.Tombstoning.SurvivesRestart]
        public string Word2 { get; set; }

        public MainPage()
            : base(App.ViewModels.Get<SilverlightRun.IPhoneService>())
        {
        }
    }
}
";
            zoomcontent.ZoomRight();
        }
    }
}