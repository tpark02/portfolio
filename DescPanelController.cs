using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DescPanelController : MonoBehaviour, IPointerClickHandler
{
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
