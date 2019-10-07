using UnityEngine;
using System.Collections;
using skaters.stats;

public class SkaterStatsOld : MonoBehaviour {

    [SerializeField] private Skater stats;
    [Header("Skater health")]
    public float startingHealth = 100;
    [SerializeField]
    private float currentHealth;

    public float timeForDespawn;
    bool isDead = false;

    void Awake() {
        currentHealth = startingHealth;
    }
    
    public void TakeDamage(float amount) {
        if (isDead)
            return;

        currentHealth -= amount;

        if (currentHealth <= 0) {
            Death();
        }
    }
    
    void Death() {
        isDead = true;
        //Change layer to dead or similar
        gameObject.layer = LayerMask.NameToLayer("Death");
        //Do death animation?

        StartCoroutine(DeSpawnDead(timeForDespawn));
    }

    IEnumerator DeSpawnDead(float timeForRemoval)
    {
        yield return new WaitForSeconds(timeForRemoval);
        gameObject.GetComponent<Collider>().enabled = false;
        //Debug.Log("Should have disabled collider now");
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);//if we don't use a dictionary then use destroy here instead
        Debug.LogWarning("A skater was set inactive instead of detroyed... Remember to create a dictionary");
    }
}