using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureCrossSection : MonoBehaviour
{
	private bool enabled = false;

	private Camera camera;
	private GameObject target;

	// dist to target that the gestures central point started at
	private float beginDistPoint = 0;

	// Radius of castle + a little bit extra to avoid clipping on edges of castle
	private float targetRadius = 17;

	void Start ()
	{
		target = GameObject.Find("ObjectTarget");
		camera = transform.gameObject.GetComponent<Camera>();
	}

	void Update () {
		if (!enabled)
		{
			return;
		}

		float dist = Vector3.Distance(transform.position, target.transform.position);
//		Debug.LogError("dist:           " + dist);
//		Debug.LogError("beginDistPoint: " + beginDistPoint);

		if (beginDistPoint == 0)
		{
			beginDistPoint = dist;
		}

		float clipNear = dist + (beginDistPoint - dist)*4;

//		if (clipNear > dist + targetRadius)
//		{
//			clipNear = dist + targetRadius;
//			beginDistPoint = beginDistPoint - targetRadius;
//			Debug.LogError("MAX");
//		}
//		else if (clipNear < dist - targetRadius)
//		{
//			clipNear = dist - targetRadius;
//			beginDistPoint = beginDistPoint + targetRadius;
//			Debug.LogError("MIN");
//		}

		clipNear = clipNear < 0.01 ? 0.01f : clipNear;
//		beginDistPoint = beginDistPoint < 0 ? 0 : beginDistPoint;

		camera.nearClipPlane = clipNear;
	}

	public bool isEnabled()
	{
		return enabled;
	}

	public void toggle()
	{
		enabled = !enabled;

		if (!enabled)
		{
			reset();
		}
	}

	private void reset()
	{
		camera.nearClipPlane = 0.01f;
		beginDistPoint = 0;
	}

}
