using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundCastle : MonoBehaviour {

	public float speed = 1f;
	public bool shouldMove = true;
	public bool forceChildToNotMove = false;
	private float rotationMult = 2f;

	// Update is called once per frame
	void Update () {
		if (shouldMove)
		{
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
		}

		if (forceChildToNotMove)
		{
			transform.GetChild(0).transform.position = transform.position;
		}

		transform.Rotate(Vector3.up, speed * rotationMult * Time.deltaTime);
	}
}
