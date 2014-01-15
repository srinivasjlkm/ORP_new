using UnityEngine;
using System.Collections;

public class DetectObjects : MonoBehaviour {
	

	public Texture2D cursorTextureInteract;
	public Texture2D cursorTextureTalk;
	CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = new Vector2(100,0);
	
	PhotonView hitObjPhotonView;
	
	
	private Vector3 objects_camera_view_position = new Vector3(0,0.0f,-1.112997f);
	private Vector3 npc_camera_view_position = new Vector3(0,2.0f,-1.5112997f);
	private Vector3 camera_view_rotation = new Vector3(0,0,0);
	
	public GameObject currentHitObj;
	
	public bool mouseClick;
	public bool enteredDialog;
	
	Transform dugManager;
	Shader originalShader;
	
	void Start()
	{
		mouseClick = false;
		currentHitObj = null;
		enteredDialog = false;
		
	}
	
	
	void  OnGUI (){
		
		Ray ray= Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		
		
		if (Physics.Raycast (ray, out hit, 3)) {
			// display word hint
			displayHint(hit.collider.name);
			
			

			
			// check if the ray hit another object
			if( hit.collider.gameObject == currentHitObj)
			{

				
				if (hit.collider.gameObject.tag == "interactive" || hit.collider.gameObject.tag == "pickable") {

					// change mouse cursor to interactive



					if(hit.collider.gameObject.renderer.material.shader != null && enteredDialog == false)
					{
						// set the cursor and shader 
						Cursor.SetCursor(cursorTextureInteract, hotSpot, cursorMode);	
						hit.collider.gameObject.renderer.material.shader = Shader.Find("Toon/Basic");
					}

					dugManager = transform.Find("DUGManager");
					
					hitObjPhotonView = PhotonView.Get(hit.collider.gameObject);
					
					//hit.transform.renderer.material.color = Color.green;
					
					if (Input.GetKeyUp (KeyCode.Mouse0)) {
						mouseClick = true;
						enteredDialog = true;

						if(enteredDialog && mouseClick)
						{
							currentHitObj.renderer.material.shader = originalShader;
							Cursor.SetCursor(null, Vector2.zero, cursorMode);
							dugManager.GetComponent<DUGView>().visible = true;
//							print(hit.collider.name);
							dugManager.GetComponent<DialogueController>().setActiveDialogue(hit.collider.name);
							disableCameraAndMotor();
							moveCameraToObject(hit.collider.gameObject);
							mouseClick = !mouseClick;

						}
						
						
					}
				}

				else if( hit.collider.gameObject.tag == "NPC")
				{


					// change mouse cursor to talk
					//Cursor.SetCursor(cursorTextureTalk, hotSpot, cursorMode);

					dugManager = transform.Find("DUGManager");
					
					hitObjPhotonView = PhotonView.Get(hit.collider.gameObject);
					
					//hit.transform.renderer.material.color = Color.green;
					
					if (Input.GetKeyUp (KeyCode.Mouse0)) {
						mouseClick = true;
						enteredDialog = true;
						
						if(enteredDialog && mouseClick)
						{
							//currentHitObj.renderer.material.shader = originalShader;


							Cursor.SetCursor(null, Vector2.zero, cursorMode);


							dugManager.GetComponent<DUGView>().visible = true;
							print(hit.collider.name);
							dugManager.GetComponent<DialogueController>().setActiveDialogue(hit.collider.name);
							disableCameraAndMotor();
							moveCameraToObject(hit.collider.gameObject);
							mouseClick = !mouseClick;
							
						}
						
						
					}
				}
				else if (hit.collider.gameObject.tag == "desk")
				{
					
					if(hit.collider.gameObject.renderer.material.shader != null && enteredDialog == false)
					{
						// set the cursor and shader 
						Cursor.SetCursor(cursorTextureInteract, hotSpot, cursorMode);	
						hit.collider.gameObject.renderer.material.shader = Shader.Find("Toon/Basic");
					}
					

					
					//hitObjPhotonView = PhotonView.Get(hit.collider.gameObject);
					
					//hit.transform.renderer.material.color = Color.green;
					
					if (Input.GetKeyUp (KeyCode.Mouse0)) {
						mouseClick = true;
						enteredDialog = true;
						
						if(enteredDialog && mouseClick)
						{
							currentHitObj.renderer.material.shader = originalShader;
							Cursor.SetCursor(null, Vector2.zero, cursorMode);
							//dugManager.GetComponent<DUGView>().visible = true;
							//print(hit.collider.name);
							//dugManager.GetComponent<DialogueController>().setActiveDialogue(hit.collider.name);


							this.GetComponent<CharacterMotor>().inputMoveDirection = Vector3.zero;
							disableCameraAndMotor();
							moveCameraToDesk(hit.collider.gameObject);
							// player sit animation



							hit.transform.GetComponent<DeskMode>().enabled = true;
							mouseClick = !mouseClick;
							
						}
						
						
					}
				}

				else if (hit.collider.gameObject.tag == "door")
				{



						if(hit.collider.gameObject.renderer.material.shader != null)
					{
						// set the cursor and shader 
						Cursor.SetCursor(cursorTextureInteract, hotSpot, cursorMode);	
						hit.collider.gameObject.renderer.material.shader = Shader.Find("Toon/Basic");
					}
					

					

					
					//hit.transform.renderer.material.color = Color.green;
					
					if (Input.GetKeyUp (KeyCode.Mouse0)) {
						mouseClick = true;

						
						if( mouseClick)
						{

							hit.collider.transform.parent.parent.parent.GetComponent<DoorHandler>().clicked = true;

							currentHitObj.renderer.material.shader = originalShader;
							Cursor.SetCursor(null, Vector2.zero, cursorMode);
							mouseClick = !mouseClick;
							
						}
						
						
					}

				}



			}
			else // if the hitted obj is a new obj
			{

				Cursor.SetCursor(null, Vector2.zero, cursorMode);


				if( currentHitObj !=null && currentHitObj.tag == "NPC")
				{


					
					mouseClick = false;
					currentHitObj = hit.collider.gameObject;



				}
				else{

				if(currentHitObj != null && enteredDialog == false)
					{
						// set back the shader and cursor
						if(currentHitObj.renderer)
						currentHitObj.renderer.material.shader = originalShader;

					}

				mouseClick = false;
				currentHitObj = hit.collider.gameObject;
				if(currentHitObj.renderer)
				originalShader = currentHitObj.renderer.material.shader;
				}
				//print (currentHitObj.name);
			}
			
			
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
	
	void disableCameraAndMotor(){
		if(this != null){
			this.GetComponent<MouseCamera>().enabled =false;
			Camera.main.GetComponent<MouseCamera>().enabled = false;
				
			this.GetComponent<ClickMove>().enabled = false;
			this.GetComponent<DetectObjects>().enabled = false;
		}
	}


	public void moveCameraToDesk(GameObject desk){
		// calculate camera XY position
		float cameraX = (desk.collider.bounds.max.x + desk.collider.bounds.min.x)/2;
		float cameraZ = (desk.collider.bounds.max.z + desk.collider.bounds.min.z)/2;
		// calculate camera height

		float objXLength = desk.collider.bounds.size.x;
		float objZLength = desk.collider.bounds.size.z;
		float cameraYOffset = objXLength < objZLength ? objXLength:objZLength;


		float deskTopY = desk.collider.bounds.max.y;

		float cameraY = deskTopY + cameraYOffset - 0.1f;
		// calculate camera local eulerangles


		Vector3 newPosition = new Vector3(56.45924f,3.803851f,54.16652f);
		Vector3 newRotation = new Vector3(15,270f,0);

		// set camera position and rotation
		Camera.main.transform.parent = null;
		//Camera.main.transform.localPosition = new Vector3(cameraX,cameraY,cameraZ);
		//Camera.main.transform.localEulerAngles = new Vector3(90f,-90f,0);

		Camera.main.transform.localPosition = newPosition;
		Camera.main.transform.localEulerAngles = newRotation;



	}
	
	public void moveCameraToObject(GameObject obj){

		//calculate the center of the obj
		float objXmid = (obj.collider.bounds.max.x + obj.collider.bounds.min.x)/2;
		float objYmid = (obj.collider.bounds.max.y + obj.collider.bounds.min.y)/2;
		float objZmid = (obj.collider.bounds.max.z + obj.collider.bounds.min.z)/2;

		// calculate how far should the camera be

		float cameraOffset = (obj.collider.bounds.max.y - obj.collider.bounds.min.y)*2;



		if(obj.transform.rotation.eulerAngles.y <=45 || obj.transform.rotation.eulerAngles.y > 315)
		{
			objects_camera_view_position = new Vector3(0f,0f,cameraOffset);
			// calculate the front angle
			camera_view_rotation = new Vector3(0f,180f,0f);
		}
		else if (obj.transform.rotation.eulerAngles.y <=135 && obj.transform.rotation.eulerAngles.y > 45)
		{
			objects_camera_view_position = new Vector3(cameraOffset,0f,0f);
				camera_view_rotation = new Vector3(0f,270f,0f);
		}
		else if (obj.transform.rotation.eulerAngles.y <=225 && obj.transform.rotation.eulerAngles.y > 135)
		{
			objects_camera_view_position = new Vector3(0f,0f,-cameraOffset);
			camera_view_rotation = new Vector3(0f,0f,0f);
		}
		else if (obj.transform.rotation.eulerAngles.y <=315 && obj.transform.rotation.eulerAngles.y > 225)
		{
			objects_camera_view_position = new Vector3(-cameraOffset,0f,0f);
			camera_view_rotation = new Vector3(0f,90f,0f);
		}






		Camera.main.transform.parent = null;
		if(obj.tag == "NPC")
			Camera.main.transform.localPosition = obj.transform.position+ npc_camera_view_position;
		else
			Camera.main.transform.localPosition = new Vector3(objXmid,objYmid,objZmid) + objects_camera_view_position;
		Camera.main.transform.localEulerAngles = camera_view_rotation;
		
	}
	
	[RPC]
	public void moveCameraToPlayer(){
		
		
		
		//	print (this);
		if(this != null){
			Camera.main.transform.parent = this.transform;
			Camera.main.transform.localPosition = this.GetComponent<ThirdPersonNetworkVik>().cameraRelativePosition;
			Camera.main.transform.localEulerAngles = new Vector3(0.6651921f, 0, 0);
		}
	}
	
	[RPC]
	public void enableCameraAndMotor(){
		if(this != null)
		{
			this.GetComponent<MouseCamera>().enabled = true;
			this.GetComponent<ClickMove>().enabled = true;
			this.GetComponent<ClickMove>().targetPosition = this.transform.position;
			Camera.main.GetComponent<MouseCamera>().enabled = true;
			this.GetComponent<DetectObjects>().enabled = true;
		}
	}

	
	
	
	
	
	
}