using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCont : MonoBehaviour {
	public float speed = 3.0f;
	public float runSpeed = 7.0f; 
	public float jumpSpeed = 5.0f;
	public float gravity = 15.0f;

	private Vector3 moveDirection = Vector3.zero;
    private Vector3 moveRotation = Vector3.zero;
    private CharacterController controller;

    public float XPosition;
	public float YPosition;//normally 5
	public float ZPosition;
	public float YPositionCam;

    private float rotateSpeed = 1.0f;

	public GameObject Body;
    public Transform Player;
    public Transform Cam;


   

    void Start()
	{
		controller = GetComponent<CharacterController>();
        Cam = this.gameObject.transform;

        // let the gameObject fall down
        // gameObject.transform.position = new Vector3(0, 5, 0);
      
        gameObject.transform.position = new Vector3(XPosition, YPosition, ZPosition);
       // Debug.Log(XPosition+","+YPosition+","+ZPosition);
    }

	void Update()
	{
        //LookRotation(Cam.transform);
        PlayerMovement();
        if(Time.timeScale == 1)
            if(Input.GetAxis("RightJoystickHorizontal")==1 || Input.GetAxis("RightJoystickHorizontal") == -1)
            {
                float turn = Input.GetAxis("RightJoystickHorizontal") * rotateSpeed;
                Player.transform.Rotate(0.0f, turn, 0.0f);
            }

    }

	public void PlayerMovement()
	{
        if (controller.isGrounded)
		{
			// We are grounded, so recalculate
			// move direction directly from axes

			moveDirection = new Vector3(Input.GetAxis("LeftJoystickHorizontal"), 0.0f, Input.GetAxis("LeftJoystickVertical"));
            //Debug.Log(""+moveDirection.x+","+moveDirection.z);
            moveDirection = transform.TransformDirection(moveDirection);
			moveDirection = moveDirection * speed;

			if (Input.GetButton("Trigger")) {

				moveDirection = moveDirection *runSpeed;
			} 


			if (Input.GetButton("R1"))
			{
				moveDirection.y = jumpSpeed;
			}
		}

		// Apply gravity
		moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

		// Move the controller
		controller.Move(moveDirection * Time.deltaTime);
	
	}

	

   
    
}
