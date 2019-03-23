using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShop : MonoBehaviour {

    TowerBlueprintContainer blueprintContainer;

    private void Start()
    {
        blueprintContainer = GetComponent<TowerBlueprintContainer>();
    }

    public void purchaseTower()
    {
        //if enough currency to cover the cost
        if(PlayerStats.currentCurrency < blueprintContainer.getTowerCost())
        {
            Debug.Log("player cant afford the tower");
            //inform the player thay cant afford tower
            return;
        }
        blueprintContainer.setTowerBlueprint();
        Debug.Log("Player have enough money to buy tower, it has now been selected");
        //subtract the cost from currency only when tower is placed
    }
}
