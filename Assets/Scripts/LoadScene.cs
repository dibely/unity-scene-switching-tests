﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour, ITriggerable {
    public string sceneName;
    public bool setSceneActive = false;

    // Use this for initialization
    void Start () {}
	
	// Update is called once per frame
	void Update () {}

    // Entry point for starting a scene load
    private void PerformLoadScene() {
        if (string.IsNullOrEmpty(sceneName)) {
            Debug.LogError("Invalid scene name specified");
            return;
        }

        Scene scene = SceneManager.GetSceneByName(sceneName);

        if (!scene.isLoaded) {
            StartCoroutine("LoadSceneAsyncAndSetActive");
        }
        else {
            Debug.LogWarning("Scene " + sceneName + " is already loaded.");
            if (setSceneActive && (SceneManager.GetActiveScene() != scene)) {
                SetSceneActive();
            }
        }
    }

    // Loads a scene Asynchronously and once complete will set the scene active if necessary
    IEnumerator LoadSceneAsyncAndSetActive() {
        Debug.Log("Loading scene " + sceneName);

        AsyncOperation sceneAsyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        sceneAsyncOperation.allowSceneActivation = false;

        while (!sceneAsyncOperation.isDone) {
            float progress = Mathf.Clamp01(sceneAsyncOperation.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            if (sceneAsyncOperation.progress >= 0.9f) {
                Debug.Log("Scene " + sceneName + " loaded");
                sceneAsyncOperation.allowSceneActivation = true;
            }

            yield return new WaitForSeconds(.1f);
        }

        if (setSceneActive) {
            SetSceneActive();
        }
    }

    // Sets the specified scene active
    private void SetSceneActive() {
        Debug.Log("Setting scene " + sceneName + " active");
        Scene theScene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(theScene);
    }


    // ITriggerable methods

    public void Activate() {
        PerformLoadScene();
    }

    public void Deactivate() { }
}