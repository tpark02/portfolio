using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class CardFormatter : MonoBehaviour
{
    public GameObject desc;
    public GameObject vocab;
    public GameObject cardDescList;

    private List<GameObject> itemList;
    [HideInInspector] public string vocabforrecord;
    void Awake()
    {
        itemList = new List<GameObject>();
    }
    //public void SetDesc(string[] l)
    //{
    //    var p = transform.Find("Desc").transform;
    //    int listsize = l.Length < 3 ? l.Length : 3;
    //    for (int i = 0; i < listsize; i++)
    //    {
    //        var d = Instantiate(PrefabManager.Instance.descitem);
    //        {
    //            d.transform.SetParent(p, false);
    //            d.transform.Find("desc").GetComponent<Text>().text = l[i];

    //            if (i == l.Length - 1)
    //            {
    //                d.transform.Find("space").gameObject.SetActive(false);
    //            }
    //        }
    //        itemList.Add(d);
    //    }

    //    for (int i = listsize; i < 7; i++)
    //    {
    //        var d = Instantiate(PrefabManager.Instance.descitem);
    //        {
    //            d.transform.SetParent(p, false);
    //            d.transform.Find("desc").GetComponent<Text>().text = string.Empty;
    //            d.transform.Find("space").gameObject.SetActive(false);
    //        }
    //        itemList.Add(d);
    //    }
    //}
    public void SetVocab(string s)
    {
        vocab.GetComponent<Text>().text = "<b>" + s + "</b>";
        vocabforrecord = s;
    }

    public void SetDescList(Dictionary<string, string> d)
    {
        for (int i = 0; i < 5; i++)
        {
            var item = Instantiate(PrefabManager.Instance.cardDescItem);
            item.GetComponent<SentenceCardDescController>().SetAlpah(0f);
            item.transform.SetParent(cardDescList.transform, false);
        }
        for (int i = 0; i < d.Count; i++)
        {
            if (i > 4)
            {
                break;
            }
            string pstype = d.ElementAt(i).Value;
            string def = d.ElementAt(i).Key;
            cardDescList.transform.GetChild(i).GetComponent<SentenceCardDescController>().SetPsType(pstype);
            cardDescList.transform.GetChild(i).GetComponent<SentenceCardDescController>().SetDesc(def);
            cardDescList.transform.GetChild(i).GetComponent<SentenceCardDescController>().SetAlpah(1f);
        }
    }
}
