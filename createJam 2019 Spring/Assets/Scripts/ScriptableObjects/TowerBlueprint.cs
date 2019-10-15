using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Towers")]
public class TowerBlueprint : ScriptableObject {

    public enum TowerType
    {
        Standard,
        Zapper,
        Cannon
    }

    [Header("Reference attributs")]
    public GameObject towerPrefab;
    public new string name;
    public TowerType towertype;

    [Header("Attack attributes")]
    public float range;
    public float dps;
    public float attackrate;

    [Header("Other attributes")]
    public int cost;

    public List<TowerBlueprint> upgradesTo = new List<TowerBlueprint>();
    public List<TowerBlueprint> upgradesFrom = new List<TowerBlueprint>();

    //if zapper have a fade time
}
