using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NodeBasedEditorNew : EditorWindow
{
    [MenuItem("Window/Node Based Editor (own)")]
    private static void OpenWindow()
    {
        NodeBasedEditor window = GetWindow<NodeBasedEditor>();
        window.titleContent = new GUIContent("Node Based Editor");
    }

    private void OnGUI()
    {
        DrawNodes();

        ProcessEvents(Event.current);

        if (GUI.changed) Repaint();
    }

    private void DrawNodes()
    {
    }

    private void ProcessEvents(Event e)
    {
    }
}
