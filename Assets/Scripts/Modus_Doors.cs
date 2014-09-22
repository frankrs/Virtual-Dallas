using UnityEngine;
using System.Collections;


public class Modus_Doors : MonoBehaviour 
{

	public bool slidingDoor = false;
		
	public DoorStartState _doorStartState;
		
	public GameObject doorTrigger;
	private Trigger_Door myTrigger;
		
	[HideInInspector] public bool doorIsActive = false;
		
	public bool autoClose;
	public float autoCloseTime = 5;
		
	private float timeToNextState;
		
	public Vector3 doorOpenAmount;
	public Vector3 doorCloseAmount;
		
	public float doorOpenTime = 1.0f;
	public float doorCloseTime = 1.0f;
		
	public bool openBounceBack;

	public Vector3 openBounceAmount;

	public float openBounceTime = 1.25f;

	private Vector3 openStartPos;
	private Vector3 openDestPos;
	private Vector3 closeStartPos;
	private Vector3 closeDestPos;
	private Vector3 bounceStartPos;
	private Vector3 bounceDestPos;
	
	private Vector3 currentMoveAmount;
		
	private float currentLerp;

	public enum DoorStartState
			
		{
			StartOpened,
			StartClosed,
			StartLocked
		}	
	
	public enum DoorState
			
		{		
			Opening,
			Opened,
			BounceIdle,
			OpenBounce,
			OpenedIdle,
			Closing,
			Closed,
			ClosedIdle,
			Locked,
			Unlocking	
		}
		
	[HideInInspector] public DoorState _currentState;
	[HideInInspector] public DoorState _nextState;		

	public GameObject[] animatedHardware;
		
	public GameObject audioSourceDoor;


	void Awake()
	{

	}

	// Use this for initialization
	void Start () {	

		//null check the trigger is set in the inspector
		if (doorTrigger == null) 
		{
			Debug.LogWarning("There is a door without a trigger!");
		}
		if (doorTrigger != null)
		{
			myTrigger = doorTrigger.GetComponent<Trigger_Door>();
		}
					
		//If Sliding Door is checked in the inspector
		if (slidingDoor)
		{

			//Door Start State set in the inspector
			switch(_doorStartState)
			{	
				//Door starts open
				case DoorStartState.StartOpened:
				_currentState = DoorState.OpenedIdle;

				currentLerp = 1;

				openStartPos = transform.localPosition;
				openDestPos = openStartPos + doorOpenAmount;

				currentMoveAmount = Vector3.Lerp(openStartPos, openDestPos, currentLerp);
				transform.localPosition = currentMoveAmount;

				break;


				//Door starts closed
				case DoorStartState.StartClosed:
				_currentState = DoorState.ClosedIdle;

				currentLerp = 0;

				openStartPos = transform.localPosition;
				openDestPos = openStartPos + doorOpenAmount;

				currentMoveAmount = Vector3.Lerp(openStartPos, openDestPos, currentLerp);
				transform.localPosition = currentMoveAmount;									
				
				break;


				//Door starts locked
				case DoorStartState.StartLocked:
				
				_currentState = DoorState.Locked;			
				_nextState = DoorState.Locked;

				currentLerp = 0;

				openStartPos = transform.localPosition;
				openDestPos = openStartPos + doorOpenAmount;
	
				currentMoveAmount = Vector3.Lerp(openStartPos, openDestPos, currentLerp);
				transform.localPosition = currentMoveAmount;

				break;
			
			}				
		}

		//if Sliding is unchecked in the inspector then this will be the rotate start state
		else
		{

			//Door Start State set in the inspector
			switch(_doorStartState)
			{	
				//Door starts open
				case DoorStartState.StartOpened:

				_currentState = DoorState.OpenedIdle;

				currentLerp = 1;
				openStartPos = transform.localEulerAngles;
				openDestPos = openStartPos + doorOpenAmount;

				currentMoveAmount = Vector3.Lerp(openStartPos, openDestPos, currentLerp);
				transform.localEulerAngles = currentMoveAmount;
				
				break;

				//Door starts closed
				case DoorStartState.StartClosed:

				_currentState = DoorState.ClosedIdle;

				currentLerp = 0;
				openStartPos = transform.localEulerAngles;
				openDestPos = openStartPos + doorOpenAmount;

				currentMoveAmount = Vector3.Lerp(openStartPos, openDestPos, currentLerp);
				transform.localEulerAngles = currentMoveAmount;
				
				break;

				//Door starts locked
				case DoorStartState.StartLocked:
				
				_currentState = DoorState.Locked;			
				_nextState = DoorState.Locked;

				currentLerp = 0;
				openStartPos = transform.localEulerAngles;
				openDestPos = openStartPos + doorOpenAmount;

				currentMoveAmount = Vector3.Lerp(openStartPos, openDestPos, currentLerp);
				transform.localEulerAngles = currentMoveAmount;
				
				break;			
			}

		}

	}
	
