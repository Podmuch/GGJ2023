<!-- TABLE OF CONTENTS -->
<h2 style="display: inline-block">Table of Contents</h2>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#installation-guide">Installation Guide</a></li>
      </ul>
      <ul>
        <li><a href="#event-bus">Event Bus</a></li>
      </ul>
        <ul>
        <li><a href="#register">Register</a></li>
      </ul>
      <ul>
        <li><a href="#fire">Fire</a></li>
      </ul>
      <ul>
        <li><a href="#event-collector">Event Collector</a></li>
      </ul>
    </li>
    <li><a href="#license">License</a></li>
  </ol>
  
<!-- ABOUT THE PROJECT -->
<a name="about-the-project"></a>
## About The Project
Simple Event Bus which can be used as a communication layer between objects in your application.

<a name="installation-guide"></a>
### Installation Guide
-------------------------------------------------------------------------------
Package can be installed via Unity Package Manager using git url:
https://docs.unity3d.com/Manual/upm-ui-giturl.html

<a name="event-bus"></a>
### Event Bus
-------------------------------------------------------------------------------
Package provides two abstract classes EventBusSingleton and EventBus. Just inherit from one of them to create your own event bus e.g.:
```
public class ProjectEventBus : EventBusSingleton<ProjectEventBus>
{
}
```
You can have multiple event buses in your project, but usually one is enough.

<a name="register"></a>
### Register
-------------------------------------------------------------------------------
To subscribe for specific events triggered by event bus, you need to register your method:
```
public class PlayerView
{
	public void Initialize(IEventBus eventBus)
	{
		eventBus.Register<PlayerMovedEvent>(OnPlayerMoved);
	}
	
	private void OnPlayerMoved(PlayerMovedEvent eventData)
	{
	}
}
```
If you don't want to subscribe longer, you need to unregister your method from event bus:
```
eventBus.Unregister<PlayerMovedEvent>(OnPlayerMoved);
```
Event classes have to inherit from EventData class:
```
public class PlayerMovedEvent : EventData { }
```

<a name="fire"></a>
### Fire
-------------------------------------------------------------------------------
To trigger event you need to use Fire method:
```
eventBus.Fire<PlayerMovedEvent>();
```
or:
```
eventBus.Fire(new PlayerMovementEvent());
```

<a name="event-collector"></a>
### Event Collector
-------------------------------------------------------------------------------
EventBus can store fired events in EventCollector if needed. It's useful when they contain data which can be used some time after the event was fired. 
For example we can have StartGameRequestEvent which triggers transition from menu to gameplay:
```
public class StartGameRequestEvent : EventData
{
	public string MapId;
}
```
And after transition we would like to know what were starting parameters. In this case we can get event by:
```
var startGameRequestEvent = eventBus.GetCollector().GetFirstEventWithType<StartGameRequestEvent>();
```
There are two ways to inform collector that it should store event:
- In inspector you can set **storeAllEventsInCollector** to store each event
- You can set **StoreInCollector** flag for specific event before firing it
```
eventBus.Fire(new StartGameRequestEvent()
{
	StoreInCollector = true
});
```
If event is no longer needed you can set **IsDestroyed** flag and then call:
```
eventBus.GetCollector().ClearDestroyed();
```

<a name="license"></a>
<!-- LICENSE -->
## License
Distributed under the MIT License. See `LICENSE` for more information.