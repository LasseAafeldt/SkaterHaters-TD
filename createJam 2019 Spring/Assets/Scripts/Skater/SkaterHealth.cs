using UnityEngine;

public class SkaterHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;

    bool isDead = false;

    void Awake() {
        currentHealth = startingHealth;
    }
    
    public void TakeDamage(int amount, Vector3 hitPoint) {
        if (isDead)
            return;

        currentHealth -= amount;

        if (currentHealth <= 0) {
            Death();
        }
    }
    
    void Death() {
        isDead = true;
    }
}