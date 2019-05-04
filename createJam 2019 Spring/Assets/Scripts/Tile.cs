using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour {

    public bool hasTower = false;
    public bool canRecieveTower = true;
    public bool enemyCanPass = true;

    public Color highLightColor;
    private Color startColor;
    private Renderer rend;

    private TowerBlueprint towerBlueprint;
    private static TowerBlueprint selectedToBuildTowerBlueprint;
    private GameObject tower;
    private float offset;
    private TowerBlueprintSelector tbSelector;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        offset = rend.bounds.size.y / 2f;
        tbSelector = FindObjectOfType<TowerBlueprintSelector>();
        //towerBlueprint = null;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            //pointer is over an evensystem component (a UI element)
            return;
        }

        if (tower != null)
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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            //pointer is over an evensystem component (a UI element)
            return;
        }


        //if no tower has been selected to be build yet then return
        if (TowerBlueprintSelector.towerBlueprintToBuild == null)
            return;

        rend.material.color = highLightColor;
        //Debug.Log("I am changing color");
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
    #endregion

    void buildTower()
    {
        //set the tower of the tile to the selected tower blueprint
        towerBlueprint = TowerBlueprintSelector.towerBlueprintToBuild;
        //Debug.Log("tower blueprint = " + towerBlueprint);
        if(towerBlueprint == null)
        {
            //show no tower selected message maybe?
            Debug.Log("No tower has been selected in the build menu yet");
            return;
        }
        //tower = towerBlueprint.towerPrefab;
        //instatiate tower prefab
        tower = Instantiate(towerBlueprint.towerPrefab, 
            new Vector3(transform.position.x, transform.position.y + offset, transform.position.z), 
            Quaternion.identity);
        PlayerStats.currentCurrency -= towerBlueprint.cost;
        Debug.Log("Player bought "+ towerBlueprint.name +" for " + towerBlueprint.cost+ " gold! Player has "
            + PlayerStats.currentCurrency + " gold remaining.");
        if(PlayerStats.currentCurrency < towerBlueprint.cost)
        {
            //display play has run out of money for this tower
            //deselect towerblueprint
            tbSelector.deselectTowerBluerpint();
        }
    }


}
