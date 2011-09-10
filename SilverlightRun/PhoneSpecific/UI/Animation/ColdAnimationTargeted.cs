using System.Windows;

namespace SilverlightRun.PhoneSpecific.UI.Animation
{
    /// <summary>
    /// Represents an UIElement dependent timeline.
    /// </summary>
    public class ColdAnimationTargeted
    {
        public bool RequiresPlaneProjection { get; internal set; }
        public UIElement Target { get; internal set; }
        public ColdAnimationRegistered Registered { get; internal set; }

        internal ColdAnimationTargeted()
        { }
    }
}
