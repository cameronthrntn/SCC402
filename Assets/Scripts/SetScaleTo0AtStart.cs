using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScaleTo0AtStart : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		gameObject.transform.localScale = new Vector3(0, 0, 0);
		gameObject.SetActive(false);
	}

}
