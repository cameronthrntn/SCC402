using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantMovement : MonoBehaviour
{

	private float assistantDistanceFromCamera = 20;


	private float smoothMoveSpeed = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);

		Debug.LogError(pos.x + "  " + transform.position.x);

		float distX = pos.x - transform.position.x;
		float distY = pos.y - transform.position.y;

		if (distX > smoothMoveSpeed)
		{
			distX = smoothMoveSpeed;
		} else if (distX < -smoothMoveSpeed)
		{
			distX = -smoothMoveSpeed;
		}

		if (distY > smoothMoveSpeed)
		{
			distY = smoothMoveSpeed;
		} else if (distY < -smoothMoveSpeed)
		{
			distY = -smoothMoveSpeed;
		}

		pos.x = Mathf.Clamp01(transform.position.x + distX);
		pos.y = Mathf.Clamp01(transform.position.y + distY);
		pos.z = assistantDistanceFromCamera;
		transform.position = Camera.main.ViewportToWorldPoint(pos);
	}
}
