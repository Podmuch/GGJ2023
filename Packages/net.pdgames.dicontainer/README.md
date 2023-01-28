<!-- TABLE OF CONTENTS -->
<h2 style="display: inline-block">Table of Contents</h2>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#installation-guide">Installation Guide</a></li>
      </ul>
      <ul>
        <li><a href="#dependency-injection-container">Dependency Injection Container</a></li>
      </ul>
        <ul>
        <li><a href="#context">Context</a></li>
      </ul>
      <ul>
        <li><a href="#register">Register</a></li>
      </ul>
      <ul>
        <li><a href="#fetch">Fetch</a></li>
      </ul>
      <ul>
        <li><a href="#getreference">GetReference</a></li>
      </ul>
    </li>
    <li><a href="#license">License</a></li>
  </ol>

<!-- ABOUT THE PROJECT -->
<a name="about-the-project"></a>
## About The Project
Simple Dependency Injection container which can be used as a base element to manage objects/references in your application.

<a name="installation-guide"></a>
### Installation Guide
-------------------------------------------------------------------------------
Package can be installed via Unity Package Manager using git url:
https://docs.unity3d.com/Manual/upm-ui-giturl.html

<a name="dependency-injection-container"></a>
### Dependency Injection Container
-------------------------------------------------------------------------------
Package provides two abstract classes DIContainerSingleton and DIContainer. Just inherit from one of them to create your own containers e.g.:
```
public class ProjectDIContainer : DIContainerSingleton<ProjectDIContainer>
{
}
```
You can have multiple containers in your project, but usually one is enough. Objects can be group in contexts if needed.

<a name="context"></a>
### Context
-------------------------------------------------------------------------------
Container can store only one object of specific type. If you want to have more, you need to put them into separate contexts.
- context should be provided for each method Register/Fetch/GetReferences
- by default container use empty/null context
- contexts can be nested
- each context is nested in empty/null context. It means when you try to find object with specific type, container will first look into provided context. When nothing is found, container will also check default context (null)

<a name="register"></a>
### Register
-------------------------------------------------------------------------------
To add object/reference to the container, you need to just register it:
```
diContainer.Register(objectToRegister);
```
Register method has two optional parameters: context, type:
- context - context in which object will be registered. By default null
- type - type for which this object will be returned (e.g. you would like to base class of the object). Type of the object is used by default

Objects can be unregisted if you want to remove them from container:
```
diContainer.Unregister(objectToRegister);
```

<a name="fetch"></a>
### Fetch
-------------------------------------------------------------------------------
To automatically fill object with references, you can use Fetch method. Each field with **[DIInject]** attribute will be checked, by container and filled with correct object (if object of specific type is registed in container). E.g.:
```
public class PlayerController
{
	[DIInject]
	private PlayerInput input;
	
	[DIInject]
	private PlayerView view;
	
	public void Initialize(IDIContainer diContainer)
	{
		diContainer.Fetch(this);
	}
}
```
Fetch method has two optional parameters:
- context - context where container will be looking for objects. By default null
- forceFetch - flag to force getting new reference even when field is not null. By default only empty fields will be filled with objects

<a name="getreference"></a>
### GetReference
-------------------------------------------------------------------------------
There is also possibility to get single reference from container by GetReference method:
```
var playerView = diContainer.GetReference<PlayerView>(null);
```
It's useful when you are not sure if object of specfic type is already registered in the container or if you don't have good place to call Fetch for an object (e.g. no initialization method).
GetReference has only one mandatory parameter: context.

<a name="license"></a>
<!-- LICENSE -->
## License
Distributed under the MIT License. See `LICENSE` for more information.