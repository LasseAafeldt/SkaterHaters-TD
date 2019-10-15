using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShop : MonoBehaviour {

    TowerBlueprintContainer blueprintContainer;

    private void Start()
    {
        blueprintContainer = GetComponent<TowerBlueprintContainer>();
        if (blueprintContainer == null)
            Debug.LogError("A blueprintContainer is missing from the shop");
    }

    public void purchaseTower()
    {
        //if enough currency to cover the cost
        Debug.Log("purchase tower was called");
        if(PlayerStats.currentCurrency < blueprintContainer.getTowerCost())
        {
            Debug.Log("player cant afford the tower");
            //inform the player thay cant afford tower
            return;
        }
        blueprintContainer.setTowerBlueprint();
        Debug.Log("Player has " + PlayerStats.currentCurrency + " money and SELECTED a Tower for " + blueprintContainer.getTowerCost() + " money");
        //Debug.Log("Player have enough money to buy tower, it has now been selected");
        //subtract the cost from currency only when tower is placed
    }
}