	// Update is called once per frame
	void Update () {
		

		

	// We're sliding not rotating	
	if (slidingDoor)
			
		{
			switch(_currentState) 
				
			{
			case DoorState.Opening:

				//We turn the trigger off while the door is opening
				myTrigger.gameObject.SendMessage("triggerOff");
				
				if (currentMoveAmount != openDestPos) 
				{
			    currentLerp = Mathf.Clamp01(currentLerp += Time.deltaTime / doorOpenTime);	
			    currentMoveAmount = Vector3.Lerp(openStartPos, openDestPos, currentLerp);
				transform.localPosition = currentMoveAmount;	
				}								
						
				
				if (currentMoveAmount == openDestPos) 
				{				
				_nextState = DoorState.Opened;	
				callNextState();				
				}	
				break;
				
			case DoorState.Opened:		

				myTrigger.gameObject.SendMessage("triggerOff");
				
				if (currentMoveAmount == openDestPos)
				{
				_nextState = DoorState.OpenedIdle;
				callNextState();
				}
				break;
				
			case DoorState.OpenedIdle:
				
				myTrigger._curTrigState = Trigger_Door.triggerState.trigUse;
				myTrigger.gameObject.SendMessage("triggerOn");

				if (openBounceBack) 
				{
					currentLerp = 0;
					bounceStartPos = transform.localPosition;
					bounceDestPos = bounceStartPos + openBounceAmount;

					_nextState = DoorState.OpenBounce;

					callNextState();
		        	break;
				}
				
				else 
				{			

					_nextState = DoorState.Closing;

					currentLerp = 0;
					
					closeStartPos = transform.localPosition;
					closeDestPos = closeStartPos + doorCloseAmount;
					
						if (autoClose && Time.time > timeToNextState) 
						{
						callNextState();
						}			
					break;				
				}		
				
			case DoorState.OpenBounce:

				myTrigger.gameObject.SendMessage("triggerOff");
	
				if (currentMoveAmount != bounceDestPos) 
				{
				    currentLerp = Mathf.Clamp01(currentLerp += Time.deltaTime / openBounceTime);	
				    currentMoveAmount = Vector3.Lerp(bounceStartPos, bounceDestPos, currentLerp);
					transform.localPosition = currentMoveAmount;									
				}			
				
				
				if (currentMoveAmount == bounceDestPos)
				{							
					_nextState = DoorState.BounceIdle;
					callNextState();
				}

	        	break;	


			case DoorState.BounceIdle:	
				myTrigger._curTrigState = Trigger_Door.triggerState.trigUse;

				myTrigger.gameObject.SendMessage("triggerOn");
				
				currentLerp = 0;

				closeStartPos = transform.localPosition;
				closeDestPos = closeStartPos + doorCloseAmount;
				_nextState = DoorState.Closing;
				
				
				if (autoClose && Time.time > timeToNextState) 
				{
					callNextState();
				}
				
				break;
			
				
			case DoorState.Closing:

				myTrigger.gameObject.SendMessage("triggerOff");
					
				if (openBounceBack && currentMoveAmount != closeDestPos)
	
				{							
				    currentLerp = Mathf.Clamp01(currentLerp += Time.deltaTime / doorCloseTime);	
				    currentMoveAmount = Vector3.Lerp(closeStartPos, closeDestPos, currentLerp);
					transform.localPosition = currentMoveAmount;					
				}
				
				else if (currentMoveAmount != closeDestPos)
				{	

				    currentLerp = Mathf.Clamp01(currentLerp += Time.deltaTime / doorCloseTime);	
				    currentMoveAmount = Vector3.Lerp(closeStartPos, closeDestPos, currentLerp);
					transform.localPosition = currentMoveAmount;					
				}			
				
				if (currentMoveAmount == closeDestPos) 
				{	
					_nextState = DoorState.Closed;	
					callNextState();				
				}			
				break;																					
	
			case DoorState.Closed:

				myTrigger.gameObject.SendMessage("triggerOff");
					
				_nextState = DoorState.ClosedIdle;
				callNextState();			
				break;
				
			case DoorState.ClosedIdle:
				
				myTrigger._curTrigState = Trigger_Door.triggerState.trigUse;
				myTrigger.gameObject.SendMessage("triggerOn");
					
				currentLerp = 0;

				_nextState = DoorState.Opening;

				break;
				
			case DoorState.Locked:

				myTrigger.doorIsLocked = true;
				myTrigger._curTrigState = Trigger_Door.triggerState.trigLocked;
				myTrigger.gameObject.SendMessage("triggerOn");
					
				currentLerp = 0;			
	
				break;
				
			case DoorState.Unlocking:

				myTrigger.doorIsLocked = false;
				myTrigger.gameObject.SendMessage("triggerOn");
					
				currentLerp = 0;
					
				_nextState = DoorState.Opening;
				myTrigger._curTrigState = Trigger_Door.triggerState.trigUse;			
				break;
				
			}				
				
		}
		
	
		
	else
			
// We rotate instead of slide		
			
		{
			switch(_currentState) 
				
			{
			case DoorState.Opening:

				myTrigger.gameObject.SendMessage("triggerOff");
					
				if (currentMoveAmount != openDestPos) 
				{	
				currentLerp = Mathf.Clamp01(currentLerp += Time.deltaTime / doorOpenTime);	
				currentMoveAmount = Vector3.Lerp(openStartPos, openDestPos, currentLerp);
				transform.localEulerAngles = currentMoveAmount;
				}								
				
				if (currentMoveAmount == openDestPos) 
				{				
				_nextState = DoorState.Opened;	
				callNextState();				
				}	
				break;
				
			case DoorState.Opened:		

				myTrigger.gameObject.SendMessage("triggerOff");
				
				if (currentMoveAmount == openDestPos)
				{
				_nextState = DoorState.OpenedIdle;
				callNextState();
				}

				break;
				
			case DoorState.OpenedIdle:
				
				myTrigger._curTrigState = Trigger_Door.triggerState.trigUse;

				myTrigger.gameObject.SendMessage("triggerOn");
				
				if (openBounceBack) 
				{
					currentLerp = 0;
					bounceStartPos = transform.localEulerAngles;
					bounceDestPos = bounceStartPos + openBounceAmount;

					_nextState = DoorState.OpenBounce;
					callNextState();
					break;
				}
				
				else 
				{		

					currentLerp = 0;
					closeStartPos = transform.localEulerAngles;
					closeDestPos = closeStartPos + doorCloseAmount;

				
					_nextState = DoorState.Closing;
					
						if (autoClose && Time.time > timeToNextState) 
						{
						callNextState();
						}			
					break;				
				}		
								
			case DoorState.OpenBounce:

				myTrigger.gameObject.SendMessage("triggerOff");
				
				if (currentMoveAmount != bounceDestPos) 
				{
					currentLerp = Mathf.Clamp01(currentLerp += Time.deltaTime / openBounceTime);	
					currentMoveAmount = Vector3.Lerp(bounceStartPos, bounceDestPos, currentLerp);
					transform.localEulerAngles = currentMoveAmount;									
				}			
				
				
				if (currentMoveAmount == bounceDestPos)
				{							
					_nextState = DoorState.BounceIdle;
					callNextState();
				}

	        	break;	

			case DoorState.BounceIdle:		

				myTrigger._curTrigState = Trigger_Door.triggerState.trigUse;
				
				myTrigger.gameObject.SendMessage("triggerOn");

				currentLerp = 0;
				closeStartPos = transform.localEulerAngles;
				closeDestPos = closeStartPos + doorCloseAmount;

				_nextState = DoorState.Closing;
				
				
				if (autoClose && Time.time > timeToNextState) 
				{
					callNextState();
				}

				break;
				
			case DoorState.Closing:

				myTrigger.gameObject.SendMessage("triggerOff");
				
				if (openBounceBack && currentMoveAmount != closeDestPos)
					
				{							
					currentLerp = Mathf.Clamp01(currentLerp + Time.deltaTime * doorCloseTime);	
					currentMoveAmount = Vector3.Lerp(closeStartPos, closeDestPos, currentLerp);
					transform.localEulerAngles = currentMoveAmount;
				}
				
				else if (currentMoveAmount != closeDestPos)
				{	
					
					currentLerp = Mathf.Clamp01(currentLerp += Time.deltaTime / doorCloseTime);	
					currentMoveAmount = Vector3.Lerp(closeStartPos, closeDestPos, currentLerp);
					transform.localEulerAngles = currentMoveAmount;
				}			
				
				if (currentMoveAmount == closeDestPos) 
				{	
					_nextState = DoorState.Closed;	
					callNextState();				
				}		

				break;																					
	
			case DoorState.Closed:

				myTrigger.gameObject.SendMessage("triggerOff");
																		
				_nextState = DoorState.ClosedIdle;
				callNextState();			
				break;
				
			case DoorState.ClosedIdle:
				
				myTrigger._curTrigState = Trigger_Door.triggerState.trigUse;

				myTrigger.gameObject.SendMessage("triggerOn");
				
				currentLerp = 0;
				openStartPos = transform.localEulerAngles;
				openDestPos = openStartPos + doorOpenAmount;

				_nextState = DoorState.Opening;
				break;
				
			case DoorState.Locked:

				myTrigger.doorIsLocked = true;
				myTrigger._curTrigState = Trigger_Door.triggerState.trigLocked;

				myTrigger.gameObject.SendMessage("triggerOn");

				currentLerp = 0;

				openStartPos = transform.localEulerAngles;
				openDestPos = openStartPos + doorOpenAmount;					
	
				break;
				
			case DoorState.Unlocking:
				myTrigger.doorIsLocked = false;
				
				currentLerp = 0;
				_nextState = DoorState.Opening;
				myTrigger._curTrigState = Trigger_Door.triggerState.trigUse;			
				break;
				
			}				
		}
		

	}
	
