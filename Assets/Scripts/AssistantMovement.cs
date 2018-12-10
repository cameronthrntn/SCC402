using System;
using UnityEngine;

public class AssistantMovement : MonoBehaviour
{

	private const float defaultSmoothTime = 0.3F;
	private float smoothTime = defaultSmoothTime;
	private Vector3 velocity = Vector3.zero;


	private static Vector3 assistantPosTalking = new Vector3(4, 2, 11);
	private static Vector3 assistantPosDefault = new Vector3(6, 3, 15);
	private static Vector3 assistantPosIntro = new Vector3(0, 0.5f, 10);
	private static Vector3 assistantPosSettings = new Vector3(3, -1, 11);

	

	private Vector3 assistantPos = assistantPosDefault;


	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 curPosition;
	private Vector3 prevPosition;
	
	private GameObject assistant;
	private AudioSource assistantAudioSource;
	
	private float time = 0;
	private Vector3 initialTouch;

	void OnMouseDown()
	{
		smoothTime = 0.08f;
		
		screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint(
			         new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

		time = Time.time * 1000;
		initialTouch = screenPoint;
	}

	void OnMouseDrag()
	{
		smoothTime = 0.08f;
		
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		prevPosition = curPosition;
		curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
	}

	void OnMouseUp()
	{
		Debug.Log(Math.Abs((initialTouch - curPosition).magnitude));
		if (Time.time * 1000 - time < 90 || Math.Abs((initialTouch - curPosition).magnitude) < 5)
		{
			if (assistantAudioSource.isPlaying)
			{
				GameObject.Find("MediaControls").GetComponent<AssistantMediaControls>().toggleGrowing();
			}
			else
			{
				GameObject.Find("EventSystem").GetComponent<Test>().TaskOnClick();
			}
		}
		else
		{
			moveSettingsIn = prevPosition.y > curPosition.y;

			smoothTime = defaultSmoothTime;
			curPosition = Vector3.zero;
		}
	}

	private void Start()
	{
		settingsMenu = GameObject.Find("SettingsMenu").GetComponent<RectTransform>();
		initialPos = settingsMenu.position;
		
		assistant = GameObject.Find("Assistant");
		assistantAudioSource = assistant.gameObject.GetComponent<AudioSource>();
	}

	private bool moveSettingsIn = false;
	private RectTransform settingsMenu;
	private Vector3 initialPos;
	private Vector3 buttonVelocity = Vector3.zero;
	
	private float transitionSpeed = 10f;
	private int transitionTime = 1000; //In milliseconds
	private int transitionStepTime = 100; //In milliseconds
	private float travelDistance;
	private bool settingsVisible = false;
	private System.Collections.IEnumerator settingsFunc;
	
	
	
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
		switch (action) {
			case RayCast.MEDIA_EVENT_PLAYING:
				assistantPos = assistantPosTalking;
				break;
			default:
				assistantPos = assistantPosDefault;
				break;
		}
	}

	int hasInitiallySetEmoji = -1;

	void Update()
	{
		// Debug.Log("402HCI" + Camera.main.transform.rotation);
		// Debug.Log("402HCI" + assistant.transform.rotation);

		Vector3 targetPosition = Camera.main.transform.TransformPoint(assistantPos);
		
		if (assistantAudioSource.isPlaying)
		{
			if (assistantAudioSource.clip.name.Equals("Intro") && !assistantAudioSource.clip.name.Equals("Ding"))
			{
				targetPosition = Camera.main.transform.TransformPoint(assistantPosIntro);
				transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
				transform.LookAt(Camera.main.transform);
				hasInitiallySetEmoji = 0;

				if (Camera.main.transform.rotation.x == -1)
				{
					transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 180, transform.rotation.w);
				}
				return;
			}
		}
		if (hasInitiallySetEmoji == 0 && !assistantAudioSource.isPlaying && assistantAudioSource.clip.name.Equals("Intro")) {
			hasInitiallySetEmoji = 1;
			GameObject.Find("display").GetComponent<Renderer>().material = (Material) Resources.Load(AssistantEmojis.mic, typeof(Material));
		}
		
		if (moveSettingsIn) { //Making settings visible
			targetPosition = Camera.main.transform.TransformPoint(assistantPosSettings);
			settingsMenu.position = Vector3.Lerp(settingsMenu.position, Vector3.zero, Time.deltaTime * transitionSpeed);
			settingsVisible = true;

			//settingsMenu.position = Vector3.SmoothDamp(initialPos, Vector3.zero, ref buttonVelocity, 0.02f);
			//settingsMenu.position = Vector3.Lerp(initialPos, Vector3.zero, Time.deltaTime * transitionSpeed);        
		}
		else { //Making settings invisible
			settingsMenu.position = Vector3.Lerp(settingsMenu.position, initialPos, Time.deltaTime * transitionSpeed);
			settingsVisible = false;
			//			settingsMenu.position = Vector3.SmoothDamp(Vector3.zero, initialPos, ref buttonVelocity, 0.02f);
		}
		
		if (curPosition != Vector3.zero)
		{
			targetPosition = curPosition;
		}
		
		
//		Plane plane = new Plane(Vector3.up, new Vector3(0, 2, 0));
//		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//		float distance;
//		if (plane.Raycast(ray, out distance)) {
//			targetPosition.x = ray.GetPoint(distance).x;
//			targetPosition.y = ray.GetPoint(distance).y;
//			targetPosition.z = ray.GetPoint(distance).z;
//		}
		
		// Setting the position manually like this will not work well with different phones screen sizes
		// If all devices are 16:9 then it should be ok
		// Example: Vector3(8, 3, 20) =
		//		8 game metres to right of center screen
		//		3 game metres above center of screen
		//		20 game metres in front of camera
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
		transform.LookAt(Camera.main.transform);

		if (Camera.main.transform.rotation.x == -1)
		{
			transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 180, transform.rotation.w);
		}
	}
}
