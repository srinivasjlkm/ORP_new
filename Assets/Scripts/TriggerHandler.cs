﻿//This script is attached to the object
//For player to enterTrigger and interact with the objects.


using UnityEngine;
using System.Collections;


public class TriggerHandler: MonoBehaviour {

	public string colliderTag;
	public Collider enteredObj = null;
	public bool mouseClick;
	public PhotonView photonView;
	private Vector3 camera_view_position = new Vector3(0,2.72f,2.3f);
	private Vector3 camera_view_rotation = new Vector3(0,180f,0);


	bool enter;
	bool onInteractLock;
	
	Transform dugManager;

	void Start () {
		mouseClick = false;
		enteredObj = null;
		enter = false;
		onInteractLock = false;
		photonView = PhotonView.Get (this);

	}

	// Update is called once per frame
	void Update () {
		actionOnTrigger();
	}

	void OnTriggerEnter(Collider Co){
		if(Co.tag == colliderTag && Co.GetComponent<PhotonView>().isMine )
		{
			enter = true;
			enteredObj = Co;
			dugManager = enteredObj.transform.Find("DUGManager");
		}
	}

	void OnTriggerExit(Collider Co){
		if(Co == enteredObj && Co.GetComponent<PhotonView>().isMine){
			enteredObj = null;
			mouseClick = false;
			enter = false;
			//dugManager.GetComponent<DUGView>().visible = false;

		}
	}

	public void actionOnTrigger(){
		if(enter && mouseClick){
			if(!onInteractLock){
				photonView.RPC("setInteractLock",PhotonTargets.AllBuffered, true);
				dugManager.GetComponent<DUGView>().visible = true;

				dugManager.GetComponent<DialogueController>().setActiveDialogue(this.name);
				disableCameraAndMotor();
				moveCameraToObject();
				mouseClick = !mouseClick;
			}
		}
	}

	[RPC]
	void setInteractLock(bool b){
		onInteractLock = b;
	}

	void disableCameraAndMotor(){
		if(enteredObj != null){
			enteredObj.GetComponent<MouseCamera>().enabled =false;
			enteredObj.GetComponent<ClickMove>().enabled = false;
		}
	}
	
	public void moveCameraToObject(){
		
		Camera.main.transform.parent = this.transform;
		Camera.main.transform.localPosition = camera_view_position;
		Camera.main.transform.localEulerAngles = camera_view_rotation;
		
	}
	
	[RPC]
	public void moveCameraToPlayer(){
		//	print (enteredObj);
		if(enteredObj != null){
			Camera.main.transform.parent = enteredObj.transform;
			Camera.main.transform.localPosition = enteredObj.GetComponent<ThirdPersonNetworkVik>().cameraRelativePosition;
			Camera.main.transform.localEulerAngles = new Vector3(0.6651921f, 0, 0);
		}
	}
	
	[RPC]
	public void enableCameraAndMotor(){
		if(enteredObj != null)
		{
			enteredObj.GetComponent<MouseCamera>().enabled = true;
			enteredObj.GetComponent<ClickMove>().enabled = true;
		}
	}
	
	
	[RPC]
	void destroyNPC(){
		GameObject go = GameObject.Find("npcTrigger").gameObject;
		if(go != null){
			Destroy(go);
		}
	}
}