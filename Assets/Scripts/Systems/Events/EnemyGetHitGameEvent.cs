using PDGames.EventBus;

namespace BoxColliders.Project
{
    public sealed class EnemyGetHitEvent : EventData
    {
        public int EnemyId;
        public float PushPower;
    }
}