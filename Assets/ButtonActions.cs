using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;

public class ButtonActions : MonoBehaviour
{
    private AudioSource assistantAudioSource;
    private ArrayList timingIntervalForAudio = new ArrayList();
    private float delayTimeForForewardRewind = 0.001f;

    public Button fastFowardbtn, pausebtn, playbtn, rewindbtn;

    public String buttonAction;

    void Start()
    {


     //   fastFowardbtn = GetComponent<Button>();
     // fastFowardbtn.gameObject.GetComponent<SpriteRenderer>().enabled = false;
     //fastFowardbtn.onClick.AddListener(
     //       delegate {
     //          Debug.LogError("fastFowardbtn");
     //         ControlMediaOutput("fastFoward");
     //      });


     //   fastFowardbtn.onClick.AddListener(TaskOnClick);


     //   pausebtn = GetComponent<Button>();
     //   pausebtn.gameObject.GetComponent<SpriteRenderer>().enabled = false;
     //   pausebtn.onClick.AddListener(delegate {ControlMediaOutput("pause"); });
        
     //   playbtn = GetComponent<Button>();
     // playbtn.gameObject.GetComponent<SpriteRenderer>().enabled = false;
     // playbtn.onClick.AddListener(delegate {ControlMediaOutput("play"); });
       

     // rewindbtn = GetComponent<Button>();
     // rewindbtn.gameObject.GetComponent<SpriteRenderer>().enabled = false;
     //rewindbtn.onClick.AddListener(delegate {ControlMediaOutput("rewind"); });
      

     assistantAudioSource = transform.parent.gameObject.GetComponent<AudioSource>();

        

    }

    void Update()
    {

     ////  Debug.LogError("Updatedddddddd!!!!!");

     //   if (assistantAudioSource == null)
     //   {
     //       return;
     //   }

     //   AudioClip clip = assistantAudioSource.clip;
     //   if (clip != null)
     //   {
     //      Debug.LogError("We are in audio clippppppppppppppppppppp");

     //       fastFowardbtn.gameObject.GetComponent<SpriteRenderer>().enabled = true;
     //       pausebtn.gameObject.GetComponent<SpriteRenderer>().enabled = true;
     // //      playbtn.gameObject.GetComponent<SpriteRenderer>().enabled = true;
     //       rewindbtn.gameObject.GetComponent<SpriteRenderer>().enabled = true;

     //       float audioSourceLength = clip.length;
     //       Debug.LogError(audioSourceLength);
     //       double totalSizeOfAudio = Math.Round(audioSourceLength, 3);
     //       Debug.LogError(totalSizeOfAudio);
     //       double numberOfIntervalSplits = Math.Round((totalSizeOfAudio / delayTimeForForewardRewind), 3);

     //       Debug.LogError(numberOfIntervalSplits);

     //       float timeInterval = 0;

     //       for (int i = 0; i < (int)numberOfIntervalSplits; i++)
     //       {
     //           Debug.LogError(i);
     //           timeInterval = timeInterval + delayTimeForForewardRewind;
     //           timingIntervalForAudio.Add(timeInterval);
     //       }

     //   }

    }


     void OnMouseDown()
    {

        switch(buttonAction)
        {
            case "play":

                if (pausebtn.gameObject.activeSelf == false)
                {
                    assistantAudioSource.UnPause();
                }
                else
                {
                    assistantAudioSource.Play();
                };

                Debug.LogError("play");
                break;
            case "pause":

                assistantAudioSource.Pause();

                break;
            case "rewind":

                float currentTimeOfAudio = assistantAudioSource.time;

                var rewindArray = timingIntervalForAudio.ToArray(typeof(int)) as int[];

                Debug.LogError(rewindArray.Length);
                Debug.LogError(timingIntervalForAudio.Count);

                var indexOfClosestTimeOfAudioForRewind = timingIntervalForAudio.IndexOf(rewindArray, rewindArray.OrderBy(a => Math.Abs(currentTimeOfAudio - a)).First());

                assistantAudioSource.Stop();

                assistantAudioSource.time = rewindArray[indexOfClosestTimeOfAudioForRewind - 1];

                assistantAudioSource.Play();

                Debug.LogError("rewind");
                break;
            case "fastForward":
                currentTimeOfAudio = assistantAudioSource.time;

                int[] array = timingIntervalForAudio.ToArray(typeof(int)) as int[];

                Debug.LogError(array.Length);
                Debug.LogError(timingIntervalForAudio.Count);

                var indexOfClosestTimeOfAudio = timingIntervalForAudio.IndexOf(array, array.OrderBy(a => Math.Abs(currentTimeOfAudio - a)).First());

                assistantAudioSource.Stop();

                assistantAudioSource.time = array[indexOfClosestTimeOfAudio + 1];

                assistantAudioSource.Play();

                break;
        }
    }


    //void ControlMediaOutput(string mediaButton)
    //{

    //    Debug.LogError("The buttons have been activated");

    //    switch (mediaButton)
    //    {
    //        case "fastFoward":

    //            float currentTimeOfAudio = assistantAudioSource.time;

    //            int[] array = timingIntervalForAudio.ToArray(typeof(int)) as int[];

    //            var indexOfClosestTimeOfAudio = timingIntervalForAudio.IndexOf(array, array.OrderBy(a => Math.Abs(currentTimeOfAudio - a)).First());

    //            assistantAudioSource.Stop();

    //            assistantAudioSource.time = array[indexOfClosestTimeOfAudio + 1];

    //            assistantAudioSource.Play();

    //            break;


    //        case "pause":
    //            assistantAudioSource.Pause();


    //            break;


    //        case "play":

    //            if (pausebtn.gameObject.activeSelf == false)
    //            {
    //                assistantAudioSource.UnPause();
    //            }
    //            else
    //            {
    //                assistantAudioSource.Play();
    //            };

    //            break;

    //        case "rewind":

    //            currentTimeOfAudio = assistantAudioSource.time;

    //            var rewindArray = timingIntervalForAudio.ToArray(typeof(int)) as int[];

    //            var indexOfClosestTimeOfAudioForRewind = timingIntervalForAudio.IndexOf(rewindArray, rewindArray.OrderBy(a => Math.Abs(currentTimeOfAudio - a)).First());

    //            assistantAudioSource.Stop();

    //            assistantAudioSource.time = rewindArray[indexOfClosestTimeOfAudioForRewind - 1];

    //            assistantAudioSource.Play();



    //            break;
    //    }
    //}
}

