using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Towers")]
public class TowerBlueprint : ScriptableObject {

    public GameObject towerPrefab;
    public new string name;

    public int cost;
}
