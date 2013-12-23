using UnityEngine;
using System.Collections;

public class NPCFollow : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.GetComponent<DUGView>().visible = false;

		//enablePlayerRenderer();

		PhotonView photonView = GameObject.Find ("npc").GetPhotonView();

//		this.transform.parent.GetComponent<NPCController>().moveCameraToPlayer();
//		this.transform.parent.GetComponent<NPCController>().enableCameraAndMotor();
		photonView.RPC("moveCameraToPlayer",PhotonTargets.All);
		photonView.RPC("enableCameraAndMotor", PhotonTargets.All);
		photonView.RPC("destroyNPC",PhotonTargets.All);

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
