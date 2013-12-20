using UnityEngine;
using System.Collections;

public class NPCController: MonoBehaviour {
	//string dugManager 
	//string runFromFireGUI = "RunFromFireGUI";
	string GUI_name = "DUGManager";

	public Collider enteredObj = null;
	// Use this for initialization

	private Vector3 NPC_camera_view_position = new Vector3(0,2.72f,2.3f);
	private Vector3 NPC_camera_view_rotation = new Vector3(0,180f,0);


	void Start () {
		this.transform.FindChild(GUI_name).GetComponent<DUGView>().visible = false;
		enteredObj = null;
	//	(this.transform.FindChild(GUI_name).GetComponent("Halo") as Behaviour).enabled = false;
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider Co){
		if( Co.tag == "manager" )
		{	
			enteredObj = Co;
		//	(this.transform.FindChild(GUI_name).GetComponent("Halo") as Behaviour).enabled = true;
		}
	}
	void OnTriggerStay(Collider Co){
		if(Input.GetKeyDown("f"))
		{
			this.transform.FindChild(GUI_name).GetComponent<DUGView>().visible = true;

			disableCameraAndMotor(Co);

			moveCameraToNPC();

			Renderer[] rs = Co.GetComponentsInChildren<Renderer>();
			foreach(Renderer r in rs)
					r.enabled = false;

		}
	}

	void OnTriggerExit(Collider Co){
		if(Co == enteredObj)
			enteredObj = null;
		//	(this.transform.FindChild(GUI_name).GetComponent("Halo") as Behaviour).enabled = false;

	}

	public void moveCameraToNPC(){

		Camera.main.transform.parent = this.transform;
		Camera.main.transform.localPosition = NPC_camera_view_position;
		Camera.main.transform.localEulerAngles = NPC_camera_view_rotation;

	}


	public void moveCameraToPlayer(){
	//	print (enteredObj);
		if(enteredObj!=null){
		Camera.main.transform.parent = enteredObj.transform;
		Camera.main.transform.localPosition = new Vector3(-0.04869021f,1.303013f, 0.08047496f);
		Camera.main.transform.localEulerAngles = new Vector3(0.6651921f, 0, 0);
		}
	}

	void disableCameraAndMotor(Collider co){
		co.GetComponent<MouseCamera>().enabled =false;
		co.GetComponent<ClickMove>().enabled = false;
	}

	public void enableCameraAndMotor(){
		if(enteredObj)
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