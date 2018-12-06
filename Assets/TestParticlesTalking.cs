using UnityEngine;
 
 public class TestParticlesTalking : MonoBehaviour {
 
     public AudioSource audioSource;
     public float updateStep = 0.01f;
     public int sampleDataLength = 1024;
 
     private float currentUpdateTime = 0f;
 
     private float clipLoudness;
     private float[] clipSampleData;
 
	private float defaultHaloSize = 2f;

	private Light haloLight;

     // Use this for initialization
     void Start () {
     
         if (!audioSource) {
             Debug.LogError(GetType() + ".Awake: there was no audioSource set.");
         }
         clipSampleData = new float[sampleDataLength];
 
		haloLight = GetComponent<Light>();
     }
     
     // Update is called once per frame
     void Update () {
     
         currentUpdateTime += Time.deltaTime;
         if (currentUpdateTime >= updateStep) {
             currentUpdateTime = 0f;
             audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
             clipLoudness = 0f;
             foreach (var sample in clipSampleData) {
                 clipLoudness += Mathf.Abs(sample);
             }
             clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
         }

		 haloLight.range = defaultHaloSize + clipLoudness;
 
     }
 
 }