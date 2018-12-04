using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;

public class ButtonActions : MonoBehaviour
{
    private AudioSource assistantAudioSource;
    private ArrayList timingIntervalForAudio = new ArrayList();
    private int delayTimeForForewardRewind = 1;

    public delegate void MediaEvent(int eventType, string emoji);
    public static event MediaEvent OnMediaEvent;


    private Button btn;

    void Start()
    {
        btn = GetComponent<Button>();
        
        assistantAudioSource = GameObject.Find("Assistant").GetComponent<AudioSource>();
    }

    void Update()
    {

        if (assistantAudioSource == null)
        {
            return;
        }

        AudioClip clip = assistantAudioSource.clip;

        if (assistantAudioSource.isPlaying)
        {
            int totalSizeOfAudio = (int)Math.Round(clip.length, 0);

            float timeInterval = 0;

            for (int i = 0; i < totalSizeOfAudio; i++)
            {
                timeInterval = timeInterval + delayTimeForForewardRewind;
                //    Debug.LogError("timeInterval    " + timeInterval);
                timingIntervalForAudio.Add(timeInterval);
            }
        }
    }
    
    void OnMouseDown() {
        switch (gameObject.name){
            case "play":
                if (assistantAudioSource.isPlaying) {
                    OnMediaEvent(RayCast.MEDIA_EVENT_PAUSED, "");
                } else {
                    OnMediaEvent(RayCast.MEDIA_EVENT_PLAYING, "");
                }
                break;
            case "prev":
                OnMediaEvent(RayCast.MEDIA_EVENT_PREV, "");
                break;
            case "next":
                OnMediaEvent(RayCast.MEDIA_EVENT_NEXT, "");
                break;
        }
    }
}

