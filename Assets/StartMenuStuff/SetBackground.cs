using UnityEngine;
using System.Collections;


[ExecuteInEditMode]

public class SetBackground : MonoBehaviour {

	public int h;
	public int w;
	public GUITexture guiTexture;


	// Update is called once per frame
	void Update () {
		h = Screen.height;
		w = Screen.width;
		guiTexture.pixelInset = new Rect(0f,0f,w,h);
	}
}
