using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialController : MonoBehaviour, IPointerClickHandler
{
    private float right = 100f;
    private float left = -100f;
    private Sequence seq = null;
    void Awake()
    {
        transform.Find("Swipe").localPosition = new Vector2(-100f, transform.localPosition.y);
        GetComponent<CanvasGroup>().alpha = 0f;
    }

    void OnBecameVisible()
    {
        UIStaticManager.RescaleToRectTransform(transform);
    }

    public void CradlePanel()
    {
        seq = DOTween.Sequence();
        seq.Append(transform.Find("Swipe").DOLocalMoveX(right, 1f));
        seq.Append(transform.Find("Swipe").DOLocalMoveX(left, 1f));
        seq.SetLoops(-1, LoopType.Restart);
        GetComponent<CanvasGroup>().DOFade(1f, 0f);
    }

    public void OnClickTutorialPanel()
    {
        gameObject.SetActive(false);
        //OX_DataLoader.isTutorialFirstTime = false;
        GameModeManager.SetTutorialFirstTime(false);
        PrefabManager.Instance.destroyCard.transform.Find("Panel").GetComponent<CardMove>().TouchEnable(true);
        seq.Kill();
        seq = null;
        Destroy(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        OnClickTutorialPanel();
    }
}
