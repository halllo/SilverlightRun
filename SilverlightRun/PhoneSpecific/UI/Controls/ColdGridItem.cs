using System.Windows;
using System.Windows.Controls;

namespace SilverlightRun.PhoneSpecific.UI
{
    /// <summary>
    /// These items are the view modeles for the items placed on the SelfSizedGrid.
    /// </summary>
    public class ColdGridItem : FrameworkElement
    {
        private ColdGridItem() { }

        public static ColdGridItem Representing(FrameworkElement fe)
        {
            var dgi = new ColdGridItem();
            Grid.SetColumn(dgi, Grid.GetColumn(fe));
            Grid.SetRow(dgi, Grid.GetRow(fe));
            Grid.SetColumnSpan(dgi, Grid.GetColumnSpan(fe));
            Grid.SetRowSpan(dgi, Grid.GetRowSpan(fe));
            dgi.DataContext = fe;
            return dgi;
        }
    }
}
