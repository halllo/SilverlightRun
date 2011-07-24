using Microsoft.Phone.Controls;

namespace SilverlightRunSample
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            DataContext = App.ViewModels.Get<VM.MainPage>();
            Loaded += new System.Windows.RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            tb_Info1.Foreground = App.Helper.AccentBrush;
            tb_Info2.Foreground = App.Helper.AccentBrush;
        }

        private void AboutPage(object sender, System.EventArgs e)
        {
            App.Helper.NavigateTo("/AboutPage.xaml");
        }

        private void ShowViewModel(object sender, System.EventArgs e)
        {
            App.Helper.NavigateTo("/ShowViewModel.xaml");
        }
    }
}