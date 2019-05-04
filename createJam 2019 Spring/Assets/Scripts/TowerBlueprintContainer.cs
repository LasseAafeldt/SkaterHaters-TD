using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBlueprintContainer : MonoBehaviour {

    //public TowerBlueprint[] towerBluprints;
    //public List<TowerBlueprint> towerBlueprints = new List<TowerBlueprint>();
    public TowerBlueprint towerBluprint;


    public void setTowerBlueprint()
    {
        TowerBlueprintSelector.towerBlueprintToBuild = towerBluprint;
    }

    //used in shop
    public int getTowerCost()
    {
        return towerBluprint.cost;
    }
}
