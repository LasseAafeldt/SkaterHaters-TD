using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBlueprintSelector : MonoBehaviour {
    public static TowerBlueprint towerBlueprint;

    public void setTowerBlueprint(TowerBlueprint selectedTower)
    {
        Debug.Log("Setting tower blueprint");
        towerBlueprint = selectedTower;
    }
}
