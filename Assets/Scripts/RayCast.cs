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

        if (objectPerformingActionOn != null && !assistantAudioSource.isPlaying && !isPaused) {

            if (audio != null && audio.Length > currentAudioIndex + 1)
            {
                currentAudioIndex++;
                assistantAudioSource.clip = audio[currentAudioIndex];
                if (OnMediaEvent != null)
                {
                    OnMediaEvent(MEDIA_EVENT_PLAYING, emoji[currentAudioIndex]);
                }

                Debug.Log("Next audio");
                assistantAudioSource.Play();
                setFloatingTextActive(false);
            }
            else
            {
                // ((Light)assistant.GetComponent<Light>()).enabled = false;
                assistant.GetComponent<Renderer>().material = assistantMat;
                objectPerformingActionOn = null;
                //setFloatingTextActive(false);
                onStoppedAudio();
            }


        }

    }

    public void onStoppedAudio()
    {
        if (OnMediaEvent != null)
        {
            OnMediaEvent(MEDIA_EVENT_STOPPED, AssistantEmojis.mic);
        }
    }

    public bool isPlaying()
    {
        return assistantAudioSource.isPlaying;
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
        if (objectRayHit.name.Equals("AssistantContainer"))
        {
            return;
        }

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
//        gazeLeftObject(gameObject);
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
//            prevGameObjectHit = null;
        }

        timeGazing = 0;
//        if (gameObjectHit != null) {
//            prevGameObjectHit = gameObjectHit;
//        }
        gameObjectHit = null;
        radialProgressBarFill.fillAmount = 0;
    }

    private void stopAction()
    {
        if (prevGameObjectHit != null) {
            setAssistantPlaying(prevGameObjectHit, false);

            MediaDisplay mediaDisplay = prevGameObjectHit.GetComponentInChildren<MediaDisplay>();
            if (mediaDisplay != null) {
                mediaDisplay.stopAction();
            }
            prevGameObjectHit = null;
        }

        timeGazing = 0;
//        gameObjectHit = null;
        radialProgressBarFill.fillAmount = 0;
    }

    private void performAction()
    {
        stopAction();
        prevGameObjectHit = gameObjectHit;

        isPaused = false;
        hasPerformedActionOnObject = true;
        setAssistantPlaying(gameObjectHit, true);

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

    private AudioClip[] audio;
    private string[] emoji;
    private int currentAudioIndex = 0;

    private void setAssistantPlaying(GameObject gameObject, bool play)
    {
        if (play)
        {
            audio = gameObject.GetComponent<HotspotName>().audio;
            emoji = gameObject.GetComponent<HotspotName>().emoji;
            currentAudioIndex = 0;

            if (audio != null && audio.Length > 0) {
                assistantAudioSource.clip = audio[currentAudioIndex];
                if (OnMediaEvent != null)
                {
                    OnMediaEvent(MEDIA_EVENT_PLAYING, emoji[currentAudioIndex]);
                }

                Debug.Log("Playing Audio");
                assistantAudioSource.Play();
                setFloatingTextActive(false);
            }
        } else {
            onStoppedAudio();
            assistantAudioSource.Stop();
        }

        assistant.GetComponent<Renderer>().material = assistantSpeakingMat;
    }

    private bool isPaused = false;

    public void pauseAudio()
    {
        isPaused = true;
        assistantAudioSource.Pause();
    }

    public void unPauseAudio()
    {
        isPaused = false;
        assistantAudioSource.UnPause();
    }

    public void nextAudio()
    {
        if (!assistantAudioSource.isPlaying)
        {
            return;
        }

        currentAudioIndex++;
        if (audio != null && audio.Length > currentAudioIndex + 1)
        {
            assistantAudioSource.clip = audio[currentAudioIndex];
            if (OnMediaEvent != null)
            {
                OnMediaEvent(MEDIA_EVENT_PLAYING, emoji[currentAudioIndex]);
            }
            assistantAudioSource.Play();
        }
    }

    public void prevAudio()
    {
        if (!assistantAudioSource.isPlaying)
        {
            return;
        }

        currentAudioIndex--;
        if (currentAudioIndex < 0)
        {
            currentAudioIndex = 0;
        }
        if (audio != null && audio.Length > 0)
        {
            assistantAudioSource.clip = audio[currentAudioIndex];
            if (OnMediaEvent != null)
            {
                OnMediaEvent(MEDIA_EVENT_PLAYING, emoji[currentAudioIndex]);
            }
            assistantAudioSource.Play();
        }
    }

    private void findFloatingTextIn(GameObject gameObject)
    {
        //floatingText = gameObject.transform.Find("FloatingText").gameObject;
        setFloatingTextActive(true);
//        Debug.Log("true");
        floatTextBack.fillAmount = 0;
        floatingTextGrowth = 0;
        HotspotName hotspotName = gameObject.GetComponent<HotspotName>();

        if (hotSpotText != null && hotspotName != null)
        {
            hotSpotText.text = hotspotName.name;
        }
    }
}