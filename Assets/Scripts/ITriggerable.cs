using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ITriggerable : IEventSystemHandler {

    void Activate();

    void Deactivate();
}
