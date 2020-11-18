using System.Collections;
using DG.Tweening;
using UnityEngine;
using Michsky.UI.ModernUIPack;

public class TimeController : MonoBehaviour
{
    [HideInInspector] public float timeLeft = 0f;
    [HideInInspector] public bool isTimerStart = false;

    public UIManagerProgressBarLoop filled;
    public int timeLimit = 2;
    //private Image fill;
    
    void Start()
    {

#if TEST
        //transform.Find("TimeText").gameObject.SetActive(true);
        //transform.Find("TimeText").GetComponent<Text>().text = ((int) timeLeft).ToString();        
#else
        transform.Find("TimeText").gameObject.SetActive(false);
#endif       
        filled.bar.fillAmount = 0f;
    }
    void FixedUpdate()
    {
        if (GameModeManager.IsTimeAttackMode())
        {
            ProcessAttackTimer();
        }
        else if (GameModeManager.IsTurnBaseMode())
        {
           ProcessTurnTimer();
        }
    }
    public void StartTimerSentence()
    {
        StartCoroutine(BeginTimerSentence());
    }
    public void StartOXTimer(GameObject o)
    {
        StartCoroutine(BeginOXTimer(o));
    }

    public void PauseTimer()
    {
        isTimerStart = false;
    }

    public void ResumeTimer()
    {
        isTimerStart = true;
    }
    private IEnumerator BeginTimerSentence()
    {
        var bg = PrefabManager.Instance.bg.GetComponent<BackgroundController>();
        yield return new WaitWhile(() => true == bg.isOXshow);
        isTimerStart = true;
    }
    private IEnumerator BeginOXTimer(GameObject o)
    {
        yield return new WaitWhile(() => false == o.transform.Find("Panel").GetComponent<CardMove>().isStartFinished);

        var bg = PrefabManager.Instance.bg.GetComponent<BackgroundController>();
        yield return new WaitWhile(() => true == bg.isOXshow);
        
        isTimerStart = true;
    }
    private void ProcessAttackTimer()
    {
#if TEST
        if (timeLimit < 0)
        {
            return;
        }
#endif
        if (PrefabManager.Instance.tutorial_panel != null && PrefabManager.Instance.tutorial_panel.activeSelf)      // tutorial이 켜져있으면, 시간이 안가야한다.
        {
            return;
        }

        if (PrefabManager.Instance.bg.GetComponent<BackgroundController>().finish.gameObject.activeSelf) // 피니쉬가 나오면, 더이상 타이머를 진행하지않는다.
        {
            return;
        }

        if (timeLeft >= timeLimit) // 타임 오버. 게임 끝!
        {
            PrefabManager.Instance.bg.GetComponent<BackgroundController>().timeup.gameObject.SetActive(true);
            PrefabManager.Instance.bg.GetComponent<BackgroundController>().timeup.StartShow();
            return;
        }

        if (isTimerStart == false)
        {
            return;
        }

        timeLeft += Time.deltaTime;
        float f = timeLeft / timeLimit;
        filled.bar.DOFillAmount(f, Time.deltaTime);

//#if TEST
//        transform.Find("TimeText").GetComponent<Text>().text = (timeLeft).ToString();
//#endif
    }

    private void ProcessTurnTimer()
    {
        if (PrefabManager.Instance.tutorial_panel != null && PrefabManager.Instance.tutorial_panel.activeSelf)      // tutorial이 켜져있으면, 시간이 안가야한다.
        {
            return;
        }

        if (isTimerStart == false)
        {
            return;
        }

        if (timeLeft >= timeLimit)
        {
            Debug.Log("<color=yellow>" + timeLeft + "</color>");
            timeLeft = 0f;
            
            if (GameModeManager.GetGameType().Equals("SentenceGame"))
            {
                PrefabManager.Instance.SentenceTurnBaseTimesUp();
                return;
            }
            if (GameModeManager.GetGameType().Equals("OXGame"))
            {
                PrefabManager.Instance.OXTurnBaseTimesUp();
                return;
            }
        }
        timeLeft += Time.deltaTime;
        float f = timeLeft / timeLimit;
        filled.bar.DOFillAmount(f, Time.deltaTime);
#if TEST
        //transform.Find("TimeText").GetComponent<Text>().text = (timeLeft).ToString();
#endif
    }

    //private void SentenceGameOver()
    //{
    //    SceneManager.LoadScene("Scenes/ResultScene");
    //}

    //private void OXGameOver()
    //{
    //    SceneManager.LoadScene("Scenes/ResultScene");
    //}
}
