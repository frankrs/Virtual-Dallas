using UnityEngine;
using System.Collections;

public class Trigger_Gunshot : MonoBehaviour {

	public AudioClip gunshot;

	public GameObject target;

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider target) {
		//audio.clip = gunshot;
		//audio.Play();

		if (!audio.isPlaying) 
		{	
			audio.Play();
			
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
