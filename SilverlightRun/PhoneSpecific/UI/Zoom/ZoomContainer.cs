using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Phone.Controls;

namespace SilverlightRun.PhoneSpecific.UI.Zoom
{
    /// <summary>
    /// Allows the content of this ContentControl to be zoomable and panable.
    /// Max zoom level is MAX_THING_ZOOM.
    /// </summary>
    public class ZoomContainer : ContentControl
    {
        ContentPresenter _presenter;

        public FrameworkElement ZoomableContent { get { return _presenter; } }
        public bool CacheManipulations { set { if (value) SetCache(); else RemoveCache(); } }

        public string InfoText
        {
            get { return (string)GetValue(InfoTextProperty); }
            set { SetValue(InfoTextProperty, value); }
        }
        public static readonly DependencyProperty InfoTextProperty = DependencyProperty.Register("InfoText", typeof(string), typeof(ZoomContainer), null);

        public ZoomContainer()
        {
            DefaultStyleKey = typeof(ZoomContainer);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _presenter = this.GetTemplateChild("ContentContainer") as ContentPresenter;
            SetupZoomRight();
            SetupGestureListener(this.GetTemplateChild("ContentBorder"));
        }

        private void SetupZoomRight()
        {
            var zoomContainerContent = _presenter.GetValue(ContentPresenter.ContentProperty) as ZoomContainerContent;
            if (zoomContainerContent != null) zoomContainerContent.SetZoomContainer(this);
        }

        private void SetupGestureListener(DependencyObject dpo)
        {
            GestureListener gestures = GestureService.GetGestureListener(dpo);
            gestures.DoubleTap += new EventHandler<GestureEventArgs>(gestures_DoubleTap);
            gestures.PinchStarted += new EventHandler<PinchStartedGestureEventArgs>(gestures_PinchStarted);
            gestures.PinchDelta += new EventHandler<PinchGestureEventArgs>(gestures_PinchDelta);
            gestures.DragDelta += new EventHandler<DragDeltaGestureEventArgs>(gestures_DragDelta);
            theThingToZoom = dpo as FrameworkElement;
        }

        #region pinching & paning logic
        private FrameworkElement theThingToZoom;

        private double TotalThingScale = 1d;
        private Point ThingPosition = new Point(0, 0);

        private const double MAX_THING_ZOOM = 5;
        private Point _oldFinger1;
        private Point _oldFinger2;
        private double _oldScaleFactor;

        void gestures_DragDelta(object sender, DragDeltaGestureEventArgs e)
        {
            var translationDelta = new Point(e.HorizontalChange, e.VerticalChange);

            if (IsDragValid(1, translationDelta))
                UpdateImagePosition(translationDelta);
        }

        void gestures_PinchStarted(object sender, PinchStartedGestureEventArgs e)
        {
            _oldFinger1 = e.GetPosition(theThingToZoom, 0);
            _oldFinger2 = e.GetPosition(theThingToZoom, 1);
            _oldScaleFactor = 1;
        }

        void gestures_PinchDelta(object sender, PinchGestureEventArgs e)
        {
            var scaleFactor = e.DistanceRatio / _oldScaleFactor;
            if (!IsScaleValid(scaleFactor))
                return;

            var currentFinger1 = e.GetPosition(theThingToZoom, 0);
            var currentFinger2 = e.GetPosition(theThingToZoom, 1);

            var translationDelta = GetTranslationDelta(
                currentFinger1,
                currentFinger2,
                _oldFinger1,
                _oldFinger2,
                ThingPosition,
                scaleFactor);

            _oldFinger1 = currentFinger1;
            _oldFinger2 = currentFinger2;
            _oldScaleFactor = e.DistanceRatio;

            UpdateImageScale(scaleFactor);
            UpdateImagePosition(translationDelta);
        }

        void gestures_DoubleTap(object sender, GestureEventArgs e)
        {
            ResetScaleAndTranslation();
        }

        private void SetCache()
        {
            if (_presenter.CacheMode == null) _presenter.CacheMode = new BitmapCache();
        }

        private void RemoveCache()
        {
            if (_presenter.CacheMode != null) _presenter.CacheMode = null;
        }

        private Point GetTranslationDelta(
            Point currentFinger1, Point currentFinger2,
            Point oldFinger1, Point oldFinger2,
            Point currentPosition, double scaleFactor)
        {
            var newPos1 = new Point(
             currentFinger1.X + (currentPosition.X - oldFinger1.X) * scaleFactor,
             currentFinger1.Y + (currentPosition.Y - oldFinger1.Y) * scaleFactor);

            var newPos2 = new Point(
             currentFinger2.X + (currentPosition.X - oldFinger2.X) * scaleFactor,
             currentFinger2.Y + (currentPosition.Y - oldFinger2.Y) * scaleFactor);

            var newPos = new Point(
                (newPos1.X + newPos2.X) / 2,
                (newPos1.Y + newPos2.Y) / 2);

            return new Point(
                newPos.X - currentPosition.X,
                newPos.Y - currentPosition.Y);
        }

        private void UpdateImageScale(double scaleFactor)
        {
            TotalThingScale *= scaleFactor;
            ApplyScale();
        }

        private void ApplyScale()
        {
            ((CompositeTransform)theThingToZoom.RenderTransform).ScaleX = TotalThingScale;
            ((CompositeTransform)theThingToZoom.RenderTransform).ScaleY = TotalThingScale;
        }

        private void UpdateImagePosition(Point delta)
        {
            var newPosition = new Point(ThingPosition.X + delta.X, ThingPosition.Y + delta.Y);

            if (newPosition.X > 0) newPosition.X = 0;
            if (newPosition.Y > 0) newPosition.Y = 0;

            if ((theThingToZoom.ActualWidth * TotalThingScale) + newPosition.X < theThingToZoom.ActualWidth)
                newPosition.X = theThingToZoom.ActualWidth - (theThingToZoom.ActualWidth * TotalThingScale);

            if ((theThingToZoom.ActualHeight * TotalThingScale) + newPosition.Y < theThingToZoom.ActualHeight)
                newPosition.Y = theThingToZoom.ActualHeight - (theThingToZoom.ActualHeight * TotalThingScale);

            ThingPosition = newPosition;

            ApplyPosition();
        }

        private void ApplyPosition()
        {
            ((CompositeTransform)theThingToZoom.RenderTransform).TranslateX = ThingPosition.X;
            ((CompositeTransform)theThingToZoom.RenderTransform).TranslateY = ThingPosition.Y;
        }

        public void ResetScaleAndTranslation()
        {
            TotalThingScale = 1;
            ThingPosition = new Point(0, 0);
            ApplyScale();
            ApplyPosition();
        }

        private bool IsDragValid(double scaleDelta, Point translateDelta)
        {
            if (ThingPosition.X + translateDelta.X > 0 || ThingPosition.Y + translateDelta.Y > 0)
                return false;

            if ((theThingToZoom.ActualWidth * TotalThingScale * scaleDelta) + (ThingPosition.X + translateDelta.X) < theThingToZoom.ActualWidth)
                return false;

            if ((theThingToZoom.ActualHeight * TotalThingScale * scaleDelta) + (ThingPosition.Y + translateDelta.Y) < theThingToZoom.ActualHeight)
                return false;

            return true;
        }

        private bool IsScaleValid(double scaleDelta)
        {
            return (TotalThingScale * scaleDelta >= 1) && (TotalThingScale * scaleDelta <= MAX_THING_ZOOM);
        }
        #endregion
    }
}
