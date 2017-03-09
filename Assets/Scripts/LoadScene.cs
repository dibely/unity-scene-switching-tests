using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour, ITriggerable, ISceneLoadEvents {
    private LoadSceneController loadSceneController = new LoadSceneController();
    public string sceneName;
    public bool setSceneActive = false;

    private void OnEnable() {
        loadSceneController.OnSceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        loadSceneController.OnSceneLoaded -= OnSceneLoaded;
    }

    // Update is called once per frame
    void Update () {}


    // ITriggerable methods

    public void Activate() {
        loadSceneController.LoadScene(sceneName, setSceneActive, this);
    }

    public void Deactivate() { }

    // ISceneLoadEvents methods

    public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) {
        Debug.Log("LoadScene: scene " + scene.name + " loaded in mode "+sceneMode);
    }
}
