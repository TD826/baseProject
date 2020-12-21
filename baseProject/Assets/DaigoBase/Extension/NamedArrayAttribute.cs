using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// このスクリプトはEditorスクリプトの内部に置かず、Assembly-CSharpの内部に置くようお願いします
// 使用タイミングと使用例
// 使用タイミング:要素数が決まっているListや配列などに使います
// 使用例：[NamedArray(new string[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" })] int[] i = new int[10];
// [SerializeField]のような形で使用してください

// ※2次元配列などの多次元配列には使えません

public class NamedArrayAttribute : PropertyAttribute
{
    public readonly string[] names;
    public NamedArrayAttribute(string[] names) { this.names = names; }
}
