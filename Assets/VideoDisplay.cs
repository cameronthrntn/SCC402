using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoDisplay : MonoBehaviour
{

    public GameObject hotVideo;         //The image GameObject to manipulate.
    public GameObject ARCamera;         //The AR's camera.
    public Button btn;                  //The button that triggers actions on the camera. (It's probably on a hot spot.)
    public VideoPlayer player;


    public float objectScale = 1.0f;
    private Vector3 initialScale;
    public float rateOfGrowth = 0.1f;      //Rate at which the image grows and shrinks as a percentage.
    private float growth = 0f;              //The current growth
    private bool growing = false;           //If the picture is growing or shrinking.


    // Use this for initialization
    void Start()
    {
        //hotImage.SetActive(false);
        if (btn != null) {
            btn.onClick.AddListener(click);
        }

        // record initial scale, use this as a basis
        initialScale = transform.localScale;
        hotVideo.transform.localScale = initialScale * 0f;
        //Ensure video is not playing
        player.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (ARCamera != null) {
            hotVideo.transform.rotation = ARCamera.transform.rotation;
        }

        lock (this) {
            //Growth of picture
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

        Plane plane = new Plane(ARCamera.transform.forward, ARCamera.transform.position);
        float dist = plane.GetDistanceToPoint(hotVideo.transform.position);
        hotVideo.transform.localScale = initialScale * dist * objectScale * growth;

        GameObject.Find("Text").GetComponent<Text>().text = growth.ToString();
    }

    private void click()
    {
        //hotImage.SetActive(!hotImage.activeSelf);
        lock (this) {
            growing = !growing;
            if (growing) { //Kick start growth process.
                growth = rateOfGrowth;
            } else {
                growth = 1 - rateOfGrowth;
            }
        }
        if (growing) {
            player.Play();
        } else {
            player.Stop();
        }
    }
}
