/** Author: Douglas Barcelos **/

using UnityEngine;
using System.Collections;

public class SwitchCameras : MonoBehaviour 
{
	public Transform myListener;
	//public GameObject headJoint;
	public Camera[] cameras;
	private int cameraIndex = 0;
	
	void Start () 
	{
		cameraIndex = 0;
		SelectCamera(cameraIndex);
	}


	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			if(cameraIndex >= cameras.Length - 1)
			{
				cameraIndex = 0;
			}

			else
			
			{
				cameraIndex++;
			}

			
			SelectCamera(cameraIndex);
		}
		
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if(cameraIndex <= 0)
			{
				cameraIndex = cameras.Length - 1;
			}

			else
			{
				cameraIndex--;
			}

			
			SelectCamera(cameraIndex);
		}
	}


	

	
	void SelectCamera(int idCamera)
	{		
		if (cameras[idCamera] != null)
		{
			foreach(Camera cam in cameras)
			{
				cam.enabled = false;

			}

			
			cameras[idCamera].camera.enabled = true;
			myListener.transform.parent = cameras[idCamera].transform;
			myListener.localPosition = Vector3.zero;
			myListener.localRotation = Quaternion.identity;


		}
	}	
}