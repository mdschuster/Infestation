using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorTrigger : MonoBehaviour
{

    public UnityEvent doorTriggered;
    private bool canOpenDoor;

    private void Start() {
        canOpenDoor = false;
    }

    private void Update() {
        if (Input.GetButton("Submit") && canOpenDoor) {
            //set door to open
            doorTriggered.Invoke();
        }
    }
    private void OnTriggerEnter(Collider other) {
        canOpenDoor = true;
    }

    private void OnTriggerExit(Collider other) {
        canOpenDoor = false;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        //set door to open
    //        doorTriggered.Invoke();
    //    }
    //}
}
