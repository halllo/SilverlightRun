using System.Windows;
using System.Windows.Media;

namespace SilverlightRun.PhoneSpecific.UI.Animation
{
    /// <summary>
    /// Allows flip animations to be performed on any (container|front|back) UIElement triple.
    /// </summary>
    public class ColdFlipAnimation
    {
        #region Animation details
        int _flipTime;

        ColdAnimation _flip_r2l;
        ColdAnimationRegistered _flip_r2l_a1;
        ColdAnimationRegistered _flip_r2l_a2;
        ColdAnimationRegistered _flip_r2l_a3;

        ColdAnimation _flip_r2la;
        ColdAnimationRegistered _flip_r2la_a1;
        ColdAnimationRegistered _flip_r2la_a2;
        ColdAnimationRegistered _flip_r2la_a3;

        ColdAnimation _flip_l2r;
        ColdAnimationRegistered _flip_l2r_a1;
        ColdAnimationRegistered _flip_l2r_a2;
        ColdAnimationRegistered _flip_l2r_a3;

        ColdAnimation _flip_l2ra;
        ColdAnimationRegistered _flip_l2ra_a1;
        ColdAnimationRegistered _flip_l2ra_a2;
        ColdAnimationRegistered _flip_l2ra_a3;

        ColdAnimation _flip_t2b;
        ColdAnimationRegistered _flip_t2b_a1;
        ColdAnimationRegistered _flip_t2b_a2;
        ColdAnimationRegistered _flip_t2b_a3;

        ColdAnimation _flip_t2ba;
        ColdAnimationRegistered _flip_t2ba_a1;
        ColdAnimationRegistered _flip_t2ba_a2;
        ColdAnimationRegistered _flip_t2ba_a3;

        ColdAnimation _flip_b2t;
        ColdAnimationRegistered _flip_b2t_a1;
        ColdAnimationRegistered _flip_b2t_a2;
        ColdAnimationRegistered _flip_b2t_a3;

        ColdAnimation _flip_b2ta;
        ColdAnimationRegistered _flip_b2ta_a1;
        ColdAnimationRegistered _flip_b2ta_a2;
        ColdAnimationRegistered _flip_b2ta_a3;

        private ColdFlipAnimation(int milliseconds)
        {
            _flipTime = milliseconds;

            _flip_r2l = new ColdAnimation();
            _flip_r2l_a1 = _flip_r2l.RegisterRotateY(_flipTime, 0, 180);
            _flip_r2l_a2 = _flip_r2l.RegisterVisibility(0, Visibility.Visible, _flipTime / 2, Visibility.Collapsed);
            _flip_r2l_a3 = _flip_r2l.RegisterVisibility(0, Visibility.Collapsed, _flipTime / 2, Visibility.Visible);

            _flip_r2la = new ColdAnimation();
            _flip_r2la_a1 = _flip_r2la.RegisterRotateY(_flipTime, 180, 360);
            _flip_r2la_a2 = _flip_r2la.RegisterVisibility(0, Visibility.Collapsed, _flipTime / 2, Visibility.Visible);
            _flip_r2la_a3 = _flip_r2la.RegisterVisibility(0, Visibility.Visible, _flipTime / 2, Visibility.Collapsed);

            _flip_l2r = new ColdAnimation();
            _flip_l2r_a1 = _flip_l2r.RegisterRotateY(_flipTime, 360, 180);
            _flip_l2r_a2 = _flip_l2r.RegisterVisibility(0, Visibility.Visible, _flipTime / 2, Visibility.Collapsed);
            _flip_l2r_a3 = _flip_l2r.RegisterVisibility(0, Visibility.Collapsed, _flipTime / 2, Visibility.Visible);

            _flip_l2ra = new ColdAnimation();
            _flip_l2ra_a1 = _flip_l2ra.RegisterRotateY(_flipTime, 180, 0);
            _flip_l2ra_a2 = _flip_l2ra.RegisterVisibility(0, Visibility.Collapsed, _flipTime / 2, Visibility.Visible);
            _flip_l2ra_a3 = _flip_l2ra.RegisterVisibility(0, Visibility.Visible, _flipTime / 2, Visibility.Collapsed);

            _flip_t2b = new ColdAnimation();
            _flip_t2b_a1 = _flip_t2b.RegisterRotateX(_flipTime, 0, 180);
            _flip_t2b_a2 = _flip_t2b.RegisterVisibility(0, Visibility.Visible, _flipTime / 2, Visibility.Collapsed);
            _flip_t2b_a3 = _flip_t2b.RegisterVisibility(0, Visibility.Collapsed, _flipTime / 2, Visibility.Visible);

            _flip_t2ba = new ColdAnimation();
            _flip_t2ba_a1 = _flip_t2ba.RegisterRotateX(_flipTime, 180, 360);
            _flip_t2ba_a2 = _flip_t2ba.RegisterVisibility(0, Visibility.Collapsed, _flipTime / 2, Visibility.Visible);
            _flip_t2ba_a3 = _flip_t2ba.RegisterVisibility(0, Visibility.Visible, _flipTime / 2, Visibility.Collapsed);

            _flip_b2t = new ColdAnimation();
            _flip_b2t_a1 = _flip_b2t.RegisterRotateX(_flipTime, 360, 180);
            _flip_b2t_a2 = _flip_b2t.RegisterVisibility(0, Visibility.Visible, _flipTime / 2, Visibility.Collapsed);
            _flip_b2t_a3 = _flip_b2t.RegisterVisibility(0, Visibility.Collapsed, _flipTime / 2, Visibility.Visible);

            _flip_b2ta = new ColdAnimation();
            _flip_b2ta_a1 = _flip_b2ta.RegisterRotateX(_flipTime, 180, 0);
            _flip_b2ta_a2 = _flip_b2ta.RegisterVisibility(0, Visibility.Collapsed, _flipTime / 2, Visibility.Visible);
            _flip_b2ta_a3 = _flip_b2ta.RegisterVisibility(0, Visibility.Visible, _flipTime / 2, Visibility.Collapsed);
        }

