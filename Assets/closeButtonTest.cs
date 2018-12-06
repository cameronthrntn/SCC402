using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeButtonTest : MonoBehaviour {

	void OnMouseDown() {
		Camera.main.GetComponent<RayCast>().stopAction();
	}
}
