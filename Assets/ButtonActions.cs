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
    
    void OnMouseDown()
    {
        switch (gameObject.name)
        {
            case "play":
                if (assistantAudioSource.isPlaying)
                {
                    assistantAudioSource.Pause();
                }
                else
                {
                    assistantAudioSource.UnPause();
                }
                break;
            case "prev":
                float currenttimeofaudio = assistantAudioSource.time;

                int[] rewindarray = timingIntervalForAudio.ToArray(typeof(int)) as int[];

                Debug.LogError(rewindarray.Length);

                var indexofclosesttimeofaudioforrewind = timingIntervalForAudio.IndexOf(rewindarray, rewindarray.OrderBy(a => Math.Abs(currenttimeofaudio - a)).First());

                assistantAudioSource.Stop();

                assistantAudioSource.time = rewindarray[indexofclosesttimeofaudioforrewind - 1];

                assistantAudioSource.Play();
                break;
            case "next":
                currenttimeofaudio = assistantAudioSource.time;

                int[] array = timingIntervalForAudio.ToArray(typeof(int)) as int[];

                Debug.LogError(array.Length);


                var indexofclosesttimeofaudio = timingIntervalForAudio.IndexOf(array, array.OrderBy(a => Math.Abs(currenttimeofaudio - a)).First());

                assistantAudioSource.Stop();

                assistantAudioSource.time = array[indexofclosesttimeofaudio + 1];

                assistantAudioSource.Play();

                break;
        }
    }
}

