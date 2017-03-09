using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadScene : MonoBehaviour, ITriggerable, ISceneUnloadEvents {
    public string sceneName;
    private UnloadSceneController unloadSceneController = new UnloadSceneController();
 
    void OnEnable() {
        unloadSceneController.OnSceneUnloaded += OnSceneUnloaded;
    }

    void OnDisable() {
        unloadSceneController.OnSceneUnloaded -= OnSceneUnloaded;
    }

    // Update is called once per frame
    void Update () {}

    // ITriggerable methods

    public void Activate() {
        unloadSceneController.UnloadScene(sceneName, this);
    }

    public void Deactivate() { }

    // ISceneUnloadEvents methods
    public void OnSceneUnloaded(Scene scene) {
        Debug.Log("UnloadScene: scene " + scene.name+ " unloaded");
    }

}
