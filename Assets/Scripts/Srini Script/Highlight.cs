using UnityEngine;
using System.Collections;

public class Highlight : MonoBehaviour {

	// Use this for initialization
	private float blueMultiply = 3.50f;
	
	private float redGreenMultiply = 0.50f; 
	
	
	
	private Color originalColor;
	
	
	
	private void Start()
		
	{
		
		originalColor = gameObject.renderer.material.color;
		
	}
	
	
	
	void OnMouseEnter()
		
	{

		renderer.material.shader = Shader.Find("Toon/Basic Outline");
		//AddHighlight();
		
	}
	
	
	
	void OnMouseExit()
		
	{
		//renderer.material.shader = Shader.Find("Toon/Basic Outline");
		RemoveHighlight();
		renderer.material.shader = Shader.Find("Diffuse");

		
	}
	
	
	
//	private void  AddHighlight() 
//		
//	{
//		
//		float red = originalColor.r * redGreenMultiply;
//		
//		float blue = originalColor.b * blueMultiply;
//		
//		float green = originalColor.g * redGreenMultiply;
//		
//		
//		
//		gameObject.renderer.material.color = new Color(red, green, blue);
//		
//	}
	
	
	
	private void RemoveHighlight()
		
	{
		
		gameObject.renderer.material.color = originalColor;
		
	}
}
