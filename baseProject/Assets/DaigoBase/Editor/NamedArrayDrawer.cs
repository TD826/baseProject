using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// このスクリプトはListや配列のElement名を変更する拡張スクリプトです
// Editorフォルダ直下にこのスクリプトを置いてください
// ※2次元配列などの多次元配列には使えません


[CustomPropertyDrawer(typeof(NamedArrayAttribute))]
public class NamedArrayDrawer : PropertyDrawer
{
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        try
        {
            int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
            EditorGUI.PropertyField(rect, property, new GUIContent(((NamedArrayAttribute)attribute).names[pos]));

        }
        catch
        {
            EditorGUI.ObjectField(rect, property, label);
        }
    }
}
