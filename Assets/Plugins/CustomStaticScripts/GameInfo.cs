using UnityEngine;
using System.Collections;

public static class GameInfo  {

	public static AvatarName avatarName;
	public static GameMode gameMode;

}


[System.Serializable]
public enum AvatarName{
	JasonScaledNet,
	PrestonScaledNet,
	OVRBlank
}

[System.Serializable]
public enum GameMode{
	Normal,
	VR
}