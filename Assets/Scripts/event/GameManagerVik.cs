using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using System.Collections.Generic;

public class GameManagerVik : Photon.MonoBehaviour {

    // this is a object name (must be in any Resources folder) of the prefab to spawn as player avatar.
    // read the documentation for info how to spawn dynamically loaded game objects at runtime (not using Resources folders)
	public GameObject[] playerPrefabList;
	public string[] playerList;
	public Transform[] managerSpawnPositionList;
	public Transform[] ITSpawnPositionList;
	public Transform[] customerSpawnPositionList;
	public Transform[] officerSpawnPositionList;
	private Vector3 spawnPosition;
	HashSet<string> selectedPlayerList = new HashSet<string>();
	bool roleSelected = false;
	public PlayMakerFSM EventManager;


    void OnJoinedRoom()
    {


    }

	Vector3 randomSpawnPosition(Transform[] positionList){
		Transform tr = positionList[Random.Range(0,positionList.Length)];
		return tr.position;

	}


	void OnGUI(){

		if (PhotonNetwork.room == null) return;


		// quit button GUI
		if (GUILayout.Button("Leave& QUIT"))
		{

			PhotonView photonView = this.gameObject.GetPhotonView();
			
			
			photonView.RPC ("setRoleAvailable",PhotonTargets.AllBuffered,PlayerPrefs.GetString("playerName"));
			PhotonNetwork.LeaveRoom();
		}


		// if role selection not completed, draw GUI
		if(!roleSelected)
		{

		GUILayout.BeginArea(new Rect((Screen.width - 400) / 2, (Screen.height - 300) / 2, 600, 300));
		GUILayout.BeginHorizontal();
		GUILayout.Label("Chose a role:", GUILayout.Width(150));

		PhotonView photonView = this.gameObject.GetPhotonView();

			for(int i =0;i<playerList.Length;i++)
			{
				if (!selectedPlayerList.Contains(playerList[i]))
				{
					if(GUILayout.Button(playerList[i],GUILayout.Width(100)) )
					{
						PhotonNetwork.playerName = playerList[i];
						PlayerPrefs.SetString("playerName", playerList[i]);
						roleSelected = true;

						// broadcast role selected
						photonView.RPC ("setRoleUnavailable",PhotonTargets.AllBuffered,playerList[i]);
						
						StartGame();
					}
				}
			}

		GUILayout.EndHorizontal();
		GUILayout.EndArea();
		}

		// if number of  players reach maximum, player cannot select role
//		if(PhotonNetwork.playerList.Length < playerList.Length)
//			GUILayout.BeginArea(new Rect((Screen.width - 400) / 2, (Screen.height - 300) / 2, 600, 300));
	}
    IEnumerator OnLeftRoom()
    {
        //Easy way to reset the level: Otherwise we'd manually reset the camera

        //Wait untill Photon is properly disconnected (empty room, and connected back to main server)
        while(PhotonNetwork.room!=null || PhotonNetwork.connected==false)
            yield return 0;
		Application.LoadLevel(Application.loadedLevel);

    }


	void OnPhotonPlayerConnected(){
		print ("Now we have: "+PhotonNetwork.playerList.Length+" players in total.");
		EventManager.FsmVariables.GetFsmInt("playerNum").Value = PhotonNetwork.playerList.Length;

	}

	void OnPhotonPlayerDisconnected(){
		print ("Now we have: "+PhotonNetwork.playerList.Length+" players in total.");
		EventManager.FsmVariables.GetFsmInt("playerNum").Value = PhotonNetwork.playerList.Length;

	}

    void StartGame()
    {

		print ("Now we have: "+PhotonNetwork.playerList.Length+" players in total.");
		EventManager.FsmVariables.GetFsmInt("playerNum").Value = PhotonNetwork.playerList.Length;
        Camera.main.farClipPlane = 1000; //Main menu set this to 0.4 for a nicer BG    

        //prepare instantiation data for the viking: Randomly diable the axe and/or shield
        bool[] enabledRenderers = new bool[2];
        enabledRenderers[0] = Random.Range(0,2)==0;//Axe
        enabledRenderers[1] = Random.Range(0, 2) == 0; ;//Shield
        
        object[] objs = new object[1]; // Put our bool data in an object array, to send
        objs[0] = enabledRenderers;


		string playerName = PlayerPrefs.GetString("playerName");






		// instantiate prefab based on the name
		for(int i = 0 ; i < playerPrefabList.Length; i++)
		{
			if(playerName == playerPrefabList[i].name)
			{
				switch(playerName)
				{
				case "manager":
					spawnPosition = randomSpawnPosition(managerSpawnPositionList);
					PhotonNetwork.Instantiate(playerPrefabList[i].name, spawnPosition, Quaternion.identity, 0, objs);
					PlayerPrefs.SetInt("manager_pid", PhotonNetwork.player.ID);

					break;
				case "IT":
					spawnPosition = randomSpawnPosition(ITSpawnPositionList);
					PhotonNetwork.Instantiate(playerPrefabList[i].name, spawnPosition, Quaternion.identity, 0, objs);
					PlayerPrefs.SetInt("IT_pid", PhotonNetwork.player.ID);

					break;
				case "customer":
					spawnPosition = randomSpawnPosition(customerSpawnPositionList);
					PhotonNetwork.Instantiate(playerPrefabList[i].name, spawnPosition, Quaternion.identity, 0, objs);
					PlayerPrefs.SetInt("customer_pid", PhotonNetwork.player.ID);

					break;
				case "officer":
					spawnPosition = randomSpawnPosition(officerSpawnPositionList);
					PhotonNetwork.Instantiate(playerPrefabList[i].name, spawnPosition, Quaternion.identity, 0, objs);
					PlayerPrefs.SetInt("officer_pid", PhotonNetwork.player.ID);

					break;
				default:
					break;
					
					
				}
	       		
			}
		}




    }
	[RPC]

	void setRoleUnavailable(string role){
		selectedPlayerList.Add(role);
	}

	[RPC]
	void setRoleAvailable(string role){
		selectedPlayerList.Remove(role);
	}

	

    void OnDisconnectedFromPhoton()
    {
        Debug.LogWarning("OnDisconnectedFromPhoton");
    }    
}
