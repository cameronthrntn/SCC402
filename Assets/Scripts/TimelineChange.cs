using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineChange : MonoBehaviour {


    public Slider mainSlider;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Invoked when the value of the slider changes.
    public void modelChange() {
        float val = mainSlider.value;

        if(val < 0.25) {

        }

        Debug.Log(mainSlider.value);
    }

}
