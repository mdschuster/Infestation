using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public float maxSpeed;
    private GameObject target;
    private Vector3 moveAmount;
    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.deltaTime);
        rb.rotation = Quaternion.LookRotation(target.transform.position);
    }

    public void setMovement(Vector3 movement)
    {
        moveAmount = movement*maxSpeed;   
    }
    public void setTarget(GameObject target) {
        this.target = target;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        if (collider.gameObject.tag == "Player")
        {
            print("Hit player");
            Destroy(this.gameObject);
        }

    }
}
