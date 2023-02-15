using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Services
{
    public class ServicesManager
    {
        private List<IService> services = new List<IService>();

        public List<IService> Services
        {
            get => services;
        }

        public void Initialize()
        {
            services = (List<IService>) GetInstancedListOfServices<IService>();
            InitialzeServices();
        }
        
        public void Deinitialize()
        {
            DeinitializeServices();
        }
        
        public T GetService<T>() where T : class
        {
            foreach (var service in services)
            {
                if (service is T) return service as T;
            }

            return null;
        }
        
        private IEnumerable<T> GetInstancedListOfServices<T>() where T : class
        {
            List<T> services = new List<T>();
            
            var interfaceType = typeof(T); 
            
            var servicesTypes = Assembly.GetAssembly(typeof(T)).GetTypes().Where(serviceType =>
                serviceType.IsClass && !serviceType.IsAbstract && interfaceType.IsAssignableFrom(serviceType));

            foreach (Type type in
                servicesTypes)
            {
                services.Add((T) Activator.CreateInstance(type));
            }
            
            return services;
        }
        
        private void InitialzeServices()
        {
            foreach (var service in services)
            { 
                InitializeService(service);
            }
        }

        private void InitializeService(IService serviceForInitialization)
        {
            if (serviceForInitialization.IsInitialized) return;
            
            var serviceForInitializationType = serviceForInitialization.GetType();
            var requireServiceAttribute = serviceForInitializationType.GetCustomAttribute<RequireService>();

            if (requireServiceAttribute != null)
            {
                foreach (var service in services)
                {
                    foreach (var requiredService in requireServiceAttribute.requiredServices)
                    {
                        var serviceType = service.GetType();
                        if (requiredService == serviceType)
                        {
                            InitializeService(service);
                        }
                    }
                }
            }
            
            serviceForInitialization.Initialize();
        }

        private void DeinitializeServices()
        {
            foreach (var service in services)
            {
                service.Deinitialize();
            }
            services.Clear();
        }
    }
}
