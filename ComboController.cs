using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ComboController : MonoBehaviour
{
    public Sprite[] alphabet;
    public Sprite[] numbers;
    void Start()
    {
        ResetComboCount();
    }
    private void ResetComboCount()
    {
        GetComponent<CanvasGroup>().DOFade(1f, 0f);

        var label = transform.Find("ComboLabel");
        label.transform.DOScale(new Vector3(1.5f, 1.5f, 1f), 0f);

        var count = transform.Find("ComboCount");
        
        for (int i = 0; i < 4; i++)
        {
            var c = count.transform.GetChild(i);
            c.transform.DOScale(new Vector3(1.5f, 1.5f, 1f), 0f);
            c.gameObject.SetActive(false);
        }
    }
    private void SetComboCount(bool visible, int cnt)
    {
        ResetComboCount();

        if (visible == false)
        {
            return;
        }

        var label = transform.Find("ComboLabel");
        StartCoroutine(ComboAnimation(label));

        var count = transform.Find("ComboCount");

        for (int i = 0; i < cnt; i++)
        {
            var c = count.transform.GetChild(i);
            if (c)
            {
                c.gameObject.SetActive(visible);
            }
        }
        
        for (int i = 0; i < cnt; i++)
        {
            var c = count.transform.GetChild(i);
            if (c)
            {
                StartCoroutine(ComboAnimation(c));
            }
        }
    }
    IEnumerator ComboAnimation(Transform c)
    {
        c.transform.DOScale(new Vector3(1f, 1f, 1f), PrefabManager.waitforseconds);
        yield return new WaitForSeconds(PrefabManager.waitforseconds);
        GetComponent<CanvasGroup>().DOFade(0f, PrefabManager.waitforseconds);
    }
    private Transform SelectComboCountDigit(int index)
    {
        return transform.Find("ComboCount").transform.GetChild(index);
    }
    public void SetCombo(int combocount)
    {
        transform.Find("ComboLabel").gameObject.SetActive(true);
        
        string combocntstr = combocount.ToString();

        if (combocntstr.Length >= 4)
        {
            SelectComboCountDigit(0).GetComponent<Image>().sprite = numbers[(int)Char.GetNumericValue(combocntstr[0])];
            SelectComboCountDigit(1).GetComponent<Image>().sprite = numbers[(int)Char.GetNumericValue(combocntstr[1])];
            SelectComboCountDigit(2).GetComponent<Image>().sprite = numbers[(int)Char.GetNumericValue(combocntstr[2])];
            SelectComboCountDigit(3).GetComponent<Image>().sprite = numbers[(int)Char.GetNumericValue(combocntstr[3])];
            SetComboCount(true, 4);
        }
        else if (combocntstr.Length == 3)
        {
            SelectComboCountDigit(0).GetComponent<Image>().sprite = numbers[(int)Char.GetNumericValue(combocntstr[0])];
            SelectComboCountDigit(1).GetComponent<Image>().sprite = numbers[(int)Char.GetNumericValue(combocntstr[1])];
            SelectComboCountDigit(2).GetComponent<Image>().sprite = numbers[(int)Char.GetNumericValue(combocntstr[2])];
            SetComboCount(true, 3);
        }
        else if (combocntstr.Length == 2)
        {
            SelectComboCountDigit(0).GetComponent<Image>().sprite = numbers[(int)Char.GetNumericValue(combocntstr[0])];
            SelectComboCountDigit(1).GetComponent<Image>().sprite = numbers[(int)Char.GetNumericValue(combocntstr[1])];
            SetComboCount(true, 2);
        }
        else
        {
            int numIndex = (int)Char.GetNumericValue(combocntstr[0]);
            SelectComboCountDigit(0).GetComponent<Image>().sprite = numbers[numIndex];
            SetComboCount(true, 1);
        }
    }
    public void HideCombo()
    {
        transform.Find("ComboLabel").gameObject.SetActive(false);
        SetComboCount(false, 4);
    }
}
