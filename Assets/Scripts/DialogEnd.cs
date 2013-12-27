using UnityEngine;
using System.Collections;

public class DialogEnd : MonoBehaviour {

	void Start(){

		transform.GetComponent<DUGView>().visible = false;

		string triggerName = transform.GetComponent<DialogueController>().activeDialogue;



		if(triggerName == "")
			return;

		GameObject.Find(triggerName).GetComponent<TriggerHandler>().moveCameraToPlayer();
        GameObject.Find(triggerName).GetComponent<TriggerHandler>().enableCameraAndMotor();
		GameObject.Find(triggerName).GetComponent<TriggerHandler>().mouseClick = false;

		GameObject.Destroy(this.gameObject.transform.GetComponent<DialogEnd>());
		//this.transform.parent.GetComponent<TriggerHandler>().moveCameraToPlayer();
		//this.transform.parent.GetComponent<TriggerHandler>().enableCameraAndMotor();
	}
}