        private void Flip_r2l(UIElement container, UIElement front, UIElement back)
        {
            _flip_r2l.Animate(_flip_r2l_a1.On(container), _flip_r2l_a2.On(front), _flip_r2l_a3.On(back));
        }

        private void Flip_r2la(UIElement container, UIElement front, UIElement back)
        {
            _flip_r2la.Animate(_flip_r2la_a1.On(container), _flip_r2la_a2.On(front), _flip_r2la_a3.On(back));
        }

        private void Flip_l2r(UIElement container, UIElement front, UIElement back)
        {
            _flip_l2r.Animate(_flip_l2r_a1.On(container), _flip_l2r_a2.On(front), _flip_l2r_a3.On(back));
        }

        private void Flip_l2ra(UIElement container, UIElement front, UIElement back)
        {
            _flip_l2ra.Animate(_flip_l2ra_a1.On(container), _flip_l2ra_a2.On(front), _flip_l2ra_a3.On(back));
        }

        private void Flip_t2b(UIElement container, UIElement front, UIElement back)
        {
            _flip_t2b.Animate(_flip_t2b_a1.On(container), _flip_t2b_a2.On(front), _flip_t2b_a3.On(back));
        }

        private void Flip_t2ba(UIElement container, UIElement front, UIElement back)
        {
            _flip_t2ba.Animate(_flip_t2ba_a1.On(container), _flip_t2ba_a2.On(front), _flip_t2ba_a3.On(back));
        }

        private void Flip_b2t(UIElement container, UIElement front, UIElement back)
        {
            _flip_b2t.Animate(_flip_b2t_a1.On(container), _flip_b2t_a2.On(front), _flip_b2t_a3.On(back));
        }

        private void Flip_b2ta(UIElement container, UIElement front, UIElement back)
        {
            _flip_b2ta.Animate(_flip_b2ta_a1.On(container), _flip_b2ta_a2.On(front), _flip_b2ta_a3.On(back));
        }
        #endregion

        public static ColdFlipAnimation WithDuration(int milliseconds)
        {
            return new ColdFlipAnimation(milliseconds);
        }

        public void FlipLeft(UIElement container, UIElement front, UIElement back)
        {
            if (front.Visibility == Visibility.Visible)
                Flip_r2l(container, front, back);
            else
                Flip_r2la(container, front, back);
        }

        public void FlipRight(UIElement container, UIElement front, UIElement back)
        {
            if (front.Visibility == Visibility.Visible)
                Flip_l2r(container, front, back);
            else
                Flip_l2ra(container, front, back);
        }

        public void FlipDown(UIElement container, UIElement front, UIElement back)
        {
            if (front.Visibility == Visibility.Visible)
                Flip_t2b(container, front, back);
            else
                Flip_t2ba(container, front, back);
        }

        public void FlipUp(UIElement container, UIElement front, UIElement back)
        {
            if (front.Visibility == Visibility.Visible)
                Flip_b2t(container, front, back);
            else
                Flip_b2ta(container, front, back);
        }

        public bool FlippedHorizontally(UIElement container)
        {
            var pp = container.Projection as PlaneProjection;
            if (pp != null) return pp.RotationY == 180;
            else return false;
        }

        public bool FlippedVertically(UIElement container)
        {
            var pp = container.Projection as PlaneProjection;
            if (pp != null) return pp.RotationX == 180;
            else return false;
        }

        public void AdjustHorizontalOrientationOfContent(UIElement container, UIElement frontContent, UIElement backContent)
        {
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

        public void AdjustVerticalOrientationOfContent(UIElement container, UIElement frontContent, UIElement backContent)
        {
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

        public bool IsFlipping()
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

        public void Reset()
        {
            _flip_r2l.Reset();
            _flip_r2la.Reset();
            _flip_l2r.Reset();
            _flip_l2ra.Reset();
            _flip_t2b.Reset();
            _flip_t2ba.Reset();
            _flip_b2t.Reset();
            _flip_b2ta.Reset();
        }
    }
}
