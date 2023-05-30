using System.Collections.Generic;
using BoxColliders.Project;
using DG.Tweening;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class GameSlowMotionReactSystem : ReactiveSystem<GameSlowMotionEvent>, IInitializeSystem
    {
        private IDIContainer diContainer;
        private object diContext;

        public GameSlowMotionReactSystem(IEventBus eventBus, IDIContainer diContainer, object diContext) :
            base(eventBus)
        {
            this.diContainer = diContainer;
            this.diContext = diContext;
        }

        public void Initialize()
        {
            diContainer.Fetch(this, diContext);
        }

        protected override void Execute(List<GameSlowMotionEvent> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].IsDestroyed = true;
            }

            var data = entities[^1];

            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, data.targetTimeScale, data.duration)
                .SetEase(data.ease).SetUpdate(true).onComplete = () =>
            {
                DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1, 0.1f);};
        }
    }
}