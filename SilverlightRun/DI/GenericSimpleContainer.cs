
namespace SilverlightRun.DI
{
    /// <summary>
    /// Generic wrapper for SimpleContainer.
    /// Allows per-request or singleton returning.
    /// </summary>
    public class GenericSimpleContainer
    {
        private SimpleContainer Container { get; set; }

        public GenericSimpleContainer()
        {
            Container = new SimpleContainer();
        }

        public T New<T>()
        {
            return (T)Container.BuildInstance(typeof(T));
        }

        public T GetA<T>()
        {
            return (T)Container.GetInstance(typeof(T), null);
        }

        public void DeclareRequest<S, T>()
        {
            Container.RegisterPerRequest(typeof(S), null, typeof(T));
        }

        public void DeclareSingleton<S, T>()
        {
            Container.RegisterSingleton(typeof(S), null, typeof(T));
        }
    }
}
