using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{

	private GameObject camera;

	void Start()
	{
		camera = GameObject.Find("ARCamera");
	}

	// Update is called once per frame
	void Update () {
		if (camera != null)
		{
			gameObject.transform.LookAt(camera.transform);
		}
	}
}
