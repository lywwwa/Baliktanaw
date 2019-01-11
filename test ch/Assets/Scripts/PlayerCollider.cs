using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollider : MonoBehaviour {

	//Image m_image;
	//public Sprite m_Sprite;
	//public GameObject guiObject;
	public GameObject guiObject;

	void Start()
	{
		Debug.Log ("hello with player");
		guiObject.SetActive(false);
		//m_image = GetComponent<Image> ();
	}



	void OnTriggerStay(Collider collider){

		if (collider.gameObject.tag == "Player") {
			guiObject.SetActive (true);
			//m_image.sprite = m_Sprite;
			Debug.Log ("Collided with player");
		}
		
	}

	void OnTriggerExit()
	{
		guiObject.SetActive(false);
		Debug.Log ("bye with player");
	}


}
