using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DictionaryLoader
{
    public static Dictionary<string, KeyValuePair<string, string>> dictionaryList = null;
    public static List<Dictionary<string, object>> dictionaryData = null;
    public static Dictionary<string, string> partofspeech = null;
    static DictionaryLoader()
    {
        partofspeech = new Dictionary<string, string>();
        partofspeech["n."] = "명사";
        partofspeech["pron."] = "대명사";
        partofspeech["v."] = "동사";
        partofspeech["adj."] = "형용사";
        partofspeech["adv."] = "부사";
        partofspeech["prep."] = "전치사";
        partofspeech["conj."] = "접속사";
        partofspeech["interj."] = "감탄사";
        partofspeech["x."] = "";
        partofspeech["vt."] = "";
        partofspeech["vi."] = "";
        partofspeech["a."] = "";
        partofspeech["ad."] = "";
        string path = "Data/extra_new";
        dictionaryData = CSVReader.Read(path);
        
        if (dictionaryData.Count <= 0)
        {
            Debug.Log("<color=red> OX DATA is not Loaded !!!</color>");
            return;
        }

        dictionaryList = new Dictionary<string, KeyValuePair<string, string>>();
    }

    public static void ReplaceParenthesisComma() // replace [x]
    {
        string totalvocab = string.Empty;
        string totaldef = string.Empty;
        string totalps = string.Empty;

        for (int i = 0; i < dictionaryData.Count; i++)
        {
            var def = ((string)dictionaryData[i]["def"]).Trim();
            var ps = ((string)dictionaryData[i]["ps"]).Trim();

            for (int j = 0; j < def.Length; j++)
            {
                if (def[j].Equals('('))
                {
                    for (int k = j; k < def.Length; k++)
                    {
                        if (def[k].Equals(')'))
                        {
                            break;
                        }
                        else if (def[k].Equals(','))
                        {
                            def = def.Substring(0, k) + "[x]" + def.Substring(k + 1);
                        }
                    }
                }
            }
            var v = ((string)dictionaryData[i]["vocab"]).Trim();
            totalvocab += v + "\n";
            totaldef += def + "\n";
            totalps += ps + "\n";
        }
        
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\test\vocab.txt"))
        {
            file.WriteLine(totalvocab);
        }
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\test\def.txt"))
        {
            file.WriteLine(totaldef);
        }
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\test\ps.txt"))
        {
            file.WriteLine(totalps);
        }
    }
    public static void CreatePreCSVFiles()
    {
        string totalvocab = string.Empty;
        string totaldef = string.Empty;
        string totalps = string.Empty;

        for (int i = 0; i < dictionaryData.Count; i++)
        {
            var def = ((string)dictionaryData[i]["def"]).Trim();
            string ps = string.Empty;
            foreach (var p in partofspeech)
            {
                if (def.IndexOf(p.Key) >= 0)
                {
                    ps += p.Key + ":";
                    def = def.Replace(p.Key, "");
                }
            }

            if (ps.Equals("") == false && def.Equals("") == false)
            {
                var v = ((string)dictionaryData[i]["vocab"]).Trim();
                totalvocab += v + "\n";
                totaldef += def + "\n";
                totalps += ps + "\n";
            }
        }

        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\test\vocab.txt"))
        {
            file.WriteLine(totalvocab);
        }
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\test\def.txt"))
        {
            file.WriteLine(totaldef);
        }
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\test\ps.txt"))
        {
            file.WriteLine(totalps);
        }
    }

    public static void SplitSentence()
    {
        string word = string.Empty;
        string def = string.Empty;

        for (int i = 0; i < dictionaryData.Count; i++)
        {
            var w = ((string)dictionaryData[i]["word"]).Trim();
            var list = w.Split(':').ToList();
            if (list.Count < 2)
            {
                Debug.Log(list[0]);
                continue;
            }
            word += list[0] + "\n";
            def += list[1] + "\n";
        }

        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\test\vocab2.txt"))
        {
            file.WriteLine(word);
        }
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\test\def2.txt"))
        {
            file.WriteLine(def);
        }
    }

    public static void CompareWords()
    {
        string word = string.Empty;
        List<string> list = new List<string>();
        List<string> list2 = new List<string>();
        for (int i = 0; i < dictionaryData.Count; i++)
        {
            var w1 = ((string) dictionaryData[i]["w1"]).Trim();
            list.Add(w1);
           
        }
        for (int i = 0; i < dictionaryData.Count; i++)
        {
            var w2 = ((string)dictionaryData[i]["w2"]).Trim();
            list2.Add(w2);

        }
        for (int j = 0; j < list2.Count; j++)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list2[j].Equals(list[i]))
                {
                    word += list2[j] + "\n";
                }
            }
        }
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\test\same.txt"))
        {
            file.WriteLine(word);
        }
    }

    public static void DeleteExtra()
    {
        string word = string.Empty;
        string def = string.Empty;
        Dictionary<string, string> list = new Dictionary<string, string>();
        Dictionary<string, string> list2 = new Dictionary<string, string>();

        for (int i = 0; i < dictionaryData.Count; i++)
        {
            var w1 = ((string)dictionaryData[i]["vocab"]).Trim();
            var d = ((string)dictionaryData[i]["def"]).Trim();
            if (list.ContainsKey(w1) == false)
                list.Add(w1, d);
        }
        for (int i = 0; i < dictionaryData.Count; i++)
        {
            var w2 = ((string)dictionaryData[i]["extra"]).Trim();
            if (w2.Equals(""))
            {
                continue;
            }
            if (list2.ContainsKey(w2) == false)
                list2.Add(w2, "");
        }

        foreach (var d in list2)
        {
            if (list.ContainsKey(d.Key))
            {
                list.Remove(d.Key);
            }
        }

        foreach (var d in list)
        {
            word += d.Key + "\n";
            def += d.Value + "\n";
        }
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\test\finalword.txt"))
        {
            file.WriteLine(word);
        }
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\test\finaldef.txt"))
        {
            file.WriteLine(def);
        }
    }
    public static void test()
    {}
}
