using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    public Transform viewPort;
    public Transform content;
    public Button backButton;
    [SerializeField] private TitlePanelController titlePanel;
    private List<GameObject> dgBoxlist = new List<GameObject>();
    private bool isDGBoxOn = false;
    void Start()
    {
        isDGBoxOn = false;
        string gametype = GameModeManager.GetGameType();
        if (gametype.Equals("OXGame"))
        {
            SetOXResultPanel();
        }
        else if (gametype.Equals("SentenceGame"))
        {
            SetSentenceResultPanel();
        }
        else if (gametype.Equals("StudyVocab"))
        {
            SetStudyVocabPanel();
        }
        else if (gametype.Equals("MyList"))
        {
            SetMyListPanel();
        }
        else if (gametype.Equals("MySentenceList"))
        {
            SetMyListSentencePanel();
        }
        //backButton.onClick.AddListener(() => { SceneManager.LoadScene("Scenes/MenuScene"); });
    }
    void OnBecameVisible()
    {
        UIStaticManager.RescaleToRectTransform(transform);
    }
    void SetMyListPanel()
    {
        OX_DataLoader.ClearData();
        OX_DataLoader.PrepareAllData();
        OX_DataLoader.InitAllWordList();
        var list = UserDataManager.Instance.GetUserStudyVocabList();

        foreach (var d in list)
        {
            var data = OX_DataLoader.GetVocab(d.Key);
            string vocab = d.Key;
            string desc = data.Value[(int) OX_DataLoader.Index.answer].Trim();
            var r = Instantiate(PrefabManager.Instance.oxresultItem);
            {
                r.GetComponent<ResultItem>().SetVocabDescForStudy(vocab, desc);
                r.GetComponent<ResultItem>().SetViewPort(viewPort);
                r.transform.SetParent(content, false);

                r.GetComponent<ResultItem>().SetEmptyStar();
                if (UserDataManager.Instance.IsVocabExist(vocab))
                {
                    r.GetComponent<ResultItem>().SetStar();
                }
            }
            SetPreVocab(vocab, r.transform);
        }
    }

    private void SetPreVocab(string vocab, Transform r)
    {
        var prelist = PrefabManager.Instance.CreatePreVocabDesc(vocab, r.transform);
        r.GetComponent<ResultItem>().defPrefab = prelist[0];
        r.GetComponent<ResultItem>().examplePrefab = prelist[1];
        r.GetComponent<ResultItem>().symPrefab = prelist[2];
        r.GetComponent<ResultItem>().aymPrefab = prelist[3];

        dgBoxlist.Add(prelist[0]);
        dgBoxlist.Add(prelist[1]);
        dgBoxlist.Add(prelist[2]);
        dgBoxlist.Add(prelist[3]);

        foreach (var o in dgBoxlist)
        {
            if (o != null)
            {
                o.transform.localPosition = new Vector3(o.transform.localPosition.x - 1280f
                    , o.transform.localPosition.y
                    , o.transform.localPosition.z);
            }
        }

        r.transform.SetParent(content, false);
    }
    void SetStudyVocabPanel()
    {
        OX_DataLoader.ClearData();
        GameModeManager.SetQuestionSize(100);
        OX_DataLoader.PrepareOriginalData();
        OX_DataLoader.OX_InitWordList();

        foreach (var p in OX_DataLoader.GetWordList())
        {
            string vocab = p.Key;
            //string desc = p.Value.Key;
            string desc = p.Value.Value[(int)OX_DataLoader.Index.answer].Trim();
            var r = Instantiate(PrefabManager.Instance.oxresultItem);
            {
                r.GetComponent<ResultItem>().SetVocabDescForStudy(vocab, desc);
                r.GetComponent<ResultItem>().SetViewPort(viewPort);
                r.transform.SetParent(content, false);

                r.GetComponent<ResultItem>().SetEmptyStar();
                if (UserDataManager.Instance.IsVocabExist(vocab))
                {
                    r.GetComponent<ResultItem>().SetStar();
                }

                SetPreVocab(vocab, r.transform);
            }
        }
    }
    void SetMyListSentencePanel()
    {
        Sentence_DataLoader.ClearData();
        Sentence_DataLoader.PrepareAllData();
        Sentence_DataLoader.InitAllSentenceList();
        var list = UserDataManager.Instance.GetUserSentenceList();

        foreach (var item in list)
        {
            int id = item.Key;
            var d = Sentence_DataLoader.GetSentenceListDataById(id);
            var r = Instantiate(PrefabManager.Instance.sentenceResultItem);
            {
                string sentence = UIStaticManager.ReplaceUnderline(d.Value[0]);
                r.GetComponent<ResultItem>().SetVocabForMyListSentence(sentence, "");
                SetPreSentence(r.transform, id);
                r.GetComponent<ResultItem>().SetEmptyStar();
                if (UserDataManager.Instance.IsSentenceDataExist(id))
                {
                    r.GetComponent<ResultItem>().SetStar();
                }

            }
        }
    }
    public void SetOXResultPanel()
    {
        int corretCnt = 0;
        foreach (var item in OX_DataLoader.records)
        {
            string vocab = item.Key;
            bool isCoreect = item.Value;
            var word = OX_DataLoader.GetVocab(vocab);
            var r = Instantiate(PrefabManager.Instance.oxresultItem);
            r.GetComponent<ResultItem>().SetVocabDesc(vocab, "", isCoreect);
            
            SetPreVocab(vocab, r.transform);
           if (isCoreect)
            {
                corretCnt++;
            }
            r.GetComponent<ResultItem>().SetEmptyStar();
            if (UserDataManager.Instance.IsVocabExist(vocab))
            {
                r.GetComponent<ResultItem>().SetStar();
            }
        }

        // write data
        int totalQuestionSize = GameModeManager.GetQuestionSize();
        float finalscore =  ((float) ((float)corretCnt / (float)totalQuestionSize) * 100f);
        UserDataManager.Instance.SetUserOXFinalScore(finalscore);
        int dayid = GameModeManager.GetCurrentDay();
        UserDataManager.Instance.SetUserOXDayResult(dayid, finalscore);
        UserDataManager.Instance.SetOXNextDayUnlock(dayid);
        string filename = FileReadWrite.Instance.GetOXFileName();
        FileReadWrite.Instance.WriteUserData(filename);
    }

    public void SetPreSentence(Transform r, int id)
    {
        r.GetComponent<ResultItem>().nId = id;
        var list = PrefabManager.Instance.CreatePreSentenceVocab(id, r.transform);
        r.GetComponent<ResultItem>().answerPrefab = list[0];
        r.GetComponent<ResultItem>().explainPrefab = list[1];
        r.GetComponent<ResultItem>().translatePrefab = list[2];
        r.transform.SetParent(content, false);
        dgBoxlist.Add(list[0]);
        dgBoxlist.Add(list[1]);
        dgBoxlist.Add(list[2]);
    }

    public void SetSentenceResultPanel()
    {
        int corretCnt = 0;
        foreach (var item in Sentence_DataLoader.GetRecord())
        {
            int id = item.Key;
            bool isCoreect = item.Value;
            //var d = Sentence_DataLoader.sentenceList[index];
            var d = Sentence_DataLoader.GetSentenceListDataById(id);
            var r = Instantiate(PrefabManager.Instance.sentenceResultItem);
            {
                string sentence = UIStaticManager.ReplaceUnderline(d.Value[0]);
                r.GetComponent<ResultItem>().SetVocabDesc(sentence, "", isCoreect);

                if (isCoreect)
                {
                    corretCnt++;
                }

                SetPreSentence(r.transform, id);

                r.GetComponent<ResultItem>().SetEmptyStar();
                if (UserDataManager.Instance.IsSentenceDataExist(id))
                {
                    r.GetComponent<ResultItem>().SetStar();
                }
            }
        }

        // write data
        int totalQuestionSize = GameModeManager.GetQuestionSize();
        float finalscore = ((float)((float)corretCnt / (float)totalQuestionSize) * 100f);
        UserDataManager.Instance.SetUserSentenceFinalScore(finalscore);
        int dayid = GameModeManager.GetCurrentDay();
        UserDataManager.Instance.SetUserSentenceDayResult(dayid, finalscore);
        UserDataManager.Instance.SetSentenceNextDayUnlock(dayid);
        string filename = FileReadWrite.Instance.GetSentenceFileName();
        FileReadWrite.Instance.WriteUserData(filename);
    }
    private void Update()
    {                
    }

    public void TurnOffDGBox()
    {
        if (isDGBoxOn == false)
        {
            foreach (var child in dgBoxlist)
            {
                if (child != null)
                    child.SetActive(false);
            }
            isDGBoxOn = true;
        }
    }

    public void SetTitle(string s)
    {
        titlePanel.SetTitle(s);
    }

    public GameObject GetTitle()
    {
        return titlePanel.gameObject;
    }
}
