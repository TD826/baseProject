using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(
    fileName = "TextDataList",
    menuName = "ScriptableObject/TextDataList",
    order = 0)]
public class TextDataListObject : ScriptableObject
{
    [Serializable]
    public class TextData
    {
        [Tooltip("テキストにつけるタグ")]
        public string textTag;
        [Tooltip("表示するテキスト")]
        public string text;
    }
    public TextData[] textDatas;
}
