using UnityEngine;
using System.Collections;

public class DeskMode : MonoBehaviour {
	public string deskOwner;
	float w,h;
	public enum DeskModeSubMode: int{FileMode,PCMode,PhoneMode,None};
	
	public DeskModeSubMode mode;
	
	public GameObject FileMode;
	public GameObject PCMode;
	public GameObject TelephoneMode;



	Vector3 FileModeOriginalPosition;
	Vector3 PCModeOriginalPosition;
	Vector3 TelephoneModeOriginalPosition;

	public int FileModeFileIndex;
	public int FileModeMaxIndex;
	public Light highlight;

	float lightOffset;
	float cameraOffset;

	public bool sending;
	public bool checking;
	// email content flags
	bool computerIsOn;
	// Use this for initialization
	void Start () {
		w = Screen.width;
		h = Screen.height;
		mode = DeskModeSubMode.None;

		sending = false;
		checking = false;
		computerIsOn = false;

		FileModeOriginalPosition = FileMode.transform.position;
		//PCModeOriginalPosition = PCMode.transform.position;
		//TelephoneModeOriginalPosition = TelephoneMode.transform.position;

		FileModeFileIndex=1;
		lightOffset = 0.32f;
		cameraOffset = 0.47f;

		enableChildren();
	}
	
	void OnGUI(){
		switch (mode)
		{
		case DeskModeSubMode.FileMode:
		{



			if(GUI.Button( new LTRect(w - 200f, .9f*h - 50f, 100f, 50f ).rect, "Next"))
			{
				if(FileModeFileIndex<FileModeMaxIndex)
				{
					// remove viewer for current obj
					if(GameObject.Find("File"+FileModeFileIndex).GetComponent<ObjectViewer>())
					Destroy(GameObject.Find("File"+FileModeFileIndex).GetComponent<ObjectViewer>());
					// add viewer for next obj
					FileModeFileIndex++;
					Transform nextTr = GameObject.Find ("File"+FileModeFileIndex).transform;
					nextTr.gameObject.AddComponent<ObjectViewer>();

					// calculate the next obj mid point
					float midX = (nextTr.renderer.bounds.max.x + nextTr.renderer.bounds.min.x)/2;
					float midY = (nextTr.renderer.bounds.max.y + nextTr.renderer.bounds.min.y)/2;
					float midZ = (nextTr.renderer.bounds.max.z + nextTr.renderer.bounds.min.z)/2;

					LeanTween.move(Camera.main.gameObject,new Vector3(midX,midY+cameraOffset,midZ),.6f).setEase(LeanTweenType.easeOutQuint);
					LeanTween.move(highlight.gameObject,new Vector3(midX,midY+lightOffset,midZ),.6f).setEase(LeanTweenType.easeOutQuint);

				}
				else{

				}
					
			}
			if(GUI.Button( new LTRect(100f, .9f*h - 50f, 100f, 50f ).rect, "Back"))
			{
				if(FileModeFileIndex>1)
				{
					// remove viewer for current obj
					if(GameObject.Find("File"+FileModeFileIndex).GetComponent<ObjectViewer>())
						Destroy(GameObject.Find("File"+FileModeFileIndex).GetComponent<ObjectViewer>());
					// add viewer for next obj
					FileModeFileIndex--;
					Transform nextTr = GameObject.Find ("File"+FileModeFileIndex).transform;
					nextTr.gameObject.AddComponent<ObjectViewer>();



					float midX = (nextTr.renderer.bounds.max.x + nextTr.renderer.bounds.min.x)/2;
					float midY = (nextTr.renderer.bounds.max.y + nextTr.renderer.bounds.min.y)/2;
					float midZ = (nextTr.renderer.bounds.max.z + nextTr.renderer.bounds.min.z)/2;
					
					LeanTween.move(Camera.main.gameObject,new Vector3(midX,midY+cameraOffset,midZ),.6f).setEase(LeanTweenType.easeOutQuint);
					LeanTween.move(highlight.gameObject,new Vector3(midX,midY+lightOffset,midZ),.6f).setEase(LeanTweenType.easeOutQuint);
					
				}
				else{
					
				}
				
			}
			if(GUI.Button( new LTRect(w - 200f, .1f*h - 50f, 100f, 50f ).rect, "Back to DeskMode"))
			{
				mode = DeskModeSubMode.None;
				moveCameraToDesk();
				
			}

			break;

		}
		case DeskModeSubMode.PhoneMode:
		{
			break;
		}
		case DeskModeSubMode.PCMode:
		{

			if(!sending && !checking)
			{
			if(GUI.Button( new LTRect(0.6f*w, .5f*h - 50f, 100f, 50f ).rect, "Send Email"))
			{
				sending = true;
			}

			if(GUI.Button( new LTRect(0.6f*w, .7f*h - 50f, 100f, 50f ).rect, "Check Email"))
			{
				checking = true;
			}
			}
			else if (sending)
			{
				if(GUI.Button( new LTRect(0.3f*w, 0.2f*h, 300f,30f).rect, "Send to IT asking him to fix the computer."))
				   {

					// RPC call to display email
					PhotonView photonView = this.gameObject.GetPhotonView();
					
					
					photonView.RPC ("receiveEmail",PhotonTargets.OthersBuffered, "computer" , PlayerPrefs.GetInt("IT_pid"));

					print(PlayerPrefs.GetInt("IT_pid"));



					}
				if(GUI.Button( new LTRect(0.3f*w, 0.3f*h, 300f,30f).rect, "Send to Customer to thank him."))
				{
				}
				if(GUI.Button( new LTRect(0.3f*w, 0.4f*h, 300f,30f).rect, "Send to Mum to cook the dinner."))
				{
				}
				if(GUI.Button( new LTRect(0.3f*w, 0.5f*h, 300f,30f).rect, "Send to my dog asking it eat less."))
				{
				}
			}
			else if (checking)
			{
				// to be decided later
				if(computerIsOn)
					if(GUI.Button ( new LTRect(.3f*w, .5f*h,300f,30f).rect, "Please come down and help me fix the computer"))
				{
					// mark as re
				}

			}


			if(GUI.Button( new LTRect(1.0f*w - 100f, 1.0f*h - 50f, 100f, 50f ).rect, "Back to DeskMode"))
			{
				mode = DeskModeSubMode.None;
				moveCameraToDesk();
				
			}

			break;
		}
		case DeskModeSubMode.None:
		{
			if(GUI.Button( new LTRect(1.0f*w - 100f, 1.0f*h - 50f, 100f, 50f ).rect, "Quit DeskMode"))
			{
				disableChildren();
				GameObject.FindGameObjectWithTag(deskOwner).GetComponent<DetectObjects>().moveCameraToPlayer();
				GameObject.FindGameObjectWithTag(deskOwner).GetComponent<DetectObjects>().enableCameraAndMotor();
				GameObject.FindGameObjectWithTag(deskOwner).GetComponent<DetectObjects>().enteredDialog = false;
				
				GetComponent<DeskMode>().enabled = false;
				
			}
			break;
		}
			
		}
	}
	
