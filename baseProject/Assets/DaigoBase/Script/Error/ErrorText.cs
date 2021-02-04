using UnityEngine;
using TMPro;
public class ErrorText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI errorText;
    void Start()
    {
        errorText.text = TextManager.GetText("ErrorCoin");
    }
}
