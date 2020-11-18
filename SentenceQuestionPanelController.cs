using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class SentenceQuestionPanelController : MonoBehaviour
{
    public Text question;
    public void DisplayAnswer(string userchoice)
    {
        string q = Sentence_DataLoader.GetCurrentUserChoiceQuestion(userchoice);
        var t = transform.Find("QuestionBG").transform.Find("Question").GetComponent<Text>();
        t.text = q;
        if (Sentence_DataLoader.CheckAnswer(userchoice))
        {
            StartCoroutine(BounceScaleAnimation());
        }
        else
        {
            transform.DOShakePosition(0.2f, 20f, 50);
        }
    }

    IEnumerator BounceScaleAnimation()
    {
        transform.DOScale(1.1f, 0.1f);
        yield return new WaitForSeconds(0.1f);
        transform.DOScale(1f, 0.1f);
    }

    public void SetFontSize(int n)
    {
        question.fontSize = n;
    }
}
