using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

    public LoadScreen loadScreen;
    public GameObject lastText;
    public AudioSource intro;

	// Use this for initialization
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, 0.045f, 0);

        if(lastText.transform.position.y >= 99f)
        {
            //Debug.Log(lastText.transform.position.y);
            //loadScreen.LoadLevel(3);
            // SceneManager.LoadScene(3);
            Debug.Log("Done");
            loadScreen.LoadLevel();

        }
        else if (Input.GetButton("XButton"))
        {
            var voice = intro;
            voice.Stop();
            loadScreen.LoadLevel();
        }
	}
}


