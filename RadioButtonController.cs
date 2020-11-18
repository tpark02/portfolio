using UnityEngine;
using UnityEngine.EventSystems;

public class RadioButtonController : MonoBehaviour, IPointerClickHandler
{
    public delegate void Callback(string s);
    private Callback callback = null;
    public GameObject selectBtn;

    void OnBecameVisible()
    {
        UIStaticManager.RescaleToRectTransform(transform);
    }
    void Start()
    {
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        string btnName = eventData.pointerCurrentRaycast.gameObject.name;
        Debug.Log("Clicked: " + btnName);

        callback(btnName);
    }
    public void SetCallback(Callback cal)
    {
        callback = cal;
    }
}