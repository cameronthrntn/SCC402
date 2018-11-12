using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobUpAndDown : MonoBehaviour {

	float originalY;
	public float floatStrength = 1;

	void Start()
	{
		originalY = transform.position.y;
	}

	void Update()
	{
//		transform.position = new Vector3(transform.position.x,
//			originalY + (float)Math.Sin(Time.time) * floatStrength,
//			transform.position.z);
	}
}
