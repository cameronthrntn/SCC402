using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;

public class changeAssistantsEmotions : MonoBehaviour
{
    public GameObject emotionImages;

    private AudioSource assistantAudioSource;

    void Start()
    {
        emotionImages = GameObject.Find("emotionImages");
        
        assistantAudioSource = transform.parent.gameObject.GetComponent<AudioSource>();
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

//            if (assistantAudioSource.time > 10) {
//                emotionImages.GetComponent<MeshRenderer>().materials.ElementAt(0) = 
//            }
//            else if(assistantAudioSource.time > )
//            {

//            }




        }
    }
}
