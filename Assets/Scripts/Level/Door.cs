/*
Copyright (c) 2022, Micah Schuster
All rights reserved.
Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject openDoorPosition;
    public GameObject closeDoorPosition;
    public GameObject doorOpenSound;
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
        if (!isDoorMoving)
        {
            StartCoroutine(openDoor());
            Instantiate(doorOpenSound, this.transform.position, Quaternion.identity);
        }
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
