using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSmoothCamera : MonoBehaviour {

	SmoothArCamera smoothArCameraScript = null;

	// Use this for initialization
	void Start () {
		smoothArCameraScript = Camera.main.GetComponent<SmoothArCamera>();
	}
	
	public void ToggleSmoothCameraScript() {
		smoothArCameraScript.smoothEnabled = !smoothArCameraScript.smoothEnabled;
		Vibration.Vibrate(20);
	}

}
