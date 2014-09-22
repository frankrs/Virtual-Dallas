using UnityEngine;
using System.Collections;

public class NeighborhoodAlert : MonoBehaviour {

	public Neighborhood neighborhood;

	void OnTriggerEnter (Collider col) {
		col.BroadcastMessage("EnterNeighborhood",neighborhood,SendMessageOptions.DontRequireReceiver);
	}

	void OnTriggerExit (Collider col) {
		col.BroadcastMessage("EnterNeighborhood",Neighborhood.NonDiscript,SendMessageOptions.DontRequireReceiver);
	}



}


[System.Serializable]
public enum Neighborhood{
	NonDiscript,
	DealeyPlaza
}