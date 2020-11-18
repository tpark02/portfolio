using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitlePanelController : MonoBehaviour, IPointerClickHandler
{
    //public GameObject goBackButton;
    public Text title;
    public Button goBackbutton;
    public GameObject timer;
    public GameObject count;
    public void Start()
    {
    }

    public void SetTitle(string s)
    {
        title.text = s;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        PrefabManager.Instance.ClickBackButton();
    }
}
