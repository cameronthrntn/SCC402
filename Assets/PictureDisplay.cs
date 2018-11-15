using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureDisplay : MonoBehaviour {

    public GameObject hotImage;
    public Button btn; 

	// Use this for initialization
	void Start () {
        hotImage.SetActive(false);

        if (btn != null) {
            btn.onClick.AddListener(click);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void click() {
        //hotImage.GetComponent<MeshRenderer>().enabled = !hotImage.GetComponent<MeshRenderer>().enabled;
        hotImage.SetActive(!hotImage.activeSelf);
    }
}
