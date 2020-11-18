using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class BackgroundController : MonoBehaviour
{
    public GameObject right, left;
    // Finish, Time up sign
    public GameOverController timeup;
    public GameOverController finish;
    public GameObject titlePanelForGame;
    [HideInInspector] public bool isOXshow = false;
    void Start()
    {
        right.GetComponent<Image>().DOFade(0f, 0f);
        left.GetComponent<Image>().DOFade(0f, 0f);

        right.SetActive(true);
        left.SetActive(true);
    }
    public void SetBothInvisible()
    {
        right.GetComponent<Image>().DOFade(0f, 0f);
        left.GetComponent<Image>().DOFade(0f, 0f);
    }
    public void ShowO()
    {
        StartCoroutine(StartAnimation(right.GetComponent<Image>()));
    }
    public void ShowX()
    {
        StartCoroutine(StartAnimation(left.GetComponent<Image>()));
    }

    IEnumerator StartAnimation(Image o)
    {       
        isOXshow = true;
        o.DOFade(1f, 0f);
        yield return new WaitForSeconds(PrefabManager.waitforseconds);
        o.DOFade(0f, PrefabManager.waitforseconds);
        isOXshow = false;
    }
}
