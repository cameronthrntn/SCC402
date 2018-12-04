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

    private Button btn;

    private RayCast rayCastScript;

    void Start()
    {
        btn = GetComponent<Button>();
        
        assistantAudioSource = GameObject.Find("Assistant").GetComponent<AudioSource>();

        rayCastScript = Camera.main.GetComponent<RayCast>();
    }

    void Update()
    {

//        if (assistantAudioSource == null)
//        {
//            return;
//        }
//
//        AudioClip clip = assistantAudioSource.clip;
//
//        if (assistantAudioSource.isPlaying)
//        {
//            int totalSizeOfAudio = (int)Math.Round(clip.length, 0);
//
//            float timeInterval = 0;
//
//            for (int i = 0; i < totalSizeOfAudio; i++)
//            {
//                timeInterval = timeInterval + delayTimeForForewardRewind;
//                //    Debug.LogError("timeInterval    " + timeInterval);
//                timingIntervalForAudio.Add(timeInterval);
//            }
//        }
    }
    
    void OnMouseDown()
    {
        switch (gameObject.name)
        {
            case "play":
                if (assistantAudioSource.isPlaying)
                {
                    rayCastScript.pauseAudio();
//                    assistantAudioSource.Pause();
                    GetComponent<Renderer>().material = (Material) Resources.Load("playButton", typeof(Material));
                }
                else
                {
                    rayCastScript.unPauseAudio();
//                    assistantAudioSource.UnPause();
                    GetComponent<Renderer>().material = (Material) Resources.Load("pauseButton", typeof(Material));
                }
                break;
            case "prev":

                rayCastScript.prevAudio();

//                float currenttimeofaudio = assistantAudioSource.time;
//                int[] rewindarray = timingIntervalForAudio.ToArray(typeof(int)) as int[];
//                var indexofclosesttimeofaudioforrewind = timingIntervalForAudio.IndexOf(rewindarray, rewindarray.OrderBy(a => Math.Abs(currenttimeofaudio - a)).First());
//                assistantAudioSource.Stop();
//                assistantAudioSource.time = rewindarray[indexofclosesttimeofaudioforrewind - 1];
//                assistantAudioSource.Play();
                break;
            case "next":

                rayCastScript.nextAudio();

//                currenttimeofaudio = assistantAudioSource.time;
//                int[] array = timingIntervalForAudio.ToArray(typeof(int)) as int[];
//                var indexofclosesttimeofaudio = timingIntervalForAudio.IndexOf(array, array.OrderBy(a => Math.Abs(currenttimeofaudio - a)).First());
//                assistantAudioSource.Stop();
//                assistantAudioSource.time = array[indexofclosesttimeofaudio + 1];
//                assistantAudioSource.Play();
                break;
        }
    }
}

