using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SentenceDescPanelController : MonoBehaviour, IPointerClickHandler
{
    public Transform content;
    public Transform panel;
    public Text vocab;
    void Start()
    {
        //transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    PrefabManager.Instance.OnCloseDescPanel();
        //});
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        PrefabManager.Instance.OnCloseDescPanel();
    }
}
