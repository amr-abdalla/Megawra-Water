using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITest : MonoBehaviour
{
	[SerializeField] TMP_InputField inputField;
    [SerializeField] AngularAccelerationData accelerationData;

	private void Start()
	{
		inputField.text = accelerationData.maxAngularVelocity.ToString();
		inputField.onValueChanged.AddListener(ChangeMaxAngularVelocity);
	}

	private void ChangeMaxAngularVelocity(string velocity)
	{
		accelerationData.maxAngularVelocity = float.Parse(velocity);
	}
}
