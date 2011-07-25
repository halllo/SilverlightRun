using SilverlightRun.DI;

namespace SilverlightRun.PhoneSpecific
{
    /// <summary>
    /// Provides a basic type locator functionality with default IPhoneService initialized.
    /// Best staticly initialized in App.xaml.cs to be accessible everywhere.
    /// </summary>
    public abstract class PhoneTypeCenter
    {
        GenericSimpleContainer _container;

        public PhoneTypeCenter()
        {
            _container = new GenericSimpleContainer();
            _container.DeclareSingleton<IPhoneService, PhoneServiceAdapter>();
            ContainerSetup(_container);
        }

        protected abstract void ContainerSetup(GenericSimpleContainer container);

        public T Get<T>()
        {
            return _container.Prepare(_container.GetA<T>());
        }
    }
}
