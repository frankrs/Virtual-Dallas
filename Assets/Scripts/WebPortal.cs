using UnityEngine;
using System.Collections;

public class WebPortal : MonoBehaviour {

	public string website;


	void OnTriggerEnter (Collider col) {
		if(col.tag == "Player"){
			Application.OpenURL(website);
		}
	}

}
