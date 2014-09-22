using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour {
	

	public Transform spawnPoint;
	public MiniMapCam map;
	public GameObject camPrefab;


	private GameObject st;
	
	// Use this for initialization
	void Awake () {

		//PhotonNetwork.offlineMode = true;

		if(PhotonNetwork.offlineMode){
			PhotonNetwork.CreateRoom("OnePlayer");
			}

		spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
		map = GameObject.Find("MiniMapCam").GetComponent<MiniMapCam>();

		GameObject player = PhotonNetwork.Instantiate("PrestonScaledNet",spawnPoint.position,Quaternion.identity,0);

		map.tran = player.transform;
		map.enabled = true;

		player.BroadcastMessage("TurnOn");

		//GameObject cam = Instantiate("Free Look Camera Rig",spawnPoint.position,Quaternion.identity);

		GameObject cam = Instantiate(camPrefab,spawnPoint.position,Quaternion.identity) as GameObject;

		cam.BroadcastMessage("TurnOn");

		if(PhotonNetwork.offlineMode == false){
			if(PhotonNetwork.isMasterClient){
				st = PhotonNetwork.InstantiateSceneObject("Stanographer",new Vector3(0f,0f,0f),Quaternion.identity,0,null);
			}
			if(PhotonNetwork.isMasterClient == false){
				st = GameObject.FindGameObjectWithTag("Stanographer");
			}
			GameObject voice =  PhotonNetwork.Instantiate("Voice",new Vector3(0f,0f,0f),Quaternion.identity,0);
			voice.GetComponent<Voice>().sta = st;
		}

	}
	


	void Update(){
		if(Input.GetKeyDown("escape")){
			PhotonNetwork.LeaveRoom();
			PhotonNetwork.LoadLevel(0);
		}




	}

}
