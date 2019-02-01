using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

    public GameObject lastText;

	// Use this for initialization
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, 0.08f, 0.01f);
		if (lastText.transform.position.y >= 206f)
        {
            //Debug.Log(lastText.transform.position.y);
            //loadScreen.LoadLevel(3);
            // SceneManager.LoadScene(3);
            Debug.Log("Done");
            SceneManager.LoadScene(0);

        }
	}
}


