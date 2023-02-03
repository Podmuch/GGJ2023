using System;
using PDGames.EventBus;

namespace PDGames.UserInterface
{
    public sealed class UiHideWindowEvent : EventData
    {
        public string Id;
        public Type Type;
    }
}