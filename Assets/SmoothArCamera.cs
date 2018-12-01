using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothArCamera : MonoBehaviour
{
	private Vector3 cameraPosFixed = Vector3.zero;
	private Vector3 cameraPosLate = Vector3.zero;

	private Quaternion cameraRotFixed;
	private Quaternion cameraRotLate;

	
	
	private Vector3 targetPosFixed = Vector3.zero;
	private Vector3 targetPosLate = Vector3.zero;

	private Quaternion targetRotFixed;
	private Quaternion targetRotLate;
	
	

	private float changeMult = 10f;

	private float changeMultTarget = 0.2f;
	
	private GameObject target;

	// Use this for initialization
	void Start ()
	{
		target = GameObject.Find("ObjectTarget");
	}

	// Called before Vuforia updates positions of stuff
	private void FixedUpdate()
	{
		cameraPosFixed = transform.position;
		cameraRotFixed = transform.rotation;
		
		targetPosFixed = target.transform.position;
		targetRotFixed = target.transform.rotation;
	}

	
	
	// Called after all update functions (so after Vuforia has updated camera position)
	private void LateUpdate()
	{
		cameraPosLate = transform.position;
		cameraRotLate = transform.rotation;
		
		targetPosLate = target.transform.position;
		targetRotLate = target.transform.rotation;
		
		float dist = Vector3.Distance(cameraPosFixed, cameraPosLate);
		float rotDiff = Quaternion.Angle(cameraRotFixed, cameraRotLate);
		
		float distTarget = Vector3.Distance(targetPosFixed, targetPosLate);
		float rotDiffTarget = Quaternion.Angle(targetRotFixed, targetRotLate);

//		Debug.LogError(cameraPosFixed + "   " + cameraPosUpdate + "   " + cameraPosLate);
//		Debug.LogError(cameraRotFixed + "   " + cameraRotUpdate + "   " + cameraRotLate);
//		Debug.LogError(dist);
//		Debug.LogError(rotDiff);

		
		
		
		// 20f for all works fine but moving phone is smooth

		float testMult = (dist / 1f) * 20f;
		float testMult2 = (rotDiff / 1f) * 20f;
		
//		if (dist < 1f)
//		{
			transform.position = cameraPosFixed;
			transform.position = Vector3.Lerp(transform.position, cameraPosLate, Time.deltaTime * testMult);
//		}
//		if (rotDiff < 1f)
//		{
			transform.rotation = cameraRotFixed;
			transform.rotation = Quaternion.Lerp(transform.rotation, cameraRotLate, Time.deltaTime * testMult2);
//		}

		if (distTarget < 20f)
		{
			target.transform.position = targetPosFixed;
			target.transform.position = Vector3.Lerp(target.transform.position, targetPosLate, Time.deltaTime * changeMultTarget);
		}
		if (rotDiffTarget < 20f)
		{
			target.transform.rotation = targetRotFixed;
			target.transform.rotation = Quaternion.Lerp(target.transform.rotation, targetRotLate, Time.deltaTime * changeMultTarget);
		}

		
		
	}
}
