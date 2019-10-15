using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerNode : DrawableInfo
{
    private TowerBlueprint towerBlueprint;
    //public enum towerType { Standard, Zapper, Cannon }
    public TowerBlueprint.TowerType type;

    private GUIContent content;

    public TowerNode()
    {
        title = "New tower";
        towerBlueprint = new TowerBlueprint();
        type = towerBlueprint.towertype;
        content = new GUIContent("Tower type");
        height = 300f;
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

        EditorGUI.LabelField(baseRect, "<color=#ffa500ff> Tower </color>", textStyle);

        float marginLeft = 5f;
        float marginRight = marginLeft + 0f;

        Rect defaultRect = new Rect(baseRect.position.x + marginLeft, baseRect.position.y, baseRect.size.x - marginRight, baseRect.size.y / 2f);

        GUI.Label(new Rect(defaultRect.x, defaultRect.y + 30f, defaultRect.width, defaultRect.height), "<color=#ffa500ff> Title </color>", textStyle);
        title = EditorGUI.TextField(new Rect(baseRect.position.x + marginLeft, baseRect.position.y + 45f, baseRect.size.x - marginRight, baseRect.size.y / 2f), title);

        
        GUI.Label(new Rect(defaultRect.x, defaultRect.y + 65f, defaultRect.width, defaultRect.height), "<color=#ffa500ff> Tower Type </color>", textStyle);
        type = (TowerBlueprint.TowerType)EditorGUI.EnumPopup(new Rect(defaultRect.x, defaultRect.y + 85f, defaultRect.width, defaultRect.height), type);
        /*if(EditorGUI.DropdownButton(new Rect(baseRect.position.x + marginLeft, baseRect.position.y + 65f, baseRect.size.x - marginRight, baseRect.size.y / 2f), content, FocusType.Keyboard))
        {
            type = towerBlueprint.towertype;
        }*/
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
