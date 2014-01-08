using UnityEngine;
using System.Collections;

public class DeskMode : MonoBehaviour {
	public string deskOwner;
	float w,h;

	// Use this for initialization
	void Start () {
		w = Screen.width;
		h = Screen.height;


		enableChildren();
	}

	void OnGUI(){



		//GUILayout.Button(
		if(GUI.Button( new LTRect(1.0f*w - 100f, 1.0f*h - 50f, 100f, 50f ).rect, "Quit Desk Mode"))
		{
			disableChildren();
			GameObject.FindGameObjectWithTag(deskOwner).GetComponent<DetectObjects>().moveCameraToPlayer();
			GameObject.FindGameObjectWithTag(deskOwner).GetComponent<DetectObjects>().enableCameraAndMotor();
			GameObject.FindGameObjectWithTag(deskOwner).GetComponent<DetectObjects>().enteredDialog = false;

			GetComponent<DeskMode>().enabled = false;

		}
	}

	void enableChildren(){
		foreach(Transform child in transform)
			child.gameObject.AddComponent<DeskObjectHandler>();
	}

	void disableChildren(){
		foreach(Transform child in transform)
			if(child.gameObject.GetComponent<DeskObjectHandler>() !=null)
				Destroy(child.gameObject.GetComponent<DeskObjectHandler>());
	}

	// Update is called once per frame
	void Update () {
		if(!transform.GetComponentInChildren<DeskObjectHandler>())
			enableChildren();
	}
}
