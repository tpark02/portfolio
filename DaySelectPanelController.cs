using UnityEngine;
using UnityEngine.UI;

public class DaySelectPanelController : MonoBehaviour
{
    public GameObject grid;
    public int totalButtonCnt = 20;
    private void Awake()
    {
        InitDayButtons();
    }    
    public void SetDayButtonAlphaValue(int n)
    {
        for (int i = 0; i < totalButtonCnt; i++)
        {
            var g = grid.transform.GetChild(i);
            g.GetComponent<CanvasGroup>().alpha = 0f;
        }

        for (int i = 0; i < n; i++)
        {
            var g = grid.transform.GetChild(i);
            g.GetComponent<CanvasGroup>().alpha = 1f;
        }
    }

    public void SetDayButtonsForStudyVocab()
    {
        for (int i = 0; i < totalButtonCnt; i++)
        {
            var g = grid.transform.GetChild(i);
            g.GetComponent<CanvasGroup>().alpha = 1f;
            g.GetComponent<DayButtonController>().SetUnLock();
        }
    }

    public void ResetDayButtons()
    {
        for (int i = 0; i < totalButtonCnt; i++)
        {
            var g = grid.transform.GetChild(i);
            if (i == 0)
            {
                g.GetComponent<DayButtonController>().SetUnLock();
            }
            else
            {
                g.GetComponent<DayButtonController>().SetLock();
            }

            g.GetComponent<CanvasGroup>().alpha = 0f;
        }
    }

    private void InitDayButtons()
    {
        for (int i = 0; i < totalButtonCnt; i++)
        {
            var g = Instantiate(PrefabManager.Instance.DayButton);
            g.transform.SetParent(grid.transform, false);
            g.GetComponent<DayButtonController>().SetDayText((i + 1).ToString());
            if (i == 0)
            {
                g.GetComponent<DayButtonController>().SetUnLock();
            }
            else
            {
                g.GetComponent<DayButtonController>().SetLock();
            }

            g.GetComponent<CanvasGroup>().alpha = 0f;
        }
    }

    public void SetDayButtonUnlock(int n, string gametype)
    {
        for (int i = 0; i < n; i++)
        {
            var g = grid.transform.GetChild(i);
            if (i == 0)
            {
                g.GetComponent<DayButtonController>().StartUnlockAnimation();
                g.GetComponent<DayButtonController>().SetUnLock();
            }
            else
            {
                UserData d = null;
                if (gametype.Equals("OXGame"))
                {
                    d = UserDataManager.Instance.GetOXProgressListByIndex(i + 1);
                }
                else if (gametype.Equals("SentenceGame"))
                {
                    d = UserDataManager.Instance.GetSentenceProgressListByIndex(i + 1);
                }

                if (d.isUnlock.Equals("true"))
                {
                    int lastindex = -1;
                    if (gametype.Equals("OXGame"))
                    {
                        lastindex = UserDataManager.Instance.OxProgressListTrueCount() - 1;
                    }
                    else if (gametype.Equals("SentenceGame"))
                    {
                        lastindex = UserDataManager.Instance.SentenceProgressListTrueCount() - 1;
                    }

                    if (i == lastindex)
                    {
                        grid.transform.GetChild(0).GetComponent<DayButtonController>().StopUnlockAnimation();
                        g.GetComponent<DayButtonController>().StartUnlockAnimation();
                    }

                    g.GetComponent<DayButtonController>().SetUnLock();

                }
                else
                {
                    g.GetComponent<DayButtonController>().SetLock();
                }
            }
        }
    }

    public void ResetDayButtonCheck()
    {
        for (int i = 0; i < totalButtonCnt; i++)
        {
            var g = grid.transform.GetChild(i);
            g.GetComponent<DayButtonController>().SetCheck(false);
        }
    }

    public void SetDayButtonCheck(int n, string gametype)
    {
        for (int i = 0; i < n; i++)
        {
            UserData data = null;
            if (gametype.Equals("OXGame"))
            {
                data = UserDataManager.Instance.GetOXProgressListByIndex(i + 1);
            }
            else if (gametype.Equals("SentenceGame"))
            {
                data = UserDataManager.Instance.GetSentenceProgressListByIndex(i + 1);
            }

            if (data.isCheck.Equals("true"))
            {
                var g = grid.transform.GetChild(i);
                g.GetComponent<DayButtonController>().SetCheck(true);
            }
        }
    }

    public void ResetUnlockBGColor()
    {
        for (int i = 0; i < totalButtonCnt; i++)
        {
            var g = grid.transform.GetChild(i);
            g.GetComponent<DayButtonController>().ResetUnlockBGColor();
        }
    }

}
