using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectionController : MonoBehaviour {
	
	public EventSystem eventSystem;
	public GameObject selectedObject;

	private bool buttonSelected;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetAxis ("LeftJoystickVertical") != 0 && buttonSelected == false) 
		{
			eventSystem.SetSelectedGameObject(selectedObject);
			buttonSelected = true;
		}

		if (Input.GetButton("BButton")) {
			Debug.Log ("b button clicked");
		}
	}

	private void OnDisable()
	{
		buttonSelected = false;
	}

	private void OnSelected()
	{
		
	}

}
