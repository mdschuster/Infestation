using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject openDoorPosition;
    public GameObject closeDoorPosition;
    public float timeOpen;
    public float doorSpeed;
    public float tol;
    private float time;
    private bool isDoorMoving;

     
    // Start is called before the first frame update
    void Start()
    {
        isDoorMoving = false;
    }

    public void onDoorTriggered()
    {
        if(!isDoorMoving)
            StartCoroutine(openDoor());
    }

    private IEnumerator openDoor()
    {
        isDoorMoving = true;
        while ((this.transform.position - openDoorPosition.transform.position).magnitude > tol)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, openDoorPosition.transform.position, doorSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(timeOpen);
        while ((this.transform.position - closeDoorPosition.transform.position).magnitude > tol)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, closeDoorPosition.transform.position, doorSpeed * Time.deltaTime);
            yield return null;
        }
        isDoorMoving = false;
    }

    //private void openDoor()
    //{
    //    this.transform.position = Vector3.MoveTowards(closeDoorPosition.transform.position, openDoorPosition.transform.position, doorSpeed * Time.deltaTime);
    //    if ((this.transform.position - openDoorPosition.transform.position).magnitude <= 0.01f)
    //    {
    //        isDoorOpening = false;
    //    }
    //}
    //private void closeDoor()
    //{
    //    this.transform.position = Vector3.MoveTowards(openDoorPosition.transform.position, closeDoorPosition.transform.position, doorSpeed * Time.deltaTime);
    //}



}
