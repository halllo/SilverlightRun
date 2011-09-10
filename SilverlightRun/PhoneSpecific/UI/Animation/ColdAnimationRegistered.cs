using System.Windows;

namespace SilverlightRun.PhoneSpecific.UI.Animation
{
    /// <summary>
    /// Represents an UIElement independent timeline.
    /// </summary>
    public class ColdAnimationRegistered
    {
        public bool RequiresPlaneProjection { get; internal set; }

        internal ColdAnimationRegistered()
        {
            RequiresPlaneProjection = false;
        }

        public ColdAnimationTargeted On(UIElement uie)
        {
            return new ColdAnimationTargeted
            {
                RequiresPlaneProjection = RequiresPlaneProjection,
                Target = uie,
                Registered = this
            };
        }
    }
}
