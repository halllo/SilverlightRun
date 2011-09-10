using System.Windows;
using System.Windows.Controls;

namespace SilverlightRun.PhoneSpecific.UI
{
    /// <summary>
    /// Clickable content with description. Supports single clicks.
    /// </summary>
    public class ColdDescriptionButton : Button
    {
        ContentControl _descContent;

        public bool DisableAfterClick { get; set; }

        public object Description
        {
            get { return (object)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(object), typeof(ColdDescriptionButton), new PropertyMetadata(DescriptionPropertyChanged));

        private static void DescriptionPropertyChanged(DependencyObject dob, DependencyPropertyChangedEventArgs e)
        {
            var cb = dob as ColdDescriptionButton;
            if (cb._descContent != null) cb._descContent.Content = e.NewValue;
        }

        public ColdDescriptionButton()
        {
            DefaultStyleKey = typeof(ColdDescriptionButton);
            DisableAfterClick = false;
        }

        protected override void OnClick()
        {
            base.OnClick();
            if (DisableAfterClick) IsEnabled = false;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            (_descContent = GetTemplateChild("DescriptionContainer") as ContentControl).Content = Description;
        }
    }
}
