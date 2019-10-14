using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new skater type", menuName = "Skater")]
public class SkaterStats : ScriptableObject
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float deathDespawnTime;


    public float getMaxHealth()
    {
        return maxHealth;
    }

    public float getMoveSpeed()
    {
        return moveSpeed;
    }

    public float getDeathDespawnTime()
    {
        return deathDespawnTime;
    }
}
