using System.Threading;
using UnityEngine;

public class AssistantMovement : MonoBehaviour
{

	private const float defaultSmoothTime = 0.3F;
	private float smoothTime = defaultSmoothTime;
	private Vector3 velocity = Vector3.zero;

	private int offsetX = 5;
	private int offsetY = 3;
	private int offsetZ = 15;

	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 curPosition;
	private Vector3 prevPosition;
	
	private GameObject assistant;
	private AudioSource assistantAudioSource;
	public int numberOfTimesIntroHasBeenPlayed = 0;
	
	void OnMouseDown()
	{
		smoothTime = 0.08f;
		
		screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint(
			         new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
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
		moveSettingsIn = prevPosition.y > curPosition.y;
		
		smoothTime = defaultSmoothTime;
		curPosition = Vector3.zero;
	}

	private void Start()
	{
		settingsMenu = GameObject.Find("SettingsMenu").GetComponent<RectTransform>();
		initialPos = settingsMenu.position;
		
		assistant = GameObject.Find("Assistant");
		assistantAudioSource = assistant.gameObject.GetComponent<AudioSource>();
        travelDistance = -settingsMenu.position.x;
	}

	private bool moveSettingsIn = true;
	private RectTransform settingsMenu;
	private Vector3 initialPos;
	private Vector3 buttonVelocity = Vector3.zero;
    private float transitionSpeed = 1f;
    private int transitionTime = 1000; //In milliseconds
    private int transitionStepTime = 100; //In milliseconds
    private float travelDistance;
    private bool settingsVisible = false;
    private System.Collections.IEnumerator settingsFunc;
	
	void Update()
	{
		Vector3 targetPosition = Camera.main.transform.TransformPoint(new Vector3(offsetX, offsetY, offsetZ));
		
		
		if (assistantAudioSource.isPlaying)
		{
			if (assistantAudioSource.clip.name == "Intro")
			{
				targetPosition = Camera.main.transform.TransformPoint(new Vector3(0, 0, 10));
				transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
				transform.LookAt(Camera.main.transform);
				numberOfTimesIntroHasBeenPlayed = numberOfTimesIntroHasBeenPlayed + 1;
				return;
			}           
		}




        if (!moveSettingsIn) {
            targetPosition = Camera.main.transform.TransformPoint(new Vector3(2, -2, 10));
            if (!settingsVisible) {
                //			settingsMenu.position = Vector3.SmoothDamp(initialPos, Vector3.zero, ref buttonVelocity, 0.02f);

                settingsMenu.position = Vector3.Lerp(Vector3.zero, initialPos, Time.deltaTime * transitionSpeed);


            //StartCoroutine(settingsVisible());

            settingsVisible = true;
        }
        }
		else {
            if (settingsVisible) {
                //			settingsMenu.position = Vector3.SmoothDamp(Vector3.zero, initialPos, ref buttonVelocity, 0.02f);

                settingsMenu.position = Vector3.Lerp(initialPos, Vector3.zero, Time.deltaTime * transitionSpeed);

                settingsVisible = false;
            }
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
	}


    System.Collections.IEnumerator settingsVisibility(bool makeVisible) {
        if (makeVisible) {
            if (!settingsVisible) {
                int steps = transitionTime / transitionStepTime;
                for (int i = 0; i < steps; i++) {
                    Debug.Log("Running Foward");
                    float newX = settingsMenu.position.x + ((i + 1) / steps) * travelDistance;
                    settingsMenu.position = new Vector3(newX, settingsMenu.position.y, settingsMenu.position.z);
                    yield return new WaitForSeconds(transitionStepTime / 1000);
                }
                settingsVisible = true;
            }
        } else {
            if (settingsVisible) {
                int steps = transitionTime / transitionStepTime;
                for (int i = 0; i < steps; i++) {
                    Debug.Log("Running Back");
                    float newX = settingsMenu.position.x - ((i + 1) / steps) * travelDistance;
                    settingsMenu.position = new Vector3(newX, settingsMenu.position.y, settingsMenu.position.z);
                    yield return new WaitForSeconds(transitionStepTime / 1000);
                }
                settingsVisible = false;
            }
        }
    }

}
