using UnityEngine;
using System.Collections;

public class MoviePlay : MonoBehaviour {

	public MovieTexture mov;
	public KeyCode key;

	void Awake(){
		mov.Stop();
	}

	void Update(){
		if(Input.GetKeyDown(key)){
			PlayScenerio();
		}
	}
	

	void PlayScenerio() {
		mov.Stop();
		mov.Play();
	}
	

}
