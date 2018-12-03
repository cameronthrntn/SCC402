using UnityEngine;


public class playIntroductionAudio : MonoBehaviour
{
	public GameObject assistant;
    private AudioSource assistantAudioSource;
    private int numberOfTimesVisible = 0;
	
void Start()
    {
        assistant = GameObject.Find("Assistant");
        assistantAudioSource = assistant.gameObject.GetComponent<AudioSource>();  
    }


void OnBecameVisible()
{
  
     if (numberOfTimesVisible < 1)
     {
        assistantAudioSource.Play();
     }

    numberOfTimesVisible = numberOfTimesVisible + 1;
    enabled = true;
}
}
