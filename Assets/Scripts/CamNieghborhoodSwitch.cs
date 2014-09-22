using UnityEngine;
using System.Collections;

public class CamNieghborhoodSwitch : MonoBehaviour {
	public NEffects[] nEffects;
	public GUIText sighn;

	void Awake(){
		sighn = GameObject.Find("niehborhoodSighn").guiText;
	}

	public void EnterNeighborhood (Neighborhood nb){
		foreach (NEffects nfx in nEffects){
			if(nb == nfx.name){
				if(nb == Neighborhood.NonDiscript){
					sighn.text = "";
				}
				else{
					sighn.text = nb.ToString();
				}
				foreach(Behaviour b in nfx.camEffectsOn){
					b.enabled = true;
				}
				foreach(Behaviour b in nfx.camEffectsOff){
					b.enabled = false;
				}
			}
		}
	}
}


[System.Serializable]
public class NEffects{
	public Neighborhood name;
	public Behaviour[] camEffectsOn;
	public Behaviour[] camEffectsOff;
}