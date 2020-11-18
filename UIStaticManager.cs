using System;
using System.Collections.Generic;
using UnityEngine;

public static class UIStaticManager
{ 
    public static void RescaleToRectTransform<T>(this T collider) where T : Transform
    {
        // boxcollider2d의 사이즈를 Image의 사이즈에 맞게 자동 조절 해주는 함수이다.
        // OnBecomeVisible에서 쓰는데, 해당 오브젝트에는 Image의 alpha값이 0이되면 안되며, SpriteRenderer 컴포넌트가 있었야한다.
        if (collider is RectTransform)
        {
            if (collider)
            {
                Vector2 newSize = collider.GetComponent<RectTransform>().rect.size;
                collider.GetComponent<BoxCollider2D>().size = new Vector2(newSize.x, newSize.y);
            }
        }
    }

    public static string ReplaceUnderline(string s)
    {
        return String.Copy(s.Replace("[ans]", "________"));
    }

    public static List<KeyValuePair<string, string>> FormatDescDef(string s)
    {
        string[] strlist = null;
        List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
        strlist = s.Split(new string[] { "[t]" }, StringSplitOptions.None);
        foreach (var s1 in strlist)
        {
            if (s1.Equals(""))
            {
                continue;
            }
            string str = s1.Trim();
            str = str.Replace("[x]", ",");
            var pstype = GetPsType(str).Trim();
            var desc = GetDesc(str).Trim();
            list.Add(new KeyValuePair<string, string>(pstype, desc));
        }
        return list;
    }
    public static string GetDesc(string s)
    {
        string[] r = null;
        if (s.Contains("(n.)"))
        {
            r = s.Split(new string[] { "(n.)" }, StringSplitOptions.None);
        }
        else if (s.Contains("(v.)"))
        {
            r = s.Split(new string[] { "(v.)" }, StringSplitOptions.None);
        }
        else if (s.Contains("(xx.)"))
        {
            r = s.Split(new string[] { "(xx.)" }, StringSplitOptions.None);
        }
        else if (s.Contains("(adj.)"))
        {
            r = s.Split(new string[] { "(adj.)" }, StringSplitOptions.None);
        }
        else if (s.Contains("(adv.)"))
        {
            r = s.Split(new string[] { "(adv.)" }, StringSplitOptions.None);
        }
        else if (s.Contains("(prep.)"))
        {
            r = s.Split(new string[] { "(prep.)" }, StringSplitOptions.None);
        }
        else if (s.Contains("(pron.)"))
        {
            r = s.Split(new string[] { "(pron.)" }, StringSplitOptions.None);
        }
        else if (s.Contains("(determiner.)"))
        {
            r = s.Split(new string[] { "(determiner.)" }, StringSplitOptions.None);
        }
        else if (s.Contains("(vtr.)"))
        {
            r = s.Split(new string[] { "(vtr.)" }, StringSplitOptions.None);
        }
        else if (s.Contains("(vi.)"))
        {
            r = s.Split(new string[] { "(vi.)" }, StringSplitOptions.None);
        }
        else if (s.Contains("(conj.)"))
        {
            r = s.Split(new string[] { "(conj.)" }, StringSplitOptions.None);
        }
        //else
        //{
        //    r = new string[2];
        //    r[1] = "X";
        //}
        Debug.Log(s);
        return s;
    }
    public static string GetPsType(string s)
    {
        string r = string.Empty;
        if (s.Contains("(n.)"))
        {
            r = "명사";
        }
        else if (s.Contains("(v.)"))
        {
            r = "동사";
        }
        else if (s.Contains("(xx.)"))
        {
            r = "숙어";
        }
        else if (s.Contains("(adj.)"))
        {
            r = "형용사";
        }
        else if (s.Contains("(adv.)"))
        {
            r = "부사";
        }
        else if (s.Contains("(prep.)"))
        {
            r = "전치사";
        }
        else if (s.Contains("(pron.)"))
        {
            r = "대명사";
        }
        else if (s.Contains("(determiner.)"))
        {
            r = "한정사";
        }
        else if (s.Contains("(vtr.)"))
        {
            r = "타동사";
        }
        else if (s.Contains("(vi.)"))
        {
            r = "자동사";
        }
        else if (s.Contains("(conj.)"))
        {
            r = "접속사";
        }
        //else
        //{
        //    r = "X";
        //}
        return r;
    }
    public static Color GetPsTypeColor(string s)
    {
        string r = string.Empty;
        if (s.Equals("명사"))
        {
            return new Color32(57, 202, 0, 255); // green
        }
        if (s.Equals("동사"))
        {
            return new Color32(255, 33, 4, 255); // red
        }
        if (s.Equals("숙어"))
        {
            return new Color32(171, 171, 171, 255);  // grey
        }
        if (s.Equals("형용사"))
        {
            return new Color32(81, 173, 255, 255); // blue
        }
        if (s.Equals("부사"))
        {
            return new Color32(255, 82, 171, 255); // pink
        }
        if (s.Equals("전치사"))
        {
            return new Color32(90, 255, 250, 255); // cyan
        }
        if (s.Equals("대명사"))
        {
            return new Color32(220, 90, 255, 255); // purple
        }
        if (s.Equals("한정사"))
        {
            return new Color32(255, 218, 90, 255); // orange
        }
        if (s.Equals("타동사"))
        {
            return new Color32(255, 33, 4, 255); // red
        }
        if (s.Equals("자동사"))
        {
            return new Color32(255, 33, 4, 255); // red
        }
        if (s.Equals("접속사"))
        {
            return new Color32(171, 171, 171, 255);  // grey
        }

        if (s.Equals("X"))
        {
            return Color.black;
        }
        return Color.black;
    }
    public static Dictionary<string, string> TrimDesc(string s, string issettrick)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        var list = s.Split(new string[] { "[t]" }, StringSplitOptions.None);
        foreach (var s1 in list)
        {
            if (s1.Equals(""))
            {
                continue;
            }

            string str = s1.Trim();
            str = str.Replace("[x]", ",");
            var pstype = GetPsType(str);
            var desc = GetDesc(str);
#if TEST
            if (issettrick.Equals("true"))
            {
                desc = "<color=red>" + desc + "</color>";
            }
#endif
            if (dic.ContainsKey(desc) == false)
            {
                dic.Add(desc, pstype);
            }
        }
        return dic;
    }
}
