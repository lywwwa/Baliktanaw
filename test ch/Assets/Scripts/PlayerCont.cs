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


    private bool grabObj = false;
    private GameObject hitObj;
    private RaycastHit hit;

    bool ladder = false;

    void Start()
	{
		controller = GetComponent<CharacterController>();
        Cam = this.gameObject.transform;

        // let the gameObject fall down
        // gameObject.transform.position = new Vector3(0, 5, 0);
       // LookRotation(Cap.transform);
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

        GrabObject();
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

			if (Input.GetButton("XButton")) {

				moveDirection = moveDirection *runSpeed;
			} 


			if (Input.GetButton("YButton"))
			{
				moveDirection.y = jumpSpeed;
			}
		}

		// Apply gravity
		moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

		// Move the controller
		controller.Move(moveDirection * Time.deltaTime);
	
	}

	public void LookRotation(Transform character){
        ///CHARACTER ROTATE
  //      Debug.Log("rotate");
		////change the view x/y to joystick right
		//float yRot = Input.GetAxis ("RightJoystickVertical") *Time.deltaTime*3.0f ;
		//float xRot = Input.GetAxis ("RightJoystickHorizontal") *Time.deltaTime*3.0f ;

		////Camera.main.transform.localRotation *= Quaternion.Euler (-xRot, 0f, 0f);
		////camera.rotation*= Quaternion.Euler(0f,yRot,0f);
		//character.rotation = Quaternion.Euler (xRot,0F, 0f);
		//camera.position = new Vector3(character.position.x,YPositionCam,character.position.z);
		//camera.position=character.position;

	}

    public void GrabObject()
    {
        //if (hit.collider.gameObject && (Input.GetButton("Fire2")|| Input.GetButton("XButton")) && grabObj == false)
        //{
        //    hitObj = hit.collider.gameObject;
        //    grabObj = true;
        //}
        //else if ((Input.GetButton("Fire2")|| Input.GetButton("XButton")) && grabObj == true)
        //{
        //    grabObj = false;
        //}

        //		if (grabObj) {
        //			hitObj.transform.position.x = gameObject.transform.position.x;
        //			hitObj.transform.position.y = gameObject.transform.position.y;
        //			hitObj.transform.position.z = gameObject.transform.position.z+2;
        //		}
    }
}
