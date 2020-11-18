using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Sentence_DataLoader
{
    public enum Index : int
    {
        sentence = 0,
        answer = 1,
        w1 = 2,
        w2 = 3,
        w3 = 4,
        explain = 5,
        ans = 6,
        vocab = 7,
        translate = 8,
        MAX
    }
    public static List<Dictionary<string, object>> originalData = null;
    public static List<Dictionary<string, object>> sentence_data = null;
    private static Dictionary<int, KeyValuePair<int, List<string>>> sentenceList = null;
    public static System.Random random = new System.Random();        // 난수 생성은 한번만 해야, 같은 숫자가 생성안된다. 참고 : https://crynut84.tistory.com/15
    private static Dictionary<int, bool> records = null;
    private static int sentenceIndex = 0;
    // for sentence game vocabs
    public static List<Dictionary<string, object>> vocab_data = null;
    //public static Dictionary<string, string> vocablist = null;
    private static Dictionary<string, KeyValuePair<int, List<string>>> vocablist = null;
    static Sentence_DataLoader()
    {
        string path = "Data/sentence_csv";
        originalData = CSVReader.ReadSetence(path);
        sentenceList = new Dictionary<int, KeyValuePair<int, List<string>>>();
        sentence_data = new List<Dictionary<string, object>>();
        records = new Dictionary<int, bool>();

        string path2 = "Data/ox_csv3";
        vocab_data = CSVReader.Read(path2);
        InitSentenceVocabs();
    }
    public static void ClearData()
    {
        //combocount = 0;
        sentenceIndex = 0;
        sentence_data.Clear();
        sentenceList.Clear();
        records.Clear();
        //usedTrickList.Clear();
    }
    public static void InitAllSentenceList()
    {        
        for (var i = 0; i < sentence_data.Count; i++)
        {
            int id = ((int)sentence_data[i]["id"]);
            if (id < 0)
            {
                Debug.Log("<color=yellow> Sentence Data Finished Loading !!! </color>");
                break;
            }
            string sentence = ((string)sentence_data[i]["sentence"]).Trim();
            string answer = ((string)sentence_data[i]["answer"]).Trim();
            string w1 = ((string)sentence_data[i]["w1"]).Trim();
            string w2 = ((string)sentence_data[i]["w2"]).Trim();
            string w3 = ((string)sentence_data[i]["w3"]).Trim();
            string explain = ((string)sentence_data[i]["explain"]).Trim();
            string ans = ((string)sentence_data[i]["ans"]).Trim();
            string vocabs = ((string)sentence_data[i]["vocabs"]).Trim();
            string translate = ((string)sentence_data[i]["translate"]).Trim();

            if (sentence.Equals("[END]"))
            {
                break;
            }
            if (answer.Equals("") || w1.Equals("") || w2.Equals("") || w3.Equals(""))       // skip if no answer or wrong.
            {
                Debug.Log("<color=red>" + " no answer : index : " + i + " : " + sentence_data[i]["sentence"] + "</color>");
                continue;
            }
            var list = new List<string>();
            {
                list.Add(sentence);
                list.Add(answer);
                list.Add(w1);
                list.Add(w2);
                list.Add(w3);
                list.Add(explain);
                list.Add(ans);          // 6
                list.Add(vocabs);       // 7
                list.Add(translate);    // 8
            }
            sentenceList.Add(i, new KeyValuePair<int, List<string>>((id), list));
            //Debug.Log("<color=yellow> added >> " + (i).ToString() + "</color>\n\n"
            //          + "<color=yellow> question :  " + sentence + "</color>\n\n"
            //          + "<color=yellow> answer : " + answer + "</color>\n" 
            //          + "<color=yellow> w1 : " + w1 + "</color>\n"  
            //          + "<color=yellow> w2 : " + w2 + "</color>\n" 
            //          + "<color=yellow> w3 : " + w3 + "</color>");
        }
    }
    public static void PrepareAllData()
    {
        for (int i = 0; i < originalData.Count; i++)
            sentence_data.Add(originalData[i]);
    }
    public static void PrepareOriginalData()
    {
        var r = GameModeManager.GetCurrentDayRange("SentenceGame");

        int st = r[0];
        int end = r[1];

        for (int i = st; i < end; i++)
        {
            sentence_data.Add(originalData[i]);
        }
    }
    public static void CheckSymbol()
    {
        foreach (var item in sentenceList)
        {
            if (item.Value.Value[(int)Sentence_DataLoader.Index.sentence].IndexOf("[ans]") < 0)
            {
                Debug.Log("<color=yellow>"+ item.Value.Key +"</color>");
            }
        }
    }
    public static void Sentence_InitList()
    {
        int cnt = GameModeManager.GetQuestionSize();
        for (var i = 0; i < cnt; i++)
        {
            int id = ((int) sentence_data[i]["id"]);
            if (id < 0)
            {
                Debug.Log("<color=yellow> Sentence Data Finished Loading !!! </color>");
                break;
            }
            string sentence = ((string)sentence_data[i]["sentence"]).Trim();
            string answer = ((string)sentence_data[i]["answer"]).Trim();
            string w1 = ((string)sentence_data[i]["w1"]).Trim();
            string w2 = ((string)sentence_data[i]["w2"]).Trim();
            string w3 = ((string)sentence_data[i]["w3"]).Trim();
            string explain = ((string)sentence_data[i]["explain"]).Trim();
            string ans = ((string)sentence_data[i]["ans"]).Trim();
            string vocabs = ((string)sentence_data[i]["vocabs"]).Trim();
            string translate = ((string)sentence_data[i]["translate"]).Trim();

            if (sentence.Equals("[END]"))
            {
                break;
            }
            if (answer.Equals("") || w1.Equals("") || w2.Equals("") || w3.Equals(""))       // skip if no answer or wrong.
            {
                Debug.Log("<color=red>" + " no answer : index : " + i + " : " + sentence_data[i]["sentence"] + "</color>");
                continue;
            }
            var list = new List<string>();
            {
                list.Add(sentence);
                list.Add(answer);
                list.Add(w1);
                list.Add(w2);
                list.Add(w3);
                list.Add(explain);
                list.Add(ans);          // 6
                list.Add(vocabs);       // 7
                list.Add(translate);    // 8
            }
            sentenceList.Add(i, new KeyValuePair<int, List<string>>((id), list));
            //Debug.Log("<color=yellow> added >> " + (i).ToString() + "</color>\n\n"
            //          + "<color=yellow> question :  " + sentence + "</color>\n\n"
            //          + "<color=yellow> answer : " + answer + "</color>\n" 
            //          + "<color=yellow> w1 : " + w1 + "</color>\n"  
            //          + "<color=yellow> w2 : " + w2 + "</color>\n" 
            //          + "<color=yellow> w3 : " + w3 + "</color>");
        }
    }
    public static void Sentence_Shuffle()   // shuffle questions
    {
        int n = sentence_data.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);     // 2보다 크거나 같고 MaxValue보다 작은 부호 있는 32비트 정수입니다.
            var value = sentence_data[k];
            sentence_data[k] = sentence_data[n];
            sentence_data[n] = value;
        }
    }
    public static void Sentence_Choice_Shuffle(List<string> list)   // shuffle choices
    {
        int n = 4;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);     // 0보다 크거나 같고 MaxValue보다 작은 부호 있는 32비트 정수입니다.
            var value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    public static bool CheckAnswer(string q)
    {
        string ans = sentenceList[sentenceIndex].Value[(int) Index.answer];
        return q.Equals(ans);
    }
    public static KeyValuePair<int, List<string>> GetVocabDatabyVocab(string s)
    {
        if (vocablist.ContainsKey(s))
        {
            return vocablist[s];
        }
        return new KeyValuePair<int, List<string>>(-1, new List<string>());
    }
    public static Dictionary<string, KeyValuePair<int, List<string>>> GetVocabsUsedInSentence(int id)
    {
        var s = sentenceList.Where(item => item.Value.Key == id).Select(item => item.Value).ToList()[0].Value[(int)Index.ans];
        List<string> l = s.Split(' ').ToList();
        Dictionary<string, KeyValuePair<int, List<string>>> vlist =
            new Dictionary<string, KeyValuePair<int, List<string>>>();
        foreach (var v in l)
        {
            if (vocablist.ContainsKey(v))
            {
                if (vlist.ContainsKey(v) == false)
                {
                    var d = vocablist[v];
                    vlist[v] = d;
                }
            }
        }

        var ll = GetAnswerChoicesByIndex(id);
        foreach (var v in ll)
        {
            if (vocablist.ContainsKey(v))
            {
                if (vlist.ContainsKey(v) == false)
                {
                    var list = vocablist[v];
                    vlist[v] = list;
                }
            }
        }
        return vlist;
    }
    public static void InitSentenceVocabs()
    {
        vocablist = new Dictionary<string, KeyValuePair<int, List<string>>>();

        for (var i = 0; i < vocab_data.Count; i++)
        {
            int id = ((int)vocab_data[i]["id"]);
            Debug.Log(id.ToString());
            if (id < 0)
            {
                Debug.Log("<color=yellow> Vocab For Sentence Finished Loading !!! </color>");
                break;
            }
            string v = ((string)vocab_data[i]["vocab"]).Trim();
            string d = ((string)vocab_data[i]["def"]).Trim();
            string e1 = ((string)vocab_data[i]["e1"]).Trim();
            string t1 = ((string)vocab_data[i]["t1"]).Trim();
            string e2 = ((string)vocab_data[i]["e2"]).Trim();
            string t2 = ((string)vocab_data[i]["t2"]).Trim();
            string sym = ((string)vocab_data[i]["sym"]).Trim();
            string aym = ((string)vocab_data[i]["aym"]).Trim();

            if (v.Equals("") || d.Equals(""))
            {
                Debug.Log("<color=red>" + " no vocab : index : " + i + " : " + vocab_data[i]["vocab"] + "</color>");
                continue;
            }
            if (vocablist.ContainsKey(v))
            {
                Debug.Log("<color=red>" + v + "</color>");
                continue;
            }
            List<string> infolist = new List<string>();
            {
                infolist.Add("false");
                infolist.Add(d);
                infolist.Add(e1);
                infolist.Add(t1);
                infolist.Add(e2);
                infolist.Add(t2);
                infolist.Add(sym);          // 6
                infolist.Add(aym);       // 7
            }
            vocablist.Add(v, new KeyValuePair<int, List<string>>(id, infolist));
        }
    }
    public static string GetCurrentQuestion()
    {
        string q = sentenceList[sentenceIndex].Value[(int) Index.sentence];
        q = String.Copy(q.Replace("[ans]", "________"));
        return q;
    }
    public static string GetCurrentUserChoiceQuestion(string userchoice)
    {
        string q = sentenceList[sentenceIndex].Value[(int) Index.sentence];
        string ans = sentenceList[sentenceIndex].Value[(int) Index.answer].ToString();
        if (ans.Equals(userchoice))
        {
            q = String.Copy(q.Replace("[ans]", "<color=blue><size=40>" + userchoice + "</size></color>"));
        }
        else
        {
            q = String.Copy(q.Replace("[ans]", "<color=red><size=40>" + userchoice + "</size></color>"));
        }
        return q;
    }
    public static KeyValuePair<int, List<string>> GetCurrentSentenceData()
    {
        return sentenceList[sentenceIndex];
    }

    public static void NextQuestion()
    {
        sentenceIndex++;
    }

    public static int GetCurrentSentenceIndex()
    {
        return sentenceIndex;
    }
    /// <summary>
    /// tool scene fuctions.
    /// </summary>
    public static void SplitStringByColon()
    {
        string path = "Data/vocabs";
        var data = CSVReader.Read(path);
        List<string> list = new List<string>();
        for (var i = 0; i < data.Count; i++)
        {
            string v = ((string)data[i]["vocabs"]).Trim();
            list.Add(v);
        }

        string totalvocab = string.Empty;
        for (int i = 0; i < list.Count; i++)
        {
            var ans = list[i];
            if (ans.Equals(""))
            {
                continue;
            }
            Debug.Log("<color=yellow>"+ ans.ToString() +"</color>");
            string[] v = ans.Split(new string[] { ":" }, StringSplitOptions.None);
            {
                totalvocab += v[0] + "\n";
            }
        }
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\test\VV.txt"))
        {
            file.WriteLine(totalvocab);
        }
    }

    public static void ShowOnlyTranslateKoreanFromAns()
    {
        Sentence_InitList();
        string total_sentence = string.Empty;
        for (int i = 0; i < sentenceList.Count; i++)
        {
            var ans= sentenceList[i].Value[(int) Index.ans];
            string[] sentences = ans.Split(new string[] { "[t]" }, StringSplitOptions.None);
            {
                total_sentence += sentences[0] + "\n";
            }
        }
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\test\translate.txt"))
        {
            file.WriteLine(total_sentence);
        }
    }
    public static void SearchForSameAnswerChoice()
    {
        Sentence_InitList();
        Debug.Log("<color=red>"
            + "Same Answer Choice"
            + "</color>");
        Debug.Log("<color=yellow>"
                  + "=============START============="
                  + "</color>");
        int cnt = 0;
        for (int i = 0; i < sentenceList.Count; i++)
        {
            var choices = sentenceList[i].Value;
            string ans = choices[1];
            for (int j = 2; j < 5; j++)
            {
                if (ans.Equals(choices[j]))
                {
                    Debug.Log("<color=yellow>"
                              + " index : " + (i + 2).ToString()
                              + " ans : " + ans
                              + " choices : " + choices[j]
                              +"</color>");
                    cnt++;
                }
            }
        }
        Debug.Log("<color=yellow>"
                  + "==============END=============="
                  + "</color>");
        Debug.Log("<color=yellow>"
                  + " Total : " + cnt.ToString()
                  + "</color>");
    }
    public static Dictionary<int, bool> GetRecord()
    {
        return records;
    }

    public static void Sentence_SetRecord(bool isCorrect)
    {
        //if (records.ContainsKey(sentenceIndex))
        //{
        //    records[sentenceIndex] = isCorrect;
        //}
        //else
        //{
        //    records.Add(sentenceIndex, isCorrect);
        //}
        var data = GetCurrentSentenceData();
        if (records.ContainsKey(data.Key))
        {
            records[data.Key] = isCorrect;
        }
        else
        {
            records.Add(data.Key, isCorrect);
        }
    }

    public static List<string> GetAnswerChoices()
    {
        return new List<string>()
        {
            sentenceList[sentenceIndex].Value[(int) Index.answer]
            , sentenceList[sentenceIndex].Value[(int) Index.w1]
            , sentenceList[sentenceIndex].Value[(int) Index.w2]
            , sentenceList[sentenceIndex].Value[(int) Index.w3]
        };
    }

    private static List<string> GetAnswerChoicesByIndex(int id)
    {
        return new List<string>()
        {
            sentenceList.Where(item => item.Value.Key == id).Select(item => item.Value).ToList()[0].Value[(int) Index.answer]
            , sentenceList.Where(item => item.Value.Key == id).Select(item => item.Value).ToList()[0].Value[(int) Index.w1]
            , sentenceList.Where(item => item.Value.Key == id).Select(item => item.Value).ToList()[0].Value[(int) Index.w2]
            , sentenceList.Where(item => item.Value.Key == id).Select(item => item.Value).ToList()[0].Value[(int) Index.w3]
        };
    }

    public static KeyValuePair<int, List<string>> GetSentenceListDataById(int id)
    {
        return sentenceList.Where(i => i.Value.Key == id).Select(i => i.Value).ToList()[0];
    }
}