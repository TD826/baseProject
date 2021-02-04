using UnityEngine;
using TMPro;
public class ErrorTextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI errorText;
    void Start()
    {
        errorText.text = TextManager.GetText("ErrorCoin");
    }
}
