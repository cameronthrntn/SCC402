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

	private void Start()
	{
		toggle = transform.gameObject.GetComponent<Toggle>();

		toggleModels = GameObject.Find("Timeline").GetComponent<ToggleModels>();
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
			if (toggleModels != null)
			{
				toggleModels.toggleWireframe();
			}
		}
		else
		{
			GetComponentInChildren<Image>().sprite = off;
		}
	}

}
