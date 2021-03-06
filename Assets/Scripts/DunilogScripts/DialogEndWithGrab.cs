﻿using UnityEngine;
using System.Collections;

public class DialogEndWithGrab: MonoBehaviour {
	
	void Start(){
		
		transform.GetComponent<DUGView>().visible = false;
		
		string hitColliderName = transform.GetComponent<DialogueController>().activeDialogue;
		
		



		transform.parent.GetComponent<DetectObjects>().moveCameraToPlayer();
		transform.parent.GetComponent<DetectObjects>().enableCameraAndMotor();
		transform.parent.GetComponent<DetectObjects>().enteredDialog = false;
		//set lock to false so others can interact with this object
		//transform.GetComponent<DetectObjects>().photonView.RPC("setInteractLock",PhotonTargets.AllBuffered, false);
		
		//delete the useless script generated by dunilog
		GameObject targetObj = findGrabObjWithTag(GameObject.Find(hitColliderName).transform);

		if(targetObj != null)
		{
		moveToPlayerPosition(targetObj);
		disableRender(targetObj);
		disableCollider (targetObj);
		enableInventory(targetObj);
		}


		GameObject.Destroy(this.gameObject.transform.GetComponent<DialogEnd>());
		//this.transform.parent.GetComponent<TriggerHandler>().moveCameraToPlayer();
		//this.transform.parent.GetComponent<TriggerHandler>().enableCameraAndMotor();
	}
	void moveToPlayerPosition(GameObject obj){
		obj.transform.position = this.transform.position;
		obj.transform.parent = this.transform;

	}

	GameObject findGrabObjWithTag( Transform parent){

		if (parent.tag == "pickable")
			return parent.gameObject;
		else foreach (Transform child in parent){
			if(child.gameObject.tag == "pickable"){
				print (child.gameObject.name);
				return child.gameObject;
			}
		}

		return null;

	}

	void disableCollider(GameObject obj){
		Collider[] colliders = obj.GetComponentsInChildren<Collider>();
		
		foreach ( Collider c in colliders)
		{
			if(c.enabled == true)
				c.enabled = false;
		}

	}
	void disableRender(GameObject obj)
	{
		Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

		foreach ( Renderer r in renderers)
		{
			if(r.enabled == true)
				r.enabled = false;
		}
	}

	void enableInventory(GameObject obj){

		GameObject.Find ("Inventory").GetComponent<GUITexture>().enabled = true;
		GameObject.Find ("Inventory").GetComponent<inventory>().updateInventoryObject(obj);

	}
}
