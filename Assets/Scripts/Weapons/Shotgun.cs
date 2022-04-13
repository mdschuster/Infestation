using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{

    public float timeBetweenShots;
    private float time;

    private Animator animator;
    public LayerMask layerMask;
    public ParticleSystem shootEffect;
    public ParticleSystem wallHitEffect;

    private RaycastHit rayHit;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        time-=Time.deltaTime;
        if(time<=0 && Input.GetButton("Fire1")) {
            time = timeBetweenShots;
            animator.SetTrigger("Shoot");
            shootEffect.Play();
            //animator.SetBool("Shoot", false);
            bool hit = Physics.Raycast(transform.position, transform.forward,out rayHit,100f,layerMask);
            if (hit) {
                if (rayHit.collider.tag == "Wall") {
                    Quaternion rotation = Quaternion.LookRotation(rayHit.normal, Vector3.up);
                    Instantiate(wallHitEffect, rayHit.point, rotation);
                }
                if (rayHit.collider.tag == "Enemy")
                {
                    rayHit.collider.gameObject.GetComponent<Enemy>().registerHit(rayHit.point);
                    //do enemy hit stuff
                }
            }

        }

    }
}
