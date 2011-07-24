using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SilverlightRun.PhoneSpecific.UI.DynamicGrid
{
    /// <summary>
    /// 
    /// </summary>
    public class SelfSizedGrid : ItemsControl
    {
        private Grid _theGrid;
        private List<FrameworkElement> _desiredItems;

        public IEnumerable DynamicItemsSource
        {
            get { return (IEnumerable)GetValue(DynamicItemsSourceProperty); }
            set { SetValue(DynamicItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty DynamicItemsSourceProperty =
            DependencyProperty.Register("DynamicItemsSource", typeof(IEnumerable), typeof(SelfSizedGrid), new PropertyMetadata(DynItemsSourceChanged));

        private static void DynItemsSourceChanged(DependencyObject dob, DependencyPropertyChangedEventArgs e)
        {
            var ssgrid = dob as SelfSizedGrid;
            var dgiCollection = e.NewValue as ObservableCollection<DynamicGridItem>;
            if (dgiCollection != null)
            {
                foreach (var item in dgiCollection) ssgrid.AddDynamicItemToGrid(item);
                dgiCollection.CollectionChanged += (s, e1) => CoolCollectionChanged(ssgrid, e1);
            }
        }

        private static void CoolCollectionChanged(SelfSizedGrid grid, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems) grid.AddDynamicItemToGrid(item as DynamicGridItem);
            }
        }

        public SelfSizedGrid()
        {
            DefaultStyleKey = typeof(SelfSizedGrid);
            _desiredItems = new List<FrameworkElement>();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _theGrid = this.GetTemplateChild("theGrid") as Grid;
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

        private void AddDynamicItemToGrid(DynamicGridItem item)
        {
            var uiItem = new ContentPresenter() { Content = item.DataContext, ContentTemplate = this.ItemTemplate };
            Grid.SetColumn(uiItem, item.GridColumn);
            Grid.SetRow(uiItem, item.GridRow);
            Grid.SetColumnSpan(uiItem, item.GridColumnSpan);
            Grid.SetRowSpan(uiItem, item.GridRowSpan);
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
