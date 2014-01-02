using UnityEngine;
using System.Collections;

public class inventory : MonoBehaviour {


	public GameObject inventoryObject;
	public Texture inventoryObjectTexture;

	// Use this for initialization
	void Start () {
		inventoryObject = null;
		inventoryObjectTexture = null;
		this.GetComponent<GUITexture>().texture = null;
		this.GetComponent<GUITexture>().pixelInset= new Rect(Screen.width/2 - 130, -(Screen.height/2), 100 ,100);


	}
	
	// Update is called once per frame
	void Update () {
		if(inventoryObject != null)
		{
			if(!inventoryObjectTexture){
				Debug.LogError("Assign a Texture in the inspector.");
				return;
			}
			

			this.GetComponent<GUITexture>().texture = inventoryObjectTexture;
		}
	}


	public void updateInventoryObject(GameObject obj){
		inventoryObject = obj;
		string texture = "Assets/Resources/Textures/"+obj.name+".png";
		inventoryObjectTexture = (Texture)Resources.LoadAssetAtPath(texture, typeof(Texture));
	}

	public void clearInventory(){
		inventoryObject = null;
		inventoryObjectTexture = null;
		this.GetComponent<GUITexture>().texture = null;

	}
}
