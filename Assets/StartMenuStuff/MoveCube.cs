using UnityEngine;
using System.Collections;

public class MoveCube : MonoBehaviour {

	public float speed = 1f;
	
	// Update is called once per frame
	void LateUpdate () {
		transform.Translate(new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical")) * Time.deltaTime);
	}
}
