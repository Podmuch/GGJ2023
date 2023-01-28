using UnityEngine;

namespace PDGames.Systems
{
    public abstract class SystemsCascadeSingleton<T> : SystemsCascadeController where T : SystemsCascadeController
    {
        public static T Instance { get; protected set; }
        
        [SerializeField] 
        protected bool isDestroyableOnLoad = true;
        
        #region MONO BEHAVIOUR

        protected override void Start()
        {
			if (Instance == null)
			{
				Instance = (T)GetComponent(typeof(T));
				if (!isDestroyableOnLoad)
				{
					DontDestroyOnLoad(gameObject);
				}
			}
			else
			{
				Destroy(gameObject);
				return;
			}
            base.Start();
        }

        protected override void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
            base.OnDestroy();
        }

        #endregion
    }
}