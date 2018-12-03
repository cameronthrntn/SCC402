using UnityEngine;

public class AssistantMovement : MonoBehaviour
{

	public float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;

	private int offsetX = 0;
	private int offsetY = 0;
	private int offsetZ = 18;


    public GameObject assistant;
    private AudioSource assistantAudioSource;
    public int numberOfTimesIntroHasBeenPlayed = 0;

    public AudioClip Intro; 


    void Start()
    {     
        assistant = GameObject.Find("Assistant");
        assistantAudioSource = assistant.gameObject.GetComponent<AudioSource>();
        
    }

    
	void Update()
	{
        // Setting the position manually like this will not work well with different phones screen sizes
        // If all devices are 16:9 then it should be ok
        // Example: Vector3(8, 3, 20) =
        //		8 game metres to right of center screen
        //		3 game metres above center of screen
        //		20 game metres in front of camera

        if (assistantAudioSource.isPlaying)
        {
              if (assistantAudioSource.clip.name == "Intro")
            {
                Debug.LogError("number of times  intro play " + assistantAudioSource.clip.name);
                Vector3 targetPosition = Camera.main.transform.TransformPoint(new Vector3(offsetX, offsetY, offsetZ));
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
                numberOfTimesIntroHasBeenPlayed = numberOfTimesIntroHasBeenPlayed + 1;
            }           
        }
        else {
            Vector3 targetPosition = Camera.main.transform.TransformPoint(new Vector3(5, 3, 18));
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }

		transform.LookAt(Camera.main.transform);


	}
}
