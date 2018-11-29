using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCrossSectionEvent : MonoBehaviour
{

	private GestureCrossSection gestureCrossSection;

	// Use this for initialization
	void Start ()
	{
		gestureCrossSection = Camera.main.GetComponent<GestureCrossSection>();
	}

	public void toggleCrossSection()
	{
		gestureCrossSection.toggle();
	}
}
