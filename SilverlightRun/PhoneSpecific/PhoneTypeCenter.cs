using SilverlightRun.DI;

namespace SilverlightRun.PhoneSpecific
{
    /// <summary>
    /// Provides a basic type locator functionality with default IPhoneService initialized.
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
            return _container.GetA<T>();
        }
    }
}
