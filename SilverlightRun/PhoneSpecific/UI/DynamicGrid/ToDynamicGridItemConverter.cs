using System;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Collections.Generic;

namespace SilverlightRun.PhoneSpecific.UI.DynamicGrid
{
    /// <summary>
    /// Converts view models of type T to DynamicGridItem objects. 
    /// Original view model is accessible by GetDataContext.
    /// Deriving class should set Get_ mappings for table positioning.
    /// </summary>
    /// <typeparam name="T">Any type. Must be specified by deriving class.</typeparam>
    public abstract class ToDynamicGridItemConverter<T> : IValueConverter
    {
        protected Func<T, int> GetColumn { private get; set; }
        protected Func<T, int> GetRow { private get; set; }
        protected Func<T, int> GetColumnSpan { private get; set; }
        protected Func<T, int> GetRowSpan { private get; set; }
        protected Func<T, object> GetDataContext { private get; set; }

        public ToDynamicGridItemConverter()
        {
            GetColumn = (o) => 0;
            GetRow = (o) => 0;
            GetColumnSpan = (o) => 1;
            GetRowSpan = (o) => 1;
            GetDataContext = (o) => null;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                var target = new ObservableCollection<DynamicGridItem>();

                var ensource = value as IEnumerable<T>;
                if (ensource != null) foreach (var item in ensource) target.Add(Convert(item));

                var obsource = value as ObservableCollection<T>;
                if (obsource != null)
                {
                    obsource.CollectionChanged += (s, e) =>
                    {
                        target.Clear();
                        foreach (var item in obsource) target.Add(Convert(item));
                    };
                }
                return target;
            }
            else return null;
        }

        private DynamicGridItem Convert(T value)
        {
            return new DynamicGridItem()
            {
                GridColumn = GetColumn(value),
                GridRow = GetRow(value),
                GridColumnSpan = GetColumnSpan(value),
                GridRowSpan = GetRowSpan(value),
                DataContext = GetDataContext(value)
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
