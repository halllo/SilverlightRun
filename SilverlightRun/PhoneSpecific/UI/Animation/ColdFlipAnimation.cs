using System.Windows;
using System.Windows.Media;

namespace SilverlightRun.PhoneSpecific.UI.Animation
{
    /// <summary>
    /// ALlows flip animations to be performed on any (container|front|back) UIElement triple.
    /// </summary>
    public class ColdFlipAnimation
    {
        public static void FlipLeft(UIElement container, UIElement front, UIElement back)
        {
            if (IsFlipping()) return;
            if (front.Visibility == Visibility.Visible)
                Flip_r2l(container, front, back);
            else
                Flip_r2la(container, front, back);
        }

        public static void FlipRight(UIElement container, UIElement front, UIElement back)
        {
            if (IsFlipping()) return;
            if (front.Visibility == Visibility.Visible)
                Flip_l2r(container, front, back);
            else
                Flip_l2ra(container, front, back);
        }

        public static void FlipDown(UIElement container, UIElement front, UIElement back)
        {
            if (IsFlipping()) return;
            if (front.Visibility == Visibility.Visible)
                Flip_t2b(container, front, back);
            else
                Flip_t2ba(container, front, back);
        }

        public static void FlipUp(UIElement container, UIElement front, UIElement back)
        {
            if (IsFlipping()) return;
            if (front.Visibility == Visibility.Visible)
                Flip_b2t(container, front, back);
            else
                Flip_b2ta(container, front, back);
        }

        public static bool FlippedHorizontally(UIElement container)
        {
            var pp = container.Projection as PlaneProjection;
            if (pp != null) return pp.RotationY == 180;
            else return false;
        }

        public static bool FlippedVertically(UIElement container)
        {
            var pp = container.Projection as PlaneProjection;
            if (pp != null) return pp.RotationX == 180;
            else return false;
        }

        public static void AdjustHorizontalOrientationOfContent(UIElement container, UIElement frontContent, UIElement backContent)
        {
            if (IsFlipping()) return;
            var backProjection = ColdAnimation.EnsurePlaneProjection(backContent);
            var frontProjection = ColdAnimation.EnsurePlaneProjection(frontContent);
            if (FlippedVertically(container))
            {
                frontProjection.RotationY = 0;
                frontProjection.RotationX = 180;
                backProjection.RotationX = 180;
                backProjection.RotationY = 180;
            }
            else
            {
                frontProjection.RotationX = 0;
                frontProjection.RotationY = 0;
                backProjection.RotationX = 0;
                backProjection.RotationY = 180;
            }
        }

        public static void AdjustVerticalOrientationOfContent(UIElement container, UIElement frontContent, UIElement backContent)
        {
            if (IsFlipping()) return;
            var backProjection = ColdAnimation.EnsurePlaneProjection(backContent);
            var frontProjection = ColdAnimation.EnsurePlaneProjection(frontContent);
            if (FlippedHorizontally(container))
            {
                frontProjection.RotationX = 0;
                frontProjection.RotationY = 180;
                backProjection.RotationX = 180;
                backProjection.RotationY = 180;
            }
            else
            {
                frontProjection.RotationX = 0;
                frontProjection.RotationY = 0;
                backProjection.RotationX = 180;
                backProjection.RotationY = 0;
            }
        }

        #region Animation details
        static ColdAnimation _flip_r2l = new ColdAnimation();
        static ColdAnimationRegistered _flip_r2l_a1 = _flip_r2l.RegisterRotateY(500, 0, 180);
        static ColdAnimationRegistered _flip_r2l_a2 = _flip_r2l.RegisterVisibility(0, Visibility.Visible, 250, Visibility.Collapsed);
        static ColdAnimationRegistered _flip_r2l_a3 = _flip_r2l.RegisterVisibility(0, Visibility.Collapsed, 250, Visibility.Visible);

        private static void Flip_r2l(UIElement container, UIElement front, UIElement back)
        {
            _flip_r2l.Animate(_flip_r2l_a1.On(container), _flip_r2l_a2.On(front), _flip_r2l_a3.On(back));
        }

        static ColdAnimation _flip_r2la = new ColdAnimation();
        static ColdAnimationRegistered _flip_r2la_a1 = _flip_r2la.RegisterRotateY(500, 180, 360);
        static ColdAnimationRegistered _flip_r2la_a2 = _flip_r2la.RegisterVisibility(0, Visibility.Collapsed, 250, Visibility.Visible);
        static ColdAnimationRegistered _flip_r2la_a3 = _flip_r2la.RegisterVisibility(0, Visibility.Visible, 250, Visibility.Collapsed);

        private static void Flip_r2la(UIElement container, UIElement front, UIElement back)
        {
            _flip_r2la.Animate(_flip_r2la_a1.On(container), _flip_r2la_a2.On(front), _flip_r2la_a3.On(back));
        }

