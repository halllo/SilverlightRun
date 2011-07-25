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

        public ColdViewModel()
        {
            PropertyChanged += (s, e) => { };
        }

        public void Changed<R>(Expression<Func<R>> property)
        {
            string propertyName = GetNameForLocator(property);
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private static string GetNameForLocator<R>(Expression<Func<R>> locator)
        {
            LambdaExpression lambdaExp = (LambdaExpression)locator;
            MemberExpression memExp = (MemberExpression)lambdaExp.Body;
            return memExp.Member.Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
