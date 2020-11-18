using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class GameModeManager
{
    private static string _fontsizestr = "medium";
    public static string fontsizestr // medium = 30
    {
        get => _fontsizestr;
        set
        {
            if (value.Equals("small"))
            {
                fontsize = 25;
                _fontsizestr = "small";
            }
            else if (value.Equals("medium"))
            {
                fontsize = 30;
                _fontsizestr = "medium";
            }
            else if (value.Equals("large"))
            {
                fontsize = 35;
                _fontsizestr = "larget";
            }
            else
            {
                fontsize = 30;
                _fontsizestr = "medium";
            }
        }
    }
    public static int fontsize = 30;
    //private static int timelimit = 0;
    private static bool isTimeAttack = false;
    private static bool isTurnBase = false;

    private static string _gameType = string.Empty;
    public static string gameType
    {
        get => _gameType;
        set
        {
            if (value.Equals("OXGame"))
            {
                _dayButtonSize = 20;                
            }
            else if (value.Equals("SentenceGame"))
            {
                _dayButtonSize = 20;
            }
            else if (value.Equals("StudyVocab"))
            {
                _dayButtonSize = 20;
            }
            else
            {
                _dayButtonSize = -1;
            }
            _gameType = value;
        }
    }
    private static int currentDay = 0;
    //private static float bonusTime = 5f;
    private static int userSelectedQuestionSize = 0;
    private static bool isGameFinished = false;
    private static bool isTutorialFirstTime = true;
    private static int _dayButtonSize = 0;
    public static int dayButtonSize
    {
        get => _dayButtonSize;
        set
        {
            _dayButtonSize = value;
        }        
    }
    
    public static bool IsTutorialFirstTime()
    {
        return isTutorialFirstTime;
    }
    public static void SetTutorialFirstTime(bool b)
    {
        isTutorialFirstTime = b;
    }
    public static bool IsGameFinished()
    {
        return isGameFinished;
    }

    public static void SetGameFinished(bool b)
    {
        isGameFinished = b;
    }
    public static void SetGameFinished(int n)
    {
        isGameFinished = userSelectedQuestionSize - 1 < n;
    }
    public static int GetQuestionSize()
    {
        return userSelectedQuestionSize;
    }
    public static void SetQuestionSize(int n)
    {
        userSelectedQuestionSize = n;
    }
    //public static float GetBonusTime()
    //{
    //    return bonusTime;
    //}
    public static void SetTimeAttackMode()
    {
        isTimeAttack = true;
        isTurnBase = false;
    }
    public static void SetTurnBaseMode()
    {
        isTimeAttack = false;
        isTurnBase = true;
    }

    public static bool IsTimeAttackMode()
    {
        return isTimeAttack;
    }
    public static bool IsTurnBaseMode()
    {
        return isTurnBase;
    }

    public static void SetGameType(string s)
    {
        gameType = s;
    }

    public static string GetGameType()
    {
        return gameType;
    }

    public static void SetDay(int n)
    {
        currentDay = n;
    }

    public static int GetCurrentDay()
    {
        return currentDay;
    }

    public static List<int> GetCurrentDayRange(string gametype)
    {
        int st = 0;
        int end = 0;
        List<int> r = new List<int>();
        int questionSize = 0;
        
        if (gametype.Equals("OXGame"))
        {
            questionSize = 100;
        }
        else if (gametype.Equals("SentenceGame"))
        {
            questionSize = 30;
        }
        
        if (currentDay == 1)
        {
            r.Add(0);
            r.Add(100);
        }
        else
        {
            st = ((currentDay - 1) * questionSize);
            end = currentDay * questionSize;
            r.Add(st);
            r.Add(end);
        }

        return r;
    }
}