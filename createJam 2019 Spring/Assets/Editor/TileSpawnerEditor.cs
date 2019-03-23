using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileSpawner))]
public class TileSpawnerEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TileSpawner tile = (TileSpawner)target;
        
        if (GUILayout.Button("Generate Grid"))
        {          
            tile.overwriteGrid();
        }       
    }
}
