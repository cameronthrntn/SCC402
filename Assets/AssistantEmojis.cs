using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantEmojis : MonoBehaviour
{
	public const string cry = "cry";
	public const string eye = "eye";
	public const string hand = "hand";
	public const string largesmile = "largesmile";
	public const string normal = "normal";
	public const string numberone = "numberone";
	public const string shocked = "shocked";
	public const string smile = "smile";
	public const string thinkingm = "thinkingm";
	public const string twofingersm = "twofingersm";
	public const string zoom = "zoom";
	public const string mic = "mic";

	private Renderer assistantScreenImage;
	
	private void Start()
	{
		assistantScreenImage = gameObject.transform.GetChild(0).GetComponent<Renderer>();
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
		assistantScreenImage.material = getEmojiMaterial(emoji);
		
		switch (action)
		{
			case RayCast.MEDIA_EVENT_PLAYING:
				
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

	public Material getEmojiMaterial(string emoji)
	{
		Debug.Log(emoji);
		return (Material) Resources.Load(emoji, typeof(Material));
	}

	
}
