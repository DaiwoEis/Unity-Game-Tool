using System.Reflection;
using UnityEditor;
using UnityEngine;

public class DebuggerWindow : EditorWindow
{
    [MenuItem("Window/Debugger")]
    private static void Init()
    {        
        DebuggerWindow window = (DebuggerWindow)GetWindow(typeof(DebuggerWindow));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Debugger Window", EditorStyles.boldLabel);
        if (!Application.isPlaying)
        {
            GUILayout.Label("enabled window show in play mode");
            return;
        }
        foreach (var fieldInfo in typeof(LogType).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            Debugger.SetState(fieldInfo.Name,
                EditorGUILayout.Toggle(fieldInfo.Name, Debugger.GetState(fieldInfo.Name)));
        }
    }
}
