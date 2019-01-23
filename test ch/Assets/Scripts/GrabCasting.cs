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

    public GameObject Spear;

    public Vector3 pickPosition;
    public Vector3 pickRotation;

    public Vector3 pickPositionS;
    public Vector3 pickRotationS;
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
        if (TridentController.fishing)
        {
            Destroy(Spear);
            anim.SetBool("isHolding", false);
        }
        if (TridentController.staking)
        {
            anim.SetBool("isHolding", false);
        }

        var fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, 500))
        {
            ////Debug.Log(hit.collider.gameObject.tag);

            //if (hit.collider.gameObject.tag == "Grass")
            //{
            //    if (Input.GetButton("AButton"))
            //    {
            //        hit.transform.SetParent(this.transform);
            //        hit.transform.localPosition = new Vector3(0f, -.672f, 0f);
            //    }
            //}
            if (hit.collider.gameObject.tag == "Weapon")
            {
                if (Input.GetButton("AButton") && Test.fishingActivated)
                {
                    anim.SetBool("isHolding", true);
                    holdObj = hit.collider.gameObject;
                    Debug.Log("Collided with player-weapon");
                    holdObj.transform.parent = this.gameObject.transform;
                    holdObj.transform.localPosition = pickPosition;
                    holdObj.transform.localEulerAngles = pickRotation;
                }
            }
            else if (hit.collider.gameObject.tag == "Stake")
            {
                if (Input.GetButton("AButton") && Test.stakeActivated)
                {
                    anim.SetBool("isHolding", true);
                    holdObj = hit.collider.gameObject;
                    Debug.Log("Collided with player-stake");
                    holdObj.transform.parent = this.gameObject.transform;
                    holdObj.transform.localPosition = pickPositionS;
                    holdObj.transform.localEulerAngles = pickRotationS;
                }
            }

        }
    }

    public void GrabObject()
    {

        anim.SetBool("isGrabbing", Input.GetButton("AButton"));
    }
}
