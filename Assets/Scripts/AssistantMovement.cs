using UnityEngine;

public class AssistantMovement : MonoBehaviour
{

	public float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;

	private int offsetX = 5;
	private int offsetY = 3;
	private int offsetZ = 15;

	void Update()
	{
		
		Vector3 targetPosition = Camera.main.transform.TransformPoint(new Vector3(offsetX, offsetY, offsetZ));
		
		
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
