using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeButtonTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		Debug.LogError("clicked close");
		Camera.main.GetComponent<RayCast>().stopAction();
	}
}
