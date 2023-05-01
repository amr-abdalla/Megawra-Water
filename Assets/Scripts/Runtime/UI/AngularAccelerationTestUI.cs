using TMPro;
using UnityEngine;

public class AngularAccelerationTestUI : MonoBehaviour
{
	public enum ControlledVariable
	{
		maxAngularVelocity,
		angularAcceleration,
		angularDesceleration
	}

	[SerializeField] TMP_InputField inputField;
	[SerializeField] AngularAccelerationData accelerationData;
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
			case ControlledVariable.angularAcceleration:
				{
					return accelerationData.angularAcceleration;
				}
			case ControlledVariable.angularDesceleration:
				{
					return accelerationData.angularDesceleration;
				}
			case ControlledVariable.maxAngularVelocity:
				{
					return accelerationData.maxAngularVelocity;
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
			case ControlledVariable.angularAcceleration:
				{
					accelerationData.angularAcceleration = float.Parse(value);
					break;
				}
			case ControlledVariable.angularDesceleration:
				{
					accelerationData.angularDesceleration = float.Parse(value);
					break;
				}
			case ControlledVariable.maxAngularVelocity:
				{
					accelerationData.maxAngularVelocity = float.Parse(value);
					break;
				}
			default:
				{
					break;
				}
		}
	}
}
