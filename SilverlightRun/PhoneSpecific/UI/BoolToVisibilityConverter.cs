using System.Windows;
using SilverlightRun.ViewModel;

namespace SilverlightRun.PhoneSpecific.UI
{
    /// <summary>
    /// Converts a boolean value of the view model to a visibility value in the view.
    /// </summary>
    public class BoolToVisibilityConverter : ColdBindingConverter<bool, Visibility>
    {
        public BoolToVisibilityConverter()
            : base((b) => b ? Visibility.Visible : Visibility.Collapsed)
        { }
    }
}
