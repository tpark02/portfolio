using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour, IPointerClickHandler
{
    public Transform destination;
    void OnBecameVisible()
    {
        UIStaticManager.RescaleToRectTransform(transform);
    }
    void Awake()
    {
        GetComponent<CanvasGroup>().alpha = 0f;
    }
    public void StartShow()
    {
        StartCoroutine(StartFadeAnimation());
    }

    IEnumerator StartFadeAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<CanvasGroup>().alpha = 1;
        yield return new WaitForSeconds(1f);
        DOTween.KillAll();
        SceneManager.LoadScene("Scenes/ResultScene");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        return;
    }
}
