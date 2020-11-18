using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SentenceChoiceController : MonoBehaviour
{
    public Text choice;
    public Text desc;
    public delegate void Callback();

    private Callback callback;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            callback();
            PrefabManager.Instance.tbar.GetComponent<TimeController>().PauseTimer();
            
            string userchoice = desc.text;

            PrefabManager.Instance.destroyQuestionPanel.GetComponent<SentenceQuestionPanelController>().DisplayAnswer(userchoice);

            bool isCorrect = Sentence_DataLoader.CheckAnswer(userchoice);
            
            if (isCorrect)
            {
                StartCoroutine(StartResultAnimation(true));
            }
            else
            {
                StartCoroutine(StartResultAnimation(false));
            }
            //Sentence_DataLoader.Sentence_SetRecord(isCorrect);
            Debug.Log("<color=yellow>" + isCorrect + "</color>");
        });
    }
    IEnumerator StartResultAnimation(bool isCorrect)
    {
        PrefabManager.Instance.destroyQuestionPanel.transform.SetAsFirstSibling();
        PrefabManager.Instance.destroyChoicePanel.GetComponent<CanvasGroup>().alpha = 0f;
        var bg = PrefabManager.Instance.bg.GetComponent<BackgroundController>();
        if (isCorrect)
        {
            bg.ShowO();
        }
        else
        {
            bg.ShowX();
            
        }
        
        yield return new WaitWhile(() => bg.isOXshow == true);
        yield return new WaitForSeconds(PrefabManager.waitforseconds);

        //PrefabManager.Instance.count.GetComponent<QuestionCountController>().SentenceIncreaseCount();

        if (isCorrect)
        {
            //PrefabManager.Instance.score.GetComponent<ScoreController>().Sentence_IncreaseScore();
            UserDataManager.Instance.IncreaseUserSentenceScore();
        }
        
        PrefabManager.Instance.destroyChoicePanel.GetComponent<CanvasGroup>().alpha = 0f;

        Sentence_DataLoader.Sentence_SetRecord(isCorrect);

        Sentence_DataLoader.NextQuestion();
        
        int currentIndex = Sentence_DataLoader.GetCurrentSentenceIndex();
        
        GameModeManager.SetGameFinished(currentIndex);

        PrefabManager.Instance.count.GetComponent<QuestionCountController>().SentenceIncreaseCount(currentIndex);
        
        PrefabManager.Instance.CreateSentenceCard();

        if (GameModeManager.IsTurnBaseMode())
        {
            PrefabManager.Instance.tbar.GetComponent<TimeController>().timeLeft = 0f;
        }
    }
    public void SetCallBack(Callback o)
    {
        callback = o;
    }

    public void SetFontSize(int n)
    {
        desc.fontSize = n;
    }
}
