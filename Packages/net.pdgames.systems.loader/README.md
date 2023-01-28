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
        <li><a href="#loader-queue">Loader Queue</a></li>
      </ul>
        <ul>
        <li><a href="#loader-step">Loader Step</a></li>
      </ul>
      <ul>
        <li><a href="#binding-user-interface">Binding User Interface</a></li>
      </ul>
    </li>
    <li><a href="#license">License</a></li>
  </ol>
  
<!-- ABOUT THE PROJECT -->
<a name="about-the-project"></a>
## About The Project
Loader is a module based on Systems framework which allow to organize initial loading and transitions in the application. To enable loader you just need to add LoaderQueueSystems to your systems cascade data:
```
Add(new LoaderQueueSystems(eventBus, diContainer));
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

<a name="loader-queue"></a>
### Loader Queue
-------------------------------------------------------------------------------
LoaderQueue is a holder for **loader steps**. To start transition loader steps have to be added to the queue and then the queue should be started, by setting **IsStarted** flag. Example of a system which trigger initial loading with four steps:

```
public sealed class StartInitialLoadingSystem : ReactiveSystem<StartInitialLoadingEvent>, IInitializeSystem
{
	[DIInject] 
	private LoaderQueue loaderQueue = default;
	
	private IDIContainer diContainer;

	public StartInitialLoadingSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
	{
		this.diContainer = diContainer;
	}

	public void Initialize()
	{
		diContainer.Fetch(this);
	}

	protected override void Execute(List<StartInitialLoadingEvent> entities)
	{
		loaderQueue.List.Add(new LoadWindowsLoaderStep(eventBus, DefinedStrings.initialLoading));
		loaderQueue.List.Add(new LoadBackgroundLoaderStep(eventBus, diContainer));
		loaderQueue.List.Add(new InitializeMainCameraLoaderStep(eventBus, diContainer));
		loaderQueue.List.Add(new InitialLoadingEndLoaderStep(eventBus, diContainer));
		
		loaderQueue.IsStarted = true;
	}
```
}

<a name="loader-step"></a>
### Loader Step
-------------------------------------------------------------------------------
Loader Step is a single step which should contain piece of logic needed for transition. To create new step you have to inherit from LoaderStep class. There are a few abstract methods, which has to be implemented:

- IsReady - when step is done, method should return **true** to allow loading queue go further,
- GetProgress - method can be used to update user interface based on loading progress (e.g. updating loading bar),
- GetDescription - method can be used to update user interface based on loading progres (e.g. displaying user-friendly description of current loading step),
- PerformStep - method which should contain logic e.g. loading resources, scenes etc.

Steps were designed to perform actions taking more than one frame. Therefore loading queue will check periodically progress and readiness of current loader step.
Example of loading step which will load necessary windows asynchronously:

```
public class LoadWindowsLoaderStep : LoaderStep
{
    [DIInject]
    private UIHolder uiHolder = default;

    private const string windowsResourcesPath = "Windows/";

    private IDIContainer diContainer;

    private bool isReady = false;
    private int loadedWindows = 0;
    private int windowsToLoad = 0;

