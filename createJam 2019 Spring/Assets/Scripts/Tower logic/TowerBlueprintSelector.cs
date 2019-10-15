using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBlueprintSelector : MonoBehaviour {
    public static TowerBlueprint towerBlueprintToBuild;

    public void setTowerBlueprintToBuild(TowerBlueprint selectedTower)
    {
        Debug.Log("Setting tower blueprint");
        towerBlueprintToBuild = selectedTower;
    }

    public void deselectTowerBluerpint()
    {
        towerBlueprintToBuild = null;
    }
}
