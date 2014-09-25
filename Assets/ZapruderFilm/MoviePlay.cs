using UnityEngine;
using System.Collections;

public class MoviePlay : MonoBehaviour {

	public MovieTexture mov;

	void Awake(){
		mov.Stop();
	}

	void Update(){
		if(Input.GetKeyDown("p")){
			PlayScenerio();
		}
	}
	

	void PlayScenerio() {
		mov.Stop();
		mov.Play();
	}
	

}
