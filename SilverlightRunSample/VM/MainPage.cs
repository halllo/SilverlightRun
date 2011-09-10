using System.Collections.ObjectModel;
using System.Windows;
using SilverlightRun.PhoneSpecific.UI;
using SilverlightRun.Tombstoning;
using SilverlightRun.ViewModel;

namespace SilverlightRunSample.VM
{
    public class MainPage : ColdViewModel<MainPage>
    {
        [SurvivesTombstoning]
        public string Word1 { get; set; }

        [SurvivesRestart]
        public string Word2 { get; set; }

        public ObservableCollection<ColdGridItem> GridCells { get; set; }
        public ObservableCollection<UIElement> ListCells { get; set; }

        public MainPage()
        {
            GridCells = new ObservableCollection<ColdGridItem>();
            ListCells = new ObservableCollection<UIElement>();
        }
    }
}
