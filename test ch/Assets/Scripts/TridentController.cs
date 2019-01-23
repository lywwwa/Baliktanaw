using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TridentController : MonoBehaviour
{

    public Animator anim;
    public GameObject hand;
    private GameObject stabObj;

    public GameObject stake;
    public GameObject deadfish;
    public GameObject stakeland;

    public static bool staking = false;
    bool freeHand = false;
    public static bool fishSpear = false;
    public static bool fishing = false;


    Test testScript;

    // Use this for initialization
    void Start() {
      //  deadfish = GetComponent<GameObject>();
        deadfish.SetActive(false);
        stakeland.SetActive(false);
        //stake = gameObject.GetComponent<GameObject>();
    }
    void Update()
    {
        WeaponTrigger();
        //anim = trident.GetComponent<Animator> ();
        //Debug.Log("weapon");
        if (Test.activeSpear)
        {
            deadfish.SetActive(false);
            fishSpear = false;
            fishing = true;
        }
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
        
        if (Input.GetButtonDown("BButton"))
        {
            anim.SetTrigger("isStabbing");
            if (col.gameObject.tag == "Fish")
            {
                Debug.Log("FIsh dead"+col.gameObject.tag);
                deadfish.SetActive(true);
                fishSpear = true;

                //put hand animation
                Destroy(col.gameObject);
               
                //stabObj = col.gameObject;
                //stabObj.transform.parent = this.gameObject.transform;
                //stabObj.transform.localPosition = pickPosition;
                //stabObj.transform.localEulerAngles = pickRotation;
            }
            else if (col.gameObject.tag == "Land")
            {
                Debug.Log("Land" + col.gameObject.tag);
                stakeland.SetActive(true);
                //put hand animation
                Destroy(stake);

                //stabObj = col.gameObject;
                //stabObj.transform.parent = this.gameObject.transform;
                //stabObj.transform.localPosition = pickPosition;
                //stabObj.transform.localEulerAngles = pickRotation;

                staking = true;

                Test.stakeFinish = true;
                Test.q[3] = false;
                Test.questActive = false;

            }
        }
    }
}
