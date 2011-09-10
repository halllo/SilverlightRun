using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SilverlightRun.PhoneSpecific.UI
{
    /// <summary>
    /// This grid adjusts is row heights/column widths as new items are added to it by observering their DependencyProperties.
    /// </summary>
    public class ColdGrid : ItemsControl
    {
        private Grid _theGrid;
        private List<FrameworkElement> _desiredItems;

        public IEnumerable DynamicItemsSource
        {
            get { return (IEnumerable)GetValue(DynamicItemsSourceProperty); }
            set { SetValue(DynamicItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty DynamicItemsSourceProperty =
            DependencyProperty.Register("DynamicItemsSource", typeof(IEnumerable), typeof(ColdGrid), new PropertyMetadata(DynItemsSourceChanged));

        private static void DynItemsSourceChanged(DependencyObject dob, DependencyPropertyChangedEventArgs e)
        {
            var ssgrid = dob as ColdGrid;
            var dgiCollection = e.NewValue as ObservableCollection<ColdGridItem>;
            if (dgiCollection != null)
            {
                foreach (var item in dgiCollection) ssgrid.AddDynamicItemToGrid(item);
                dgiCollection.CollectionChanged += (s, e1) => CoolCollectionChanged(ssgrid, e1);
            }
        }

        private static void CoolCollectionChanged(ColdGrid grid, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems) grid.AddDynamicItemToGrid(item as ColdGridItem);
            }
        }

        public ColdGrid()
        {
            DefaultStyleKey = typeof(ColdGrid);
            _desiredItems = new List<FrameworkElement>();
        }

        public override void OnApplyTemplate()
        {
            if (System.ComponentModel.DesignerProperties.IsInDesignTool) return;
            base.OnApplyTemplate();
            _theGrid = GetTemplateChild("theGrid") as Grid;
            _theGrid.Children.Clear();
            AddRememberedItems();
        }

        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    var newItem = item as FrameworkElement;
                    if (newItem != null) AddToGridOrRemember(newItem);
                }
            }
        }

        private void AddToGridOrRemember(FrameworkElement newItem)
        {
            if (_theGrid == null)
                _desiredItems.Add(newItem);
            else
                AddItemToGrid(newItem);
        }

        private void AddRememberedItems()
        {
            foreach (var item in _desiredItems) AddItemToGrid(item);
            _desiredItems.Clear();
        }

        private void AddDynamicItemToGrid(ColdGridItem item)
        {
            var uiItem = new ContentPresenter() { Content = item.DataContext, ContentTemplate = this.ItemTemplate };
            Grid.SetColumn(uiItem, Grid.GetColumn(item));
            Grid.SetRow(uiItem, Grid.GetRow(item));
            Grid.SetColumnSpan(uiItem, Grid.GetColumnSpan(item));
            Grid.SetRowSpan(uiItem, Grid.GetRowSpan(item));
            this.Items.Add(uiItem);
        }

        private void AddItemToGrid(FrameworkElement newItem)
        {
            int row = Grid.GetRow(newItem);
            int col = Grid.GetColumn(newItem);
            for (int i = _theGrid.RowDefinitions.Count; i <= row; i++) _theGrid.RowDefinitions.Add(new RowDefinition());
            for (int i = _theGrid.ColumnDefinitions.Count; i <= col; i++) _theGrid.ColumnDefinitions.Add(new ColumnDefinition());
            _theGrid.Children.Add(newItem);
        }
    }
}
