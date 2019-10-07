using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace skaters.stats
{
    [CreateAssetMenu(fileName = "new skater type", menuName = "Skater")]
    public class Skater : ScriptableObject
    {
        public int maxHealth;
        public float moveSpeed;


        public float deathDespawnTime;

    }
}
