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
        <li><a href="#systems-framework">Systems Framework</a></li>
      </ul>
      <ul>
        <li><a href="#systems-cascade-controller">Systems Cascade Controller</a></li>
      </ul>
      <ul>
        <li><a href="#systems-cascade-data">Systems Cascade Data</a></li>
      </ul>
        <ul>
        <li><a href="#system-types">System Types</a></li>
      </ul>
    </li>
    <li><a href="#license">License</a></li>
  </ol>
  
<!-- ABOUT THE PROJECT -->
<a name="about-the-project"></a>
## About The Project
Systems-based architecture is one of the most common patterns. Usually it's combined with Entities and Components what can create together Entity-Component-System framework. 
However, systems can be used separately. It needs only some communication layer. This framework uses **Event Bus** for this purpose. In addition it uses **Dependency Injection Container** for storing and retrieving data objects.

<a name="installation-guide"></a>
### Installation Guide
-------------------------------------------------------------------------------
Package can be installed via Unity Package Manager using git url:
https://docs.unity3d.com/Manual/upm-ui-giturl.html

Apart from the package you need to install all dependencies listed below.

<a name="dependencies"></a>
### Dependencies
-------------------------------------------------------------------------------
- Event Bus - https://bitbucket.org/pdgames_net/eventbus
- Dependency Injection Container - https://bitbucket.org/pdgames_net/dicontainer

<a name="systems-framework"></a>
### Systems Framework
-------------------------------------------------------------------------------
System is a small piece of code which should be responsible only for one thing. For example:

- ShowWindowSystem - should be responsible for showing windows
- HideWindowSystem - should be responsible for hiding windows
- StartGameSystem - should trigger transition to gameplay
- etc.

Systems are managed by **Systems Cacade Controller** and executed in specific pre-defined order.

<a name="systems-cascade-controller"></a>
### Systems Cascade Controller
-------------------------------------------------------------------------------

Systems Cascade Controller is responsible for executing systems loop. Package contains abstract class, just inherit from it to create your own e.g.:
```
public class ProjectSystemsCascadeController : SystemsCascadeSingleton<ProjectSystemsCascadeController>
{
    [SerializeField]
    private ProjectDIContainer diContainer;
    [SerializeField]
    private ProjectEventBus eventBus;

    protected override SystemsCascadeData CreateSystems(IEventBus eventBus, IDIContainer diContainer)
    {
        return new ProjectSystemsCascadeData(eventBus, diContainer);
    }

    protected override IDIContainer GetDiContainer()
    {
        return diContainer;
    }

    protected override IEventBus GetEventBus()
    {
        return eventBus;
    }
}
```
Controller has a few parameters to set in inspector:

- UpdateType - controller can execute in different types of Updates (Normal, Fixed, Late),
- InitializeOnStart - controller has to be initialized. It can be initialized automatically on Start, or manually in any moment,
- IsDestroyableOnLoad - for singleton version. Controller can be destroyed during scenes transition or be persistent.

You can have multiple Systems Cascade Controllers in the application. For example:

- separate project-wise controller and gameplay controller,
- physics controller executed in FixedUpdate and standard controller executed in Update.

Systems which will be controlled by Systems Cascade Controller have to be defined in **Systems Cascade Data**.

<a name="systems-cascade-data"></a>
### Systems Cascade Data
-------------------------------------------------------------------------------

Systems Cascade Data contains systems which will be executed in order based on when they were added e.g.:
```
public class ProjectSystemsCascadeData : SystemsCascadeData
{
    public ProjectSystemsCascadeData(IEventBus eventBus, IDIContainer diContainer)
    {
        Add(new LoaderQueueSystems(eventBus, diContainer));
        Add(new CommandQueueSystems(eventBus, diContainer));

        Add(new UISystems(eventBus, diContainer));
    }
}
```
In this example LoaderQueueSystems will be executed first, then CommandQueueSystems and UISystems at the end. Systems Cascade Data can be nested, it means it can contain other SystemsCascadeDatas or single systems.

<a name="system-types"></a>
### System Types
-------------------------------------------------------------------------------
There is four base system types and one derived type:

- IInitializeSystem - initialize systems are called once during Systems Cascade Controller initialization,
- IExecuteSystem - execute systems are called each frame in specific update type of Systems Cascade Controller (Normal, Fixed, Late Update),
- ICleanupSystem - clean up systems are called each frame, but after all execute systems. Can be useful to clean up some temporary data at the end of the frame,
- ITearDownSystem - tear down systems are called once during Systems Cascade Controller deinitialization,
- ReactiveSystem - reactive system is an extension of IExecuteSystem. It's called only when specific type of event was fired,

Because systems are based on interfaces, one class can implement multiple of them. The most common examples are: IInitializeSystem + IExecuteSystem/ReactiveSystem e.g.:
```
public class UIShowWindowSystem : ReactiveSystem<UIShowWindowEvent>, IInitializeSystem
{
    private IDIContainer diContainer;

    public UIShowWindowSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
    {
        this.diContainer = diContainer;
    }

    public void Initialize()
    {
        diContainer.Fetch(this);
    }

    protected override void Execute(List<UIShowWindowEvent> eventDatas)
    {
        for (int i = 0; i < eventDatas.Count; i++)
        {
            eventDatas[i].window.Show();
        }
    }
}
```
```
public class UIShowWindowEvent : EventData
{
    public IBaseWindow window;
}
```

<a name="license"></a>
<!-- LICENSE -->
## License
Distributed under the MIT License. See `LICENSE` for more information.