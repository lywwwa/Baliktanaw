using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TridentController : MonoBehaviour
{

    public Animator anim;
    public GameObject hand;
    private GameObject stabObj;
    public Vector3 pickPosition;
    public Vector3 pickRotation;
    // Use this for initialization
    void Update()
    {
        WeaponTrigger();
        //anim = trident.GetComponent<Animator> ();
        //Debug.Log("weapon");
    }


    public void WeaponTrigger()
    {
        ////StabObject ();
        //Debug.Log("weapon1");
        //RaycastHit hit;

        //var fwd = transform.TransformDirection(Vector3.forward);

        //if (Physics.Raycast(transform.position, fwd, out hit, 500))
        //{
        //    Debug.Log(hit.collider.gameObject.tag);

        //    if (hit.collider.gameObject.tag == "Fish")
        //    {

        //        if (Input.GetButton("AButton"))
        //        {
        //            //put hand animation
        //            anim.SetTrigger("isStabbing");
        //            stabObj = hit.collider.gameObject;
        //            stabObj.transform.parent = this.gameObject.transform;
        //            stabObj.transform.localPosition = pickPosition;
        //            stabObj.transform.localEulerAngles = pickRotation;
        //        }
        //    }
        //}
    }

    void OnTriggerStay(Collider col)
    {
        //Debug.Log(col.gameObject.tag);
        if(col.gameObject.tag == "Fish")
        {
            if (Input.GetButton("AButton"))
            {
                //put hand animation
                //anim.SetTrigger("isStabbing");
                stabObj = col.gameObject;
                stabObj.transform.parent = this.gameObject.transform;
                stabObj.transform.localPosition = pickPosition;
                stabObj.transform.localEulerAngles = pickRotation;
            }
        }
    }
}
