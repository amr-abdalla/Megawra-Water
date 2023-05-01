using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "ShabbaDragSettingsData", menuName = "ScriptableObjects/ShabbaDragSettings", order = 2)]
public class DragSettingsData : ScriptableObject
{
    public AnimationCurve dragEvolutionCurve;
    public float MinSpeed;
    public float MaxSpeed;
    public float MaxDrag;
    public float TimeToMaxDrag;
    public float InitialDrag;

}
