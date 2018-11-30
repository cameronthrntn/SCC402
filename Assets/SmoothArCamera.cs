using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothArCamera : MonoBehaviour
{
	private Vector3 cameraPosFixed = Vector3.zero;
	private Vector3 cameraPosUpdate = Vector3.zero;
	private Vector3 cameraPosLate = Vector3.zero;

	private Quaternion cameraRotFixed;
	private Quaternion cameraRotUpdate;
	private Quaternion cameraRotLate;


	private float changeMult = 10f;
	private float maxSmoothDist = 1f;



	// Use this for initialization
	void Start ()
	{

	}

	// Called before Vuforia updates positions of stuff
	private void FixedUpdate()
	{
		cameraPosFixed = transform.position;
		cameraRotFixed = transform.rotation;
	}

	// Vuforia updates position in Update I believe
	void Update () {
		cameraPosUpdate = transform.position;
		cameraRotUpdate = transform.rotation;
	}

	// Called after all update functions (so after Vuforia has updated camera position)
	private void LateUpdate()
	{
		cameraPosLate = transform.position;
		cameraRotLate = transform.rotation;
		float dist = Vector3.Distance(cameraPosFixed, cameraPosLate);
		float rotDiff = Quaternion.Angle(cameraRotFixed, cameraRotLate);

		Debug.LogError(cameraPosFixed + "   " + cameraPosUpdate + "   " + cameraPosLate);
		Debug.LogError(cameraRotFixed + "   " + cameraRotUpdate + "   " + cameraRotLate);
		Debug.LogError(dist);
		Debug.LogError(rotDiff);

		if (dist < maxSmoothDist)
		{
			transform.position = cameraPosFixed;
			transform.position = Vector3.Lerp(transform.position, cameraPosLate, Time.deltaTime * changeMult);
		}

		if (rotDiff < maxSmoothDist)
		{
			transform.rotation = cameraRotFixed;
			transform.rotation = Quaternion.Lerp(transform.rotation, cameraRotLate, Time.deltaTime * changeMult);
		}


	}
}
