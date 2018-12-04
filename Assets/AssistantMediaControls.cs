using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantMediaControls : MonoBehaviour {

	private Material playButtonMat;
	private Material pauseButtonMat;
	
	private Renderer playBtnRenderer;

	private RayCast rayCastScript;

	// Use this for initialization
	void Start () {
		playButtonMat = (Material) Resources.Load("playButton", typeof(Material));
		pauseButtonMat = (Material) Resources.Load("pauseButton", typeof(Material));
		
		playBtnRenderer = transform.Find("play").GetComponent<Renderer>();

		rayCastScript = Camera.main.GetComponent<RayCast>();
	}
	
	private void OnEnable()
	{
		RayCast.OnMediaEvent += EventAction;
	}

	private void OnDisable()
	{
		RayCast.OnMediaEvent -= EventAction;
	}

	private void EventAction(int action, string emoji)
	{
			
		switch (action)
		{
			case RayCast.MEDIA_EVENT_PLAYING:
				playBtnRenderer.material = pauseButtonMat;

				growing = true;
				
				break;
			case RayCast.MEDIA_EVENT_PAUSED:
				growing = false;
				playBtnRenderer.material = playButtonMat;
				break;
			case RayCast.MEDIA_EVENT_STOPPED:
				growing = false;
				playBtnRenderer.material = playButtonMat;
				break;
			case RayCast.MEDIA_EVENT_PREV:
				
				break;
			case RayCast.MEDIA_EVENT_NEXT:
				
				break;
		}
	}

	public void toggleGrowing()
	{
		if (rayCastScript.isPlaying())
		{
			growing = !growing;
		}

	}

	private bool growing = false;
	private float scale = 1f;
	private float rateOfGrowth = 0.1f;
	private float growth = 0f;

	private void Update()
	{
		lock (this) {
			if (growing) {
				if (growth >= 1) {
					growth = 1;
				} else {
					growth += rateOfGrowth;
				}
			} else {
				if (growth <= 0) {
					growth = 0;
				} else {
					growth -= rateOfGrowth;
				}
			}
		}

		Plane plane = new Plane(Camera.main.transform.forward, Camera.main.transform.position);
		float dist = plane.GetDistanceToPoint(transform.position);
		transform.localScale = new Vector3(1, 1, 1) * scale * growth;//* dist;

	}


	
}
