using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanelController : MonoBehaviour
{
    public Button favoriteButton;
    public Button testButton;
    public Button vocabButton;

    private void Start()
    {
        string filename = FileReadWrite.Instance.GetStudyVocabFileName();
        FileReadWrite.Instance.WriteUserData(filename);

        filename = FileReadWrite.Instance.GetStudySentencefileName();
        FileReadWrite.Instance.WriteUserData(filename);

        favoriteButton.onClick.AddListener(() =>
        {
            PrefabManager.Instance.ShowMenuButtonPanel("favorite");
        });
        testButton.onClick.AddListener(() =>
        {
            PrefabManager.Instance.ShowMenuButtonPanel("test");
        });
        vocabButton.onClick.AddListener(() =>
        {
            PrefabManager.Instance.ShowMenuButtonPanel("study");
        });
    }
}
