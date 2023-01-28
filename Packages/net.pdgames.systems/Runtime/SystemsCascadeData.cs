using System.Collections.Generic;

namespace PDGames.Systems
{
    public class SystemsCascadeData
    {
        public List<IExecuteSystem> ExecuteSystems = new List<IExecuteSystem>();
        public List<IInitializeSystem> InitializeSystems = new List<IInitializeSystem>();
        public List<ITearDownSystem> TearDownSystems = new List<ITearDownSystem>();
        public List<ICleanupSystem> CleanupSystems = new List<ICleanupSystem>();

        public SystemsCascadeData()
        {

        }

        public void Add(IBaseSystem newSystem)
        {
            if (newSystem is IExecuteSystem)
            {
                ExecuteSystems.Add(newSystem as IExecuteSystem);
            }

            if (newSystem is IInitializeSystem)
            {
                InitializeSystems.Add(newSystem as IInitializeSystem);
            }

            if (newSystem is ITearDownSystem)
            {
                TearDownSystems.Add(newSystem as ITearDownSystem);
            }

            if (newSystem is ICleanupSystem)
            {
                CleanupSystems.Add(newSystem as ICleanupSystem);
            }
        }

        public void Add(SystemsCascadeData newCascade)
        {
            ExecuteSystems.AddRange(newCascade.ExecuteSystems);
            InitializeSystems.AddRange(newCascade.InitializeSystems);
            TearDownSystems.AddRange(newCascade.TearDownSystems);
            CleanupSystems.AddRange(newCascade.CleanupSystems);
            newCascade.Clear();
        }

        public void Clear()
        {
            ExecuteSystems.Clear();
            InitializeSystems.Clear();
            TearDownSystems.Clear();
            CleanupSystems.Clear();
        }
    }
}