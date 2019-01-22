using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class LoadScreen : MonoBehaviour {

	private bool loadScene = false;

	
	public int scene;
	[SerializeField]
	private Text loadingText;

    public GameObject loadingPanel;
    public GameObject testPanel;
    public VideoPlayer vPLayer;
   
	
	[SerializeField]
	private float currentAmount;
	[SerializeField]
	private float speed;

    public Animator animator;

    public void Start()
    {
        loadingPanel.GetComponent<GameObject>();
        Debug.Log("loadscreen");
        loadingPanel.SetActive(false);

        vPLayer.Prepare();
    }

    void Update()
    {
       // LoadLevel();
    }


    public void LoadLevel(){
        //scene = sceneindex;
        // If the player has pressed the space bar and a new scene is not loading yet...
        Debug.Log("LoadLevel");
        FadeToLevel();
        testPanel.SetActive(false);
        
			// ...set the loadScene boolean to true to prevent loading a new scene more than once...
		loadScene = true;
        loadingPanel.SetActive(true);

        //play video 
        loadingText.text = "Maghintay ng ilang minuto...";
        vPLayer.Play();
        // ...change the instruction text to read "Loading..."
        
        // ...and start a coroutine that will load the desired scene.

		

		// If the new scene has started loading...
		if (loadScene == true) {
            
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));


            // ...then pulse the transparency of the loading text to let the player know that the computer is still working.


        }

    }


    public void FadeToLevel()
    {
        Debug.Log("Fadetoleel");
        //LevelToLoad = toLevel;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {

        StartCoroutine(LoadNewScene());
    }

    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
     IEnumerator LoadNewScene() {

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
