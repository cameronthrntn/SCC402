using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MediaDisplay : MonoBehaviour
{
    private GameObject ARCamera;         //The AR's camera.

    private Vector3 initialScale;
    public float rateOfGrowth = 0.01f;      //Rate at which the image grows and shrinks as a percentage.
    private float growth = 0f;              //The current growth
    private bool growing = false;           //If the picture is growing or shrinking.

    // Makes the media scale smaller. Without this they're 70m+ in size
    private float reductionMult = 0.04f;

    // Use this for initialization
    void Start()
    {
        ARCamera = Camera.main.gameObject;

        // record initial scale, use this as a basis
        initialScale = transform.localScale;

//        foreach (Transform child in transform)
//        {
//            player.Stop();
//        }

        transform.localScale = initialScale * 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (ARCamera == null)
        {
            return;
        }

        transform.rotation = ARCamera.transform.rotation;

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

        Plane plane = new Plane(ARCamera.transform.forward, ARCamera.transform.position);
        float dist = plane.GetDistanceToPoint(transform.position);
        transform.localScale = initialScale * dist*reductionMult * growth;
    }

    public void startAction()
    {
        setActionEnabled(true);
    }

    public void stopAction()
    {
        setActionEnabled(false);
    }

    private void setActionEnabled(bool enabled)
    {
        lock (this) {
            growing = enabled;
            if (growing) {
                growth = rateOfGrowth;
            } else {
                growth = 1 - rateOfGrowth;
            }
        }

        foreach (Transform child in transform)
        {
            VideoPlayer player = child.GetComponent<VideoPlayer>();
            if (player != null)
            {
                if (growing) {
                    player.Play();
                } else {
                    player.Stop();
                }
            }
        }
    }
}
