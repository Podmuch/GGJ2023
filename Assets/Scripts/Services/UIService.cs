using System.Collections.Generic;
//using Windows;
using UnityEngine;

namespace Services
{
    public class UIService : Service
    {
        private const string windowsPath = "Windows/";
        
    //     private List<IWindow> hudWindows = new List<IWindow>();
    //     private List<IWindow>  modalWindows = new List<IWindow>();
    //     private List<IWindow>  standardWindows = new List<IWindow>();
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