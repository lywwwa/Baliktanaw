﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCont : MonoBehaviour {
	public float speed = 3.0f;
	public float runSpeed = 7.0f; 
	public float jumpSpeed = 5.0f;
	public float gravity = 15.0f;

	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;

	public float XPosition;
	public float YPosition;//normally 5
	public float ZPosition;
	public float YPositionCam;

	public Transform vrCam;
	public Transform Cap;

	void Start()
	{
		controller = GetComponent<CharacterController>();
		Cap = this.gameObject.transform;
		LookRotation (Cap.transform,vrCam.transform);
		// let the gameObject fall down
		gameObject.transform.position = new Vector3(XPosition, YPosition, ZPosition);
	}

	void Update()
	{
		PlayerMovement ();
	}

	public void PlayerMovement()
	{
		if (controller.isGrounded)
		{
			// We are grounded, so recalculate
			// move direction directly from axes

			moveDirection = new Vector3(Input.GetAxis("LeftJoystickHorizontal"), 0.0f, Input.GetAxis("RightJoystickVertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection = moveDirection * speed;

			if (Input.GetButton ("Fire1")|| Input.GetButton("XButton")) {

				moveDirection = moveDirection *runSpeed;
			} 


			if (Input.GetButton("Jump")||Input.GetButton("YButton"))
			{
				moveDirection.y = jumpSpeed;
			}
		}

		// Apply gravity
		moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

		// Move the controller
		controller.Move(moveDirection * Time.deltaTime);
	
	}

	public void LookRotation(Transform character, Transform camera){

		//change the view x/y to joystick right
		float yRot = Input.GetAxis ("RightJoystickVertical") *Time.deltaTime*3.0f ;
		float xRot = Input.GetAxis ("RightJoystickHorizontal") *Time.deltaTime*3.0f ;

		//Camera.main.transform.localRotation *= Quaternion.Euler (-xRot, 0f, 0f);
		camera.rotation*= Quaternion.Euler(0f,yRot,0f);
		character.rotation = Quaternion.Euler (0f,camera.eulerAngles.y, 0f);
		camera.position = new Vector3(character.position.x,YPositionCam,character.position.z);
		//camera.position=character.position;

	}

}
