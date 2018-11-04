using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCast : MonoBehaviour {

    private Camera camera;
    private RaycastHit hit;

    private float timeGazing = 0;
    private GameObject gameObjectHit = null;
    private bool hasPerformedActionOnObject = false;

    private float timeGazingTriggerMillis = 2200;

    public Image radialProgressBar;
    public Image radialProgressBarFill;
    public GameObject assistant;
    public Material assistantMat;
    public Material assistantSpeakingMat;
    
    private AudioSource assistantAudioSource;

    // Use this for initialization
    void Start () {
        camera = GetComponent<Camera>();
        assistantAudioSource = assistant.GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            rayHit(hit);
        } else {
            gazeLeftObject();
        }

        if (!assistantAudioSource.isPlaying) {
            ((Light)assistant.GetComponent<Light>()).enabled = false;
            assistant.GetComponent<Renderer>().material = assistantMat;
        }
    }

    private void rayHit(RaycastHit hit) {
        GameObject objectRayHit = hit.transform.gameObject;

        if (gameObjectHit != objectRayHit) {
            startGazeAt(objectRayHit);
            return;
        }

        updateProgress();
    }

    private float getTimeMillis() {
        return Time.time * 1000;
    }

    private void updateProgress() {
        float progress = (getTimeMillis() - timeGazing) / timeGazingTriggerMillis;

        if (progress < 0) {
            progress = 0;
        }
        if (progress > 1) {
            progress = 1;
        }

        radialProgressBarFill.fillAmount = progress;

        if (!hasPerformedActionOnObject && getTimeMillis() - timeGazing >= timeGazingTriggerMillis) {
            performAction();
        }
    }

    private void startGazeAt(GameObject gameObject) {
        gameObjectHit = gameObject;
        timeGazing = getTimeMillis();
        hasPerformedActionOnObject = false;
    }

    private void gazeLeftObject() {
        timeGazing = 0;
        gameObjectHit = null;
        radialProgressBarFill.fillAmount = 0;
    }

    private void performAction() {
        AudioSource hotspotAudio = gameObjectHit.GetComponent<AudioSource>();

        assistantAudioSource.clip = hotspotAudio.clip;
        assistantAudioSource.Play();

        ((Light)assistant.GetComponent<Light>()).enabled = true;
        assistant.GetComponent<Renderer>().material = assistantSpeakingMat;

        hasPerformedActionOnObject = true;
    }
}
