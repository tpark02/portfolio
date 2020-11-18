using UnityEngine;
using UnityEngine.UI;

public class DynamicBGTextBoxController : MonoBehaviour
{
    public Text text;
    public Text titleText;
    public RectTransform rTransform;
    public RectTransform titleTransform;
    public RectTransform dgBoxTextBoxTransform;
    public void SetText(string s)
    {
        text.text = s;
    }

    public void SetTitleText(string s)
    {
        titleText.text = s;
    }
}
