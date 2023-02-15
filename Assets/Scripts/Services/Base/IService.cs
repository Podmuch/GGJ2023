namespace Services
{
    public interface IService
    {
        bool IsInitialized { get;}
    
        void Initialize();
    
        void Deinitialize();
    }
}
