using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "ShabbaDragSettingsData", menuName = "ScriptableObjects/ShabbaDragSettings", order = 2)]
public class DragSettingsData : ScriptableObject
{
    public AnimationCurve dragEvolutionCurve;

}
