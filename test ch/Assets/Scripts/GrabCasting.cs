using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCasting : MonoBehaviour
{

    Transform playerLocation;
    float xDistance = 3;
    float yDistance = 1;

    private Animator anim;
    private GameObject holdObj;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GrabObject();
        RaycastHit hit;

        var fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, 500))
        {
            Debug.Log(hit.collider.gameObject.tag);

            if (hit.collider.gameObject.tag == "Grass")
            {
                if (Input.GetButton("AButton"))
                {
                    hit.transform.SetParent(this.transform);
                    hit.transform.localPosition = new Vector3(0f, -.672f, 0f);
                }
            }
            else if (hit.collider.gameObject.tag == "Weapon")
            {
                if (Input.GetButton("AButton"))
                {
                    anim.SetBool("isHolding", true);
                    holdObj = hit.collider.gameObject;
                    Debug.Log("Collided with player-weapon");
                    holdObj.transform.parent = this.gameObject.transform;

                    //fix for trident pos and rot
                    holdObj.transform.position = new Vector3(0.1909f, -0.0609f, 0.0517f);
                    holdObj.transform.rotation = Quaternion.Euler(-28.947f, -173.321f,10.885f);
                }
            }

        }
    }

    public void GrabObject()
    {

        anim.SetBool("isGrabbing", Input.GetButton("AButton"));
    }
}
