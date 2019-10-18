using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Towers")]
public class TowerBlueprint : ScriptableObject {

    public enum TowerType
    {
        Standard,
        Zapper,
        Cannon
    }

    [Header("Reference attributs")]
    public GameObject towerPrefab;
    public new string name;
    public TowerType towertype;

    [Header("Attack attributes")]
    public float range;
    private float minRange = 0.1f;
    private float maxRange = 20f;
    public float dps;
    private float maxDPS = 3000f;
    public float attackrate;

    [Header("Other attributes")]
    public int cost;
    //if zapper have a fade time

    public List<TowerBlueprint> upgradesTo = new List<TowerBlueprint>();
    public List<TowerBlueprint> upgradesFrom = new List<TowerBlueprint>();

    [SerializeField][HideInInspector]
    private Vector2 editorPosition;

    public float getMinRange()
    {
        return minRange;
    }
    public float getMaxRange()
    {
        return maxRange;
    }
    public float getMaxDPS()
    {
        return maxDPS;
    }

    public Vector2 getEditorPosition()
    {
        return editorPosition;
    }
    public void setEditorPosition(Vector2 editorPos)
    {
        this.editorPosition = editorPos;
    }

}
