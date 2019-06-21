using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(World))]
public class EditorHelper : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        World worldScript = (World)target;
        if(GUILayout.Button("Flip World"))
        {
            worldScript.FlipWorld();
        }
    }
}
