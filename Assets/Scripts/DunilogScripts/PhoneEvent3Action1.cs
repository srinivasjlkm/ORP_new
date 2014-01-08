using UnityEngine;
using System.Collections;


public class PhoneEvent3Action1 : MonoBehaviour {

	void Start(){
		GameObject go = GameObject.Find("Fire_Extinguisher");
		for(int i = 0; i<go.transform.childCount;i++){
			go.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
		}
	}

	
}
