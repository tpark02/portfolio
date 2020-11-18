using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class OX_DataLoader
{
    public enum Index : int
    {
        isTrick = 0,
        answer,
        e1,
        t1,
        //e2,
        //t2,
        sym,
        aym,
        MAX
    }

    //private static Dictionary<string, KeyValuePair<string, bool>> wordList = null;
    private static Dictionary<string, KeyValuePair<int, List<string>>> wordList = null;
    public static List<Dictionary<string, object>> originalData = null;
    public static List<Dictionary<string, object>> ox_data = null;

    public static Dictionary<string, int> usedTrickList = null;

    public static System.Random
        random = new System.Random(); // 난수 생성은 한번만 해야, 같은 숫자가 생성안된다. 참고 : https://crynut84.tistory.com/15

    public static Dictionary<string, bool> records = null; // 기록

    public static int combocount = 0;
    //public static bool isTutorialFirstTime = true;
    private static int oxIndex = 0;
    public static Dictionary<string, string> aymlist = null;
    public static Dictionary<string, string> symlist = null;
    public static Dictionary<string, string> vocablist = null;
    static OX_DataLoader() // Start is called before the first frame update
    {
        string path = "Data/3000";
        originalData = CSVReader.Read(path);

        if (originalData.Count <= 0)
        {
            Debug.Log("<color=red> OX Original Data is not Loaded !!!</color>");
            return;
        }

        combocount = 0;
        ox_data = new List<Dictionary<string, object>>();
        records = new Dictionary<string, bool>();
        //wordList = new Dictionary<string, KeyValuePair<string, bool>>();
        wordList = new Dictionary<string, KeyValuePair<int, List<string>>>();
        usedTrickList = new Dictionary<string, int>();
    }

    public static void ClearData()
    {
        combocount = 0;
        oxIndex = 0;
        ox_data.Clear();
        wordList.Clear();
        records.Clear();
        usedTrickList.Clear();
    }

    public static void PrepareAllData()
    {
        for (int i = 0; i < originalData.Count; i++)
            ox_data.Add(originalData[i]);
    }

    public static void PrepareOriginalData()
    {
        var r = GameModeManager.GetCurrentDayRange("OXGame");

        int st = r[0];
        int end = r[1];

        for (int i = st; i < end; i++)
        {
            ox_data.Add(originalData[i]);
        }
    }

    public static int GetVocabIndex(string s)
    {
        for (var i = 0; i < ox_data.Count; i++)
        {
            string vocab = ((string) ox_data[i]["vocab"]).Trim();
            if (vocab.Equals(s))
            {
                return (int) ox_data[i]["id"];
            }
        }

        return -1;
    }

    public static void OX_Test(string s)
    {
        int startindex = 0;
        for (var i = 0; i < originalData.Count; i++)
        {
            string vocab = ((string) originalData[i]["vocab"]).Trim();
            if (vocab.Equals(s))
            {
                startindex = i;
                break;
            }
        }

        for (var i = startindex; i < ox_data.Count; i++)
        {
            string vocab = ((string) ox_data[i]["vocab"]).Trim();
            string answer = ((string) ox_data[i]["def"]).Trim();
            int id = ((int) ox_data[i]["id"]);
            string e1 = ((string) ox_data[i]["e1"]).Trim();
            string t1 = ((string) ox_data[i]["t1"]).Trim();
            string e2 = ((string) ox_data[i]["e2"]).Trim();
            string t2 = ((string) ox_data[i]["t2"]).Trim();
            string sym = ((string) ox_data[i]["sym"]).Trim();
            string aym = ((string) ox_data[i]["aym"]).Trim();

            //string[] list = OX_TrimDesc(answer, "false");

            bool islong = true;

            //foreach (var s in list)
            //{
            //    if (s.Length > 26)      // 26글자 넘어가는 desc가지고있는 애들을 보여준다.
            //    {
            //        Debug.Log((i).ToString() + "\n" + vocab + "\n" + s + "\n");
            //        islong = true;
            //        break;
            //    }
            //}
            if (islong)
            {
                if (wordList.ContainsKey(vocab))
                {
                    Debug.Log("<color=red>" + vocab + "</color>");
                    continue;
                }

                List<string> infolist = new List<string>();
                {
                    infolist.Add("false");
                    infolist.Add(answer);
                    infolist.Add(e1);
                    infolist.Add(t1);
                    infolist.Add(e2);
                    infolist.Add(t2);
                    infolist.Add(sym); // 6
                    infolist.Add(aym); // 7
                }
                //wordList.Add(vocab, new KeyValuePair<string, bool>(answer, false));
                wordList.Add(vocab, new KeyValuePair<int, List<string>>(id, infolist));
            }
        }
    }

    public static void InitAllWordList()
    {
        for (var i = 0; i < ox_data.Count; i++)
        {
            string vocab = ((string) ox_data[i]["vocab"]).Trim();
            string answer = ((string) ox_data[i]["def"]).Trim();
            int id = ((int) ox_data[i]["id"]);
            string e1 = ((string) ox_data[i]["e1"]).Trim();
            string t1 = ((string) ox_data[i]["t1"]).Trim();
            string e2 = ((string) ox_data[i]["e2"]).Trim();
            string t2 = ((string) ox_data[i]["t2"]).Trim();
            string sym = ((string) ox_data[i]["sym"]).Trim();
            string aym = ((string) ox_data[i]["aym"]).Trim();

            if (wordList.ContainsKey(vocab)) //  중복 막기
            {
                Debug.Log("<color=red>" + " same vocab : index : " + i + " : " + ox_data[i]["vocab"] + "</color>");
                continue;
            }

            if (answer.Equals("")) // skip if no answer
            {
                Debug.Log("<color=red>" + " no answer : index : " + i + " : " + ox_data[i]["vocab"] + "</color>");
                continue;
            }

            int b = random.Next(2);
            bool istrick = Convert.ToBoolean(b);
            //wordList.Add(vocab, new KeyValuePair<string, bool>(answer, istrick));
            List<string> infolist = new List<string>();
            {
                if (istrick)
                {
                    infolist.Add("true");
                }
                else
                {
                    infolist.Add("false");
                }

                infolist.Add(answer);
                infolist.Add(e1);
                infolist.Add(t1);
                infolist.Add(e2);
                infolist.Add(t2);
                infolist.Add(sym); // 6
                infolist.Add(aym); // 7
            }
            wordList.Add(vocab, new KeyValuePair<int, List<string>>(id, infolist));
            //Debug.Log("<color=yellow>" + (i).ToString() + ": vocab : " + vocab + "answer : " + answer + "</color>");
        }
    }

    public static void InitAllVocab()
    {
        vocablist = new Dictionary<string, string>();
        for (var i = 0; i < ox_data.Count; i++)
        {
            string v = ((string)ox_data[i]["vocab"]).Trim();
            if (v.Equals(""))
            {
                continue;
            }

            if (vocablist.ContainsKey(v))
            {
                Debug.Log("<color=red> v :" + v + "</color>");
                continue;
            } 
            vocablist.Add(v, "");
        }
    }
    public static void InitAllSym()
    {
        symlist = new Dictionary<string, string>();
        for (var i = 0; i < ox_data.Count; i++)
        {
            string sym = ((string)ox_data[i]["sym"]).Trim();
            if (sym.Equals(""))
            {
                continue;
            }

            var list = sym.Split(new [] {"[t]"}, StringSplitOptions.None);
            foreach (var s in list)
            {
                if (vocablist.ContainsKey(s))
                {
                    Debug.Log("<color=red> s : " + s + "</color>");
                    continue;
                }

                if (symlist.ContainsKey(s))
                {
                    continue;
                }
                symlist.Add(s, "");
            }
        }
    }
    public static void InitAllAym()
    {
        aymlist = new Dictionary<string, string>();
        for (var i = 0; i < ox_data.Count; i++)
        {
            string a = ((string)ox_data[i]["aym"]).Trim();
            if (a.Equals(""))
            {
                continue;
            }
            var list = a.Split(new[] { "[t]" }, StringSplitOptions.None);
            foreach (var s in list)
            {
                if (vocablist.ContainsKey(s))
                {
                    Debug.Log("<color=red> s : " + s + "</color>");
                    continue;
                }
                if (aymlist.ContainsKey(s))
                {
                    continue;
                }
                aymlist.Add(s, "");
            }
        }
    }
    public static void OX_InitWordList() // init word list
    {
        int cnt = GameModeManager.GetQuestionSize();

        for (var i = 0; i < cnt; i++)
        {
            string vocab = ((string) ox_data[i]["vocab"]).Trim();
            string answer = ((string) ox_data[i]["def"]).Trim();
            int id = ((int) ox_data[i]["id"]);
            string e1 = ((string) ox_data[i]["e1"]).Trim();
            string t1 = ((string) ox_data[i]["t1"]).Trim();
            //string e2 = ((string) ox_data[i]["e2"]).Trim();
            //string t2 = ((string) ox_data[i]["t2"]).Trim();
            string sym = ((string) ox_data[i]["sym"]).Trim();
            string aym = ((string) ox_data[i]["aym"]).Trim();

            if (wordList.ContainsKey(vocab)) //  중복 막기
            {
                Debug.Log("<color=red>" + " same vocab : index : " + i + " : " + ox_data[i]["vocab"] + "</color>");
                continue;
            }

            if (answer.Equals("")) // skip if no answer
            {
                Debug.Log("<color=red>" + " no answer : index : " + i + " : " + ox_data[i]["vocab"] + "</color>");
                continue;
            }
#if TEST
            bool istrick = false;
#else
            int b = random.Next(2);
            bool istrick = Convert.ToBoolean(b);
#endif
            //wordList.Add(vocab, new KeyValuePair<string, bool>(answer, istrick));

            List<string> infolist = new List<string>();
            {
                if (istrick)
                {
                    infolist.Add("true");
                }
                else
                {
                    infolist.Add("false");
                }

                infolist.Add(answer);
                infolist.Add(e1);
                infolist.Add(t1);
                //infolist.Add(e2);
                //infolist.Add(t2);
                infolist.Add(sym); // 6
                infolist.Add(aym); // 7
            }
            //wordList.Add(vocab, new KeyValuePair<string, bool>(answer, false));
            wordList.Add(vocab, new KeyValuePair<int, List<string>>(id, infolist));
            //Debug.Log("<color=yellow>" + (i).ToString() + ": vocab : " + vocab + "answer : " + answer + "</color>");
        }
    }

    public static string OX_GetTrick(string targetvocab)
    {
        int cnt = ox_data.Count;

        while (true)
        {
            int rInt = random.Next(0, cnt); //for ints

            string str = (string) ox_data[rInt]["vocab"];

            if (str.Equals(targetvocab) == false
                && usedTrickList.ContainsKey(str) == false)
            {
                usedTrickList.Add(str, 0);
                return (string) ox_data[rInt]["def"];
            }
        }
    }

    public static void OX_Shuffle()
    {
        int n = ox_data.Count;

        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1); // 0보다 크거나 같고 MaxValue보다 작은 부호 있는 32비트 정수입니다.
            var value = ox_data[k];
            ox_data[k] = ox_data[n];
            ox_data[n] = value;
        }
    }

    public static void OX_SetRecord(string vocab, bool isCorrect)
    {
        if (records.ContainsKey(vocab))
        {
            records[vocab] = isCorrect;
        }
        else
        {
            records.Add(vocab, isCorrect);
        }
    }

    public static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp;
        temp = lhs;
        lhs = rhs;
        rhs = temp;
    }

    public static bool IsIndexOutOfRange()
    {
        return oxIndex > ox_data.Count - 1;
    }

    public static KeyValuePair<int, List<string>> GetOXDataByVocab(string s)
    {
        if (wordList.ContainsKey(s))
        {
            return wordList[s];
        }

        return new KeyValuePair<int, List<string>>(-1, new List<string>());
    }

    public static KeyValuePair<string, KeyValuePair<int, List<string>>> GetOXDataByIndex(int n)
    {
        return wordList.ElementAt(n);
    }

    public static KeyValuePair<string, KeyValuePair<int, List<string>>> GetCurrentOXData()
    {
        return wordList.ElementAt(oxIndex);
    }

    public static void NextOXCard()
    {
        oxIndex++;
    }

    public static int GetCurrentOXIndex()
    {
        return oxIndex;
    }

    public static KeyValuePair<int, List<string>> GetVocab(string s)
    {
        return wordList[s];
    }

    public static Dictionary<string, KeyValuePair<int, List<string>>> GetWordList()
    {
        return wordList;
    }

    public static int GetCurrentDayVocabSize()
    {
        return wordList.Count;
    }

    /// <summary>
    /// tool scene fuctions.
    /// </summary>
    public static void CompareVocabs()
    {
        OX_InitWordList();
        string path = "Data/vocabs";
        var data = CSVReader.Read(path);
        Dictionary<string, string> list = new Dictionary<string, string>();
        for (int i = 0; i < data.Count; i++)
        {
            string v = ((string) data[i]["vocab"]).Trim();
            string d = ((string) data[i]["def"]).Trim();
            if (list.ContainsKey(v) == false)
            {
                list[v] = d;
            }
        }

        string totalvocab = string.Empty;
        string totaldef = string.Empty;
        //foreach (var w in wordList)
        //{
        //    bool isrepeated = false;
        //    foreach (var word in list)
        //    {
        //        if (w.Key.ToLower().Equals((word.ToLower())))
        //        {
        //            isrepeated = true;
        //            break;
        //        }
        //    }

        //    if (isrepeated == false)
        //    {
        //        totalvocab += w.Key.ToString() + "\n";
        //        totaldef += w.Value.Key.ToString() + "\n";
        //    }
        //}

        foreach (var w in list)
        {
            bool isrepeated = false;
            foreach (var word in wordList)
            {
                if (word.Key.ToLower().Equals((w.Key.ToLower())))
                {
                    isrepeated = true;
                    break;
                }
            }

            if (isrepeated == false)
            {
                totalvocab += w.Key.ToString() + "\n";
                totaldef += w.Value.ToString() + "\n";
            }
        }

        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\test\samevocabs.txt"))
        {
            file.WriteLine(totalvocab);
        }

        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\test\samedef.txt"))
        {
            file.WriteLine(totaldef);
        }
    }

    public static void CountLongestDef()
    {
        PrepareAllData();
        InitAllWordList();
        string maxvocab = wordList.ElementAt(0).Key;
        string maxdef = wordList.ElementAt(0).Value.Value[(int) Index.answer];
        for (int i = 0; i < wordList.Count; i++)
        {
            if (i >= 133 && i < 827)
            {
                continue;
            }

            string def = wordList.ElementAt(i).Value.Value[(int) Index.answer];
            string[] deflist = null;
            deflist = def.Split(new string[] {"[t]"}, StringSplitOptions.None);

            foreach (var s in deflist)
            {
                if (s.Length > maxdef.Length)
                {
                    maxdef = s;
                    maxvocab = wordList.ElementAt(i).Key;
                }
            }
        }

        Debug.Log("<color=yellow> maxvocab : " + maxvocab + " maxdef : " + maxdef + "</color>");
    }

    public static void CountMaxDef()
    {
        PrepareAllData();
        InitAllWordList();
        int maxnum = 0;
        int index = 0;
        for (int i = 0; i < wordList.Count; i++)
        {
            if (i > 134 && i < 838)
            {
                continue;
            }

            string def = wordList.ElementAt(i).Value.Value[(int) Index.answer];
            string[] deflist = null;
            deflist = def.Split(new string[] {"[t]"}, StringSplitOptions.None);

            if (maxnum < deflist.Length)
            {
                index = i;
                maxnum = deflist.Length;
            }
        }

        Debug.Log("<color=yellow>" + wordList.ElementAt(index).Key + "</color>");
    }

    public static void FindError()
    {
        PrepareAllData();
        InitAllWordList();
        for (int i = 0; i < wordList.Count; i++)
        {
            var str = wordList.ElementAt(i).Value.Value[(int) Index.answer];
            if (i >= 132 && i <= 826)
            {
                continue;
            }

            Debug.Log(i);
            UIStaticManager.TrimDesc(wordList.ElementAt(i).Value.Value[(int) Index.answer], "false");
        }
    }

    public static void FindSymAym()
    {
        PrepareAllData();
        InitAllVocab();
        InitAllSym();
        InitAllAym();
        string allsym = string.Empty;
        string allaym = string.Empty;
        foreach (var s in symlist)
        {
            if (s.Key.Equals(""))
            {
                continue;
            }
            if (vocablist.ContainsKey(s.Key))
            {
                continue;
            }

            allsym += s.Key + "\n";
        }

        foreach (var s in aymlist)
        {
            if (s.Key.Equals(""))
            {
                continue;
            }
            if (vocablist.ContainsKey(s.Key))
            {
                continue;
            }

            allaym += s.Key + "\n";
        }

        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\test\sym.txt"))
        {
            file.WriteLine(allsym);
        }
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\test\aym.txt"))
        {
            file.WriteLine(allaym);
        }
    }
}
