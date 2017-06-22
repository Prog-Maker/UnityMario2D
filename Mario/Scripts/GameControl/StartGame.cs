using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

    public string sceneName;

    public GameObject loadingScreen;

    public GameObject[] objectsForHide;

    public GameObject[] objectsForShow;

    public GameObject cam;

    //private IEnumerator Start ()
    //{

    //    startingSceneName = startScene.name;

    //    // Set the initial alpha to start off with a black screen.
    //    faderCanvasGroup.alpha = 1f;

    //    // Write the initial starting position to the playerSaveData so it can be loaded by the player when the first scene is loaded.
    //    playerSaveData.Save (PlayerMovement.startingPositionKey, initialStartingPositionName);

    //    // Start the first scene loading and wait for it to finish.
    //    yield return StartCoroutine (LoadSceneAndSetActive (startingSceneName));

    //    // Once the scene is finished loading, start fading in.
    //    StartCoroutine (Fade (0f));
    //}


    public void StartGameNow ()
    {
        StartCoroutine (LoadSceneAndSetActive (sceneName));
    }


    private IEnumerator LoadSceneAndSetActive (string sceneName)
    {
        loadingScreen.SetActive (true);
        foreach (var o in objectsForHide)
        {
            o.SetActive (false);
        }

        
        // Allow the given scene to load over several frames and add it to the already loaded scenes (just the Persistent scene at this point).
        yield return SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Additive);

        cam.SetActive (false);

        loadingScreen.SetActive (false);

        foreach (var o in objectsForShow)
        {
            o.SetActive (true);
        }

        // Find the scene that was most recently loaded (the one at the last index of the loaded scenes).
        Scene newlyLoadedScene = SceneManager.GetSceneAt (SceneManager.sceneCount - 1);

        // Set the newly loaded scene as the active scene (this marks it as the one to be unloaded next).
        SceneManager.SetActiveScene (newlyLoadedScene);
    }
}
