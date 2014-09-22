using UnityEngine;
using System.Collections;


[ExecuteInEditMode]

public class Stanographer : MonoBehaviour {


	public int h;
	public int w;


	public string[] thingstosay;

	public Stanographer st;

	void Awake(){
		st = GetComponent<Stanographer>();
	}

	// Update is called once per frame
	void Update () {
		h = Screen.height;
		w = Screen.width;
	}


	void OnGUI(){
		GUILayout.BeginArea(new Rect(0f,h*.7f,w*.4f,h*.3f), new GUIStyle("box"));
		GUILayout.BeginVertical();
		int i;
		for(i=0;i<thingstosay.Length;i++){
			GUILayout.Label(thingstosay[i]);
		}
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	[RPC]
	public void LogStatement(string st){
		ArrayList arr = new ArrayList(thingstosay);
		if(arr.Count > 6){
			arr.RemoveAt(0);
		}
		arr.Add(st);
		string[] array = new string[arr.Count];
		arr.CopyTo( array );
		thingstosay = array;
	}


	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(thingstosay);
		}
		else
		{
			thingstosay = (string[])stream.ReceiveNext();
		}
	}


}
