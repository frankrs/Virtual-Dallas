using UnityEngine;
using System.Collections;

public class Player_Raycast : MonoBehaviour {
	
	RaycastHit hit;

	public float checkDistance = 2.0f;

	[HideInInspector]public bool lookAtUseItem = false;

	// Update is called once per frame
	void Update () 
	{
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		if (Physics.Raycast(transform.position, fwd, out hit))
		{

			if (hit.distance <= checkDistance && hit.collider.gameObject.tag == "Trigger_Usable")
			{
				lookAtUseItem = true;
			}
			
			else
			{
				lookAtUseItem = false;
			}
		}
	}
}
