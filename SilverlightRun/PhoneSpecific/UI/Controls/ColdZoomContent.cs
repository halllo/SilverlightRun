using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SilverlightRun.PhoneSpecific.UI
{
    /// <summary>
    /// Automatically starts zoom at a level where everything is visible, regardless of control size.
    /// </summary>
    public class ColdZoomContent : ContentControl
    {
        ContentPresenter _presenter;
        ColdZoomContainer _zoomer;

        public ColdZoomContent()
        {
            DefaultStyleKey = typeof(ColdZoomContent);
            Loaded += new RoutedEventHandler(ZoomContainerContent_Loaded);
        }

        void ZoomContainerContent_Loaded(object sender, RoutedEventArgs e)
        {
            ZoomRight();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _presenter = this.GetTemplateChild("ContentContainer") as ContentPresenter;
        }

        internal void SetZoomContainer(ColdZoomContainer zoomer)
        {
            _zoomer = zoomer;
        }

        public void ZoomRight()
        {
            if (_presenter != null)
            {
                var uie = _presenter.Content as FrameworkElement;
                if (uie != null) uie.RenderTransform = CreateDefaultScaleTransform(uie);
            }
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
