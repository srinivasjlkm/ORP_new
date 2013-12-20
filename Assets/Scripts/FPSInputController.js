private var motor : CharacterMotor;
private var isRemotePlayer: boolean;
// Use this for initialization
function Awake () {
	motor = GetComponent(CharacterMotor);
	isRemotePlayer = false;
}

// Update is called once per frame
function Update () {
	if(isRemotePlayer)
	{}
	else
	{
	// Get the input vector from kayboard or analog stick
	var directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	
	if (directionVector != Vector3.zero) {
		// Get the length of the directon vector and then normalize it
		// Dividing by the length is cheaper than normalizing when we already have the length anyway
		var directionLength = directionVector.magnitude;
		directionVector = directionVector / directionLength;
		
		// Make sure the length is no bigger than 1
		directionLength = Mathf.Min(1, directionLength);
		
		// Make the input vector more sensitive towards the extremes and less sensitive in the middle
		// This makes it easier to control slow speeds when using analog sticks
		directionLength = directionLength * directionLength;
		
		// Multiply the normalized direction vector by the modified length
		directionVector = directionVector * directionLength;
	}
	
	// Apply the direction to the CharacterMotor
	motor.inputMoveDirection = transform.rotation * directionVector;
	if( motor.inputMoveDirection == Vector3.zero)
	{
		this.GetComponent("AnimationController").SendMessage("updateState","idle");
	}
	else{
		this.GetComponent("AnimationController").SendMessage("updateState","run");
	}
	motor.inputJump = Input.GetButton("Jump");
	}
}

function SetIsRemotePlayer(flag:boolean){
	isRemotePlayer = flag;
}

// Require a character controller to be attached to the same game object
@script RequireComponent (CharacterMotor)
@script AddComponentMenu ("Character/FPS Input Controller")
