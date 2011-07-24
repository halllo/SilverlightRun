using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SilverlightRun.PhoneSpecific.UI.Zoom
{
    /// <summary>
    /// Automatically starts zoom at a level where everything is visible, regardless of control size.
    /// </summary>
    public class ZoomContainerContent : ContentControl
    {
        ContentPresenter _presenter;
        ZoomContainer _zoomer;

        public ZoomContainerContent()
        {
            DefaultStyleKey = typeof(ZoomContainerContent);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _presenter = this.GetTemplateChild("ContentContainer") as ContentPresenter;
        }

        internal void SetZoomContainer(ZoomContainer zoomer)
        {
            _zoomer = zoomer;            
        }

        public void ZoomRight()
        {
            var uie = _presenter.Content as FrameworkElement;
            if (uie != null) uie.RenderTransform = CreateDefaultScaleTransform(uie);
        }

        private CompositeTransform CreateDefaultScaleTransform(FrameworkElement uie)
        {
            CompositeTransform trans = new CompositeTransform();
            double xScale = _zoomer.ZoomableContent.ActualWidth / uie.ActualWidth;
            double yScale = _zoomer.ZoomableContent.ActualHeight / uie.ActualHeight;
            if (!double.IsInfinity(xScale) || !double.IsInfinity(yScale))
            {
                trans.ScaleX = trans.ScaleY = Math.Min(xScale, yScale);
            }
            return trans;
        }
    }
}
