using SilverlightRun.DI;

namespace SilverlightRunSample.VM
{
    public class ViewModelCenter : SilverlightRun.PhoneSpecific.PhoneTypeCenter
    {
        protected override void ContainerSetup(GenericSimpleContainer container)
        {
            container.DeclareSingleton<VM.MainPage, VM.MainPage>();
        }
    }
}
