using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelLoader : MonoBehaviour {

	public GameObject guiObject;
	public string ToLevel;

	void Start()
	{
		guiObject.SetActive(false);
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			guiObject.SetActive(true);
			if (guiObject.activeInHierarchy == true && Input.GetButton("XButton"))
			{
				SceneManager.LoadScene(ToLevel);
			}
		}
	}

	void OnTriggerExit()
	{
		guiObject.SetActive(false);
	}

}
