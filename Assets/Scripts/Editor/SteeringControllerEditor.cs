#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SteeringController))]
public class SteeringControllerEditor : Editor
{
    private static Type[] _cachedTypes;

    private static Type[] SteeringTypes => _cachedTypes ??= AppDomain.CurrentDomain
        .GetAssemblies()
        .SelectMany(a => a.GetTypes())
        .Where(t => t.IsSubclassOf(typeof(SteeringBehaviour)) && !t.IsAbstract)
        .ToArray();
    public override void OnInspectorGUI() 
    {
        DrawDefaultInspector();
            
        SteeringController ctrl = (SteeringController)target;

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("── Add POCO Behaviour ──", 
            EditorStyles.boldLabel);

        var types = SteeringTypes;

        foreach (var type in types)
        {
            if (GUILayout.Button($"+ Add {type.Name}"))
            {
                    Undo.RecordObject(ctrl, $"Add {type.Name}");
                    ctrl.behaviors.Add(
                        (SteeringBehaviour)Activator.CreateInstance(type));
                    EditorUtility.SetDirty(ctrl);
            }
        }

        if (ctrl.behaviors.Any(b => b == null))
        {
                EditorGUILayout.Space(5);
                if (GUILayout.Button("🗑 Remove Null Behaviours"))
                    ctrl.behaviors.RemoveAll(b => b == null);
                
        }
    }
}
#endif