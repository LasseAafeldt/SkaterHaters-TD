using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBlueprintContainer : MonoBehaviour {

    //public TowerBlueprint[] towerBluprints;
    //public List<TowerBlueprint> towerBlueprints = new List<TowerBlueprint>();
    public TowerBlueprint tower;


    public void setTowerBlueprint()
    {
        TowerBlueprintSelector.towerBlueprint = tower;
    }

    public int getTowerCost()
    {
        return tower.cost;
    }
}
