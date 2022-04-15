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

public class Enemy : MonoBehaviour
{
    [Header("Enemy Information")]
    public float maxSpeed;
    public float radius;
    private Vector3 moveAmount;
    private Rigidbody rb;    

    [Header("Death Information")]
    public GameObject bloodSplatter;
    public GameObject deathSound;

    [Header("Projectile")]
    public GameObject projectile;
    public GameObject fireOrigin;
    public float maxTimeBetweenAttack;
    public float minTimeBetweenAttack;
    private float time;

    [Header("Target")]
    public GameObject target;

    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        time = Random.Range(minTimeBetweenAttack, maxTimeBetweenAttack);
        target = GameManager.Instance().player;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0f)
        {
            Vector3 vec = target.transform.position - fireOrigin.transform.position;
            vec.Normalize();
            GameObject go = Instantiate(projectile, fireOrigin.transform.position, Quaternion.identity);
            go.GetComponent<EnemyProjectile>().setMovement(vec);
            go.GetComponent<EnemyProjectile>().setTarget(target);
            time = Random.Range(minTimeBetweenAttack, maxTimeBetweenAttack);

        }

        moveAmount = target.transform.position - this.transform.position;
        moveAmount.y = 0f;

    }

    private void FixedUpdate()
    {
        Vector3 rotVec = target.transform.position - this.transform.position;
        rotVec.y = 0;
        rb.rotation = Quaternion.LookRotation(rotVec);

        if (moveAmount.magnitude <= radius) {
            animator.SetBool("isMoving", false);
            return;
        }
        animator.SetBool("isMoving", true);
        rb.MovePosition(rb.position + moveAmount.normalized*maxSpeed * Time.deltaTime);

    }

    public void registerHit(Vector3 hitPoint)
    {
        //play blood splatter
        Instantiate(bloodSplatter, hitPoint, Quaternion.identity);

        //play die animation

        //stay in death pose
        Instantiate(deathSound, hitPoint, Quaternion.identity);

        Destroy(this.gameObject);
    }
}
