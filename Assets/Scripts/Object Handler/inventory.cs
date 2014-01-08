using UnityEngine;
using System.Collections;

public class inventory : MonoBehaviour {



	public GameObject inventoryObject;
	public Texture inventoryObjectTexture;

	public Texture2D option1_texture;
	public Texture2D option2_texture;
	public Texture2D option3_texture;
	public Texture2D option4_texture;
	
	private float w;
	private float h;
	

	private LTRect option1;
	private LTRect option2;
	private LTRect option3;
	private LTRect option4;

	private bool optionIsOn;
	private bool animationIsOn;
	
	// Use this for initialization
	void Start () {

		optionIsOn = false;
		animationIsOn = false;

		w = Screen.width;
		h = Screen.height;
	
		
		initOptions();
		

		inventoryObject = null;
		inventoryObjectTexture = null;
		this.GetComponent<GUITexture>().texture = null;
		this.GetComponent<GUITexture>().pixelInset= new Rect(.5f*w - 130, -(.5f*h)+20, 100 ,100);
	}
	


	void initOptions(){
		option1 = new LTRect(1.1f*w - option1_texture.width*0.2f, 1.1f*h - option1_texture.height*0.2f, option1_texture.width*.2f, option1_texture.height*.2f );
		option2 = new LTRect(1.1f*w - option1_texture.width*0.2f, 1.1f*h - option1_texture.height*0.2f, option1_texture.width*.2f, option1_texture.height*.2f );
		option3 = new LTRect(1.1f*w - option1_texture.width*0.2f, 1.1f*h - option1_texture.height*0.2f, option1_texture.width*.2f, option1_texture.height*.2f );
		option4 = new LTRect(1.1f*w - option1_texture.width*0.2f, 1.1f*h - option1_texture.height*0.2f, option1_texture.width*.2f, option1_texture.height*.2f );
	}
	// Update is called once per frame
	void OnGUI () {

		if(optionIsOn)
		{
		if(GUI.Button( option1.rect, option1_texture))
		{
				initOptions();
				optionIsOn = false;
				animationIsOn = false;
		}
		if(GUI.Button( option2.rect, option2_texture))
			{
				initOptions();
				optionIsOn = false;
				animationIsOn = false;
			}
		if(GUI.Button( option3.rect, option3_texture))
			{
				initOptions();
				optionIsOn = false;
				animationIsOn = false;
			}
		if(GUI.Button( option4.rect, option4_texture))
			{
				initOptions();
				optionIsOn = false;
				animationIsOn = false;
			}

		}
		
		if(animationIsOn){

			optionIsOn = true;
			LeanTween.move(option1,new Vector2(0.92f*w,0.7f*h),1.0f).setEase(LeanTweenType.easeOutElastic);
			LeanTween.move(option2,new Vector2(0.84f*w,0.73f*h),1.0f).setDelay(.05f).setEase(LeanTweenType.easeOutElastic);
			LeanTween.move(option3,new Vector2(0.78f*w,0.8f*h),1.0f).setDelay(.1f).setEase(LeanTweenType.easeOutElastic);
			LeanTween.move(option4,new Vector2(0.76f*w,0.90f*h),1.0f).setDelay(.15f).setEase(LeanTweenType.easeOutElastic);

			animationIsOn = false;
		}
		GUI.matrix = Matrix4x4.identity;
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

	

	void OnMouseOver(){

		optionOn();
	}

	void optionOn(){
	
		animationIsOn = true;

	}
}
