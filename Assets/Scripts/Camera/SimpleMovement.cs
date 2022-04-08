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
        moveAmount.x = Input.GetAxisRaw("Horizontal");
        moveAmount.z = Input.GetAxisRaw("Vertical");
        moveAmount.y = 0f;
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0f);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
        moveAmount = moveAmount.normalized * walkSpeed;
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount * Time.deltaTime));
    }
}
