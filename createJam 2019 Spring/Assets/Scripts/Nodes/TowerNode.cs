using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerNode : DrawableInfo
{
    private TowerBlueprint towerBlueprint;

    public TowerNode()
    {
        title = "New tower";
        height = 300f;
    }

    public override float GetHeight()
    {
        return height;
    }

    public override void Draw(Rect rect, GUIStyle style)
    {
        Rect baseRect = new Rect(10f, 30f, rect.size.x - 30f, 30f);

        EditorGUI.LabelField(baseRect, "Tower");

        float marginLeft = 5f;
        float marginRight = marginLeft + 0f;

        GUI.Label(new Rect(baseRect.position.x + marginLeft, baseRect.position.y + 30f, baseRect.size.x - marginRight, baseRect.size.y / 2f), "Title");
        title = EditorGUI.TextField(new Rect(baseRect.position.x + marginLeft, baseRect.position.y + 45f, baseRect.size.x - marginRight, baseRect.size.y / 2f), title);

    }

    public TowerBlueprint GetTower()
    {
        TowerBlueprint tower = ScriptableObject.CreateInstance<TowerBlueprint>();
        tower.name = title;
        //tower.minimumStats = minimumStats;
        //weapon.requirementsToMeet = requirements.statsToMeet;
        return tower;
    }
}
