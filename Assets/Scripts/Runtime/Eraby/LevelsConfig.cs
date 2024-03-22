using System;
using UnityEngine;




[Serializable]
public struct LevelConfig
{
    [SerializeField]
    private PlatformManagerConfig managerConfig;


    public readonly PlatformManagerConfig ManagerConfig => managerConfig;


}

[CreateAssetMenu(
    fileName = "LevelsConfig",
    menuName = "Eraby/LevelsConfig",
    order = 1
)]

public class LevelsConfig : ScriptableObject
{
    [SerializeField]
    private LevelConfig[] levelConfigs = null;

    public LevelConfig[] LevelConfigs => levelConfigs;

    public int LevelCount => levelConfigs.Length;


}
