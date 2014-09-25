using UnityEngine;
using System.Collections;

public class Turbo : MonoBehaviour {

	private Animator anim;

	void Start(){
		anim = GetComponent<Animator>();
	}

	void FixedUpdate(){
		anim.SetBool("Turbo",Input.GetButton("Turbo"));
	}

}
