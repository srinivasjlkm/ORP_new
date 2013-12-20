using UnityEngine;
using System.Collections;

public class ThirdPersonNetworkVik : Photon.MonoBehaviour
{
    MouseCamera cameraScript;



    void Awake()
    {
		cameraScript = GetComponent<MouseCamera>();

       // controllerScript = GetComponent<FPSInputController>();

    }
    void Start()
    {
        //TODO: Bugfix to allow .isMine and .owner from AWAKE!
		if (photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts
            cameraScript.enabled = true;

			gameObject.GetComponent<CharacterMotor>().enabled = true;
			gameObject.GetComponent<FPSInputController>().enabled = true;
           // controllerScript.enabled = true;


            Camera.main.transform.parent = transform;
			Camera.main.transform.localPosition = new Vector3(-0.04869021f,1.303013f, 0.08047496f);
			Camera.main.transform.localEulerAngles = new Vector3(0.6651921f, 0, 0);
			
        }
        else
        {           
            cameraScript.enabled = false;
			gameObject.GetComponent<CharacterMotor>().enabled = false;
			gameObject.GetComponent<FPSInputController>().enabled = false;
          //  controllerScript.enabled = true;

        }

		gameObject.GetComponent<CharacterMotor>().SendMessage("SetIsRemotePlayer", !photonView.isMine);
		gameObject.GetComponent<FPSInputController>().SendMessage("SetIsRemotePlayer", !photonView.isMine);


    

        gameObject.name = gameObject.name + photonView.viewID;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //We own this player: send the others our data
           // stream.SendNext((int)controllerScript._characterState);
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);


			switch(this.GetComponent<AnimationController>().state)
			{
			case AnimationController.CharacterState.idle:
				stream.SendNext("idle");
				break;
			case AnimationController.CharacterState.run:
				stream.SendNext("run");
				break;
			case AnimationController.CharacterState.tap:
				stream.SendNext("tap");
				break;
					
			}

			
		}
		else
        {
            //Network player, receive data
            //controllerScript._characterState = (CharacterState)(int)stream.ReceiveNext();
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
    //        rigidbody.velocity = (Vector3)stream.ReceiveNext();
			correctState = (string)stream.ReceiveNext();
        }
    }

    private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
    private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this
	private string correctState = "idle";
    void Update()
    {
        if (!photonView.isMine)
        {
            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 5);


			transform.GetComponent<AnimationController>().SendMessage("updateState",correctState);

//			print ("correctPlayerPos : "+correctPlayerPos);
		//	print ("transform.position: "+transform.position);
        }

    }
}

//    void OnPhotonInstantiate(PhotonMessageInfo info)
//    {
//        //We know there should be instantiation data..get our bools from this PhotonView!
//        object[] objs = photonView.instantiationData; //The instantiate data..
//        bool[] mybools = (bool[])objs[0];   //Our bools!
//
//        //disable the axe and shield meshrenderers based on the instantiate data
//        MeshRenderer[] rens = GetComponentsInChildren<MeshRenderer>();
//        rens[0].enabled = mybools[0];//Axe
//        rens[1].enabled = mybools[1];//Shield
//
//    }
//