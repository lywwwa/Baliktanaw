using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCasting : MonoBehaviour {

    Transform playerLocation;
    float xDistance = 3;
    float yDistance = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;

        var fwd = transform.TransformDirection(Vector3.forward);
        if(Physics.Raycast(transform.position,  fwd, out hit, 500))
        {
            Debug.Log(hit.collider.gameObject.tag);
            
            if(hit.collider.gameObject.tag == "Grass")
            {
                if (Input.GetButton("AButton"))
                {
                    hit.transform.SetParent(this.transform);
                    hit.transform.localPosition = new Vector3(0f, -.672f, 0f);
                }
            }
        }
        

    }
}
