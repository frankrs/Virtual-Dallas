using UnityEngine;
using System.Collections;
//using System.Collections.Generic;


[ExecuteInEditMode]


public class NetworkStartMenu : MonoBehaviour {
	
	public int h;
	public int w;
	public GUISkin skin;
	public StartPageState startPageState;
	public string playersName;
	
	void Update () {
		h = Screen.height;
		w = Screen.width;
	}

	void OnGUI(){
		GUI.skin = skin;
		GUILayout.BeginArea(new Rect(w*.25f,h*.25f,w*.5f,h*.5f));
			GUILayout.BeginVertical();
			if(startPageState == StartPageState.frontpage){
				if(GUILayout.Button("Single Player")){
					LoadSingle();
					}
				if(GUILayout.Button("Multi Player")){
					StartLogin();
					}
				}
			if(startPageState == StartPageState.entername){
				GUILayout.Box("Your Name");
				GUILayout.BeginHorizontal();
					playersName = GUILayout.TextField(playersName);
					if(GUILayout.Button("OK")){
					if(playersName.Length > 0){
						Connect();
						}
					}
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
					if(GUILayout.Button("Jason")){
						GameInfo.avatarName = AvatarName.JasonScaledNet;
						}
					if(GUILayout.Button("Preston")){
						GameInfo.avatarName = AvatarName.PrestonScaledNet;
						}
				GUILayout.EndHorizontal();

				}
			if(startPageState == StartPageState.connecting){
				GUILayout.Box(PhotonNetwork.connectionState.ToString());
				if(PhotonNetwork.connectionState == ConnectionState.Connected){
					if(GUILayout.Button("Come On In")){
						GetInGame();
						}
					}
				}
			if(startPageState == StartPageState.entergame){
				GUILayout.Box("Stand By");
				//GUILayout.Box(PhotonNetwork.room.name);
				}
			GUILayout.EndVertical();
		GUILayout.EndArea();
	}



	void LoadSingle(){
		PhotonNetwork.offlineMode = true;
		//PhotonNetwork.CreateRoom("OnePlayer");
		PhotonNetwork.LoadLevel("Dealey_PlazaNetworked");
	}


	void StartLogin(){
		PhotonNetwork.offlineMode = false;
		startPageState = StartPageState.entername;
	}

	
	void Connect(){
		startPageState = StartPageState.connecting;
		PhotonNetwork.ConnectUsingSettings("1.0");
	}

	void GetInGame(){
		startPageState = StartPageState.entergame;
		PhotonNetwork.JoinOrCreateRoom("Dallas",null,null);
	}

	void OnJoinedRoom(){
		Debug.Log(PhotonNetwork.room.name);
		//PhotonPlayer.name = playersName;
		PhotonNetwork.player.name = playersName;
		//PhotonNetwork.LoadLevel("Net Diagnos");
		PhotonNetwork.LoadLevel("Dealey_PlazaNetworked");
	}
}


public enum StartPageState{
	frontpage,
	entername,
	connecting,
	entergame
}