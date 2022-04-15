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

public class SimpleMovement : MonoBehaviour
{
    public float lookSpeed;
    public float walkSpeed;
    public Camera mainCamera;
    private Vector3 moveAmount;
    private Rigidbody rb;

    void Start()
    {
        if (mainCamera == null)
        {
            Debug.LogError("No Main Camera Attached");
        }
        moveAmount = Vector3.zero;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance().isControlLocked())
        {
            rb.velocity = Vector3.zero;
            return;
        }
        moveAmount.x = Input.GetAxisRaw("Horizontal");
        moveAmount.z = Input.GetAxisRaw("Vertical");
        moveAmount.y = 0f;
        float input = 0f;
#if UNITY_EDITOR_OSX
        if (Mathf.Abs(Input.GetAxis("Joy X Mac")) > Mathf.Abs(Input.GetAxis("Mouse X"))) {
            input = Input.GetAxis("Joy X Mac");
        } else {
            input = Input.GetAxis("Mouse X");
        }
#else
        if (Mathf.Abs(Input.GetAxis("Joy X")) > Mathf.Abs(Input.GetAxis("Mouse X"))) {
            input = Input.GetAxis("Joy X");
        } else {
            input = Input.GetAxis("Mouse X");
        }

#endif

        transform.rotation *= Quaternion.Euler(0, input * lookSpeed, 0f);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
        moveAmount = moveAmount.normalized * walkSpeed;
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount * Time.deltaTime));
    }
}
