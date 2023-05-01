using TMPro;
using UnityEngine;

public class DragSettingsTestUI : MonoBehaviour
{
	public enum ControlledVariable
	{
        MinSpeed,
        MaxSpeed,
        MaxDrag,
        TimeToMaxDrag,
        InitialDrag
        
	}

	[SerializeField] TMP_InputField inputField;
	[SerializeField] DragSettingsData dragData;
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
			case ControlledVariable.MinSpeed:
                {
                    return dragData.MinSpeed;
                }
            case ControlledVariable.MaxSpeed:
                {
                    return dragData.MaxSpeed;
                }
            case ControlledVariable.MaxDrag:
                {
                    return dragData.MaxDrag;
                }
            case ControlledVariable.TimeToMaxDrag:
                {
                    return dragData.TimeToMaxDrag;
                }
            case ControlledVariable.InitialDrag:
                {
                    return dragData.InitialDrag;
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
			case ControlledVariable.MinSpeed: {
                    dragData.MinSpeed = float.Parse(value);
                    break;
                }
            case ControlledVariable.MaxSpeed:
                {
                    dragData.MaxSpeed = float.Parse(value);
                    break;
                }
            case ControlledVariable.MaxDrag:
                {
                    dragData.MaxDrag = float.Parse(value);
                    break;
                }
            case ControlledVariable.TimeToMaxDrag:
                {
                    dragData.TimeToMaxDrag = float.Parse(value);
                    break;
                }
            case ControlledVariable.InitialDrag:
                {
                    dragData.InitialDrag = float.Parse(value);
                    break;
                }
			default:
				{
					break;
				}
		}
	}


}
