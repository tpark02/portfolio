using UnityEngine;
using UnityEngine.UI;

public class SentenceCardDescController : MonoBehaviour
{
    public Image psTypeBG;
    public Text psType;
    public Text desc;
    public void SetPsType(string s)
    {
        psType.text = s;
        //psTypeBG.color = UIStaticManager.GetPsTypeColor(s);
    }

    public void SetDesc(string s)
    {
        desc.text = s;
    }

    public void SetAlpah(float f)
    {
        GetComponent<CanvasGroup>().alpha = f;
    }
}
