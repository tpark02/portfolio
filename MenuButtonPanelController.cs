using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonPanelController : MonoBehaviour
{
    public Button oxButton;
    public Button sentenceButton;
    public Button studyVocabButton;
    public Button myListButton;
    public Button mySentenceListButton;

    void Start()
    {
        oxButton.onClick.AddListener(() =>
        {
            PrefabManager.Instance.ShowDaySelectPanel("OXGame");
            PrefabManager.Instance.SetBackButton("DaySelectMenu");
        });

        sentenceButton.onClick.AddListener(() =>
        {
            PrefabManager.Instance.ShowDaySelectPanel("SentenceGame");
            PrefabManager.Instance.SetBackButton("DaySelectMenu");
        });
        studyVocabButton.onClick.AddListener(() =>
        {
            PrefabManager.Instance.ShowDaySelectPanel("StudyVocab");
            PrefabManager.Instance.SetBackButton("DaySelectMenu");
        });
        myListButton.onClick.AddListener(() =>
        {
            GameModeManager.SetGameType("MyList");
            PrefabManager.Instance.SetBackButton("MainScene");
            PrefabManager.Instance.ShowMyListPanel();
        });
        mySentenceListButton.onClick.AddListener(() =>
        {
            GameModeManager.SetGameType("MySentenceList");
            PrefabManager.Instance.SetBackButton("MainScene");
            PrefabManager.Instance.ShowMySentenceListPanel();
        });
    }

    public void ShowFavoriteButtons()
    {
        myListButton.gameObject.SetActive(true);
        mySentenceListButton.gameObject.SetActive(true);
        oxButton.gameObject.SetActive(false);
        sentenceButton.gameObject.SetActive(false);

        studyVocabButton.gameObject.SetActive(false);
    }
    public void ShowTestButtons()
    {
        myListButton.gameObject.SetActive(false);
        mySentenceListButton.gameObject.SetActive(false);
        oxButton.gameObject.SetActive(true);
        sentenceButton.gameObject.SetActive(true);

        studyVocabButton.gameObject.SetActive(false);
    }

    public void ShowStudyButtons()
    {
        studyVocabButton.gameObject.SetActive(true);

        myListButton.gameObject.SetActive(false);
        mySentenceListButton.gameObject.SetActive(false);
        oxButton.gameObject.SetActive(false);
        sentenceButton.gameObject.SetActive(false);
    }
}
