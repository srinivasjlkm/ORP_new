using UnityEngine;
using System.Collections;

public class DetectObjects : MonoBehaviour {
	public GameObject other;
	public ClickMove clickmove;




	void  OnGUI (){
	
		Ray ray= Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		//Vector3 mouseWorldPosition;
		
		if (Physics.Raycast (ray, out hit, 3)) {
			displayHint(hit.collider.name);
	
			
		}  
	}
	void displayHint( string objName){
		switch (objName) {
		case "Cabinet_L":
			GUI.Label( new Rect(Screen.width / 2,Screen.height/2,300,300),"Files are Stored Safely"); 
			break;
		case "Bin":
			GUI.Label( new Rect(Screen.width / 2,Screen.height/2,300,300),"Use to throw waste"); 
			break;
		case "Kettle":
			GUI.Label( new Rect(Screen.width / 2,Screen.height/2,300,300),"Kettle: Use to heat your coffee"); 
			break;
		case "Alarm":
			GUI.Label( new Rect(Screen.width / 2,Screen.height/2,300,300),"This will buzz in case of emergency"); 
			break;
		case "Printer":
			GUI.Label( new Rect(Screen.width / 2,Screen.height/2,300,300),"Do you want to print ?"); 
			break;
		case "Chair":
			GUI.Label( new Rect(Screen.width / 2,Screen.height/2,300,300),"Want to have a seat ?"); 
			break;
		



		default:
			break;
			
		}
	}


	//void  Files (){GUI.Label( new Rect(Screen.width / 2,Screen.height/2,300,300),"Files are Stored Safely"); }
	//void  Bin (){GUI.Label( new Rect(Screen.width / 2,Screen.height/2,300,300),"Use to throw waste");}
//	void  Kettle (){GUI.Label( new Rect(Screen.width / 2,Screen.height/2,300,300),"Kettle: Use to heat your coffee");}
//	void  Alarm (){GUI.Label( new Rect(Screen.width/2,Screen.height/2,300,300),"This will buzz in case of emergency");}
//	void  Printer (){GUI.Label( new Rect(Screen.width/2,Screen.height/2,300,300),"Do you want to print ?");}
//	void  Chair (){GUI.Label( new Rect(Screen.width/2,Screen.height/2,300,300),"Want to have a seat ?");}



	void  Cube(){	
				other = GameObject.Find ("Cube");
				clickmove=GetComponent<ClickMove> ();
				if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2, 300, 300), "Change Color")) {	
						other.renderer.material.color = Color.blue;
						clickmove.enabled = false;
				} else
						clickmove.enabled = true;
				
				}	


}