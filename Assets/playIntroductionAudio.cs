using UnityEngine;


public class playIntroductionAudio : MonoBehaviour
{
	private AudioSource assistantAudioSource;
	
	void Start()
	{
		assistantAudioSource = GetComponent<AudioSource>();  
	}

	bool hasPlayed = false;
	int hasPlayedPref = 0;

	void OnBecameVisible()
	{
		if (!hasPlayed && assistantAudioSource != null)
		{
			hasPlayedPref = PlayerPrefs.GetInt("played_intro");
			
			if (hasPlayedPref == 0) {
				PlayerPrefs.SetInt("played_intro", 1);
				assistantAudioSource.Play();
			}
			
			hasPlayed = true;
		}
	}
}