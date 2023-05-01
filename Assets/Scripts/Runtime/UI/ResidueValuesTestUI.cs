using TMPro;
using UnityEngine;

public class ResidueValuesTestUI : MonoBehaviour
{
	public enum ControlledVariable
	{
		pushValueMultiplier,
		pushSpeedMultiplier,
		randomPushRotationRange,
		adjacentPushValueMultiplier,
		adjacentPushSpeedMultiplier
	}

	[SerializeField] TMP_InputField inputField;
	[SerializeField] ResidueChunkPhysicsConfig residueChunkData;
	[SerializeField] ControlledVariable controlledVariable;

	private void Start()
	{
		inputField.text = GetControlledVariableData().ToString();
		inputField.onValueChanged.AddListener(SetControlledVariable);
	}

	private float GetControlledVariableData()
	{
		switch (controlledVariable)
		{
			case ControlledVariable.pushValueMultiplier:
				{
					return residueChunkData.pushValueMultiplier;
				}
			case ControlledVariable.pushSpeedMultiplier:
				{
					return residueChunkData.pushSpeedMultiplier;
				}
			case ControlledVariable.randomPushRotationRange:
				{
					return residueChunkData.randomPushRotationRange;
				}
			case ControlledVariable.adjacentPushValueMultiplier:
				{
					return residueChunkData.adjacentPushValueMultiplier;
				}
			case ControlledVariable.adjacentPushSpeedMultiplier:
				{
					return residueChunkData.adjacentPushSpeedMultiplier;
				}
			default:
				{
					return -1;
				}
		}
	}

	private void SetControlledVariable(string value)
	{
		switch (controlledVariable)
		{
			case ControlledVariable.pushValueMultiplier:
				{
					residueChunkData.pushValueMultiplier = float.Parse(value);
					break;
				}
			case ControlledVariable.pushSpeedMultiplier:
				{
					residueChunkData.pushSpeedMultiplier = float.Parse(value);
					break;
				}
			case ControlledVariable.randomPushRotationRange:
				{
					residueChunkData.randomPushRotationRange = float.Parse(value);
					break;
				}
			case ControlledVariable.adjacentPushValueMultiplier:
				{
					residueChunkData.adjacentPushValueMultiplier = float.Parse(value);
					break;
				}
			case ControlledVariable.adjacentPushSpeedMultiplier:
				{
					residueChunkData.adjacentPushSpeedMultiplier = float.Parse(value);
					break;
				}
			default:
				{
					break;
				}
		}
	}

}
