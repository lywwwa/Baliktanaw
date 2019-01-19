using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class LoadScreen : MonoBehaviour {

	private bool loadScene = false;

	[SerializeField]
	private int scene=2;
	[SerializeField]
	private Text loadingText;

    public GameObject loadingPanel;
    public GameObject testPanel;
   public VideoPlayer vPLayer;
    public Text text;
	
	[SerializeField]
	private float currentAmount;
	[SerializeField]
	private float speed;

    public void Start()
    {
        Debug.Log("loadscreen");
    }
	

		public void LoadLevel(){
        // If the player has pressed the space bar and a new scene is not loading yet...
        Debug.Log("LoadLevel");
             testPanel.SetActive(false);
             
			// ...set the loadScene boolean to true to prevent loading a new scene more than once...
			loadScene = true;
        loadingPanel.SetActive(true);
       
            //play video 

           vPLayer.Play();
        // ...change the instruction text to read "Loading..."
        text.text = "";

        // ...and start a coroutine that will load the desired scene.
        StartCoroutine(LoadNewScene());

		

		// If the new scene has started loading...
		if (loadScene == true) {
            
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));


            // ...then pulse the transparency of the loading text to let the player know that the computer is still working.


        }

    }


	// The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
	public IEnumerator LoadNewScene() {

		// This line waits for 3 seconds before executing the next line in the coroutine.
		// This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
		//yield return new WaitForSeconds(5);

		// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
		//AsyncOperation async = Application.LoadLevelAsync(scene);
		AsyncOperation async = SceneManager.LoadSceneAsync(scene);


		// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
		while (!async.isDone) {
			yield return null;
		}

	}
}
