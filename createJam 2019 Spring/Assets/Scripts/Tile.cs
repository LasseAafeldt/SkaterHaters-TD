using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public bool hasTower = false;
    public bool canRecieveTower = true;
    public bool enemyCanPass = true;

    public Color highLightColor;
    private Color startColor;
    private Renderer rend;

    private TowerBlueprint towerBlueprint;
    private GameObject tower;
    private float offset;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        offset = rend.bounds.size.y / 2f;
    }

    private void OnMouseDown()
    {
        if(tower != null)
        {
            Debug.Log("There is already a turret here");
            //can't build here, there is already a turret
            return;
        }

        buildTower();
        //build turret
    }

    #region Hover animation
    private void OnMouseEnter()
    {
        rend.material.color = highLightColor;
        Debug.Log("I am changing color");
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
    #endregion

    void buildTower()
    {
        //set the tower of the tile to the selected tower blueprint
        towerBlueprint = TowerBlueprintSelector.towerBlueprint;
        tower = towerBlueprint.towerPrefab;
        //instatiate tower prefab
        GameObject towerOnTile = Instantiate(tower, 
            new Vector3(transform.position.x, transform.position.y + offset, transform.position.z), 
            Quaternion.identity);
        PlayerStats.currentCurrency -= towerBlueprint.cost;
        Debug.Log("Subtracting cost of tower from current currency");
    }
}
