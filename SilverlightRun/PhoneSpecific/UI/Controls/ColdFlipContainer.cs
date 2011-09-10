using System.Windows;
using System.Windows.Controls;
using SilverlightRun.PhoneSpecific.UI.Animation;

namespace SilverlightRun.PhoneSpecific.UI
{
    /// <summary>
    /// Provides two sides to be flipped back and forth.
    /// </summary>
    public class ColdFlipContainer : ContentControl
    {
        ContentPresenter _frontContentPrensenter;
        ContentPresenter _backContentPrensenter;
        Grid _container;
        Border _frontBorder;
        Border _backBorder;

        public UIElement Front
        {
            get { return (UIElement)GetValue(FrontProperty); }
            set { SetValue(FrontProperty, value); }
        }
        public static readonly DependencyProperty FrontProperty = DependencyProperty.Register("Front", typeof(UIElement), typeof(ColdFlipContainer), new PropertyMetadata(FrontChanged));

        public static void FrontChanged(DependencyObject depobj, DependencyPropertyChangedEventArgs e)
        { }

        public UIElement Back
        {
            get { return (UIElement)GetValue(BackProperty); }
            set { SetValue(BackProperty, value); }
        }
        public static readonly DependencyProperty BackProperty = DependencyProperty.Register("Back", typeof(UIElement), typeof(ColdFlipContainer), new PropertyMetadata(BackChanged));

        public static void BackChanged(DependencyObject depobj, DependencyPropertyChangedEventArgs e)
        { }

        public ColdFlipContainer()
        {
            DefaultStyleKey = typeof(ColdFlipContainer);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            (_frontContentPrensenter = GetTemplateChild("FrontContent") as ContentPresenter).Content = Front;
            (_backContentPrensenter = GetTemplateChild("BackContent") as ContentPresenter).Content = Back;
            _container = GetTemplateChild("container") as Grid;
            _frontBorder = GetTemplateChild("front") as Border;
            _backBorder = GetTemplateChild("back") as Border;
        }

        public void FlipLeft()
        {
            ColdFlipAnimation.AdjustHorizontalOrientationOfContent(_container, _frontContentPrensenter, _backContentPrensenter);
            ColdFlipAnimation.FlipLeft(_container, _frontBorder, _backBorder);
        }

        public void FlipRight()
        {
            ColdFlipAnimation.AdjustHorizontalOrientationOfContent(_container, _frontContentPrensenter, _backContentPrensenter);
            ColdFlipAnimation.FlipRight(_container, _frontBorder, _backBorder);
        }

        public void FlipDown()
        {
            ColdFlipAnimation.AdjustVerticalOrientationOfContent(_container, _frontContentPrensenter, _backContentPrensenter);
            if (ColdFlipAnimation.FlippedHorizontally(_container))
                ColdFlipAnimation.FlipUp(_container, _frontBorder, _backBorder);
            else
                ColdFlipAnimation.FlipDown(_container, _frontBorder, _backBorder);
        }

        public void FlipUp()
        {
            ColdFlipAnimation.AdjustVerticalOrientationOfContent(_container, _frontContentPrensenter, _backContentPrensenter);
            if (ColdFlipAnimation.FlippedHorizontally(_container))
                ColdFlipAnimation.FlipDown(_container, _frontBorder, _backBorder);
            else
                ColdFlipAnimation.FlipUp(_container, _frontBorder, _backBorder);
        }
    }
}
