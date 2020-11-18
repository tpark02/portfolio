using System;
using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class DayButtonController : MonoBehaviour
{
    public GameObject unlockBG, lockBG, check;
    public ButtonManager daytextNew;

    void Awake()
    {
        //check.SetActive(false);
    }
    public void SetDayText(string s)
    {
        //daytext.text = s;
        daytextNew.buttonText = s;
    }

    public void SetLock()
    {
        lockBG.SetActive(true);
        unlockBG.SetActive(false);
    }

    public void SetUnLock()
    {
        lockBG.SetActive(false);
        unlockBG.SetActive(true);
    }
    public void StopUnlockAnimation()
    {
        GetComponent<CanvasGroup>().DOKill();
        //unlockbackground1.GetComponent<Image>().DOKill();
        //unlockbackground2.GetComponent<Image>().DOKill();
        //unlockText1.GetComponent<TextMeshProUGUI>().DOKill();
        //unlockText2.GetComponent<TextMeshProUGUI>().DOKill();
    }
    public void StartUnlockAnimation()
    {
        GetComponent<CanvasGroup>().DOFade(0.2f, 0.7f).SetLoops(-1, LoopType.Yoyo);
        //unlockbackground1.GetComponent<Image>().DOColor(Color.white, 0.8f).SetLoops(-1, LoopType.Yoyo);
        //unlockbackground2.GetComponent<Image>().DOColor(Color.white, 0.8f).SetLoops(-1, LoopType.Yoyo);
        //unlockText1.GetComponent<TextMeshProUGUI>().DOColor(Color.white, 0.8f).SetLoops(-1, LoopType.Yoyo);
        //unlockText2.GetComponent<TextMeshProUGUI>().DOColor(Color.white, 0.8f).SetLoops(-1, LoopType.Yoyo);
    }
    public void SetCheck(bool b)
    {        
        //check.SetActive(b);
    }

    public void OnClickDayButton()
    {
        DOTween.KillAll();

        if (lockBG.activeSelf)
        {
            return;
        }
        if (this.GetComponent<CanvasGroup>().alpha <= 0)
        {
            Debug.Log("<color=red>Click " + daytextNew.buttonText + "</color>");
            return;
        }
        Debug.Log("<color=yellow>Click " + daytextNew.buttonText + "</color>");
        GameModeManager.SetDay(Int32.Parse(daytextNew.buttonText));

        string selectedGame = GameModeManager.GetGameType();

        if (selectedGame.Equals("OXGame"))
        {
            SceneManager.LoadScene("Scenes/OXScene");
        }
        else if (selectedGame.Equals("SentenceGame"))
        {
            SceneManager.LoadScene("Scenes/SentenceScene");
        }
        else if (selectedGame.Equals("StudyVocab"))
        {
            SceneManager.LoadScene("Scenes/StudyVocabScene");
        }
    }

    public void ResetUnlockBGColor()
    {
        //unlockbackground1.GetComponent<Image>().color = new Color32(95, 105, 115, 255);
        //unlockbackground2.GetComponent<Image>().color = new Color32(95, 105, 115, 255);
        //unlockText1.GetComponent<TextMeshProUGUI>().color = new Color32(95, 105, 115, 255);
        //unlockText2.GetComponent<TextMeshProUGUI>().color = new Color32(95, 105, 115, 255);
    }
}
