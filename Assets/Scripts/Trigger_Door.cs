using UnityEngine;
using System.Collections;

public class Trigger_Door : MonoBehaviour 
{
	
	public GameObject[] doorsTriggered;
	public GameObject doorKey;	

	public string doorURL;
	
[HideInInspector] public bool triggerActive;	
[HideInInspector] public bool doorIsLocked;
[HideInInspector] public bool playerHasKey = false;
	
	//Our text tips for using doors
	public string doorTipUse = "Press E to Use!";
	public string doorTipLocked = "Door is locked.";
	public string doorTipUnlock = "Press E to Unlock!";

	//Assign GUI skin if you want
	public GUISkin doorGUISkin;

	//Check what state the trigger is in
	private bool useState = false;
	private bool lockState = false;
	private bool unlockState = false;

[HideInInspector] public triggerState _curTrigState;
	
public enum triggerState
		
	{
		trigUse,
		trigLocked,
		trigUnlockable		
	}
		
	// Use this for initialization
	void Start () 
	{
		//triggerActive = true;
		foreach (GameObject i in doorsTriggered)
		{
			if (i == null) 
				{
					Debug.LogWarning("No doors are set for the door trigger!");
				}
		}
	}

	void OnTriggerStay(Collider other) 
	{	
		if (other.gameObject.CompareTag("Player"))
		{
			//Check to see if the player is looking and either enable or disable
			if (other.GetComponent<Player_Raycast>().lookAtUseItem)
			{
				triggerActive = true;
			}
			else
			{
				triggerActive = false;
			}
			
			//If there is a doorKey, and the doorKey is a child object of the Player, then player has key.
			if (doorKey != null && doorKey.transform.IsChildOf(other.transform))				
			{
				playerHasKey = true;
			}
			
			//If the current state is locked and the player has the key...
			if (_curTrigState == triggerState.trigLocked && playerHasKey)
				
			//Then we set the current state to unlockable
			{
				_curTrigState = triggerState.trigUnlockable;
			}
		}
		
	switch(_curTrigState)
		{	
		
		//The trigger state is Use
		case triggerState.trigUse:

			    //If trigger is active
				if (triggerActive)
				{
				
					//Set the state checks
					useState = true;
					lockState = false;
					unlockState = false;

					//If the Use key is down we call next state for each door in doorsTriggered array
					if(Input.GetButtonDown("Use"))
					{
						foreach (GameObject i in doorsTriggered)
						{
						Application.OpenURL(doorURL);
						}	
					}
				}

				else 
				{
					//State checks all off
					useState = false;
					lockState = false;
					unlockState = false;
				}

			break;
		
		//The trigger state is Locked
		case triggerState.trigLocked:

			if (triggerActive)
			{
				//State checks
				lockState = true;
				useState = false;
				unlockState = false;

				//If the trigger is active and the Use key is down we call next state for each door in doorsTriggered array
				if(triggerActive == true && Input.GetButtonDown("Use"))
				{
					foreach (GameObject i in doorsTriggered)
					{
						i.SendMessage("callNextState");
					}	
				}	
			}

			else 
			{
				//State checks all off
				useState = false;
				lockState = false;
				unlockState = false;
			}

			break;

		//The trigger state is Unlockable
		case triggerState.trigUnlockable:

			if (triggerActive)
				
			{
				//State checks
				unlockState = true;
				useState = false;
				lockState = false;

				//Unlock all the doors that use this trigger
				foreach (GameObject i in doorsTriggered)
				{
					i.SendMessage("unlockFromTrigger");
				}
				
				//If Use key is down we call next state for each door in doorsTriggered array			
				if(Input.GetButtonDown("Use"))
				{
					foreach (GameObject i in doorsTriggered)
					{
						i.SendMessage("callNextState");
					}
				}
			}

			else 
			{
				//Set the state checks
				useState = false;
				lockState = false;
				unlockState = false;
			}
			
			break;

		}
	}	

	//Player exiting trigger
	void OnTriggerExit(Collider other) 
	{
		if (other.gameObject.CompareTag("Player"))
		{
			//We reset playerHasKey to false for the next time the trigger is entered
			playerHasKey = false;

			//Deactivate trigger check
			triggerActive = false;

			//State checks all off
			useState = false;
			lockState = false;
			unlockState = false;
		}
	}	

	//This function turns the door trigger on.
	public void triggerOn ()
	{
		triggerActive = true;
	}

	//This function turns the door trigger off.
	public void triggerOff ()
	{
		triggerActive = false;
	}
	
	//This is for door text
	void OnGUI() 
	{
		//Assign the GUI skin if we have one.
		GUI.skin = doorGUISkin;

		//Only draw door text if the trigger is active
		if (triggerActive == true)
			{
				//Only draw Use text if the use state checks true.
				if (useState)
				{
				GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-25, 200, 75), doorTipUse);
				}

				//Only draw Locked text if the locked state checks true.
				if (lockState)
				{
				GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-25, 200, 75), doorTipLocked);
				}
				
				//Only draw Unlock text if the unlock state checks true.
				if (unlockState)
				{
				GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-25, 200, 75), doorTipUnlock);
				}
			}
	}
}