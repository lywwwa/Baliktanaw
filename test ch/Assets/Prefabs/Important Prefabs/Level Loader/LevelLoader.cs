using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//FOR SCENE TO SCENE 
public class LevelLoader : MonoBehaviour {

	public GameObject guiObject;
	public string LevelToLoad;

	public Animator animator;
	void Start()
	{
		guiObject.SetActive(false);
	}



	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			guiObject.SetActive(true);
			if (guiObject.activeInHierarchy == true && (Input.GetButton("XButton")))
			{
				FadeToLevel();
				//SceneManager.LoadScene(ToLevel);
			}
		}
	}

	public void FadeToLevel(){

		//LevelToLoad = toLevel;
		animator.SetTrigger ("FadeOut");
	}

	public void OnFadeComplete(){
	
		SceneManager.LoadScene(LevelToLoad);
	}


	void OnTriggerExit()
	{
		guiObject.SetActive(false);
	}

}
