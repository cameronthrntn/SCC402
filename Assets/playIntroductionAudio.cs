using UnityEngine;


public class playIntroductionAudio : MonoBehaviour
{
	private AudioSource assistantAudioSource;
	private int numberOfTimesVisible = 0;
	
	void Start()
	{
		assistantAudioSource = GetComponent<AudioSource>();  
	}

	void OnBecameVisible()
	{
  
		if (numberOfTimesVisible < 1 && assistantAudioSource != null)
		{
			assistantAudioSource.Play();
		}

		numberOfTimesVisible = numberOfTimesVisible + 1;
		enabled = true;
	}
}