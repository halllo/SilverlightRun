using System;

namespace SilverlightRun.PhoneSpecific.UI
{
    /// <summary>
    /// Holds parameter characeterizing zooming status.
    /// </summary>
    public class ZoomChangedEventArgs : EventArgs
    {
        public double ZoomFactor { get; set; }
    }
}
