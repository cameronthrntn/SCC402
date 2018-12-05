using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchTarget : MonoBehaviour {

	public GameObject imageTarget;
	public GameObject objectTarget;
	public GameObject castle;

	public void change () {
		bool isOnObjectTarget = castle.transform.parent == objectTarget.transform;

		Transform objectToMoveTo = isOnObjectTarget ? imageTarget.transform : objectTarget.transform;

		castle.transform.parent = objectToMoveTo;
		castle.transform.localPosition = Vector3.zero;
		// castle.transform.localRotation = new Quaternion(0, 0, 0, castle.transform.localRotation.w);
	}
}
