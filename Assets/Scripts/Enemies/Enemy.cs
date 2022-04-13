using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Death Information")]
    public GameObject bloodSplatter;
    public AudioClip[] deathSounds;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void registerHit(Vector3 hitPoint)
    {
        //play blood splatter
        Instantiate(bloodSplatter, hitPoint, Quaternion.identity);

        //play die animation

        //stay in death pose
    }
}
