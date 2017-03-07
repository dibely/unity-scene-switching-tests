using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Timer : MonoBehaviour, ITriggerable {
    public GameObject target; // Can this be an object of type ITriggerable only?
    public float waitTime = 10.0f;
    private float timer = 0.0f;
    public bool active = false;

    /*
    private void OnValidate() {
        if(target != null) {
            ITriggerable test = (ITriggerable)target.GetComponent(typeof(ITriggerable));
            if (test == null) {
                target = null;
            }
        }
    }
    */

    // Use this for initialization
    void Start () {}

	// Update is called once per frame
	void Update () {
        if (active) {
            timer += Time.deltaTime;

            if (timer >= waitTime) {
                // Perform action
                Debug.Log("Sending activate message to " + target.name);
                ExecuteEvents.Execute<ITriggerable>(target, null, (x, y) => x.Activate());

                Deactivate();
            }
        }
    }

    // ITriggerable methods

    public void Activate() {
        active = true;
    }

    public void Deactivate() {
        // Reset
        active = false;
        timer = 0.0f;
    }
}
