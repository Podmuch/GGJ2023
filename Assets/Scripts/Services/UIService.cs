using System;
using System.Collections.Generic;
using BoxColliders.Project;
using PDGames.UserInterface;
//using Windows;
using UnityEngine;

namespace Services
{
    public class UIService : Service
    {
        private const string windowsPath = "Windows/";
        
    private List<IBaseWindow> hudWindows = new List<IBaseWindow>();
    private List<IBaseWindow>  modalWindows = new List<IBaseWindow>();
    private List<IBaseWindow>  standardWindows = new List<IBaseWindow>();
    //     
    public override void Initialize()
    {
    //         base.Initialize();
    //         var windows = MonoBehaviour.FindObjectsOfType<Window>(true);
    //         
    //         foreach (var window in windows)
    //         {
    //             switch(window.WindowType)
    //             {
    //                 case WindowType.Hud:
    //                     hudWindows.Add(window);
    //                     break;
    //                 case WindowType.Modal:
    //                     modalWindows.Add(window);
    //                     break;
    //                 case WindowType.Standard:
    //                     standardWindows.Add(window);
    //                     break;
    //             }
    //         }
    //         
    
        Debug.Log("<color=green>[Services]</color> UI Service has been Initialized");
    }

    public void ShowWindow<T>()
    {
        ProjectEventBus.Instance.Fire(new UiShowWindowEvent() { Type = typeof(T)});
    }
    
    public void HideWindow<T>()
    {
        ProjectEventBus.Instance.Fire(new UiHideWindowEvent() { Type = typeof(T) });
    }

    public T GetWindow<T>() where T : class
    {
        var foundWindow = MonoBehaviour.FindObjectOfType(typeof(T));
        if (foundWindow != null) return foundWindow as T;

        return null;
    }
    
    //
    public override void Deinitialize()
    {
        base.Deinitialize();
        Debug.Log("<color=green>[Services]</color> UI Service has been Deinitialized");
    }
    //
    //     public T GetWindow<T>() where T : Window
    //     {
    //         foreach (var hudWindow in hudWindows)
    //         {
    //             if(hudWindow is T)
    //             return hudWindow as T;
    //         }
    //         
    //         foreach (var modalWindow in modalWindows)
    //         {
    //             if(modalWindow is T)
    //             return modalWindow as T;
    //         }
    //         
    //         foreach (var standardWindow in standardWindows)
    //         {
    //             if(standardWindow is T)
    //             return standardWindow as T;
    //         }
    //         
    //         return null;
    //     }
    //     
    //     public void HideAllWindows()
    //     {
    //         foreach (var hudWindow in hudWindows)
    //         {
    //             hudWindow.Hide();
    //         }
    //         
    //         foreach (var modalWindow in modalWindows)
    //         {
    //             modalWindow.Hide();
    //         }
    //         
    //         foreach (var standardWindow in standardWindows)
    //         {
    //             standardWindow.Hide();
    //         }
    //     }
    //     
    //     public void HideOtherWindows<T>() where T : Window
    //     {
    //         foreach (var hudWindow in hudWindows)
    //         {
    //             if(hudWindow is T)
    //                 continue;
    //             
    //             hudWindow.Hide();
    //         }
    //         
    //         foreach (var modalWindow in modalWindows)
    //         {
    //             if(modalWindow is T)
    //             continue;
    //             
    //             modalWindow.Hide();
    //         }
    //         
    //         foreach (var standardWindow in standardWindows)
    //         {
    //             if(standardWindow is T)
    //                 continue;
    //             
    //             standardWindow.Hide();
    //         }
    //     }
    }
}