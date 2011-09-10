using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using SilverlightRun.PhoneSpecific.UI;

namespace SilverlightRunSample
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = App.ViewModelCenter.Get<VM.MainPage>();
            Loaded += new System.Windows.RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            tb_Info1.Foreground = App.Helper.AccentBrush;
            tb_Info2.Foreground = App.Helper.AccentBrush;
            ViewHelper.SetupInternetIndicator(internetIndicator);
            ViewHelper.SetupZoomableText(zoomableText);
            AddDefaultCells();
        }

        private void AddDefaultCells()
        {
            for (int i = 0; i < 2; i++) AddCellForDynamicGrid();
            for (int i = 0; i < 3; i++) AddCellForListBox();
        }

        private void AboutPage(object sender, System.EventArgs e)
        {
            App.Helper.NavigateTo("/AboutPage.xaml");
        }

        private void FlipLeft(object sender, RoutedEventArgs e)
        {
            flipTile.FlipLeft();
        }

        private void FlipRight(object sender, RoutedEventArgs e)
        {
            flipTile.FlipRight();
        }

        private void FlipUp(object sender, RoutedEventArgs e)
        {
            flipTile.FlipUp();
        }

        private void FlipDown(object sender, RoutedEventArgs e)
        {
            flipTile.FlipDown();
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewHelper.AdjustAppBarButtons((sender as Pivot).SelectedIndex, this);
        }

        private void ZoomContainer_ZoomChanged(object sender, ZoomChangedEventArgs e)
        {
            pivotContainer.FocusOn(2, when: Math.Abs(e.ZoomFactor - 1.0) > 0.1);
        }

        private void AddCell(object sender, System.EventArgs e)
        {
            if (pivotContainer.SelectedIndex == 3) AddCellForDynamicGrid();
            if (pivotContainer.SelectedIndex == 4) AddCellForListBox();
        }

        private static void AddCellForListBox()
        {
            App.ViewModelCenter.Get<VM.MainPage>().ListCells.Add(ViewHelper.NewContentForCell());
        }

        private void AddCellForDynamicGrid()
        {
            var newCell = ColdGridItem.Representing(ViewHelper.NewRectangleForCell());
            App.ViewModelCenter.Get<VM.MainPage>().GridCells.Add(newCell);
            ViewHelper.SetCellCountHeader(cellPivot);
        }
    }
}