﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerNode : DrawableInfo
{
    //private TowerBlueprint towerBlueprint;
    //public enum towerType { Standard, Zapper, Cannon }
    public TowerBlueprint.TowerType type;

    //stats
    private float range;
    private float dps;
    private float attackRate;

    private int cost;

    private float spacing;
    private string colorBegin;
    private string colorEnd;

    public TowerNode()
    {
        title = "New tower";
        height = 300f;
        spacing = 35f;
        colorBegin = "<color=#FF8000><b>";
        colorEnd = "</b></color>";
    }

    public override float GetHeight()
    {
        return height;
    }

    public override void Draw(Rect rect, GUIStyle style)
    {
        GUIStyle textStyle = new GUIStyle();
        textStyle.richText = true;

        
        Rect baseRect = new Rect(10f, 30f, rect.size.x - 30f, 30f);
        textStyle.alignment = TextAnchor.MiddleCenter;
        EditorGUI.LabelField(baseRect, colorBegin + "TowerBlueprint" + colorEnd, textStyle);
        textStyle.alignment = TextAnchor.UpperLeft;

        float marginLeft = 5f;
        float marginRight = marginLeft + 0f;

        title = DrawStat("Title", baseRect, marginLeft, marginRight, spacing, title, textStyle);
        type = DrawStat("Tower Type", baseRect, marginLeft, marginRight, 2*spacing, type, textStyle);
        range = DrawStat("Range", baseRect, marginLeft, marginRight, 3*spacing, range, true, GetTower().getMinRange(), GetTower().getMaxRange(), textStyle);
        dps = DrawStat("Damage/Sec", baseRect, marginLeft, marginRight, 4*spacing, dps, true, 0 ,GetTower().getMaxDPS(), textStyle);
        attackRate = DrawStat("Attackrate", baseRect, marginLeft, marginRight, 5 * spacing, attackRate, true, 0.1f, 10f, textStyle);
        cost = DrawStat("Tower Cost", baseRect, marginLeft, marginRight, 6 * spacing, cost, false, 0, 0, textStyle);
    }

    private float DrawStat(string name, Rect rect, float marginLeft, float marginRight, float topOffset, float value, bool isSLider, float min, float max, GUIStyle _style)
    {
        GUI.Label(new Rect(rect.position.x + marginLeft, rect.position.y + topOffset, rect.size.x - marginRight, rect.size.y / 2f), colorBegin+name+colorEnd, _style);
        if (!isSLider)
        {
            return EditorGUI.FloatField(new Rect(rect.position.x + marginLeft, rect.position.y + topOffset + 15f, rect.size.x - marginRight, rect.size.y / 2f), value);
        }
        return EditorGUI.Slider(new Rect(rect.position.x + marginLeft, rect.position.y + topOffset + 10f, rect.size.x - marginRight, rect.size.y / 2f), value, min, max);
    }
    private int DrawStat(string name, Rect rect, float marginLeft, float marginRight, float topOffset, int value, bool isSLider, int min, int max, GUIStyle _style)
    {
        GUI.Label(new Rect(rect.position.x + marginLeft, rect.position.y + topOffset, rect.size.x - marginRight, rect.size.y / 2f), colorBegin + name + colorEnd, _style);
        if (!isSLider)
        {
            return EditorGUI.IntField(new Rect(rect.position.x + marginLeft, rect.position.y + topOffset + 15f, rect.size.x - marginRight, rect.size.y / 2f), value);
        }
        return EditorGUI.IntSlider(new Rect(rect.position.x + marginLeft, rect.position.y + topOffset + 10f, rect.size.x - marginRight, rect.size.y / 2f), value, min, max);
    }
    private string DrawStat(string name, Rect rect, float marginLeft, float marginRight, float topOffset, string text, GUIStyle _style)
    {
        GUI.Label(new Rect(rect.position.x + marginLeft, rect.position.y + topOffset, rect.size.x - marginRight, rect.size.y / 2f), colorBegin+name+colorEnd, _style);
        return EditorGUI.TextField(new Rect(rect.position.x + marginLeft, rect.position.y + topOffset + 15f, rect.size.x - marginRight, rect.size.y / 2f), text);
    }
    private TowerBlueprint.TowerType DrawStat(string name, Rect rect, float marginLeft, float marginRight, float topOffset, TowerBlueprint.TowerType _type, GUIStyle _style)
    {
        GUI.Label(new Rect(rect.position.x + marginLeft, rect.position.y + topOffset, rect.size.x - marginRight, rect.size.y / 2f), colorBegin+name+colorEnd, _style);
        return (TowerBlueprint.TowerType)EditorGUI.EnumPopup(new Rect(rect.position.x + marginLeft, rect.position.y + topOffset + 15f, rect.size.x - marginRight, rect.size.y / 2f), _type);
    }

    public TowerBlueprint GetTower()
    {
        TowerBlueprint tower = ScriptableObject.CreateInstance<TowerBlueprint>();
        tower.name = title;
        //tower prefab??
        tower.towertype = type;
        tower.range = range;
        tower.dps = dps;
        tower.attackrate = attackRate;
        tower.cost = cost;
        //tower.minimumStats = minimumStats;
        //weapon.requirementsToMeet = requirements.statsToMeet;
        return tower;
    }
}
