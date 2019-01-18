using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {

	public GameObject title;

	//SpriteRenderer rend;

	void Start ()
	{
		//rend = GetComponent<SpriteRenderer>();
		//startFading();
		StartCoroutine(cone1UP(10f));
	}

	/*IEnumerator Fade()
	{
		for (float f = 1f; f>= -0.05f; f -= 0.05f)
		{
			Color c = rend.material.color;
			c.a = f;
			rend.material.color = c;
			yield return new WaitForSeconds (0.05f);
		}
	}

	public void startFading()
	{
		StartCoroutine("Fade");
	}*/

	IEnumerator cone1UP(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		//title.a = 0.42f;
		//title.alpha = "0";
		//cone1.transform.Translate(0, 400, 0);
	}
}