        static ColdAnimation _flip_l2r = new ColdAnimation();
        static ColdAnimationRegistered _flip_l2r_a1 = _flip_l2r.RegisterRotateY(500, 360, 180);
        static ColdAnimationRegistered _flip_l2r_a2 = _flip_l2r.RegisterVisibility(0, Visibility.Visible, 250, Visibility.Collapsed);
        static ColdAnimationRegistered _flip_l2r_a3 = _flip_l2r.RegisterVisibility(0, Visibility.Collapsed, 250, Visibility.Visible);

        private static void Flip_l2r(UIElement container, UIElement front, UIElement back)
        {
            _flip_l2r.Animate(_flip_l2r_a1.On(container), _flip_l2r_a2.On(front), _flip_l2r_a3.On(back));
        }

        static ColdAnimation _flip_l2ra = new ColdAnimation();
        static ColdAnimationRegistered _flip_l2ra_a1 = _flip_l2ra.RegisterRotateY(500, 180, 0);
        static ColdAnimationRegistered _flip_l2ra_a2 = _flip_l2ra.RegisterVisibility(0, Visibility.Collapsed, 250, Visibility.Visible);
        static ColdAnimationRegistered _flip_l2ra_a3 = _flip_l2ra.RegisterVisibility(0, Visibility.Visible, 250, Visibility.Collapsed);

        private static void Flip_l2ra(UIElement container, UIElement front, UIElement back)
        {
            _flip_l2ra.Animate(_flip_l2ra_a1.On(container), _flip_l2ra_a2.On(front), _flip_l2ra_a3.On(back));
        }

        static ColdAnimation _flip_t2b = new ColdAnimation();
        static ColdAnimationRegistered _flip_t2b_a1 = _flip_t2b.RegisterRotateX(500, 0, 180);
        static ColdAnimationRegistered _flip_t2b_a2 = _flip_t2b.RegisterVisibility(0, Visibility.Visible, 250, Visibility.Collapsed);
        static ColdAnimationRegistered _flip_t2b_a3 = _flip_t2b.RegisterVisibility(0, Visibility.Collapsed, 250, Visibility.Visible);

        private static void Flip_t2b(UIElement container, UIElement front, UIElement back)
        {
            _flip_t2b.Animate(_flip_t2b_a1.On(container), _flip_t2b_a2.On(front), _flip_t2b_a3.On(back));
        }

        static ColdAnimation _flip_t2ba = new ColdAnimation();
        static ColdAnimationRegistered _flip_t2ba_a1 = _flip_t2ba.RegisterRotateX(500, 180, 360);
        static ColdAnimationRegistered _flip_t2ba_a2 = _flip_t2ba.RegisterVisibility(0, Visibility.Collapsed, 250, Visibility.Visible);
        static ColdAnimationRegistered _flip_t2ba_a3 = _flip_t2ba.RegisterVisibility(0, Visibility.Visible, 250, Visibility.Collapsed);

        private static void Flip_t2ba(UIElement container, UIElement front, UIElement back)
        {
            _flip_t2ba.Animate(_flip_t2ba_a1.On(container), _flip_t2ba_a2.On(front), _flip_t2ba_a3.On(back));
        }

        static ColdAnimation _flip_b2t = new ColdAnimation();
        static ColdAnimationRegistered _flip_b2t_a1 = _flip_b2t.RegisterRotateX(500, 360, 180);
        static ColdAnimationRegistered _flip_b2t_a2 = _flip_b2t.RegisterVisibility(0, Visibility.Visible, 250, Visibility.Collapsed);
        static ColdAnimationRegistered _flip_b2t_a3 = _flip_b2t.RegisterVisibility(0, Visibility.Collapsed, 250, Visibility.Visible);

        private static void Flip_b2t(UIElement container, UIElement front, UIElement back)
        {
            _flip_b2t.Animate(_flip_b2t_a1.On(container), _flip_b2t_a2.On(front), _flip_b2t_a3.On(back));
        }

        static ColdAnimation _flip_b2ta = new ColdAnimation();
        static ColdAnimationRegistered _flip_b2ta_a1 = _flip_b2ta.RegisterRotateX(500, 180, 0);
        static ColdAnimationRegistered _flip_b2ta_a2 = _flip_b2ta.RegisterVisibility(0, Visibility.Collapsed, 250, Visibility.Visible);
        static ColdAnimationRegistered _flip_b2ta_a3 = _flip_b2ta.RegisterVisibility(0, Visibility.Visible, 250, Visibility.Collapsed);

        private static void Flip_b2ta(UIElement container, UIElement front, UIElement back)
        {
            _flip_b2ta.Animate(_flip_b2ta_a1.On(container), _flip_b2ta_a2.On(front), _flip_b2ta_a3.On(back));
        } 
        #endregion

        private static bool IsFlipping()
        {
            return _flip_r2l.IsRunning
                || _flip_r2la.IsRunning
                || _flip_l2r.IsRunning
                || _flip_l2ra.IsRunning
                || _flip_t2b.IsRunning
                || _flip_t2ba.IsRunning
                || _flip_b2t.IsRunning
                || _flip_b2ta.IsRunning;
        }
    }
}
