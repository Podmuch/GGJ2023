using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class GameClearDiContainerSystem : ITearDownSystem
    {
        private IDIContainer diContainer;
        private object diContext;

        public GameClearDiContainerSystem(IEventBus eventBus, IDIContainer diContainer, object diContext)
        {
            this.diContainer = diContainer;
            this.diContext = diContext;
        }
        
        public void TearDown()
        {
            diContainer.Unregister(typeof(GameplayContextsHolder));

            var objectsToDestroy = diContainer.GetReferences<MonoBehaviour>(diContext);
            for (int i = 0; i < objectsToDestroy.Count; i++)
            {
                if (objectsToDestroy[i] != null && objectsToDestroy[i].gameObject != null)
                {
                    GameObject.Destroy(objectsToDestroy[i].gameObject);
                }
            }
            
            diContainer.ClearContext(diContext);
        }
    }
}