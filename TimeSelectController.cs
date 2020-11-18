using UnityEngine;
using UnityEngine.UI;

public class TimeSelectController : MonoBehaviour
{
    public Slider slider;
    public Text timeText;
    void Start()
    {
        slider.onValueChanged.AddListener((float f) =>
        {
            timeText.text = (PrefabManager.Instance.tbar.GetComponent<TimeController>().timeLimit).ToString() + "sec";
        });

        if (GameModeManager.IsTimeAttackMode())
        {
            slider.minValue = 2f;
            slider.maxValue = 180f;
            PrefabManager.Instance.tbar.GetComponent<TimeController>().timeLimit = 2;
        }
        else if (GameModeManager.IsTurnBaseMode())
        {
            slider.minValue = 2f;
            slider.maxValue = 15f;
            PrefabManager.Instance.tbar.GetComponent<TimeController>().timeLimit = 2;
        }
        timeText.text = "2 sec";
    }
}
