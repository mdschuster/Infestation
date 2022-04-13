using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Death Information")]
    public GameObject bloodSplatter;
    public AudioClip[] deathSounds;

    [Header("Projectile")]
    public GameObject projectile;
    public GameObject fireOrigin;
    public float maxTimeBetweenAttack;
    public float minTimeBetweenAttack;
    private float time;

    [Header("Target")]
    public GameObject target;


    // Start is called before the first frame update
    void Start()
    {
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
            time = Random.Range(minTimeBetweenAttack, maxTimeBetweenAttack);

        }
    }

    public void registerHit(Vector3 hitPoint)
    {
        //play blood splatter
        Instantiate(bloodSplatter, hitPoint, Quaternion.identity);

        //play die animation

        //stay in death pose

        Destroy(this.gameObject);
    }
}
