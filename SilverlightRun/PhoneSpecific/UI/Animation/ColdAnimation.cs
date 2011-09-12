using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SilverlightRun.PhoneSpecific.UI.Animation
{
    /// <summary>
    /// Allows rotations and visibility changes to be animated.
    /// </summary>
    public class ColdAnimation
    {
        Dictionary<ColdAnimationRegistered, Timeline> _animations;
        Storyboard _allAnimations;

        public ColdAnimation()
        {
            _animations = new Dictionary<ColdAnimationRegistered, Timeline>();
        }

        public static PlaneProjection EnsurePlaneProjection(UIElement uie)
        {
            if (uie.Projection as PlaneProjection == null) uie.Projection = new PlaneProjection();
            return uie.Projection as PlaneProjection;
        }

        public void Animate(params ColdAnimationTargeted[] targets)
        {
            Reset();
            BuildAndStartNewStoryboard(targets);
        }

        public void Reset()
        {
            if (_allAnimations != null)
            {
                _allAnimations.Stop();
                _allAnimations.Children.Clear();
            }
        }

        private void BuildAndStartNewStoryboard(ColdAnimationTargeted[] targets)
        {
            _allAnimations = new Storyboard { RepeatBehavior = new RepeatBehavior(1) };
            foreach (var target in targets) AnimateTarget(_allAnimations, target);
            _allAnimations.Begin();
        }

        private void AnimateTarget(Storyboard allAnimation, ColdAnimationTargeted target)
        {
            if (_animations.ContainsKey(target.Registered))
            {
                if (target.RequiresPlaneProjection) EnsurePlaneProjection(target.Target);
                var animation = _animations[target.Registered];
                Storyboard.SetTarget(animation, target.Target);
                allAnimation.Children.Add(animation);
            }
        }

        public bool IsRunning
        {
            get
            {
                return _allAnimations != null ? _allAnimations.GetCurrentState() == ClockState.Active : false;
            }
        }

        public ColdAnimationRegistered RegisterRotateX(int msec, double from, double to)
        {
            return RegisterRotation(msec, from, to, "RotationX");
        }

        public ColdAnimationRegistered RegisterRotateY(int msec, double from, double to)
        {
            return RegisterRotation(msec, from, to, "RotationY");
        }

        public ColdAnimationRegistered RegisterRotateZ(int msec, double from, double to)
        {
            return RegisterRotation(msec, from, to, "RotationZ");
        }

        public ColdAnimationRegistered RegisterVisibility(int msec, Visibility visibility)
        {
            return RegisterVisibility(NewVisibilityChange(msec, visibility));
        }

        public ColdAnimationRegistered RegisterVisibility(int msec1, Visibility visibility1, int msec2, Visibility visibility2)
        {
            return RegisterVisibility(NewVisibilityChange(msec1, visibility1, msec2, visibility2));
        }

        private ColdAnimationRegistered RegisterVisibility(ObjectAnimationUsingKeyFrames visibilityChange)
        {
            var registered = new ColdAnimationRegistered();
            _animations.Add(registered, visibilityChange);
            return registered;
        }

        private ColdAnimationRegistered RegisterRotation(int msec, double from, double to, string rotation)
        {
            var registered = new ColdAnimationRegistered { RequiresPlaneProjection = true };
            _animations.Add(registered, NewRotation(msec, from, to, rotation));
            return registered;
        }

        private DoubleAnimationUsingKeyFrames NewRotation(int msec, double from, double to, string rotation)
        {
            var dakf = new DoubleAnimationUsingKeyFrames
            {
                KeyFrames = { 
                    new EasingDoubleKeyFrame { KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 0)), Value = from }, 
                    new EasingDoubleKeyFrame { KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, msec)), Value = to }
                }
            };
            Storyboard.SetTargetProperty(dakf, new PropertyPath("(UIElement.Projection).(PlaneProjection." + rotation + ")"));
            return dakf;
        }

        private ObjectAnimationUsingKeyFrames NewVisibilityChange(int msec1, Visibility visib1)
        {
            return NewVisibilityChange(
                new DiscreteObjectKeyFrame { KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, msec1)), Value = visib1 });
        }

        private ObjectAnimationUsingKeyFrames NewVisibilityChange(int msec1, Visibility visib1, int msec2, Visibility visib2)
        {
            return NewVisibilityChange(
                new DiscreteObjectKeyFrame { KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, msec1)), Value = visib1 },
                new DiscreteObjectKeyFrame { KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, msec2)), Value = visib2 });
        }

        private ObjectAnimationUsingKeyFrames NewVisibilityChange(params DiscreteObjectKeyFrame[] keyFrames)
        {
            var oakf = new ObjectAnimationUsingKeyFrames();
            foreach (var keyFrame in keyFrames)
            {
                oakf.KeyFrames.Add(keyFrame);
            }
            Storyboard.SetTargetProperty(oakf, new PropertyPath("(UIElement.Visibility)"));
            return oakf;
        }
    }
}
