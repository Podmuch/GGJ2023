using DG.Tweening;
using PDGames.EventBus;

namespace BoxColliders.Project
{
    public sealed class GameSlowMotionEvent : EventData
    {
        public float targetTimeScale;
        public float duration;
        public Ease ease;
    }
}