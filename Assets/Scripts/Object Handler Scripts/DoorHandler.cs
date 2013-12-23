using UnityEngine;
using System.Collections;

public class DoorHandler : Photon.MonoBehaviour {

	bool isOpen;
	bool enter;
	Collider Co;
	string colliderTag;

	// Use this for initialization
	void Start () {
		isOpen = false;
		enter = false;
		Co = null;
		colliderTag = "manager";
	}
	
	// Update is called once per frame
	void Update() {
		if(enter && Input.GetKeyDown("f")){
			Debug.Log("open door");
			PhotonView photonView = PhotonView.Get (this);
			photonView.RPC("Open",PhotonTargets.All);
		}
	}

	[RPC]
	void Open(){
		Transform child = transform.Find("doortestrun");
		isOpen = !isOpen;
		if(isOpen){
			if(child != null)
				child.animation.Play("Anim_Wooden_Open");
			else
				Debug.Log("did not get child");
		}//Debug.Log("Door is open");
		else{
			if(child != null)
				child.animation.Play("Anim_wooden_close");
			else
				Debug.Log("did not get child");
		}
	}
	

	//Activate the Main function when player is near the door
	void OnTriggerEnter (Collider collider){
		//Debug.Log("name: " + other.gameObject.transform.name);
		Co = collider;
		if(Co.GetComponent<PhotonView>().isMine && Co.gameObject.tag == colliderTag)
			enter = true;
	}

	//Deactivate the Main function when player is go away from door
	void OnTriggerExit (Collider collider){
		if(Co.GetComponent<PhotonView>().isMine && collider == Co)
			enter = false;
	}
}