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


    public Button fastFowardbtn, pausebtn, playbtn, rewindbtn;

    public Boolean currentlyPaused = false;
    public Boolean inTheProcessOfPlaying = false;



    void Start()
    {

        pausebtn = GetComponent<Button>();
        pausebtn.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        playbtn = GetComponent<Button>();
        playbtn.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        fastFowardbtn = GetComponent<Button>();
        fastFowardbtn.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        rewindbtn = GetComponent<Button>();
        rewindbtn.gameObject.GetComponent<SpriteRenderer>().enabled = false;


        //  pausebtn.gameObject.transform.localPosition = new Vector3(0.016f, -1.347f, 0);

        //   rewindbtn = GetComponent<Button>();
        //rewindbtn.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        //playbtn.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //Vector3 playbtnpos = playbtn.gameObject.transform.position;
        //playbtnpos.y = -1.347F;
        //playbtn.gameObject.transform.position = playbtnpos;

        //pausebtn.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        // Vector3 pausebtnpos = pausebtn.gameObject.transform.localPosition;
        // Debug.LogError(pausebtnpos);

        //Vector3 playbtnpos = playbtn.gameObject.transform.localPosition;
        //Debug.LogError(playbtnpos);

        //Vector3 rewindbtnpos = rewindbtn.gameObject.transform.localPosition;
        //Debug.LogError(rewindbtnpos);

        //Vector3 forwardbtnpos = fastFowardbtn.gameObject.transform.localPosition;
        //Debug.LogError(forwardbtnpos);


        //pausebtnpos.y = -1.347F;



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
            Debug.LogError("1st one");
            inTheProcessOfPlaying = true;

            fastFowardbtn.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            pausebtn.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            rewindbtn.gameObject.GetComponent<SpriteRenderer>().enabled = true;

            int totalSizeOfAudio = (int)Math.Round(clip.length, 0);

            float timeInterval = 0;

            for (int i = 0; i < totalSizeOfAudio; i++)
            {
                timeInterval = timeInterval + delayTimeForForewardRewind;
                //    Debug.LogError("timeInterval    " + timeInterval);
                timingIntervalForAudio.Add(timeInterval);
            }
        }

        if (assistantAudioSource.isPlaying && currentlyPaused == true)
        { 
            Debug.LogError("2nd one");
            pausebtn = GetComponent<Button>();
            pausebtn.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            fastFowardbtn.gameObject.GetComponent<SpriteRenderer>().enabled = true ;         
            rewindbtn.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        
        if (!assistantAudioSource.isPlaying)
        {
            Debug.LogError("3rd one");
            fastFowardbtn.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            pausebtn.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            rewindbtn.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            inTheProcessOfPlaying = false;

        }
    }
    


     void OnMouseDown()
    {

        switch (gameObject.name)
        {
            case "play":


                currentlyPaused = false;
                assistantAudioSource.UnPause();
           //     pausebtn.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            //    playbtn.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                
             
                //     playbtn.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                //Vector3 playbtnpos = playbtn.gameObject.transform.position;
                //playbtnpos.y = 1.347F;
                //playbtn.gameObject.transform.position = playbtnpos;

                //     pausebtn.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                //Vector3 pausebtnpos = pausebtn.gameObject.transform.position;
                //pausebtnpos.y = -1.347F;
                //pausebtn.gameObject.transform.position = pausebtnpos; ;



                break;

            case "pause":

                assistantAudioSource.Pause();
                currentlyPaused = true;


              //  playbtn.gameObject.GetComponent<SpriteRenderer>().enabled = true;



                //Debug.LogError("In error report and currently paused is      " + currentlyPaused);
                

               // pausebtn.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                //pausebtnpos = pausebtn.gameObject.transform.position;
                //pausebtnpos.y = 1.347F;
                //pausebtn.gameObject.transform.position = pausebtnpos; ;

               // playbtn.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                //playbtnpos = playbtn.gameObject.transform.position;
                //playbtnpos.y = -1.347F;
                //playbtn.gameObject.transform.position = playbtnpos; ;


                break;
            case "rewind":

                Debug.LogError("rewinddddddddddddddddddddd");

                float currenttimeofaudio = assistantAudioSource.time;

                int[] rewindarray = timingIntervalForAudio.ToArray(typeof(int)) as int[];

                Debug.LogError(rewindarray.Length);


                var indexofclosesttimeofaudioforrewind = timingIntervalForAudio.IndexOf(rewindarray, rewindarray.OrderBy(a => Math.Abs(currenttimeofaudio - a)).First());

                assistantAudioSource.Stop();

                assistantAudioSource.time = rewindarray[indexofclosesttimeofaudioforrewind - 1];

                assistantAudioSource.Play();


                break;

            case "fastforward":

                Debug.LogError("fastforwardddddddddddddddddddddddddddd");

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

