using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossSectionToggle : MonoBehaviour {

	private Toggle toggle;
	public Sprite on;
	public Sprite off;

	public Toggle otherToggle;

	private ToggleModels toggleModels;

	private GestureCrossSection gestureCrossSection;

	private void Start()
	{
		toggle = transform.gameObject.GetComponent<Toggle>();

		gestureCrossSection = Camera.main.GetComponent<GestureCrossSection>();
	}

	public void ValueChanged()
	{
		if (toggle == null)
		{
			toggle = transform.gameObject.GetComponent<Toggle>();
		}

		otherToggle.isOn = !toggle.isOn;

		if (toggle.isOn)
		{
			GetComponentInChildren<Image>().sprite = on;
			if (gestureCrossSection != null)
			{
				gestureCrossSection.toggle();
			}
		}
		else
		{
			GetComponentInChildren<Image>().sprite = off;
		}
	}

}
