﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToStartMenu(){
		SceneManager.LoadScene("StartMenu");
	}

    public void ToCreditScene() {
        SceneManager.LoadScene("Credit");
    }
}
