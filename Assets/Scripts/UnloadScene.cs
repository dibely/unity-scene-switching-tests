using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadScene : MonoBehaviour, ITriggerable {
    public string sceneName;
 
    // Use this for initialization
    void Start () {}
	
	// Update is called once per frame
	void Update () {}

    // Main entry point for unloading a scene
    private void PerformUnloadScene() {
        if (string.IsNullOrEmpty(sceneName)) {
            Debug.LogError("Invalid scene name specified");
            return;
        }

        Scene scene = SceneManager.GetSceneByName(sceneName);

        if (scene.isLoaded) {
            StartCoroutine(UnloadSceneAsync(scene));
        }
        else {
            Debug.LogWarning("Scene " + sceneName + " is not loaded yet when trying to unload");
        }
    }

    // Unloads a scene asynchronously
    IEnumerator UnloadSceneAsync(Scene scene) {
        Debug.Log("Unloading scene " + scene.name);

        // Unload scene
        AsyncOperation sceneAsyncOperation = SceneManager.UnloadSceneAsync(scene);

        while (!sceneAsyncOperation.isDone) {
            Debug.Log("Waiting for scene " + scene.name + " to unload");
            yield return new WaitForSeconds(.1f);
        }

        Debug.Log("Scene " + sceneName + " unloaded");
    }

    // ITriggerable methods

    public void Activate() {
        PerformUnloadScene();
    }

    public void Deactivate() { }
}
