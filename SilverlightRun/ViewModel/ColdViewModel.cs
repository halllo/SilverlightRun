using System;
using System.ComponentModel;
using System.Linq.Expressions;
using SilverlightRun.Tombstoning;

namespace SilverlightRun.ViewModel
{
    /// <summary>
    /// Basic view model implementation.
    /// </summary>
    /// <typeparam name="T">Any type that wants to be a view model.</typeparam>
    public abstract class ColdViewModel<T> : INotifyPropertyChanged
    {
        TombstoneSurvivalEngine tombstoning;
        IPhoneService _Phone;
        public IPhoneService Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                _Phone = value;
                tombstoning = TombstoneSurvivalEngine.SetupFor<T>(this, _Phone);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        
        public ColdViewModel()
        {
            PropertyChanged += (s, e) => { };
        }

        public void Changed<R>(Expression<Func<R>> property)
        {
            Changed(GetNameForLocator(property));
        }

        public void Changed<R>(Expression<Func<T, R>> property)
        {
            Changed(GetNameForLocator(property));
        }

        public void Change<R>(Expression<Func<R>> property, R to)
        {
            Changed(GetNameForLocatorAndSet<R>(this, property, to));
        }

        public void Change<R>(Expression<Func<T, R>> property, R to)
        {
            Changed(GetNameForLocatorAndSet<R>(this, property, to));
        }

        public void Changed(string prop)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private static string GetNameForLocatorAndSet<R>(object inst, LambdaExpression property, R content)
        {
            string propertyName = GetNameForLocator(property);
            SetValueForProperty(inst, propertyName, content);
            return propertyName;
        }

        private static string GetNameForLocator(LambdaExpression locator)
        {
            return ((MemberExpression)locator.Body).Member.Name;
        }

        private static void SetValueForProperty(object source, string property, object content)
        {
            source.GetType().GetProperty(property).SetValue(source, content, null);
        }
    }
}
