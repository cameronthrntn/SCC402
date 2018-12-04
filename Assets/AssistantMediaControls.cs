using System.Collections;
using System.Threading;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Emoji = AssistantEmojis;
using System;

public class AssistantMediaControls : MonoBehaviour {

    private Material playButtonMat;
    private Material pauseButtonMat;

    private Renderer playBtnRenderer;

    public static int clipPlaying = 0;

    public const string CRY = "cry";
    public const string EYE = "eye";
    public const string HAND = "hand";
    public const string LARGE_SMILE = "largesmile";
    public const string NORMAL = "normal";
    public const string NUMBER_ONE = "numberone";
    public const string SHOCKED = "shocked";
    public const string SMILE = "smile";
    public const string THINKING = "thinkingm";
    public const string PEACE_SIGN = "twofingersm";
    public const string ZOOM = "zoom";

    public Dictionary<string, string[]> audioClips;        //Holds emoji reactions in relation to clip
    public Renderer assistantScreenImage;

    AudioClip[] EB = new AudioClip[5];
    AudioClip[] DR = new AudioClip[5];

    volatile bool finishedClip = true;


    // Use this for initialization
    void Start () {
		playButtonMat = (Material) Resources.Load("playButton", typeof(Material));
		pauseButtonMat = (Material) Resources.Load("pauseButton", typeof(Material));
		
		playBtnRenderer = transform.Find("play").GetComponent<Renderer>();

        initAudioClips();
    }
	
	private void OnEnable()
	{
		RayCast.OnMediaEvent += EventAction;
	}

	private void OnDisable()
	{
		RayCast.OnMediaEvent -= EventAction;
	}

	public void EventAction(int action, string emoji) {

        if (emoji != "") {
            assistantScreenImage.material = getEmojiMaterial(emoji);
        }

        AudioSource audio = RayCast.assistantAudioSource;

        switch (action) {
			case RayCast.MEDIA_EVENT_PLAYING:
				playBtnRenderer.material = pauseButtonMat;

                //Thread childThread = new Thread(play);
                //childThread.Start(); //Runs play in separate thread

                StopCoroutine("play");
                StartCoroutine("play");

                Debug.Log("Play pressed");
				break;
			case RayCast.MEDIA_EVENT_PAUSED:
                Debug.Log("Pause pressed");
                StopCoroutine("play");
                finishedClip = false;
                audio.Pause();
                playBtnRenderer.material = playButtonMat;
				break;
			case RayCast.MEDIA_EVENT_STOPPED:
                StopCoroutine("play");
                audio.Stop();
                clipPlaying = 0;
                Debug.Log("Stopped pressed");
                assistantScreenImage.material = getEmojiMaterial(Emoji.SMILE);
                playBtnRenderer.material = playButtonMat;
				break;
			case RayCast.MEDIA_EVENT_PREV:
                StopCoroutine("play");
                audio.Stop();
                finishedClip = true;
                clipPlaying--;
                StartCoroutine("play");
                Debug.Log("Prev pressed");
				break;
			case RayCast.MEDIA_EVENT_NEXT:
                StopCoroutine("play");
                finishedClip = true;
                audio.Stop();
                clipPlaying++;
                StartCoroutine("play");
                Debug.Log("Next pressed");
                break;
		}
	}

    IEnumerator play() {
        AudioSource audio = RayCast.assistantAudioSource;
        Debug.Log(RayCast.audioName);
        Debug.Log(audioClips[RayCast.audioName][0]);
        int numClips = audioClips[RayCast.audioName].Length;

        
            for (int i = clipPlaying; i < numClips; i++) {
            if (!finishedClip) { //Finish remaining clip
                audio.UnPause();
                while (audio.isPlaying) { yield return new WaitForSeconds(0.1f); }
                i++;
            }
            string toFile = "Audio/" + RayCast.audioName + "/" + RayCast.audioName + (i + 1);
            var clip = Resources.Load<AudioClip>(toFile);
            if(clip == null) {
                break;
            }
            audio.clip = clip;
            audio.Play();
            clipPlaying = i;
            Debug.Log("" + i + " clip");
            assistantScreenImage.material = getEmojiMaterial(audioClips[RayCast.audioName][i]);
            yield return new WaitForSeconds(audio.clip.length);
            finishedClip = true;
        }

        clipPlaying = 0;

    }

    AudioClip[] getClips(string audioName) {
        if (audioName.Equals("EB")) {
            return EB;
        } else {
            return DR;
        }
    }

    private void initAudioClips() {
        audioClips = new Dictionary<string, string[]>();
        audioClips["EB"] = new string[] { Emoji.NORMAL, Emoji.SMILE, Emoji.CRY, Emoji.SHOCKED, Emoji.CRY };
        audioClips["DR"] = new string[] { Emoji.NORMAL, Emoji.THINKING, Emoji.CRY, Emoji.SHOCKED, Emoji.THINKING };
        EB[0] = Resources.Load<AudioClip>("Audio/EB/EB1");
        EB[1] = Resources.Load<AudioClip>("Audio/EB/EB2");
        EB[2] = Resources.Load<AudioClip>("Audio/EB/EB3");
        EB[3] = Resources.Load<AudioClip>("Audio/EB/EB4");
        EB[4] = Resources.Load<AudioClip>("Audio/EB/EB5");
        DR[0] = Resources.Load<AudioClip>("Audio/DR/DR1");
        DR[1] = Resources.Load<AudioClip>("Audio/DR/DR2");
        DR[2] = Resources.Load<AudioClip>("Audio/DR/DR3");
        DR[3] = Resources.Load<AudioClip>("Audio/DR/DR4");
        DR[4] = Resources.Load<AudioClip>("Audio/DR/DR5");
    }

    private void Update(){

	}

    public Material getEmojiMaterial(string emoji) {
        Debug.Log(emoji);
        return (Material)Resources.Load(emoji, typeof(Material));
    }



}
