using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grenade : MonoBehaviour
{

    public float delay = 2f;
    public float force = 800f;
    public float radius = 20f;
    public float empDowntime;

    // Layer Mask for EMP Testing
    public int layerMask = 1 << 9;

    public GameObject explosionEffect;

    float countdown;
    bool hasExploded = false;

    void Start()
    {
        countdown = delay;
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            EnemyAI enemyScript = nearbyObject.GetComponent<EnemyAI>();

            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);

                enemyScript.DisableEnemy();
            }

        }

        Destroy(gameObject);
    }


    /*
    public IEnumerator EMPShutdown() 
    {
        yield return new WaitForSeconds(empDowntime);


    }
    */

}
