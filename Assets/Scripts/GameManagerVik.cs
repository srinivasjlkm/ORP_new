using UnityEngine;
using System.Collections;

public class GameManagerVik : Photon.MonoBehaviour {

    // this is a object name (must be in any Resources folder) of the prefab to spawn as player avatar.
    // read the documentation for info how to spawn dynamically loaded game objects at runtime (not using Resources folders)
    public string managerPrefab="mr";
	public string officerPrefab="officerPrefab";
	public string womanprefab = "Smart Woman";
	public string playerprefab= "Player";

    void OnJoinedRoom()
    {
        StartGame();
    }
    
    IEnumerator OnLeftRoom()
    {
        //Easy way to reset the level: Otherwise we'd manually reset the camera

        //Wait untill Photon is properly disconnected (empty room, and connected back to main server)
        while(PhotonNetwork.room!=null || PhotonNetwork.connected==false)
            yield return 0;

        Application.LoadLevel(Application.loadedLevel);

    }

    void StartGame()
    {
        Camera.main.farClipPlane = 1000; //Main menu set this to 0.4 for a nicer BG    

        //prepare instantiation data for the viking: Randomly diable the axe and/or shield
        bool[] enabledRenderers = new bool[2];
        enabledRenderers[0] = Random.Range(0,2)==0;//Axe
        enabledRenderers[1] = Random.Range(0, 2) == 0; ;//Shield
        
        object[] objs = new object[1]; // Put our bool data in an object array, to send
        objs[0] = enabledRenderers;


		string playerName = PlayerPrefs.GetString("playerName");




		if(playerName == "manager")
		{
        PhotonNetwork.Instantiate(this.managerPrefab, transform.position, Quaternion.identity, 0, objs);
		//PhotonNetwork.isMessageQueueRunning = true;
		}
		else if(playerName == "officer")
		PhotonNetwork.Instantiate(this.officerPrefab, transform.position, Quaternion.identity, 0, objs);
		else if(playerName == "Smart Woman")
		PhotonNetwork.Instantiate(this.womanprefab, transform.position, Quaternion.identity, 0, objs);
		else if(playerName == "Player")
		PhotonNetwork.Instantiate(this.playerprefab, transform.position, Quaternion.identity, 0, objs);




    }

    void OnGUI()
    {
        if (PhotonNetwork.room == null) return; //Only display this GUI when inside a room

        if (GUILayout.Button("Leave& QUIT"))
        {
            PhotonNetwork.LeaveRoom();
        }
    }
	

    void OnDisconnectedFromPhoton()
    {
        Debug.LogWarning("OnDisconnectedFromPhoton");
    }    
}
