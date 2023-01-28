using System;
using PDGames.EventBus;

namespace PDGames.UserInterface
{
    public sealed class UiShowWindowEvent : EventData
    {
        public string Id;
        public Type Type;
    }
}