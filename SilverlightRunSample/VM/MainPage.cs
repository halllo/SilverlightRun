namespace SilverlightRunSample.VM
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
