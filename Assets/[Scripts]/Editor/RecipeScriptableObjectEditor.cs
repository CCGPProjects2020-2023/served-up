#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RecipeSO))]
public class RecipeScriptableObjectEditor : Editor {

    public override void OnInspectorGUI() {
        serializedObject.Update();

        RecipeSO recipe = (RecipeSO)target;

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("OUTPUT", new GUIStyle { fontStyle = FontStyle.Bold });
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        EditorGUILayout.BeginVertical();

        Texture texture = null;
        if (recipe.output != null) {
            texture = recipe.output.icon.texture;
        }
        GUILayout.Box(texture, GUILayout.Width(150), GUILayout.Height(150));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("output"), GUIContent.none, true, GUILayout.Width(150));
        EditorGUILayout.EndVertical();

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();



        EditorGUILayout.Space();



        GUILayout.Label("RECIPE", new GUIStyle { fontStyle = FontStyle.Bold });
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        texture = null;
        if (recipe.input1 != null) {
            texture = recipe.input1.icon.texture;
        }
        GUILayout.Box(texture, GUILayout.Width(150), GUILayout.Height(150));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("input1"), GUIContent.none, true, GUILayout.Width(150));
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        texture = null;
        if (recipe.input2 != null)
        {
            texture = recipe.input2.icon.texture;
        }
        GUILayout.Box(texture, GUILayout.Width(150), GUILayout.Height(150));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("input2"), GUIContent.none, true, GUILayout.Width(150));

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();


        serializedObject.ApplyModifiedProperties();
    }
}
#endif