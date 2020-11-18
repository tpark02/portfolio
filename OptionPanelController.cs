using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanelController : MonoBehaviour
{
    public Button Okbutton, CancelButton;
    public GameObject small, medium, large;
    [HideInInspector] public string selected_fontsize = string.Empty;

    void Start()
    {
        small.GetComponent<RadioButtonController>().SetCallback(SelectFontSize);
        medium.GetComponent<RadioButtonController>().SetCallback(SelectFontSize);
        large.GetComponent<RadioButtonController>().SetCallback(SelectFontSize);
        SelectFontSize(GameModeManager.fontsizestr);

        Okbutton.onClick.AddListener(() =>
        {
            GameModeManager.fontsizestr = selected_fontsize;
            PrefabManager.Instance.tbar.GetComponent<TimeController>().ResumeTimer();
            DestroyImmediate(this.gameObject);
        });
        CancelButton.onClick.AddListener(() =>
        {
            PrefabManager.Instance.tbar.GetComponent<TimeController>().ResumeTimer();
            DestroyImmediate(this.gameObject);
        });
    }
    void SelectFontSize(string name)
    {
        small.GetComponent<RadioButtonController>().selectBtn.SetActive(false);
        medium.GetComponent<RadioButtonController>().selectBtn.SetActive(false);
        large.GetComponent<RadioButtonController>().selectBtn.SetActive(false);

        if (name.Equals("small"))
        {
            small.GetComponent<RadioButtonController>().selectBtn.SetActive(true);
        }
        else if (name.Equals("medium"))
        {
            medium.GetComponent<RadioButtonController>().selectBtn.SetActive(true);
        }
        else if (name.Equals("large"))
        {
            large.GetComponent<RadioButtonController>().selectBtn.SetActive(true);
        }

        selected_fontsize = name;
    }
}
