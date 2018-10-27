using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour {

	public float timecycle=1f;
	public float cycleset;

	void Start(){
	
		cycleset = 0.1f / timecycle * -1;
	}
	// Update is called once per frame
	void Update () {

		transform.Rotate (0,0,cycleset,Space.World);
	}
}
