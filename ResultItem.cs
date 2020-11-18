using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultItem : MonoBehaviour//, IPointerClickHandler
{
    [HideInInspector] public string sVocab = string.Empty;
    [HideInInspector] public int nId = 0;
    [HideInInspector] public GameObject answerPrefab = null, explainPrefab = null, translatePrefab = null, defPrefab = null, examplePrefab = null, symPrefab = null, aymPrefab = null;
    public GameObject star, emptystar;
    private List<GameObject> dgBoxlist = new List<GameObject>();
    private Transform viewPort;

    public void SetViewPort(Transform t)
    {
        viewPort = t;
    }
    void OnBecameVisible()
    {
        //UIStaticManager.RescaleToRectTransform(transform);
        //UIStaticManager.RescaleToRectTransform(star.transform);
        //UIStaticManager.RescaleToRectTransform(emptystar.transform);
    }
    public void SetVocabForSentenceResult(string vocab, string desc)
    {
        sVocab = vocab;
        transform.Find("Vocab").GetComponent<Text>().text = vocab;
        transform.Find("Desc").GetComponent<Text>().text = desc;
        transform.Find("O").gameObject.SetActive(false);
        transform.Find("X").gameObject.SetActive(false);        
    }
    public void SetVocabDescForStudy(string vocab, string desc)
    {
        sVocab = vocab;
        transform.Find("Vocab").GetComponent<Text>().text = vocab;
        transform.Find("Desc").GetComponent<Text>().text = desc;
        transform.Find("O").gameObject.SetActive(false);
        transform.Find("X").gameObject.SetActive(false);        
    }
    public void SetVocabForMyListSentence(string vocab, string desc)
    {
        sVocab = vocab;
        transform.Find("Vocab").GetComponent<Text>().text = vocab;
        transform.Find("Desc").GetComponent<Text>().text = desc;
        transform.Find("O").gameObject.SetActive(false);
        transform.Find("X").gameObject.SetActive(false);
    }
    public void SetVocabDesc(string vocab, string desc, bool isCorrect)
    {
        sVocab = vocab;
        transform.Find("Vocab").GetComponent<Text>().text = vocab;
        transform.Find("Desc").GetComponent<Text>().text = desc;

    
        if (isCorrect)
        {
            transform.Find("O").gameObject.SetActive(true);
            transform.Find("X").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("O").gameObject.SetActive(false);
            transform.Find("X").gameObject.SetActive(true);
        }
    }
    void OpenDescPanel(Transform p)
    {
        var o = PrefabManager.Instance.InstantiateSentenceDescPanel();
        {
            var content = o.GetComponent<SentenceDescPanelController>().content;
            PrefabManager.Instance.CreateVocabDesc(sVocab, content.transform, defPrefab, examplePrefab, symPrefab, aymPrefab);
            o.GetComponent<SentenceDescPanelController>().vocab.text = sVocab;
            o.transform.SetParent(PrefabManager.Instance.uicanvasResult.transform, false);
        }
        TurnoffDGBoxItem();
        PrefabManager.Instance.dpanel.Push(o);
    }

    private void TurnoffDGBoxItem()
    {
        foreach (var o in dgBoxlist)
        {
            if (o != null)
            {
                o.gameObject.SetActive(false);
            }
        }
    }
    void OpenSentenceDescPanel()
    {
        var o = PrefabManager.Instance.InstantiateSentenceDescPanel();
        {
            var content = o.GetComponent<SentenceDescPanelController>().content;
            PrefabManager.Instance.CreateSetenceVocab(nId, content.transform, answerPrefab, explainPrefab, translatePrefab);
            o.transform.SetParent(PrefabManager.Instance.uicanvasResult.transform, false);

            // create setence vocabs
            var dic = Sentence_DataLoader.GetVocabsUsedInSentence(nId);
            if (dic.Count > 0)
            {
                var t = Instantiate(PrefabManager.Instance.titleForResult);
                {
                    t.transform.GetChild(0).GetComponent<Text>().text = "단     어";
                    t.transform.SetParent(content.transform, false);
                }
            }
            foreach (var f in dic)
            {
                var item = Instantiate(PrefabManager.Instance.sentenceVocabItem);
                {
                    item.GetComponent<ResultItem>().SetVocabForSentenceResult(f.Key, "");
                    // sentence game에 사용된 단어들 생성
                    var list = PrefabManager.Instance.CreatePreVocabDesc(f.Key, item.transform);
                    item.GetComponent<ResultItem>().defPrefab = list[0];
                    item.GetComponent<ResultItem>().examplePrefab = list[1];
                    item.GetComponent<ResultItem>().symPrefab = list[2];
                    item.GetComponent<ResultItem>().aymPrefab = list[3];

                    dgBoxlist.Add(list[0]);
                    dgBoxlist.Add(list[1]);
                    dgBoxlist.Add(list[2]);
                    dgBoxlist.Add(list[3]);

                    foreach (var box in dgBoxlist)
                    {
                        if (box != null)
                        {
                            box.transform.localPosition = new Vector3(box.transform.localPosition.x - 1280f
                                , box.transform.localPosition.y
                                , box.transform.localPosition.z);
                        }
                    }

                    item.transform.SetParent(content, false);

                    item.GetComponent<ResultItem>().SetEmptyStar();
                    if (UserDataManager.Instance.IsVocabExist(f.Key))
                    {
                        item.GetComponent<ResultItem>().SetStar();
                    }
                }
            }
        }
        PrefabManager.Instance.dpanel.Push(o);
    }

    public void OnClickResultItem()
    {
        string gametype = GameModeManager.GetGameType();
        if (gametype.Equals("OXGame") && PrefabManager.Instance.dpanel.Count <= 0)
        {
            OpenDescPanel(PrefabManager.Instance.uicanvasResult.transform);
        }
        else if (gametype.Equals("SentenceGame"))
        {
            //if (PrefabManager.Instance.sDescPanel != null)
            if (PrefabManager.Instance.dpanel.Count > 0)
            {
                OpenDescPanel(PrefabManager.Instance.uicanvasResult.transform);
            }
            else
            {
                OpenSentenceDescPanel();
            }
        }
        else if (gametype.Equals("StudyVocab") && PrefabManager.Instance.dpanel.Count <= 0)
        {
            OpenDescPanel(PrefabManager.Instance.uicanvasResult.transform);
        }
        else if (gametype.Equals("MyList"))
        {
            OpenDescPanel(PrefabManager.Instance.uicanvasResult.transform);
        }
        else if (gametype.Equals("MySentenceList") && PrefabManager.Instance.dpanel.Count <= 1)
        {
            //if (PrefabManager.Instance.sDescPanel != null)
            if (PrefabManager.Instance.dpanel.Count > 0)
            {
                OpenDescPanel(PrefabManager.Instance.uicanvasResult.transform);
            }
            else
            {
                OpenSentenceDescPanel();
            }
        }
    }
    public void OnClickStarForSentence()
    {
        var data = Sentence_DataLoader.GetSentenceListDataById(nId);
        if (star.activeSelf)
        {
            SetEmptyStar();
            UserDataManager.Instance.DeleteUserSentenceList(nId);
            return;
        }

        SetStar();
        UserDataManager.Instance.AddUserSentenceList(nId);
    }
    public void OnClickStar()
    {
        string vocab = transform.Find("Vocab").GetComponent<Text>().text;

        if (star.activeSelf)
        {
            SetEmptyStar();            
            UserDataManager.Instance.DeleteUserStudyVocab(vocab);
            return;
        }

        SetStar();
        UserDataManager.Instance.AddUserStudyVocab(vocab);
    }
    public void SetStar()
    {
        emptystar.SetActive(false);
        star.SetActive(true);
    }
    public void SetEmptyStar()
    {
        star.SetActive(false);
        emptystar.SetActive(true);
    }
}
