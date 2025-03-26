using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapSpawnTest : EditorWindow
{
    [MenuItem("Tools/MapSpawnTest")]
    static void Init()
    {
        MapSpawnTest window = (MapSpawnTest)EditorWindow.GetWindow(typeof(MapSpawnTest));
        window.Show();
    }
    void OnGUI()
    {
        if (GUILayout.Button("MapSpawnTest"))
        {
            MapSpawn();
        }
    }
    void MapSpawn()
    {
        MapManager.Instance.mapSpawner.MapSpawn();
    }
}
