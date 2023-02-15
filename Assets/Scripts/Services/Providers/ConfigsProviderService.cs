using Services;
using UnityEngine;

[RequireService(typeof(GameplayDataService))]
public class ConfigsProviderService : Service
{
    private const string configsPath = "Configs/";
    
    // public BaseConfigurationConfig BaseConfigurationConfig { get; private set; }
    // public RoundsConfig RoundsConfig { get; private set; }
    // public TargetsConfig TargetsConfig { get; private set; }
    //
    public override void Initialize()
    {
    //     base.Initialize();
    //     BaseConfigurationConfig = Resources.Load<BaseConfigurationConfig>(configsPath +BaseConfigurationConfig.Key);
    //     RoundsConfig = Resources.Load<RoundsConfig>(configsPath +RoundsConfig.Key);
    //     TargetsConfig = Resources.Load<TargetsConfig>(configsPath +TargetsConfig.Key);
        Debug.Log("<color=green>[Services]</color> Configs Provider Service has been Initialized");
    }

    public override void Deinitialize()
    {
        Debug.Log("<color=green>[Services]</color> Configs Provider Service has benn Deinitialized");
    }
}
