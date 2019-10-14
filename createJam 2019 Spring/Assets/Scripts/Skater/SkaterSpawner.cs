using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkaterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject skaterToSpawn;

    [SerializeField] private Vector3 spawnAreaSize;

    private void Start()
    {
        //spawnSkater(1);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            spawnSkater(1);
        }
    }

    void spawnSkater(int amountToSpawn)
    {
        Vector3 halfAreaSize = spawnAreaSize / 2;
        Vector3 positionToSpawn = new Vector3(Random.Range(-halfAreaSize.x, halfAreaSize.x),
            transform.position.y, Random.Range(-halfAreaSize.z, halfAreaSize.z));
        
        GameObject newSkater = Instantiate(skaterToSpawn, positionToSpawn + transform.position, Quaternion.identity);
        newSkater.gameObject.name = "Test Skater";
        skaterBehaviour behaviour = newSkater.GetComponent<skaterBehaviour>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawWireCube(transform.position, spawnAreaSize);
        Gizmos.DrawCube(transform.position, spawnAreaSize);
    }
}
