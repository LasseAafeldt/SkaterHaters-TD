using System;
using UnityEditor;
using UnityEngine;

public class Node
{
    public Rect nodeRect;
    public Rect collectiveRect;
    public string title;
    public bool isDragged;
    public bool isSelected;

    public ConnectionPoint inPoint;
    public ConnectionPoint outPoint;

    public GUIStyle style;
    public GUIStyle defaultNodeStyle;
    public GUIStyle selectedNodeStyle;

    public Action<Node> OnRemoveNode;

    public DrawableInfo myInfo;

    private string colorBegin;
    private string colorEnd;

    public Node(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode, DrawableInfo info)
    {
        nodeRect = new Rect(position.x, position.y, width, height);
        style = nodeStyle;
        inPoint = new ConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint);
        outPoint = new ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);
        defaultNodeStyle = nodeStyle;
        selectedNodeStyle = selectedStyle;
        OnRemoveNode = OnClickRemoveNode;
        myInfo = info;
        myInfo.style = style;
        collectiveRect = new Rect(nodeRect.position.x, nodeRect.position.y, nodeRect.size.x, 
            nodeRect.size.y + myInfo.GetHeight() + (nodeRect.size.y / 2f));
        colorBegin = "<color=#FF8000><b>";
        colorEnd = "</b></color>";
    }

    public void Drag(Vector2 delta)
    {
        nodeRect.position += delta;
    }

    public void Draw()
    {
        inPoint.Draw();
        outPoint.Draw();

        GUI.BeginGroup(new Rect(nodeRect.position.x + 5f, nodeRect.position.y + nodeRect.size.y / 2f, nodeRect.size.x - 10f, nodeRect.size.y + myInfo.GetHeight()), style);
        myInfo.Draw(collectiveRect, style);
        GUI.EndGroup();

        style.alignment = TextAnchor.MiddleCenter;
        style.richText = true;
        string titelText = colorBegin + myInfo.title + colorEnd;
        GUI.Box(nodeRect, titelText, style);
        style.alignment = TextAnchor.UpperLeft;
    }

    public bool ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    if (nodeRect.Contains(e.mousePosition))
                    {
                        isDragged = true;
                        GUI.changed = true;
                        isSelected = true;
                        style = selectedNodeStyle;
                    }
                    else
                    {
                        GUI.changed = true;
                        isSelected = false;
                        style = defaultNodeStyle;
                    }
                }

                if (e.button == 1 && isSelected && nodeRect.Contains(e.mousePosition))
                {
                    ProcessContextMenu();
                    e.Use();
                }
                break;

            case EventType.MouseUp:
                isDragged = false;
                break;

            case EventType.MouseDrag:
                if (e.button == 0 && isDragged)
                {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
                break;
        }

        return false;
    }

    private void ProcessContextMenu()
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }

    private void OnClickRemoveNode()
    {
        if (OnRemoveNode != null)
        {
            OnRemoveNode(this);
        }
    }
}