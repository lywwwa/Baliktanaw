﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
    public float walkSpeed = 2;
    public float runSpeed = 6;
    public float gravity = -12;
    public float jumpHeight = 1;

    [Range(0, 1)]
    public float airControlPercent;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;
    float velocityY;

    public float speedSmoothTime = 0.1f;
    float speedSmoothvelocity;
    float currentSpeed;
    Collision collision;
    Transform cameraT;
    CharacterController characterController_;

	public GameObject PausePanel;
	public  void Start() {

		cameraT = Camera.main.transform;
		characterController_ = GetComponent<CharacterController>();
	}

	void Update() {
		//input
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		Vector2 inputDir = input.normalized;

		Move(inputDir);
	
		if(Input.GetButton("Jump"))
		{
			Jump();
		}

		if (Input.GetButton ("Start")) {
			Debug.Log ("paused clicked");
			PauseClicked (PausePanel);
		}
	
	}


	void Move(Vector2 inputDir)
	{
		if (inputDir != Vector2.zero)
		{
			float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
		}

		float targetSpeed = runSpeed * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothvelocity, GetModifiedSmoothTime(speedSmoothTime));

		velocityY += Time.deltaTime * gravity;
	Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

		characterController_.Move(velocity * Time.deltaTime);
		currentSpeed = new Vector2(characterController_.velocity.x, characterController_.velocity.z).magnitude;
		if (characterController_.isGrounded) {
			velocityY = 0;
		}
	}

	void Jump()
	{
		if (characterController_.isGrounded)
		{
			float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
			velocityY = jumpVelocity;
		}

	}

	float GetModifiedSmoothTime(float smoothTime)
	{
		if(characterController_.isGrounded)
		{
			return smoothTime;
		}

		if(airControlPercent==0)
		{
			return float.MaxValue;
		}
		return smoothTime / airControlPercent;
	}

	public void PauseClicked(GameObject panel){

		panel.SetActive (!panel.activeSelf);
	}
}

