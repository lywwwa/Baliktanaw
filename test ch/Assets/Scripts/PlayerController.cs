using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	

	public float speed;

	public bool isGrounded;
	private Rigidbody rb;
	public Vector3 jump;
	private float jumpforce = 3.0f;
	private CharacterController controller;
	public float verticalVelocity;
	public float gravity = 14.0f;


	public GameObject PausePanel;
	// Use this for initialization
	void Start () {
		controller = GetComponent <CharacterController>();
		//rb=GetComponent<Rigidbody>();
		jump = new Vector3 (0.0f, 2.0f, 0.0f);
	}


	void OnCollisionStay(){
		//isGrounded = true;

		if(!isGrounded && controller.velocity.y <= 0) {
			isGrounded = true; 
			//jumpsLeft = 1; 
		}
	}

	// Update is called once per frame
	void Update () {

		playerMove();

		verticalVelocity = -gravity * Time.deltaTime;


		if (Input.GetButton("BButton") && isGrounded) {
				Debug.Log ("b button clicked");
				playerJump ();
			}


		if (Input.GetButton ("Start")) {
			Debug.Log ("paused clicked");
			PauseClicked (PausePanel);
		}
	}


	public void playerMove(){

		float translation = Input.GetAxis ("LeftJoystickVertical") * speed;
		float straffe = Input.GetAxis ("LeftJoystickHorizontal") * speed;
		translation *= Time.deltaTime;
		straffe *= Time.deltaTime;

		transform.Translate (straffe, 0, translation);
	}

	public void playerJump(){

		rb.AddForce (jump * jumpforce, ForceMode.Impulse);
		//verticalVelocity -= gravity * Time.deltaTime;
		isGrounded = false;

	}
	public void PauseClicked(GameObject panel){
	
		panel.SetActive (!panel.activeSelf);
	}
}
