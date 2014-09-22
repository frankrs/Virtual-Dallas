using UnityEngine;
using System.Collections;


[ExecuteInEditMode]


public class Voice : Photon.MonoBehaviour {



	public int h;
	public int w;
	
	
	public string thingtosay;

	public GameObject sta;

	public string pwd = "a pwd";

	
	// Update is called once per frame
	void Update () {
		h = Screen.height;
		w = Screen.width;
	}
	
	
	void OnGUI(){
		GUI.SetNextControlName ("MyTextField");
		GUILayout.BeginArea(new Rect(w*.4f,h*.9f,w*.6f,h*.1f), new GUIStyle("box"));
		GUILayout.BeginVertical();
		thingtosay = GUILayout.TextField(thingtosay);
		if(GUILayout.Button("Send")){
			PostMessage();
			GUI.FocusControl("Window");
		}
		if(Input.GetKeyDown(KeyCode.KeypadEnter)){
			if(thingtosay.Length > 0){
				PostMessage();
			}
		}
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	[RPC]
	void PostMessage(){
		string st = PhotonNetwork.player.name + ":  " + thingtosay;
		Debug.Log(st);
		sta = GameObject.FindGameObjectWithTag("Stanographer");
		PhotonView pv = sta.GetComponent<PhotonView>();
		PhotonNetwork.RPC(pv,"LogStatement",PhotonTargets.MasterClient,st);
		thingtosay = "";
	}
}
