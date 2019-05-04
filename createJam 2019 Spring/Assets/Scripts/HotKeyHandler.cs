using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotKeyHandler : MonoBehaviour {

    private TowerBlueprintSelector tbSelector;
	void Start () {
        tbSelector = FindObjectOfType<TowerBlueprintSelector>();
        if (tbSelector == null)
            Debug.LogError("No TbSelector was found for the HotKey handler");
	}
	
	void Update () {
        if (Input.GetButtonDown("Fire2"))// right Click
        {
            tbSelector.deselectTowerBluerpint();
        }
	}
}
