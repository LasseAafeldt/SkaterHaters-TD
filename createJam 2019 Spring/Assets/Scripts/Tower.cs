using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class Tower : MonoBehaviour {

    public TowerBlueprint TB;
    private Transform target;

    [Header("Attributes")]
    private float range;
    private float fireRate;
    private float fireCountdown = 0f;
    private float dps;

    private bool useZapper;
    private LightningBoltScript lightningZap;
    private LineRenderer ln;

    [Header("Unity Setup Fields")]
    //[SerializeField]
    private string enemyTag;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        enemyTag = "Skater";
        range = TB.range;
        fireRate = TB.attackrate;
        fireCountdown = 0f;
        dps = TB.dps;
        try
        {
            ln = GetComponent<LineRenderer>();
        }
        catch
        {
            Debug.LogError(gameObject.name + " does not have a LineRenderer component");
        }
        if (ln != null)
            ln.enabled = false;
        checkTowerType();
    }

    void checkTowerType()
    {
        if (TB.towertype.Equals(TowerBlueprint.TowerType.Zapper))
        {
            useZapper = true;
            try
            {
                lightningZap = GetComponent<LightningBoltScript>();
            }
            catch
            {
                Debug.LogError(gameObject.name + " Does not have a LightningBoltScript component");
            }
        }
    }

    private void Update()
    {
        if(target == null)
        {
            return;
        }

        if(fireCountdown <= 0f)
        {
            shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void shoot()
    {
        Debug.Log(gameObject.name + " is shooting");
        if (useZapper)
        {
            //do zapper stuff
            ln.enabled = true;

        }
        else { 
            GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            if(bullet != null)
            {
                bullet.setTarget(target);
            }
        }
    }

    //Can possibly use OnTriggerEnter to add and remove from the enemies array if we need performance?
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestEnemyDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
            if(distanceToEnemy < shortestEnemyDistance)
            {
                shortestEnemyDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestEnemyDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
