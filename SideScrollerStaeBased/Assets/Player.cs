using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Transform myTransform;

	public float walkSpeed = 2.0f;
	public float jumpForce = 250.0f;
	private float distToGround;
	private float distToWall;
	
	//Facing Direction
	public GameObject playerModel;
	private int facingDirection = 1;
	private Quaternion facingRotation;

	// Use this for initialization
	void Start () {
		myTransform = this.transform;
		
		distToGround = myTransform.GetComponent<Collider>().bounds.extents.y;
		distToWall = myTransform.GetComponent<Collider>().bounds.extents.x;
	}
	
	// Update is called once per frame
	void Update () {
			
		Controls ();
		DirectionFacing ();
	}

	//Fix Player Mesh Transform Rotation
	void DirectionFacing(){
		facingRotation = playerModel.transform.rotation;

		facingRotation.eulerAngles = new Vector3 (facingRotation.eulerAngles.x, 
			90 * facingDirection,
				facingRotation.eulerAngles.z);
		
		playerModel.transform.rotation = facingRotation;
	}

	void Controls(){
	
		 //Move Right
		if (Input.GetKey ("d")) {
			myTransform.Translate(0f, 0f, walkSpeed * Time.deltaTime);
			facingDirection = 1;
		}
		//Move Left
		else if (Input.GetKey ("a")) {
			myTransform.Translate(0f, 0f, walkSpeed * Time.deltaTime);
			facingDirection = -1;
		} 
	
		//Jumping
		if (Input.GetKeyDown ("space") && CheckGrounded() == true) {
			myTransform.GetComponent<Rigidbody>().AddForce (0,jumpForce,0);
		}
	}

	//Raycast down to check if grounded
	public bool CheckGrounded(){
		return Physics.Raycast(myTransform.position, -Vector3.up, distToGround + 0.1f);
	}

}
