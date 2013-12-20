using UnityEngine;
using System.Collections;


public class EndofTalk : MonoBehaviour {
	// Use this for initialization
	void Start () {
		transform.GetComponent<DUGView>().visible = false;


		enablePlayerRenderer();

		this.transform.parent.GetComponent<NPCController>().moveCameraToPlayer();
		this.transform.parent.GetComponent<NPCController>().enableCameraAndMotor();
	}

	void enablePlayerRenderer(){
		if(this.transform.parent.GetComponent<NPCController>().enteredObj!=null)
		{
		Renderer[] rs =  this.transform.parent.GetComponent<NPCController>().enteredObj.GetComponentsInChildren<Renderer>();
		foreach (Renderer r in rs)
			r.enabled = true;
		}
	}

}
