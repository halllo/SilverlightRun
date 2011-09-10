using System.Windows;
using System.Windows.Controls;

namespace SilverlightRun.PhoneSpecific.UI
{
    /// <summary>
    /// Supports auto scrolling to the bottom and stretches items.
    /// </summary>
    public class ColdListBox : ListBox
    {
        public ScrollViewer Scroller { get; private set; }
        public bool AutoScrolling { get; set; }

        public ColdListBox()
        {
            ItemContainerStyle = NewStretchingItemContainerStyle();
            AutoScrolling = true;
        }

        private static Style NewStretchingItemContainerStyle()
        {
            return new Style(typeof(ListBoxItem))
            {
                Setters = { new Setter(ListBoxItem.HorizontalContentAlignmentProperty, HorizontalAlignment.Stretch) }
            };
        }

        public void ScrollToTheBottom()
        {
            if (Scroller != null)
                Scroller.ScrollToVerticalOffset(double.MaxValue);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Scroller = base.GetTemplateChild("ScrollViewer") as ScrollViewer;
        }

        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            if (AutoScrolling)
                Dispatcher.BeginInvoke(() => ScrollToTheBottom());
        }
    }
}
