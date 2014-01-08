using UnityEngine;
using System.Collections;

public class NPCEvent2 : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.GetComponent<DUGView>().visible = false;
		;

		GameObject.Find ("npcTrigger").GetComponent<TriggerHandler>().moveCameraToPlayer();
		GameObject.Find ("npcTrigger").GetComponent<TriggerHandler>().enableCameraAndMotor();

		GameObject.Find ("npcTrigger").GetPhotonView().RPC("destroyNPC",PhotonTargets.AllBuffered);

	}



}
