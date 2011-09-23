using System;
using System.Windows;
using System.Windows.Input;

namespace SilverlightRun.PhoneSpecific.UI
{
    /// <summary>
    /// Checks whether a tab and move actually can be considered clicking.
    /// </summary>
    public class ClickSensor
    {
        Point _lastTouchDown;
        UIElement _lastTouchAnchor;

        public ClickSensor()
        { }

        public void TouchDown(MouseButtonEventArgs e, UIElement anchor)
        {
            _lastTouchDown = e.GetPosition(anchor);
            _lastTouchAnchor = anchor;
        }

        public bool IsConsideredClick(MouseButtonEventArgs e)
        {
            var touchRelease = e.GetPosition(_lastTouchAnchor);
            double xMove = Math.Abs(_lastTouchDown.X - touchRelease.X);
            double yMove = Math.Abs(_lastTouchDown.Y - touchRelease.Y);
            return xMove < 20 && yMove < 20;
        }
    }
}
