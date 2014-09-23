using UnityEngine;
using System.Collections;

public class PlayerNet : Photon.MonoBehaviour {

	public Animator anim;


	public Vector3 realPos;
	public Quaternion realRot;

	void Start () {
		//anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!photonView.isMine){
			transform.position = Vector3.Lerp(transform.position,realPos,.1f);
			transform.rotation = Quaternion.Lerp(transform.rotation,realRot,.1f);
		}
	}





	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);

			stream.SendNext(anim.GetFloat("Forward"));
			stream.SendNext(anim.GetFloat("Turn"));
			stream.SendNext(anim.GetFloat("Jump"));
			stream.SendNext(anim.GetFloat("JumpLeg"));

			stream.SendNext(anim.GetBool("Crouch"));
			stream.SendNext(anim.GetBool("OnGround"));

		}
		else
		{
			//transform.position = (Vector3)stream.ReceiveNext();
			//transform.rotation = (Quaternion)stream.ReceiveNext();

			realPos = (Vector3)stream.ReceiveNext();
			realRot = (Quaternion)stream.ReceiveNext();

			anim.SetFloat("Forward",(float)stream.ReceiveNext());
			anim.SetFloat("Turn",(float)stream.ReceiveNext());
			anim.SetFloat("Jump",(float)stream.ReceiveNext());
			anim.SetFloat("JumpLeg",(float)stream.ReceiveNext());

			anim.SetBool("Crouch",(bool)stream.ReceiveNext());
			anim.SetBool("OnGround",(bool)stream.ReceiveNext());
		}
	}



}
