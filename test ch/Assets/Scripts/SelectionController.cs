using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectionController : MonoBehaviour {
	
	public EventSystem eventSystem;
	public GameObject selectedObject;

	private bool buttonSelected;
	public GameObject warningPanel;
	public GameObject title;
	public AudioSource titleAudio;

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

		if (Input.GetButton("BButton") || Input.GetKeyDown("space")) {
			Debug.Log ("b button clicked");

			title.SetActive(true);
			warningPanel.SetActive(false);
			titleAudio.Play();
			Debug.Log("b button clicked");
		}
	}

	private void OnDisable()
	{
		buttonSelected = false;
	}

	private void OnSelected()
	{
		
	}

	//public void warningDisable()
	//{
	//	//if (Input.GetButton("BButton") || Input.GetKeyDown("a"))
	//	if (Input.GetKeyDown("a"))
	//	{
	//		title.SetActive(true);
	//		warningPanel.SetActive(false);
	//		titleAudio.Play();
	//		Debug.Log("b button clicked");
	//	}
	//}

}
