﻿using System;
using PDGames.EventBus;

namespace PDGames.Systems.Commands
{
    [Serializable]
    public sealed class CommandExecutedEvent : EventData
    {
        public AbstractCommand Command;
    }
}
