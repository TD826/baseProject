using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// このスクリプトは16進数カラーをカラーパレットを開かずにおけるようになる拡張スクリプトです
// Editorフォルダ直下にこのスクリプトを置いてください

[CustomPropertyDrawer(typeof(HexColorProperty))]
public class HexColorPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect htmlField = new Rect(position.x, position.y, position.width - 100, position.height);
        Rect colorField = new Rect(position.x + htmlField.width, position.y, position.width - htmlField.width, position.height);
        string htmlValue = EditorGUI.TextField(htmlField, label, "#" + ColorUtility.ToHtmlStringRGBA(property.colorValue));

        Color newColor;
        if (ColorUtility.TryParseHtmlString(htmlValue, out newColor))
        {
            property.colorValue = newColor;
        }
        property.colorValue = EditorGUI.ColorField(colorField, property.colorValue);
    }
}

//導入に当たって一言：カラーパレット開けよ