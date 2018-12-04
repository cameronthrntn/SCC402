using System.Collections;
using System.Threading;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Emoji = AssistantEmojis;


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
    private Renderer assistantScreenImage;



    // Use this for initialization
    void Start () {
		playButtonMat = (Material) Resources.Load("playButton", typeof(Material));
		pauseButtonMat = (Material) Resources.Load("pauseButton", typeof(Material));
		
		playBtnRenderer = transform.Find("play").GetComponent<Renderer>();

        assistantScreenImage = gameObject.GetComponentInChildren<Renderer>();
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

	private void EventAction(int action, string emoji) {

        if (emoji != "") {
            assistantScreenImage.material = getEmojiMaterial(emoji);
        }


        switch (action)
		{
			case RayCast.MEDIA_EVENT_PLAYING:
				playBtnRenderer.material = pauseButtonMat;

                //Thread childThread = new Thread(play);
                //childThread.Start(); //Runs play in separate thread

                StartCoroutine(play());

                Debug.Log("Play pressed");
				break;
			case RayCast.MEDIA_EVENT_PAUSED:
                Debug.Log("Pause pressed");
                playBtnRenderer.material = playButtonMat;
				break;
			case RayCast.MEDIA_EVENT_STOPPED:
                Debug.Log("Stopped pressed");
				playBtnRenderer.material = playButtonMat;
				break;
			case RayCast.MEDIA_EVENT_PREV:
                Debug.Log("Prev pressed");
				break;
			case RayCast.MEDIA_EVENT_NEXT:
                Debug.Log("Next pressed");
                break;
		}
	}

    IEnumerator play() {
        AudioSource audio = RayCast.assistantAudioSource;
        Debug.Log(RayCast.audioName);
        Debug.Log(audioClips[RayCast.audioName][0]);
        int numClips = audioClips[RayCast.audioName].Length;

        for (int i = 0; i < numClips; i++) {
            string toFile = "Assets/Audio/" + RayCast.audioName + "/" + RayCast.audioName + (i+1);
            audio.clip = (AudioClip)Resources.Load(toFile);
            audio.Play();
            Debug.Log("" + i + " clip");
            assistantScreenImage.material = getEmojiMaterial(audioClips[RayCast.audioName][i]);
            yield return new WaitForSeconds(audio.clip.length);
            //Thread.Sleep((int)(audio.clip.length * 1000)); //Wait for clip to end
        }

    }

    private void initAudioClips() {
        audioClips = new Dictionary<string, string[]>();
        audioClips["EB"] = new string[] { Emoji.NORMAL, Emoji.SMILE, Emoji.CRY, Emoji.SHOCKED, Emoji.CRY };
        audioClips["DR"] = new string[] { Emoji.NORMAL, Emoji.THINKING, Emoji.CRY, Emoji.SHOCKED, Emoji.THINKING };
    }

    private void Update(){

	}

    public Material getEmojiMaterial(string emoji) {
        Debug.Log(emoji);
        return (Material)Resources.Load(emoji, typeof(Material));
    }



}
