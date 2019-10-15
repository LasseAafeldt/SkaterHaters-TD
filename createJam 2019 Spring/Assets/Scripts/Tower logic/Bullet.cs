using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 70f;
    public GameObject hitEffect;

    private Transform target;
    private float damage;

    public void setTarget(Transform _target, float intendedDamage)
    {
        target = _target;
        damage = intendedDamage;
    }


    private void FixedUpdate()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceToMoveThisFRame = speed * Time.deltaTime;

        if(direction.magnitude < distanceToMoveThisFRame)
        {
            hitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceToMoveThisFRame, Space.World);
    }

    void hitTarget()
    {
        Debug.Log("The bullet hit "+ target.name);
        if(hitEffect != null)
        {
            GameObject effectInstance = (GameObject)Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(effectInstance, 4f);
        }
        Destroy(gameObject);
        skaterBehaviour targetsStats = target.GetComponent<skaterBehaviour>();
        targetsStats.TakeDamage(damage);
    }
}
