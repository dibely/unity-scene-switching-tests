using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Events;

// Interface for scene unload events
public interface ISceneUnloadEvents : IEventSystemHandler {
    void OnSceneUnloaded(Scene scene);
}

public class UnloadSceneController {
    // Event handler for when the scene is unloaded. Matches the signature of the SceneManager sceneUnloaded event delegate.
    public event UnityAction<Scene> OnSceneUnloaded;


    // Entry point for unloading a scene
    // sceneName The name of the scene to unload
    // behaviour The behaviour to use for starting a coroutine
    public void UnloadScene(string sceneName, MonoBehaviour behaviour) {
        if (string.IsNullOrEmpty(sceneName)) {
            Debug.LogError("Invalid scene name specified");
            return;
        }

        Scene scene = SceneManager.GetSceneByName(sceneName);

        if (scene.isLoaded) { 
            behaviour.StartCoroutine(DoUnloadScene(scene));
        }
        else {
            Debug.LogWarning("Scene " + sceneName + " is not loaded yet when trying to unload");
        }
    }

    // Async operation handler for unloading a scene
    // scene The scene to unload
    IEnumerator DoUnloadScene(Scene scene) {
        Debug.Log("Unloading scene " + scene.name);

        // Unload scene
        AsyncOperation sceneAsyncOperation = SceneManager.UnloadSceneAsync(scene);

        while (!sceneAsyncOperation.isDone) {
            Debug.Log("Waiting for scene " + scene.name + " to unload");
            yield return new WaitForSeconds(.1f);
        }

        Debug.Log("Scene " + scene.name + " unloaded");
        if(OnSceneUnloaded != null) {
            OnSceneUnloaded(scene);
        }
    }
}
