namespace Services
{
    public abstract class Service : IService
    {
        public bool IsInitialized { get; private set; }
        
        public virtual void Initialize()
        {
            IsInitialized = true;
        }
        
        public virtual void Deinitialize()
        {
            IsInitialized = false;
        }
    }
}
