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

public class Shotgun : MonoBehaviour
{

    public float timeBetweenShots;
    private float time;

    private Animator animator;
    public LayerMask layerMask;
    public GameObject shootAudio;
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
    void Update() {
        time -= Time.deltaTime;
        float fireAxis = 0f;
        float fireValue = 0f;
        bool ifFireAxis = false;
#if UNITY_EDITOR_OSX
        fireAxis=Input.GetAxisRaw("Fire1 Mac");
        fireValue = 0.5f;
        ifFireAxis = fireAxis >= fireValue;
#else
        fireAxis=Input.GetAxisRaw("Fire1");
        fireValue=0.5f;
        ifFireAxis=fireAxis>=fireValue;
#endif
        if (time <= 0 && (Input.GetButton("Fire1") || ifFireAxis)) {
            if (GameManager.Instance().isControlLocked()) return;
            time = timeBetweenShots;
            animator.SetTrigger("Shoot");
            shootEffect.Play();
            Instantiate(shootAudio, this.transform.position, Quaternion.identity);
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
