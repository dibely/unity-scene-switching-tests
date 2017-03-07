using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneController : MonoBehaviour {
    public float waitTime = 10;
    public string previousScene;
    public string nextScene;
    public bool setSceneActive;
    private bool active = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (active) {
            waitTime -= Time.deltaTime;

            if (waitTime < 0.0f) {
                ChangeScene();
                active = false;
            }
        }
	}

    private void ChangeScene() {

        if(string.IsNullOrEmpty(nextScene)) {
            Debug.LogError("Invalid next scene");
            return;
        }

        Scene scene = SceneManager.GetSceneByName(nextScene);

        if (!scene.isLoaded) {
            StartCoroutine("LoadSceneAndSetActive");
        }
        else {
            Debug.LogWarning("Scene "+nextScene+" is already loaded.");
            if (setSceneActive && (SceneManager.GetActiveScene() != scene)) {
                SceneManager.SetActiveScene(scene);
            }
        }
    }

    IEnumerator LoadSceneAndSetActive() {
        Debug.Log("Loading scene " + nextScene);

        AsyncOperation nextSceneAsyncOperation = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
        nextSceneAsyncOperation.allowSceneActivation = false;

        while (!nextSceneAsyncOperation.isDone) {
            float progress = Mathf.Clamp01(nextSceneAsyncOperation.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            if(nextSceneAsyncOperation.progress >= 0.9f) {
                Debug.Log("Scene "+nextScene+" loaded");
                nextSceneAsyncOperation.allowSceneActivation = true;
            }

            yield return new WaitForSeconds(.1f);
        }

        if (!string.IsNullOrEmpty(previousScene)) {

            // Unload previous scene
            Scene scene = SceneManager.GetSceneByName(previousScene);

            if (scene.isLoaded) {
                Debug.Log("Unloading scene " + previousScene);
                AsyncOperation previousSceneAsyncOperation = SceneManager.UnloadSceneAsync(previousScene);

                while (!previousSceneAsyncOperation.isDone) {
                    Debug.Log("Waiting for scene " + previousScene + " to unload");
                    yield return new WaitForSeconds(.1f);
                }

                Debug.Log("Scene " + previousScene + " unloaded");

            }
            else {
                Debug.Log("Previous scene " + previousScene + " is not loaded yet");
            }
        }

        if (setSceneActive) {
            Debug.Log("Setting scene " + nextScene + " active");
            Scene theNextScene = SceneManager.GetSceneByName(nextScene);
            SceneManager.SetActiveScene(theNextScene);
        }

    }
}
