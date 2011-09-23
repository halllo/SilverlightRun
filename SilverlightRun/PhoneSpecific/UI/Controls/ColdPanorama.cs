using Microsoft.Phone.Controls;
using SilverlightRun.Tombstoning;

namespace SilverlightRun.PhoneSpecific.UI
{
    /// <summary>
    /// Restores the active PanoramaItem after suspensions.
    /// </summary>
    public class ColdPanorama : Panorama
    {
        [SurvivesTombstoning]
        public int RememberedSelectedIndex
        {
            get { return SelectedIndex; }
            set { DefaultItem = Items[value]; }
        }

        public void RememberActivePanorama(IPhoneService phone)
        {
            TombstoneSurvivalEngine.SetupFor<ColdPanorama>(this, phone);
        }
    }
}
