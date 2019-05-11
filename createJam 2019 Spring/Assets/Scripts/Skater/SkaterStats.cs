using UnityEngine;

public class SkaterStats : MonoBehaviour {

    public float startingHealth = 100;
    [SerializeField]
    private float currentHealth;

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
        //Do death animation?
    }
}