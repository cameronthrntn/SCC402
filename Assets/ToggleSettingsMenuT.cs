using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSettingsMenu : MonoBehaviour {

	AssistantMovement assistant;

	void Start() {
		assistant = GameObject.Find("AssistantController").GetComponent<AssistantMovement>();
	}

	public void toggle() {
		assistant.moveSettingsIn = !assistant.moveSettingsIn;
	}

}
