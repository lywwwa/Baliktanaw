﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

	public Transform player;
	
	// Update is called once per frame
	void LateUpdate () {

		Vector3 newPos = player.position;
		newPos.y = transform.position.y;
		transform.position = newPos;

		transform.rotation = Quaternion.Euler (90f,player.eulerAngles.y,0);
	}
}
