using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
	public GameObject pauseMenu;
	public Animator anim;

	private bool clicked;
	private bool isOpen;

	private PlayerController playerController;
	// Use this for initialization
	void Start () {
	//	anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Start")) {//change to start button
			//clicked = true;
			if (!isOpen) {
				pauseMenu.SetActive (true);

				anim.SetBool ("isPaused", true);
				isOpen = true;
				//playerController.enabled = false;
				Time.timeScale = 0f;
				Debug.Log ("Menu Open");

			} 
			else {
				pauseMenu.SetActive (false);
				anim.SetBool ("isPaused", false);
				isOpen = false;
				//playerController.enabled = true;
				Time.timeScale = 1f;
				Debug.Log ("Menu Close");
			}
//			

		} 
	}
}