	void enableChildren(){
		foreach(Transform child in transform)
			child.gameObject.AddComponent<DeskObjectHandler>();
	}
	
	void disableChildren(){
		foreach(Transform child in transform)
			if(child.gameObject.GetComponent<DeskObjectHandler>() !=null)
				Destroy(child.gameObject.GetComponent<DeskObjectHandler>());
	}
	void moveCameraToDesk(){
		Vector3 newPosition = new Vector3(56.45924f,3.803851f,54.16652f);
		Vector3 newRotation = new Vector3(15,270f,0);
		
		// set camera position and rotation
		Camera.main.transform.parent = null;
		//Camera.main.transform.localPosition = new Vector3(cameraX,cameraY,cameraZ);
		//Camera.main.transform.localEulerAngles = new Vector3(90f,-90f,0);
		
		Camera.main.transform.localPosition = newPosition;
		Camera.main.transform.localEulerAngles = newRotation;
	}
	// Update is called once per frame
	void Update () {
		if(!transform.GetComponentInChildren<DeskObjectHandler>())
			enableChildren();
	}
	[RPC]
	void receiveEmail(string content, int targetPlayerID){
		if(PhotonNetwork.player.ID == targetPlayerID)
		{
		switch (content)
		{
		case "computer":
		{
			computerIsOn = true;
			break;
		}
		default:
			break;
		}
		}
	}
}
