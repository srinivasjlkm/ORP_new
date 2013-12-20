using UnityEngine;
using System.Collections;

public class DoorHandler : Photon.MonoBehaviour {

	float smooth = 2.0f;
	float DoorOpenAngle = 90.0f;
	float DoorCloseAngle = 180.0f;
	bool isOpen ;
	bool enter ;

	// Use this for initialization
	void Start () {
		isOpen = false;
		enter = false;
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
	void OnTriggerEnter (Collider other){
		//Debug.Log("name: " + other.gameObject.transform.name);
		if(other.gameObject.tag == "manager")
			enter = true;
	}

	//Deactivate the Main function when player is go away from door
	void OnTriggerExit (Collider other){
		enter = false;
	}
}