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

    private GameObject turret;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        if(turret != null)
        {
            Debug.Log("There is already a turret here");
            //can't build here, there is already a turret
            return;
        }
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
}
