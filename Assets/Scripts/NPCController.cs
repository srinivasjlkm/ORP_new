using UnityEngine;
using System.Collections;

public class NPCController: MonoBehaviour {
	//string dugManager 
	//string runFromFireGUI = "RunFromFireGUI";
	string GUI_name = "DUGManager";
	bool enter;
	string colliderTag;
	public Collider enteredObj = null;
	// Use this for initialization

	private Vector3 NPC_camera_view_position = new Vector3(0,2.72f,2.3f);
	private Vector3 NPC_camera_view_rotation = new Vector3(0,180f,0);
	PhotonView photonView;


	void Start () {
		colliderTag = "manager";
		this.transform.FindChild(GUI_name).GetComponent<DUGView>().visible = false;
		enteredObj = null;
		enter = false;
	//	(this.transform.FindChild(GUI_name).GetComponent("Halo") as Behaviour).enabled = false;
	}

	// Update is called once per frame
	void Update () {
		talkToNPC();
	}

	void OnTriggerEnter(Collider Co){
		if(Co.GetComponent<PhotonView>().isMine && Co.tag == colliderTag )
		{
			enter = true;
			enteredObj = Co;
		//	(this.transform.FindChild(GUI_name).GetComponent("Halo") as Behaviour).enabled = true;
		}
	}
//	void OnTriggerStay(Collider Co){
//		if(Co.GetComponent<PhotonView>().isMine && Input.GetKeyDown("f"))
//		{
//			this.transform.FindChild(GUI_name).GetComponent<DUGView>().visible = true;
//
//			disableCameraAndMotor(Co);
//
//			moveCameraToNPC();
//
//			Renderer[] rs = Co.GetComponentsInChildren<Renderer>();
//			foreach(Renderer r in rs)
//					r.enabled = false;
//
//		}
//	}

	void OnTriggerExit(Collider Co){
		if(Co.GetComponent<PhotonView>().isMine && Co == enteredObj){
			enteredObj = null;
			enter = false;
		//	(this.transform.FindChild(GUI_name).GetComponent("Halo") as Behaviour).enabled = false;
		}
	}

	public void talkToNPC(){
		if(enter && Input.GetKeyDown("f")){
			{
				this.transform.FindChild(GUI_name).GetComponent<DUGView>().visible = true;

				disableCameraAndMotor(enteredObj);
				moveCameraToNPC();	
			}
		}
	}

	void disableCameraAndMotor(Collider co){
		co.GetComponent<MouseCamera>().enabled =false;
		co.GetComponent<ClickMove>().enabled = false;
	}

	public void moveCameraToNPC(){

		Camera.main.transform.parent = this.transform;
		Camera.main.transform.localPosition = NPC_camera_view_position;
		Camera.main.transform.localEulerAngles = NPC_camera_view_rotation;

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
		GameObject go = GameObject.Find("npc").gameObject;
		if(go != null){
			Destroy(go);
		}
	}
}