using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MessageRelay : MonoBehaviour, ITriggerable {
    public GameObject[] objects;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // ITriggerable methods

    public void Activate() {
        for(int index = 0; index < objects.Length; index++) {
            GameObject obj = objects[index];
            Debug.Log("Sending activate message to " +obj.name);
            ExecuteEvents.Execute<ITriggerable>(obj, null, (x, y) => x.Activate());

        }
    }

    public void Deactivate() {
        for (int index = 0; index < objects.Length; index++) {
            GameObject obj = objects[index];
            Debug.Log("Sending deactivate message to " + obj.name);
            ExecuteEvents.Execute<ITriggerable>(obj, null, (x, y) => x.Deactivate());
        }
    }
}
