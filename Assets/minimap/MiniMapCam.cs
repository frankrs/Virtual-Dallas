using UnityEngine;
using System.Collections;

public class MiniMapCam : MonoBehaviour {

	public Transform tran;
	public Transform pointer;

	void Start(){
		tran = GameObject.FindGameObjectWithTag("Player").transform;
	}


	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3(tran.position.x,transform.position.y,tran.position.z);
		pointer.position = new Vector3(tran.position.x,pointer.position.y,tran.position.z);
		pointer.rotation = tran.rotation;
	}
}
