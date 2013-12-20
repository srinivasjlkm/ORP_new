using UnityEngine;
using System.Collections;


public class AnimationController: MonoBehaviour
{

	public enum CharacterState
	{
		idle,
		run,
		tap
		
	}
	
	
	public Animation target;
	// The animation component being controlled

	public CharacterState state = CharacterState.idle;
	private bool canLand = true;
	
	
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
		state = CharacterState.idle;
		
		
	}
	
	
	void Land ()
		// End a landing and transition to normal animation state (ignore if not currently landing)
	{
		
		state = CharacterState.idle;
	}
	
	

	
	
	void Update ()
		// Animation control
	{
		switch (state)
		{
		case CharacterState.idle:
			target.CrossFade ("Male_idle1_anim");
			break;
		case CharacterState.run:
			target.CrossFade ("Male_run_anim");
			break;
		case CharacterState.tap:
			target.CrossFade ("Male_tap_anim");
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
