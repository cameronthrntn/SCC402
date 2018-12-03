using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderOpacity : MonoBehaviour {

	private Scrollbar mainSlider;

	private ToggleModels toggleModels;
	
	// Use this for initialization
	void Start ()
	{
		mainSlider = GetComponent<Scrollbar>();

		toggleModels = GameObject.Find("Timeline").GetComponent<ToggleModels>();
	}
	
	public void OnOpacitySliderChanged()
	{
		if (mainSlider == null)
		{
			return;
		}
		
		toggleModels.setModelTransparency(null, mainSlider.value);
	}
}