	public void unlockFromTrigger()
		{
			_nextState = DoorState.Unlocking;
		}
	

	public void callNextState() 
		{
			timeToNextState = Time.time + autoCloseTime;
				
			switch(_nextState) 
					
				{
				case DoorState.Opening:
				
					if (audioSourceDoor != null)
						{
							audioSourceDoor.gameObject.SendMessage("doorOpeningSFX_Play");
						}

					if (animatedHardware.Length > 0)
						{
							foreach (GameObject i in animatedHardware)
								{
									if (i.animation["Opening"])
										{
											i.animation.wrapMode = WrapMode.Once;
											i.animation.Play("Opening");
										}
								}
						}	
							
					break;
					
				case DoorState.Opened:	

					if (audioSourceDoor != null)
						{	
							audioSourceDoor.gameObject.SendMessage("doorOpenedSFX_Play");	
						}

				break;
					
				case DoorState.OpenBounce:

					if (audioSourceDoor != null)
						{	
							audioSourceDoor.gameObject.SendMessage("doorOpenBounceSFX_Play");
						}
		        
				break;
					
				case DoorState.Closing:

					if (audioSourceDoor != null)
						{		
							audioSourceDoor.gameObject.SendMessage("doorClosingSFX_Play");
						}

					if (animatedHardware.Length > 0)
						{
							
							foreach (GameObject i in animatedHardware)
								{
									if (i.animation["Closing"])
									{
										i.animation.wrapMode = WrapMode.Once;
										i.animation.Play("Closing");
									}
								}
						}		
				
				break;
					
				case DoorState.Closed:

					if (audioSourceDoor != null)
						{
							audioSourceDoor.gameObject.SendMessage("doorClosedSFX_Play");
						}

				break;
				
				case DoorState.Locked:

					if (audioSourceDoor != null)

						{
							audioSourceDoor.gameObject.SendMessage("doorLockedSFX_Play");
						}
				

					if (animatedHardware.Length > 0)
						{

							foreach (GameObject i in animatedHardware)
								{
									if (i.animation["Locked"])
										{
											i.animation.wrapMode = WrapMode.Once;
											i.animation.Play("Locked");
										}
								}
						}				

				break;
				
				case DoorState.Unlocking:

					if (audioSourceDoor != null)
						{
							audioSourceDoor.gameObject.SendMessage("doorUnlockingSFX_Play");	
						}

					if (animatedHardware.Length > 0)
						{
							foreach (GameObject i in animatedHardware)
							{
								
								if (i.animation["Unlocking"])
								{
									i.animation.wrapMode = WrapMode.Once;
									i.animation.Play("Unlocking");
								}
							}
						}

				break;			
				}
			
			_currentState = _nextState;

		}		
	
}
