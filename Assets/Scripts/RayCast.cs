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
    private GameObject objectPerformingActionOn = null;

    private float timeGazingTriggerMillis = 2200;

    public Image radialProgressBar;
    public Image radialProgressBarFill;
    public GameObject assistant;
    public Material assistantMat;
    public Material assistantSpeakingMat;
    
    private AudioSource assistantAudioSource;

    private GameObject floatingText;

    private bool floatingTextGrowing = false;
    private float floatingTextScale = 0.02f;
    private float floatingTextRateOfGrowth = 0.1f;
    private float floatingTextGrowth = 0f;

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

        if (floatingText != null)
        {
            floatingText.transform.LookAt(camera.transform);
        }

        scaleFloatingtext();

        if (objectPerformingActionOn != null && !assistantAudioSource.isPlaying) {
            // ((Light)assistant.GetComponent<Light>()).enabled = false;
            assistant.GetComponent<Renderer>().material = assistantMat;
            objectPerformingActionOn = null;
            setFloatingTextActive(false);
        }

    }

    private void scaleFloatingtext()
    {
        if (floatingText == null)
        {
            return;
        }

        lock (this)
        {
            if (floatingTextGrowing)
            {
                if (floatingTextGrowth >= 1)
                {
                    floatingTextGrowth = 1;
                }
                else
                {
                    floatingTextGrowth += floatingTextRateOfGrowth;
                }
            }
            else
            {
                if (floatingTextGrowth <= 0)
                {
                    floatingTextGrowth = 0;
                }
                else
                {
                    floatingTextGrowth -= floatingTextRateOfGrowth;
                }
            }
        }

        Plane plane = new Plane(Camera.main.transform.forward, Camera.main.transform.position);
        float dist = plane.GetDistanceToPoint(floatingText.transform.position);
        floatingText.transform.localScale = new Vector3(1, 1, 1) * dist * floatingTextScale * floatingTextGrowth;

        Debug.LogError("402:   " + floatingText.transform.localScale + "  -  " + floatingTextGrowth);


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

    private void setFloatingTextActive(bool active)
    {
        if (floatingText == null)
        {
            return;
        }

//        floatingText.SetActive(active);

        floatingTextGrowing = active;
    }

    private void startGazeAt(GameObject gameObject) {
        gazeLeftObject();

        if (objectPerformingActionOn == null)
        {
            findFloatingTextIn(gameObject);
        }

        gameObjectHit = gameObject;
        timeGazing = getTimeMillis();
        hasPerformedActionOnObject = false;
    }

    private void gazeLeftObject()
    {
        if (objectPerformingActionOn == null) {
            setFloatingTextActive(false);
        }

        if (hasPerformedActionOnObject) {
            clickButton(); //Click button before exit to undo any state changes.
        }

        timeGazing = 0;
        gameObjectHit = null;
        radialProgressBarFill.fillAmount = 0;
    }

    private void clickButton() { //Clicks a button, if the gameObject has one.
        if (gameObjectHit != null) {
            Button btn = gameObjectHit.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.Invoke();
            }
        }
    }

    private void performAction() {
        AudioSource hotspotAudio = gameObjectHit.GetComponent<AudioSource>();

        if (hotspotAudio != null) {
            assistantAudioSource.clip = hotspotAudio.clip;
            assistantAudioSource.Play();
        }
        else {
            clickButton();
        }

            // ((Light)assistant.GetComponent<Light>()).enabled = true;
            assistant.GetComponent<Renderer>().material = assistantSpeakingMat;

            hasPerformedActionOnObject = true;

            if (objectPerformingActionOn != null)
            {
                setFloatingTextActive(false);
            }
            objectPerformingActionOn = gameObjectHit;
            findFloatingTextIn(gameObjectHit);
    }

    private void findFloatingTextIn(GameObject gameObject)
    {
        floatingText = gameObject.transform.Find("FloatingText").gameObject;
        setFloatingTextActive(true);
        floatingTextGrowth = 0;
    }
}
