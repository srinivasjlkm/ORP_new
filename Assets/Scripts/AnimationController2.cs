using UnityEngine;
using System.Collections;

[RequireComponent (typeof (FPSInputController))]
public class AnimationController2 : MonoBehaviour
{

	enum CharacterState
	{
		Male_idle1_anim,
		Male_run_anim,
		Male_tap_anim,
		
	}
	
	
	public Animation target;
	// The animation component being controlled

	private CharacterState state = CharacterState.Male_idle1_anim;
	private bool canLand = true;
	public Rigidbody rigidbody;
	
	
	void Reset ()
		// run setup on component attach, so it is visually more clear which references are used
	{
		Setup ();
	}
	
	
	void Setup ()
		// If target or rigidbody are not set, try using fallbacks
	{
		if (target == null)
		{
			target = GetComponent<Animation> ();
		}
		if (rigidbody == null){
			rigidbody = GetComponent<Rigidbody>();
		}
	}

	
	void Start ()
		// Verify setup, configure
	{
		Setup ();
		// Retry setup if references were cleared post-add
	}
	


	//	void Onjump ()
	//	// Start a jump
	//	{
	//		canLand = false;
	//		state = CharacterState.jump;
	//		
	//		Invoke ("idle", target["idle"].length);
	//	}
	
	
	void OnLand ()
		// Start a landing
	{
		canLand = false;
		state = CharacterState.Male_idle1_anim;
		
		
	}
	
	
	
	
	
	void Land ()
		// End a landing and transition to normal animation state (ignore if not currently landing)
	{
		
		state = CharacterState.Male_idle1_anim;
	}
	
	
	
	
	
	void Update ()
		// Animation control
	{
		switch (state)
		{
		case CharacterState.Male_idle1_anim:
			Vector3 movement = new Vector3(rigidbody.velocity.x, 0.0f, rigidbody.velocity.z); 
			print (rigidbody.velocity);
			
			if (movement.magnitude <= 0)
			{
				
				target.CrossFade ("Male_idle1_anim");
				
			}
			else
			{
				target.CrossFade ("Male_run_anim");
			}
			break;
			//			case CharacterState.jump:
			//				target.CrossFade ("jump");
			//			break;
			//			case CharacterState.Falling:
			//				target.CrossFade ("Fall");
			//			break;
			//			case CharacterState.Landing:
			//				target.CrossFade ("Land");
			//			break;
		}
	}
	
	
	//	void LateUpdate ()
	//	// Apply directional rotation of lower body
	//	{
	//		float targetAngle = 0.0f;
	//		
	//		Vector3 movement = HorizontalMovement;
	//		
	//		if (movement.magnitude >= walkSpeed)
	//		// Only calculate the target angle if we're moving sufficiently
	//		{
	//			targetAngle = Vector3.Angle (movement, new Vector3 (root.forward.x, 0.0f, root.forward.z));
	//			
	//			if (Vector3.Angle (movement, root.right) > Vector3.Angle (movement, root.right * -1))
	//			// Negative rotation if shortest route is counter-clockwise
	//			{
	//				targetAngle *= -1.0f;
	//			}
	//			
	//			if (Mathf.Abs (targetAngle) > 91.0f)
	//			// When walking backwards, don't rotate over 90 degrees and rotate opposite
	//			{
	//				targetAngle = targetAngle + (targetAngle > 0 ? -180.0f : 180.0f);
	//			}
	//		}
	//		
	//		currentRotation = Mathf.Lerp (currentRotation, targetAngle, Time.deltaTime * rotationSpeed);
	//			// Update our current rotation score
	//		
	//		hub.RotateAround (hub.position, root.up, currentRotation);
	//			// Rotate the dude
	//		spine.RotateAround (spine.position, root.up, currentRotation * -1.0f);
	//			// Rotate the upper-body to face forward
	//	}
}
