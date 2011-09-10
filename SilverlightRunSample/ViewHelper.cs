using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using SilverlightRun.PhoneSpecific.UI;

namespace SilverlightRunSample
{
    public class ViewHelper
    {
        public static void SetCellCountHeader(PivotItem pivotItem)
        {
            int cellCount = App.ViewModelCenter.Get<VM.MainPage>().GridCells.Count;
            pivotItem.Header = string.Format("{0}x{1} cells", cellCount, cellCount);
        }

        public static Rectangle NewRectangleForCell()
        {
            int cellCount = App.ViewModelCenter.Get<VM.MainPage>().GridCells.Count;
            var rect = new Rectangle { Fill = App.Helper.AccentBrush };
            Grid.SetColumn(rect, cellCount);
            Grid.SetRow(rect, cellCount);
            return rect;
        }

        public static UIElement NewContentForCell()
        {
            var button = new ColdDescriptionButton
            {
                Description = "button" + App.ViewModelCenter.Get<VM.MainPage>().ListCells.Count,
                Content = "click to disable",
                DisableAfterClick = true,
                Height = 100,
                Foreground = App.Helper.AccentBrush,
            };
            button.Click += (s, e) => button.Content = "disabled";
            return button;
        }

        public static void AdjustAppBarButtons(int selectedPivotItem, PhoneApplicationPage page)
        {
            switch (selectedPivotItem)
            {
                case 3:
                    App.Helper.AppBarButton(page, 1).IsEnabled = true;
                    break;
                case 4:
                    App.Helper.AppBarButton(page, 1).IsEnabled = true;
                    break;
                default:
                    App.Helper.AppBarButton(page, 1).IsEnabled = false;
                    break;
            }
        }

        public static void SetupZoomableText(TextBlock zoomableText)
        {
            zoomableText.Text = @"<<MainPage.cs>>
namespace SilverlightRunSample.VM
{
    public class MainPage : SilverlightRun.ViewModel.ColdViewModel<MainPage>
    {
        [SilverlightRun.Tombstoning.SurvivesTombstoning]
        public string Word1 { get; set; }

        [SilverlightRun.Tombstoning.SurvivesRestart]
        public string Word2 { get; set; }

        public MainPage()
        {
        }
    }
}

<<ViewModelCenter.cs>>
using SilverlightRun.DI;

namespace SilverlightRunSample.VM
{
    public class ViewModelCenter : SilverlightRun.PhoneSpecific.PhoneTypeCenter
    {
        protected override void ContainerSetup(GenericSimpleContainer container)
        {
            container.DeclareSingleton<VM.MainPage, VM.MainPage>();
        }
    }
}
";
        }

        public static void SetupInternetIndicator(TextBlock indicator)
        {
            indicator.Foreground = App.Helper.AccentBrush;
            indicator.Text = App.Helper.HasInternet ? "has internet connection" : "no internet connection";
        }
    }
}
