using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantEmojis : MonoBehaviour
{
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

	private Renderer assistantScreenImage;
	
	private void Start()
	{
		assistantScreenImage = gameObject.GetComponentInChildren<Renderer>();
	}

	private void OnEnable()
	{
		RayCast.OnMediaEvent += EventAction;
	}

	private void OnDisable()
	{
		RayCast.OnMediaEvent -= EventAction;
	}
	
	private void EventAction(int action, string emoji)
	{
        //if (emoji != "") {
        //    assistantScreenImage.material = getEmojiMaterial(emoji);
        //}

		switch (action)
		{
			case RayCast.MEDIA_EVENT_PLAYING:
                //Debug.Log("Play pressed in emojis");
                break;
			case RayCast.MEDIA_EVENT_PAUSED:
				
				break;
			case RayCast.MEDIA_EVENT_STOPPED:
				
				break;
			case RayCast.MEDIA_EVENT_PREV:
				
				break;
			case RayCast.MEDIA_EVENT_NEXT:
				
				break;
		}
	}

	public Material getEmojiMaterial(string emoji) {
            Debug.Log(emoji);
            return (Material)Resources.Load(emoji, typeof(Material));  
	}

	
}
