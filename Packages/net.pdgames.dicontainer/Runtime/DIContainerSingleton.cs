using UnityEngine;

namespace PDGames.DIContainer
{
    public abstract class DIContainerSingleton<T> : DIContainer where T : MonoBehaviour
    {
        protected static IDIContainer instance;

        public static IDIContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    var ins = FindObjectOfType<T>();
                    if (ins != null) instance = ins as IDIContainer;
                }
                return instance;
            }
            protected set { instance = value; }
        }

        public override void Initialize()
        {
            base.Initialize();
            if(instance == null) Instance = this;
        }

        public override void Deinitialize()
        {
            base.Deinitialize();
            if(ReferenceEquals(instance, this)) Instance = null;
        }
    }
}