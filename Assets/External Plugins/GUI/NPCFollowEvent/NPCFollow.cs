using UnityEngine;
using System.Collections;

public class NPCFollow : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.GetComponent<DUGView>().visible = false;

		enablePlayerRenderer();
		
		this.transform.parent.GetComponent<NPCController>().moveCameraToPlayer();
		this.transform.parent.GetComponent<NPCController>().enableCameraAndMotor();

		PhotonView photonView = PhotonView.Find(4);
		photonView.RPC("destroyNPC",PhotonTargets.All);

	}

	[RPC]
	void destroyNPC(){
		GameObject go = GameObject.Find("npc").gameObject;
		if(go != null){
			Destroy(go);
		}
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
