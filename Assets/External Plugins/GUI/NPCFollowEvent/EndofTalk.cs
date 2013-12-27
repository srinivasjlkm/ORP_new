using UnityEngine;
using System.Collections;


public class EndofTalk : MonoBehaviour {
	// Use this for initialization
	void Start () {
		transform.GetComponent<DUGView>().visible = false;

//		this.transform.parent.GetComponent<TriggerHandler>().moveCameraToPlayer();
//		this.transform.parent.GetComponent<TriggerHandler>().enableCameraAndMotor();
	}

}
