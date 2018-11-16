using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureDisplay : MonoBehaviour {

    public GameObject hotImage;
    public GameObject ARCamera;
    public Button btn; 


	// Use this for initialization
	void Start () {
        //hotImage.SetActive(false);

        if (btn != null) {
            btn.onClick.AddListener(click);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (ARCamera != null) {
            //hotImage.transform.LookAt(ARCamera.transform);
            hotImage.transform.rotation = ARCamera.transform.rotation;
        }
	}

    private void click() {
        //hotImage.GetComponent<MeshRenderer>().enabled = !hotImage.GetComponent<MeshRenderer>().enabled;
        hotImage.SetActive(!hotImage.activeSelf);
    }
}
