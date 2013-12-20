using UnityEngine;
using System.Collections;

public class ClickMove : MonoBehaviour
{
		public float smooth; // Determines how quickly object moves towards position
		private Camera myCamera;
		public GameObject arrowPrefab;
		private GameObject arrow;
		
		private Vector3 targetPosition;
		private Vector2 groundPosition;
		public float speed = 0.02f;
		public float heightOffset = 0f;

		// Use this for initialization
		void Start ()
		{
				groundPosition = GameObject.FindWithTag ("ground").transform.position;

				arrow = Instantiate(arrowPrefab,transform.position,Quaternion.identity) as GameObject;
				targetPosition = new Vector3(transform.position.x,transform.position.y + heightOffset,transform.position.z);
				myCamera = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<Camera>();
				
		}

		void arrowAnimation ()
		{



		}



//		void  OnControllerColliderHit ( ControllerColliderHit hit  ){
//			if (hit.normal.y > 0 && hit.normal.y > groundNormal.y && hit.moveDirection.y < 0) {
//				if ((hit.point - movement.lastHitPoint).sqrMagnitude > 0.001f || lastGroundNormal == Vector3.zero)
//					groundNormal = hit.normal;
//				else
//					groundNormal = lastGroundNormal;
//				
//				movingPlatform.hitPlatform = hit.collider.transform;
//				movement.hitPoint = hit.point;
//				movement.frameVelocity = Vector3.zero;
//			}
//		}
		// Update is called once per frame
		void Update ()
		{

				if (Input.GetKeyUp (KeyCode.Mouse0)) {



						smooth = 1;



						Plane playerPlane = new Plane (Vector3.up, groundPosition);

						
						Ray ray = myCamera.ScreenPointToRay (Input.mousePosition);

						float hitdist = 0.0f;



						if (playerPlane.Raycast (ray, out hitdist)) {



								Vector3 targetPoint = ray.GetPoint (hitdist);



								// move the arrow to the click point and spin it, disable it after 2s
								arrow.transform.position = targetPoint;

								arrowAnimation();

								targetPosition = new Vector3( ray.GetPoint (hitdist).x,ray.GetPoint (hitdist).y + heightOffset,ray.GetPoint (hitdist).z);


//								print (targetPosition);

								//Quaternion targetRotation = Quaternion.LookRotation (targetPoint - transform.position);

								//transform.rotation = targetRotation;



						}



				}

				Vector3 dir = targetPosition - transform.position;

				float dist = dir.magnitude;

				float move = speed * Time.deltaTime;

				if (dist > move) {

						transform.position += dir.normalized * move;
					

				} else {

						transform.position = targetPosition;
						
						
				}

				
				transform.position += (targetPosition - transform.position).normalized * speed * Time.deltaTime;
				
		}
}
