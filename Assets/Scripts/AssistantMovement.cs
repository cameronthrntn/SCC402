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
	}

	private bool moveSettingsIn = false;
	private RectTransform settingsMenu;
	private Vector3 initialPos;
	private Vector3 buttonVelocity = Vector3.zero;
	
	void Update()
	{
		Vector3 targetPosition = Camera.main.transform.TransformPoint(new Vector3(offsetX, offsetY, offsetZ));
		
		
		
		if (moveSettingsIn)
		{
//			settingsMenu.position = Vector3.SmoothDamp(initialPos, Vector3.zero, ref buttonVelocity, 0.02f);
			settingsMenu.position = Vector3.Lerp(Vector3.zero, initialPos, Time.deltaTime * 0.01f);
			targetPosition = Camera.main.transform.TransformPoint(new Vector3(2, -2, 10));
		}
		else
		{
//			settingsMenu.position = Vector3.SmoothDamp(Vector3.zero, initialPos, ref buttonVelocity, 0.02f);
			settingsMenu.position = Vector3.Lerp(initialPos, Vector3.zero, Time.deltaTime * 0.01f);
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
}
