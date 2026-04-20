#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SteeringController))]
public class SteeringControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SteeringController ctrl = (SteeringController)target;

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("-Add POCO Behavior", EditorStyles.boldLabel);

        var types = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsSubclassOf(typeof(SteeringBehaviour))
                    && !t.IsAbstract);
        foreach (var type in types)
        {
            if(GUILayout.Button($"+ Add {type.Name}"))
            {
                Undo.RecordObject(ctrl, $"Add {type.Name}");

                if (typeof(MonoBehaviour).IsAssignableFrom(type))
                {
                    // 1. Añadimos el componente al GameObject
                    Component newComp = ctrl.gameObject.AddComponent(type);
    
                    // 2. FORZAMOS la conversión pasando por 'object' 
                    // Esto evita el error de "built-in conversion"
                    SteeringBehaviour sb = (SteeringBehaviour)(object)newComp;
    
                    if (sb != null)
                    {
                        ctrl.behaviors.Add(sb);
                    }
                }
                else
                {
                    // Para clases normales POCO
                    var instance = (SteeringBehaviour)Activator.CreateInstance(type);
                    ctrl.behaviors.Add(instance);
                }

                EditorUtility.SetDirty(ctrl);
            }
        }

        // Limpieza de nulos
        if (ctrl.behaviors != null && ctrl.behaviors.Any(b => b == null))
        {
            EditorGUILayout.Space(5);
            if (GUILayout.Button("Remove Null Behaviors"))
            {
                Undo.RecordObject(ctrl, "Remove Nulls");
                ctrl.behaviors.RemoveAll(b => b == null);
                EditorUtility.SetDirty(ctrl);
            }
        }
        
    }
}
#endif