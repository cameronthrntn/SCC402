using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSettingsMenuT : MonoBehaviour {

	AssistantMovement assistant;

	void Start() {
		assistant = GameObject.Find("AssistantContainer").GetComponent<AssistantMovement>();
	}

	public void toggle() {
		assistant.moveSettingsIn = !assistant.moveSettingsIn;
		Vibration.Vibrate(20);
	}

}
