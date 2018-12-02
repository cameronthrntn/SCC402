using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCast : MonoBehaviour
{

    public delegate void MediaEvent(int eventType, string emoji);
    public static event MediaEvent OnMediaEvent;

    
    
    public const int MEDIA_EVENT_PLAYING = 1;
    public const int MEDIA_EVENT_PAUSED = 2;
    public const int MEDIA_EVENT_STOPPED = 3;
    public const int MEDIA_EVENT_PREV = 4;
    public const int MEDIA_EVENT_NEXT = 5;
    
    
    
    
    
    
    private Camera camera;
    private RaycastHit hit;

    private float timeGazing = 0;
    private GameObject gameObjectHit = null;
    private GameObject prevGameObjectHit = null;
    private bool hasPerformedActionOnObject = false;
    private GameObject objectPerformingActionOn = null;

    private float timeGazingTriggerMillis = 2200;

    public Image radialProgressBar;
    public Image radialProgressBarFill;
    public GameObject assistant;
    public Material assistantMat;
    public Material assistantSpeakingMat;
    public Text hotSpotText;

    private AudioSource assistantAudioSource;

    public GameObject floatingText;
    public Image floatTextBack;

    private bool floatingTextGrowing = false;
    private float floatingTextScale = 0.1f;
    private float floatingTextRateOfGrowth = 0.1f;
    private float floatingTextGrowth = 0f;

    // Use this for initialization
    void Start()
    {
        camera = GetComponent<Camera>();
        assistantAudioSource = assistant.GetComponent<AudioSource>();
        floatingTextScale = floatingText.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            rayHit(hit);

        } else {
            gazeLeftObject(null);
        }

        if (floatingText != null) {
            //floatingText.transform.LookAt(camera.transform);
        }

        scaleFloatingtext();

        if (objectPerformingActionOn != null && !assistantAudioSource.isPlaying) {
            // ((Light)assistant.GetComponent<Light>()).enabled = false;
            assistant.GetComponent<Renderer>().material = assistantMat;
            objectPerformingActionOn = null;
            //setFloatingTextActive(false);
            //Debug.Log("False from line 66");
        }

    }

    private void scaleFloatingtext()
    {
        if (floatingText == null) {
            return;
        }

        lock (this) {
            if (floatingTextGrowing) {
                if (floatingTextGrowth >= 1) {
                    floatingTextGrowth = 1;
                } else {
                    floatingTextGrowth += floatingTextRateOfGrowth;
                }
            } else {
                if (floatingTextGrowth <= 0) {
                    floatingTextGrowth = 0;
                } else {
                    floatingTextGrowth -= floatingTextRateOfGrowth;
                }
            }
        }

        Plane plane = new Plane(Camera.main.transform.forward, Camera.main.transform.position);
        float dist = plane.GetDistanceToPoint(floatingText.transform.position);
        floatingText.transform.localScale = new Vector3(1, 1, 1) * floatingTextScale * floatingTextGrowth;//* dist;

    }

    private void rayHit(RaycastHit hit)
    {
        GameObject objectRayHit = hit.transform.gameObject;
        if (gameObjectHit != objectRayHit) {
            startGazeAt(objectRayHit);
            return;
        }

        updateProgress();
    }

    private float getTimeMillis()
    {
        return Time.time * 1000;
    }

    private void updateProgress()
    {
        float progress = (getTimeMillis() - timeGazing) / timeGazingTriggerMillis;

        if (progress < 0) {
            progress = 0;
        }
        if (progress > 1) {
            progress = 1;
        }

        radialProgressBarFill.fillAmount = progress;
        floatTextBack.fillAmount = progress;



        if (!hasPerformedActionOnObject && getTimeMillis() - timeGazing >= timeGazingTriggerMillis) {
            performAction();
        }

    }

    private void setFloatingTextActive(bool active)
    {
        if (floatingText == null) {
            return;
        }

        //        floatingText.SetActive(active);

        floatingTextGrowing = active;
    }

    private void startGazeAt(GameObject gameObject)
    {
        gazeLeftObject(gameObject);
        findFloatingTextIn(gameObject);

        if (objectPerformingActionOn == null) {
            findFloatingTextIn(gameObject);
        }

        //        prevGameObjectHit = gameObjectHit;
        gameObjectHit = gameObject;
        timeGazing = getTimeMillis();
        hasPerformedActionOnObject = false;
    }

    private void gazeLeftObject(GameObject newObject)
    {

        if (objectPerformingActionOn == null || newObject == null) {
            setFloatingTextActive(false);
            //Debug.Log("False from line 173");
        }

        if (hasPerformedActionOnObject && prevGameObjectHit != null && newObject != null) {
            setAssistantPlaying(prevGameObjectHit, false);

            MediaDisplay mediaDisplay = prevGameObjectHit.GetComponentInChildren<MediaDisplay>();
            if (mediaDisplay != null) {
                mediaDisplay.stopAction();
            }
            prevGameObjectHit = null;
        }

        timeGazing = 0;
        if (gameObjectHit != null) {
            prevGameObjectHit = gameObjectHit;
        }
        gameObjectHit = null;
        radialProgressBarFill.fillAmount = 0;
    }

    //    private void clickButton(GameObject gameObject) { //Clicks a button, if the gameObject has one.
    //        if (gameObject == null)
    //        {
    //            return;
    //        }
    //
    //        Button btn = gameObject.GetComponent<Button>();
    //        if (btn != null)
    //        {
    //            btn.onClick.Invoke();
    //        }
    //    }

    private void performAction()
    {
        hasPerformedActionOnObject = true;
        setAssistantPlaying(gameObjectHit, true);

        if (OnMediaEvent != null)
        {
            OnMediaEvent(MEDIA_EVENT_PLAYING, AssistantEmojis.smile);
        }
        
        MediaDisplay mediaDisplay = gameObjectHit.GetComponentInChildren<MediaDisplay>();
        if (mediaDisplay != null) {
            mediaDisplay.startAction();
            setFloatingTextActive(false);
        }

        if (objectPerformingActionOn != null) {
            setFloatingTextActive(false);
            Debug.Log("False from line 225");
        }
        objectPerformingActionOn = gameObjectHit;
        //findFloatingTextIn(gameObjectHit);
    }

    private void setAssistantPlaying(GameObject gameObject, bool play)
    {
        if (play) {
            AudioSource hotspotAudio = gameObject.GetComponent<AudioSource>();
            if (hotspotAudio != null) {
                assistantAudioSource.clip = hotspotAudio.clip;
                Debug.Log("Playing Audio");
                assistantAudioSource.Play();
                Debug.Log("Audio Stopped");
                setFloatingTextActive(false);
            }
        } else {
            assistantAudioSource.Stop();
        }

        assistant.GetComponent<Renderer>().material = assistantSpeakingMat;
    }

    private void findFloatingTextIn(GameObject gameObject)
    {
        //floatingText = gameObject.transform.Find("FloatingText").gameObject;
        setFloatingTextActive(true);
        Debug.Log("true");
        floatTextBack.fillAmount = 0;
        floatingTextGrowth = 0;
        HotspotName hotspotName = gameObject.GetComponent<HotspotName>();
        hotSpotText.text = hotspotName.name;
    }
}