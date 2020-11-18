using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeSelectController : MonoBehaviour
{
    public delegate void Callback();
    private Callback callback;

    public GameObject modeSelect;
    public GameObject timeSelect;
    public GameObject questionSelect;

    public GameObject okCancelPanel;

    private GameObject timeAttackBtn;
    private GameObject turnBaseBtn;
    
    private Button startButton;
    private Button cancelButton;

    private string userselectmode = "TimeAttack";

    void Start()
    {
        PrepareQuestionSelectPanel();
        QuestionSelectSetting();
    }

    void PrepareQuestionSelectPanel()
    {
        timeAttackBtn = questionSelect.transform.Find("50").gameObject;
        turnBaseBtn = questionSelect.transform.Find("100").gameObject;
        SetRadioButtonForQuestionSelect("50");
    }
    void PrepareModeSelectPanel()
    {
        timeAttackBtn = modeSelect.transform.Find("TimeAttackButton").gameObject;
        turnBaseBtn = modeSelect.transform.Find("TurnBaseButton").gameObject;

        SetRadioButton(userselectmode);
    }

    void QuestionSelectSetting()
    {
        if (startButton)
        {
            startButton.onClick.RemoveAllListeners();
        }

        if (cancelButton)
        {
            cancelButton.onClick.RemoveAllListeners();
        }

        modeSelect.SetActive(false);
        timeSelect.SetActive(false);
        questionSelect.SetActive(true);

        timeAttackBtn.GetComponent<RadioButtonController>().SetCallback(SetRadioButtonForQuestionSelect);
        turnBaseBtn.GetComponent<RadioButtonController>().SetCallback(SetRadioButtonForQuestionSelect);

        startButton = okCancelPanel.transform.Find("ConfirmButton").GetComponent<Button>();
        startButton.onClick.AddListener(() =>
        {
            callback();
            gameObject.SetActive(false);
            Destroy(gameObject);
        });
        cancelButton = okCancelPanel.transform.Find("CancelButton").GetComponent<Button>();
        cancelButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Scenes/MenuScene");
        });
    }
    void ModeSelectSetting()
    {
        if (startButton)
        {
            startButton.onClick.RemoveAllListeners();
        }

        if (cancelButton)
        {
            cancelButton.onClick.RemoveAllListeners();
        }

        modeSelect.SetActive(true);
        timeSelect.SetActive(false);
        
        timeAttackBtn.GetComponent<RadioButtonController>().SetCallback(SetRadioButton);
        turnBaseBtn.GetComponent<RadioButtonController>().SetCallback(SetRadioButton);

        startButton = okCancelPanel.transform.Find("ConfirmButton").GetComponent<Button>();
        startButton.onClick.AddListener(() =>
        {
            SetStartButton();
        });
        cancelButton = okCancelPanel.transform.Find("CancelButton").GetComponent<Button>();
        cancelButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Scenes/MenuScene");
        });
    }

    void SetRadioButtonForQuestionSelect(string s)
    {
        if (s.Contains("50"))
        {
            timeAttackBtn.GetComponent<RadioButtonController>().selectBtn.SetActive(true);
            turnBaseBtn.GetComponent<RadioButtonController>().selectBtn.SetActive(false);
            GameModeManager.SetQuestionSize(50);
        }
        else if (s.Contains("100"))
        {
            timeAttackBtn.GetComponent<RadioButtonController>().selectBtn.SetActive(false);
            turnBaseBtn.GetComponent<RadioButtonController>().selectBtn.SetActive(true);
            GameModeManager.SetQuestionSize(100);
        }
    }
    public void SetRadioButton(string btnName)
    {
        if (btnName.Contains("TimeAttack"))
        {
            timeAttackBtn.GetComponent<RadioButtonController>().selectBtn.SetActive(true);
            turnBaseBtn.GetComponent<RadioButtonController>().selectBtn.SetActive(false);
            GameModeManager.SetTimeAttackMode();
        }
        else if (btnName.Contains("TurnBase"))
        {
            timeAttackBtn.GetComponent<RadioButtonController>().selectBtn.SetActive(false);
            turnBaseBtn.GetComponent<RadioButtonController>().selectBtn.SetActive(true);
            GameModeManager.SetTurnBaseMode();
        }
        userselectmode = btnName;
    }
    public void SetStartButton()
    {
        startButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();

        if (modeSelect.activeSelf)
        {
            modeSelect.SetActive(false);
            timeSelect.SetActive(true);

            cancelButton = okCancelPanel.transform.Find("CancelButton").GetComponent<Button>();
            cancelButton.onClick.AddListener(() =>
            {
                ModeSelectSetting();
            });
        }
        else if (timeSelect.activeSelf)
        {
            // begin game
            callback();
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }

        startButton.onClick.AddListener(() =>
        {
            SetStartButton();
        });
    }

    public void SetCallBack(Callback o)
    {
        callback = o;
    }
}
