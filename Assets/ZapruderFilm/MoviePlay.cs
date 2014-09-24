using UnityEngine;
using System.Collections;

public class MoviePlay : MonoBehaviour {

	public MovieTexture mov;

	void Update(){
		if(Input.GetKeyDown("space")){
			PlayScenerio();
		}
	}

	void OnTriggerEnter(Collider col){

	}


	void PlayScenerio() {
		mov.Play();
	}
	

}