    public LoadWindowsLoaderStep(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
    {
        this.diContainer = diContainer;
    }

    public override string GetDescription()
    {
        return "Preparing User Interface";
    }

    public override float GetProgress()
    {
        return windowsToLoad > 0 ? (float)loadedWindows / (float) windowsToLoad : 1.0f;
    }

    public override bool IsReady()
    {
        return isReady;
    }

    public override void PerformStep()
    {
        diContainer.Fetch(this);

        var windowsToLoadList = uiHolder.GetWindowsToLoad();
        windowsToLoad = windowsToLoadList.Count;

        StartWindowsLoading(windowsToLoadList);
    }

    private void StartWindowsLoading(List<string> windowsToLoadList)
    {
        for (int i = 0; i < windowsToLoadList.Count; i++)
        {
            var windowKey = windowsToLoadList[i];
            var resourceRequest = Resources.LoadAsync<BaseWindow>(windowsResourcesPath + windowKey);
            resourceRequest.completed += OnWindowLoaded;
        }
    }

    private void OnWindowLoaded(AsyncOperation operation)
    {
        var resourceRequest = operation as ResourceRequest;

        var windowPrefab = resourceRequest.asset as GameObject;
        uiHolder.InitializeWindow(windowPrefab);

        loadedWindows++;
        isReady = isReady || loadedWindows == windowsToLoad;
    }
}
```

There is also an option to override FinishStep method what can be useful to make clean up when transition was terminated:

```
public override void FinishStep()
{
	base.FinishStep();
	for (int i = 0; i < resourceRequestsList.Count; i++)
	{
		resourceRequestsList[i].completed -= OnWindowLoaded;
	}
}
```

<a name="binding-user-interface"></a>
### Binding User Interface
-------------------------------------------------------------------------------
Loader Queue Systems are triggering a few events which can be useful to bind loader state and progress to user interface:

- LoaderQueueStateEvent - is triggered when queue starts or ends processing (IsStarted flag changes it's value). Can be used to Show/Hide loading window:
```
public sealed class ShowLoadingWindowWhenLoaderStartedSystem : ReactiveSystem<LoaderQueueStateEvent>, IInitializeSystem
{
	[DIInject] 
	private LoaderQueue loaderQueue = default;
    [DIInject]
    private UIHolder uiHolder = default;

	private IDIContainer diContainer;

	public ShowLoadingWindowWhenLoaderStartedSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
	{
		this.diContainer = diContainer;
	}

	public void Initialize()
	{
		diContainer.Fetch(this);
	}

	protected override void Execute(List<LoaderQueueStateEvent> eventDatas)
	{
		if (loaderQueue.IsStarted)
		{
			var loadingWindow = uiHolder.GetWindow<LoadingWindow>();
			if (loadingWindow != null && !loadingWindow.IsShown)
			{
				loadingWindow.Show();
			}
		}
	}
}
```

- LoaderProgressEvent - is triggered each frame when queue is working. Can be used to update loading progress bar:
```
public sealed class UpdateLoadingWindowWhenLoaderProgressChangedSystem : ReactiveSystem<LoaderProgressEvent>, IInitializeSystem
{
	[DIInject]
	private UIHolder uiHolder = default;

	private IDIContainer diContainer;

	public UpdateLoadingWindowWhenLoaderProgressChangedSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
	{
		this.diContainer = diContainer;
	}

	public void Initialize()
	{
		diContainer.Fetch(this);
	}

	protected override void Execute(List<LoaderProgressEvent> eventDatas)
	{
		var loadingWindow = uiHolder.GetWindow<LoadingWindow>();
		if (loadingWindow != null && loadingWindow.IsShown)
		{
			for (int i = 0; i < eventDatas.Count; i++)
			{
				loadingWindow.UpdateView(eventDatas[i].Progress, eventDatas[i].Description, 
					eventDatas[i].CurrentStepIndex.ToString(), eventDatas[i].TotalSteps.ToString());
			}
		}
	}
}
```

- LoaderFinishedEvent - is triggered when queue ends processing. Can be used to Hide loading window:
```
public sealed class HideLoadingWindowWhenLoaderEndedSystem : ReactiveSystem<LoaderFinishedEvent>, IInitializeSystem
{
	[DIInject]
	private UIHolder uiHolder = default;

	private IDIContainer diContainer;
	
	public HideLoadingWindowWhenLoaderEndedSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
	{
		this.diContainer = diContainer;
	}

	public void Initialize()
	{
		diContainer.Fetch(this);
	}
	
	protected override void Execute(List<LoaderFinishedEvent> entities)
	{
		var loadingWindow = uiHolder.GetWindow<LoadingWindow>();
		if (loadingWindow != null && loadingWindow.IsShown)
		{
			loadingWindow.Hide();
		}
	}
}
```

<a name="license"></a>
<!-- LICENSE -->
## License
Distributed under the MIT License. See `LICENSE` for more information.