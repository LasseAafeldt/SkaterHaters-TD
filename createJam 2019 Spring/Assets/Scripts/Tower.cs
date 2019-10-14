using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

[SelectionBase]
public class Tower : MonoBehaviour {

    public TowerBlueprint TB;
    private Transform target;

    [Header("Attributes")]
    private float range;
    private float attacksPerSecond;
    private float fireCountdown = 0f;
    private float dps;

    private bool useZapper;
    [Header("Components")]
    private LightningBoltScript lightningZap;
    private LineRenderer ln;
    private AudioSource audioSource;

    [Header("Unity Setup Fields")]
    //[SerializeField]
    private string enemyTag;

    public GameObject bulletPrefab;
    public Transform firePoint;

    List<GameObject> enemySkaters;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        enemyTag = "Skater";
        range = TB.range;
        attacksPerSecond = TB.attackrate;
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
        try
        {
            audioSource = GetComponent<AudioSource>();
        }
        catch
        {
            Debug.LogError(gameObject.name + " does not have an AudioSource component");
        }
        if (ln != null)
            ln.enabled = false;
        checkTowerType();
        enemySkaters = new List<GameObject>();
    }

    void checkTowerType()
    {
        if (TB.towertype.Equals(TowerBlueprint.TowerType.Zapper))
        {
            useZapper = true;
            try
            {
                lightningZap = GetComponent<LightningBoltScript>();
                lightningZap.ManualMode = true;
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
            fireCountdown = 1f / attacksPerSecond;
        }

        fireCountdown -= Time.deltaTime;
    }

    void shoot()
    {
        float damage = TB.dps / TB.attackrate;
        //Debug.Log(gameObject.name + " is shooting");
        if (useZapper)
        {
            ShootInstaHitZap(damage);
            audioSource.Play();
        }
        else { 
            GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            if(bullet != null)
            {
                bullet.setTarget(target,damage);
            }
            audioSource.Play();
        }
    }

    void ShootInstaHitZap(float intendedDamage)
    {
        //needed to damage enemy
        skaterBehaviour skaterStats = target.GetComponent<skaterBehaviour>();
        //do zapper stuff
        lightningZap.enabled = true;
        lightningZap.StartObject = firePoint;
        //get the point of impact on enemy
        Vector3 enemyDir = target.transform.position - firePoint.position;
        float enemySurfaceDist = Mathf.Clamp(enemyDir.magnitude,
            enemyDir.magnitude - 2, enemyDir.magnitude - target.GetComponent<Renderer>().bounds.size.x);
        Vector3 enemyHitPoint = enemyDir.normalized * enemySurfaceDist;
        //use target.transform for now (need change in lightning script maybe)
        lightningZap.EndObject = target.transform;
        float zapDuration = (1f / TB.attackrate) / 2f;
        lightningZap.Duration = Mathf.Clamp(zapDuration, 0.03f, 0.2f);
        ln.enabled = true;
        lightningZap.Trigger();
        //StartCoroutine(fadeLigtning((1 / TB.attackrate) / 2));
        //instatiate effect at lightningZap.endPosition
        skaterStats.TakeDamage(intendedDamage);
    }

    //Can possibly use OnTriggerEnter to add and remove from the enemies array if we need performance?
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        enemySkaters.Clear();
        foreach (GameObject enemy in enemies)
        {
            if (enemy.layer.Equals(LayerMask.NameToLayer("Skater")))
            {
                enemySkaters.Add(enemy);
            }
        }
        float shortestEnemyDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemySkaters)
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
        //Gizmos.DrawSphere(transform.position, TB.range);
        Gizmos.DrawWireSphere(transform.position, TB.range);
    }

    IEnumerator fadeLigtning(float fadeTime)
    {
        yield return new WaitForSeconds(fadeTime);
        lightningZap.enabled = false;
        ln.enabled = false;
    }
}
