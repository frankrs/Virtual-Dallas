using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

public class NetDiagnos : MonoBehaviour {


	public string roomName;

	public bool online = true;

	public Transform spawnPoint;

	void Awake(){
		PhotonNetwork.offlineMode = online;
		if(PhotonNetwork.isMasterClient){
			PhotonNetwork.InstantiateSceneObject("Cube",spawnPoint.position,spawnPoint.rotation,0,null);
		}
	}

	void Update (){
		roomName = PhotonNetwork.room.name;
	}

	void OnGUI () {
		GUILayout.BeginVertical();

		GUILayout.BeginHorizontal();
		GUILayout.Box("MyName");
		GUILayout.Box (PhotonNetwork.player.name);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Box("InRoom");
		GUILayout.Box (PhotonNetwork.inRoom.ToString());
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Box("RoomName");
		GUILayout.Box (PhotonNetwork.room.name);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Box("PlayerCount");
		GUILayout.Box (PhotonNetwork.room.playerCount.ToString());
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Box("IsMaster");
		GUILayout.Box (PhotonNetwork.player.isMasterClient.ToString());
		GUILayout.EndHorizontal();

//		GUILayout.BeginHorizontal();
//		GUILayout.Box("Group");
//		GUILayout.Box (PhotonNetwork.);
//		GUILayout.EndHorizontal();
		
		GUILayout.EndVertical();
	}
}




