<!-- TABLE OF CONTENTS -->
<h2 style="display: inline-block">Table of Contents</h2>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#installation-guide">Installation Guide</a></li>
      </ul>
      <ul>
        <li><a href="#dependencies">Dependencies</a></li>
      </ul>
      <ul>
        <li><a href="#commands-queue">Commands Queue</a></li>
      </ul>
        <ul>
        <li><a href="#command">Command</a></li>
      </ul>
      <ul>
        <li><a href="#command-priorities">Command Priorities</a></li>
      </ul>
    </li>
    <li><a href="#license">License</a></li>
  </ol>
  
<!-- ABOUT THE PROJECT -->
<a name="about-the-project"></a>
## About The Project
Commands is a module based on Systems framework which allow to organize and prioritize application logic. The logic can be encapsulated in command object and then pushed to the queue and executed only if nothing else is processed. To enable commands you just need to add CommandQueueSystems to your systems cascade data:
```
Add(new CommandQueueSystems(eventBus, diContainer));
```

<a name="installation-guide"></a>
### Installation Guide
-------------------------------------------------------------------------------
Package can be installed via Unity Package Manager using git url:
https://docs.unity3d.com/Manual/upm-ui-giturl.html

Apart from the package you need to install all dependencies listed below.

<a name="dependencies"></a>
### Dependencies
-------------------------------------------------------------------------------
- Systems Framework - https://bitbucket.org/pdgames_net/systems
- Event Bus - https://bitbucket.org/pdgames_net/eventbus
- Dependency Injection Container - https://bitbucket.org/pdgames_net/dicontainer

<a name="commands-queue"></a>
### Commands Queue
-------------------------------------------------------------------------------
CommandQueue is a holder for commands. Adding new command to the queue is done by triggering CommandPushToQueueEvent e.g.:
```
var startArenaIntro = new GameStartArenaIntroCommand(eventBus, diContainer, diContext);
eventBus.Fire<CommandPushToQueueEvent>(new CommandPushToQueueEvent(){Command = startArenaIntro, Priority = CommandPriority.Normal});
```

Command Queue has to be opened to process collected commands. It can be done by setting **IsOpened** flag to true.

<a name="command"></a>
### Command
-------------------------------------------------------------------------------
Command is a single object containing piece of logic which should be executed in specific order and conditions (when no other command is in progress). For example while opening menu we would like to perform a few non-instant operations like:

- show animation of granted rewards,
- show awaiting popups,
- show notifications and tips when no other elements are displayed on top.

It can be done by creating three commands and the command queue will ensure that they won't be triggered at once and overlap each other.

To create new command you have to inherit from **AbstractCommand** class. There are two abstract methods to implemented:

- IsReady - when command is done, method should return **true** to allow queue to go the next one,
- PerformCommand - method which should contain logic e.g. animation, calculations etc.

Example of command which will trigger animation of showing rewards after the game and wait until it is done:
```
public sealed class MenuTryShowRewardsCommand : AbstractCommand
{
	[DIInject]
	private UIHolder uiHolder = default;

	private IDIContainer diContainer;
	
	private bool isReady = false;
	
	private Sequence rewardsSequence = null;
	
	public MenuTryShowRewardsCommand(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
	{
		this.diContainer = diContainer;
	}

	public override void PerformCommand()
	{
		diContainer.Fetch(this);
		rewardsSequence = uiHolder.GetWindow<MainWindow>().GetShowRewardsAfterGameSequence();
		rewardsSequence.OnComplete(OnShowRewardFinished);
		rewardsSequence.Play();
	}

	public override bool IsReady()
	{
		return isReady;
	}

	private void OnShowRewardFinished()
	{
		isReady = true;
		rewardsSequence = null;
	}
}
```

There is also an option to override FinishStep method what can be useful to make clean up when commands are terminated:
```
public override void FinishCommand()
{
	base.FinishCommand();
	if (rewardsSequence != null) rewardsSequence.Kill();
}
```

<a name="command-priorities"></a>
### Command Priorities
-------------------------------------------------------------------------------
Processing order depends on two factors:

- order of adding command to the command queue,
- priority with which command has been added.

There are a few predefined priorities defined as **CommandPriority** enum:

- Critical - when a critical command is added to the queue, all enqueued commands with lower priority will be removed. It can be used for example to force end game,
- High - high priority commands will be performed before normal even when they were added to the queue earlier,
- Normal - priority which should be used by default,
- Low - low priority commands will be removed from the queue when any other command with higher priority is added.

Priority should be set when an event is pushed to the queue via CommandPushToQueueEvent:
```
eventBus.Fire<CommandPushToQueueEvent>(new CommandPushToQueueEvent(){Command = commandToAdd, Priority = CommandPriority.Normal});
```

<a name="license"></a>
<!-- LICENSE -->
## License
Distributed under the MIT License. See `LICENSE` for more information.