using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TextManager : MonoBehaviour
{
    private static string path = "TextDataList";
    private static TextDataListObject instance;
    public static string GetText(string tag)
    {
        if(instance == null)
        {
            instance = Resources.Load<TextDataListObject>(path);
        }

        TextDataListObject.TextData textData = instance.textDatas.FirstOrDefault(x => x.textTag == tag);
        if(textData == null)
        {
            Debug.LogError(tag + "名が間違ってるかテキストが存在しません");
        }
        return textData.text;
    }
}
