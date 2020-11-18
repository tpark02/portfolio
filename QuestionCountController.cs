using System.Collections;
using Michsky.UI.ModernUIPack;
using TMPro;
using UnityEngine;

public class QuestionCountController : MonoBehaviour
{

    private Callback callback;
    public ProgressBar count;
    public delegate void Callback();
    public void OXIncreaseCount(int n)
    {
        StartIncreaseCountAnimation(n);
    }

    public void SentenceIncreaseCount(int n)
    {
        StartIncreaseCountAnimation(n);
    }

    void StartIncreaseCountAnimation(int n)
    {
        count.currentPercent = ((float)n / (float)GameModeManager.GetQuestionSize()) * 100f;
        count.UpdateUIdotween(callback, n);
    }
    public void SetCallBack(Callback o)
    {
        callback = o;
    }
}

