using UnityEditor;
using UnityEngine;

public class FixXPositionEditor : EditorWindow
{
    public float moveXAmount;
    public float moveZAmount;

    [MenuItem("Tools/Fix Object Position")]
    static void Init()
    {
        FixXPositionEditor window = (FixXPositionEditor)EditorWindow.GetWindow(typeof(FixXPositionEditor));
        window.Show();
    }

    void OnGUI()
    {
        // GUI로 X 값을 입력받기
        moveXAmount = EditorGUILayout.FloatField("Fixed X Position", moveXAmount);
        moveZAmount = EditorGUILayout.FloatField("Fixed Z Position", moveZAmount);
        if (GUILayout.Button("Fix Object Position"))
        {
            FixPosition();
        }
    }
    void FixPosition()
    {
        if (Selection.activeTransform == null)
        {
            EditorUtility.DisplayDialog("No object selected", "Please select a parent object.", "OK");
            return;
        }

        foreach (Transform child in Selection.activeTransform)
        {
            Vector3 currentPosition = child.position;
            currentPosition.x += moveXAmount;
            currentPosition.z += moveZAmount;
            child.position = currentPosition;  
        }
    }
}