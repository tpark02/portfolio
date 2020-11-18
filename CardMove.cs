using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardMove : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startpos;
    private Vector3 offset;
    private bool isCardMoving = false;
    
    public bool isMoving = true;

    [HideInInspector] public bool isStartFinished = false;
    [HideInInspector] public bool isTrickCard = false;
    [HideInInspector] public bool isBonusTimeCard = false;
    void Start()
    {
        StartCoroutine(StartAnimation());
    }
    IEnumerator StartAnimation()
    {
        GetComponent<CanvasGroup>().DOFade(1f, 0.1f);
        yield return new WaitWhile(() => GetComponent<CanvasGroup>().alpha < 1f);
        isStartFinished = true;
    }

    public void TurnBaseTimesUp()
    {
        if (PrefabManager.Instance.bg.GetComponent<BackgroundController>().isOXshow)
        {
            Debug.Log("<color=red> OX is show !!!! </color>");
            return;
        }

        if (isCardMoving)
        {
            Debug.Log("<color=red> card is moving !!! </color>");
            return;
        }

        isCardMoving = true;
        PrefabManager.Instance.tbar.GetComponent<TimeController>().PauseTimer();
        StartCoroutine(TurnBaseDisappearCard());
    }
    private IEnumerator TurnBaseDisappearCard()
    {
        GetComponent<CanvasGroup>().alpha = 0f;
        PrefabManager.Instance.bg.GetComponent<BackgroundController>().SetBothInvisible();

        string vocab = GetComponent<CardFormatter>().vocabforrecord;
        OX_DataLoader.OX_SetRecord(vocab, false);
        
        PrefabManager.Instance.bg.GetComponent<BackgroundController>().ShowX();
        
        OX_DataLoader.combocount = 0;
        //PrefabManager.Instance.combo.GetComponent<ComboController>().HideCombo();

        yield return new WaitForSeconds(PrefabManager.cardAnimationInterval);

        PrefabManager.Instance.CreateCard();
    }
    private IEnumerator DisappearCardLeft()
    {
        transform.DOLocalMove(new Vector2(-3000f, transform.localPosition.y), PrefabManager.waitforseconds);
        
        yield return new WaitForSeconds(PrefabManager.cardAnimationInterval);
        
        //isCardMoving = false;
        GetComponent<CanvasGroup>().alpha = 0f;
        PrefabManager.Instance.bg.GetComponent<BackgroundController>().SetBothInvisible();

        bool isCorrect = false;

        //PrefabManager.Instance.count.GetComponent<QuestionCountController>().OXIncreaseCount();
        
        if (isTrickCard)        // score
        {
            //PrefabManager.Instance.score.GetComponent<ScoreController>().OX_IncreaseScore();
            UserDataManager.Instance.IncreaseUserOXScore();
            OX_DataLoader.combocount++;
            isCorrect = true;

            //SetBonusTimer();
        }
        else    // if the answer is wrong ...
        {
            //PrefabManager.Instance.OXPenaltyTimer();
            OX_DataLoader.combocount = 0;
            //PrefabManager.Instance.combo.GetComponent<ComboController>().HideCombo();
        }

        string vocab = GetComponent<CardFormatter>().vocabforrecord;

        if (isCorrect)
        {
            PrefabManager.Instance.bg.GetComponent<BackgroundController>().ShowO();
            OX_DataLoader.OX_SetRecord(vocab, true);
        }
        else
        {
            PrefabManager.Instance.bg.GetComponent<BackgroundController>().ShowX();
            OX_DataLoader.OX_SetRecord(vocab, false);
        }


        //if (isCorrect && OX_DataLoader.combocount >= 2)
        //{
        //    PrefabManager.Instance.combo.GetComponent<ComboController>().SetCombo(OX_DataLoader.combocount - 1);
        //}

        yield return new WaitForSeconds(PrefabManager.cardAnimationInterval);

        int currentIndex = OX_DataLoader.GetCurrentOXIndex();
        
        PrefabManager.Instance.count.OXIncreaseCount(currentIndex);

        PrefabManager.Instance.CreateCard();

        Destroy(this.gameObject);
    }

    public void TouchEnable(bool isEnable)
    {
        GetComponent<BoxCollider2D>().enabled = isEnable;
    }
    
    private IEnumerator DisappearCardRight()
    {
        transform.DOLocalMove(new Vector2(3000f, transform.localPosition.y), PrefabManager.waitforseconds);
        
        yield return new WaitForSeconds(PrefabManager.cardAnimationInterval);
        
        //isCardMoving = false;
        GetComponent<CanvasGroup>().alpha = 0f;
        PrefabManager.Instance.bg.GetComponent<BackgroundController>().SetBothInvisible();

        bool isCorrect = false;

        //PrefabManager.Instance.count.GetComponent<QuestionCountController>().OXIncreaseCount();

        if (isTrickCard == false)       // score
        {
            //PrefabManager.Instance.score.GetComponent<ScoreController>().OX_IncreaseScore();
            UserDataManager.Instance.IncreaseUserOXScore();
            OX_DataLoader.combocount++;
            isCorrect = true;
            
            //SetBonusTimer();
        }
        else    // if the answer is wrong...
        {
            //PrefabManager.Instance.OXPenaltyTimer();
            OX_DataLoader.combocount = 0;
            //PrefabManager.Instance.combo.GetComponent<ComboController>().HideCombo();
        }

        string vocab = GetComponent<CardFormatter>().vocabforrecord;

        if (isCorrect)
        {
            PrefabManager.Instance.bg.GetComponent<BackgroundController>().ShowO();
            OX_DataLoader.OX_SetRecord(vocab, true);
        }
        else
        {
            PrefabManager.Instance.bg.GetComponent<BackgroundController>().ShowX();
            OX_DataLoader.OX_SetRecord(vocab, false);
        }

        //if (isCorrect && OX_DataLoader.combocount >= 2)
        //{
        //    PrefabManager.Instance.combo.GetComponent<ComboController>().SetCombo(OX_DataLoader.combocount - 1);
        //}

        yield return new WaitForSeconds(PrefabManager.cardAnimationInterval);

        int currentIndex = OX_DataLoader.GetCurrentOXIndex();
        PrefabManager.Instance.count.OXIncreaseCount(currentIndex);
        PrefabManager.Instance.CreateCard();
        Destroy(this.gameObject);
    }

    //private void SetBonusTimer()
    //{
    //    if (GameModeManager.IsTimeAttackMode())
    //    {
    //        if (isBonusTimeCard)
    //        {
    //            PrefabManager.Instance.tbar.GetComponent<TimeController>().timeLeft -= GameModeManager.GetBonusTime();     // if bonus time card, subtract 3 seconds
    //        }
    //    }
    //}
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        if (isMoving == false)
        {
            PrefabManager.Instance.OnCloseDescPanel();
            return;
        }
        startpos = transform.position;
        offset = startpos - Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 0f));
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isMoving == false)
        {
            PrefabManager.Instance.OnCloseDescPanel();
            return;
        }

        if (Input.touchCount > 1 || isCardMoving)
        {
            return;
        }

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 0f)) + offset;
    }

    public void SetCardMoving(bool b)
    {
        isCardMoving = b;
    }
    public bool IsCardMoving()
    {
        return isCardMoving;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isMoving == false)
        {
            PrefabManager.Instance.OnCloseDescPanel();
            return;
        }

        offset = Vector3.zero;

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPosition.x > Screen.width - (Screen.width / 10))
        {
            Debug.Log("x : " + screenPosition.x);
            if (isCardMoving == false)
            {
                isCardMoving = true;
                PrefabManager.Instance.tbar.GetComponent<TimeController>().PauseTimer();
                StartCoroutine(DisappearCardRight());
                return;
            }
        }
        if (screenPosition.x < Screen.width / 10)
        {
            Debug.Log("x : " + screenPosition.x);
            if (isCardMoving == false)
            {
                isCardMoving = true;
                PrefabManager.Instance.tbar.GetComponent<TimeController>().PauseTimer();
                StartCoroutine(DisappearCardLeft());
                return;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            transform.position = Vector2.MoveTowards(transform.localPosition, new Vector2(0f, 0f), 10f);
            transform.localPosition = Vector2.zero;
            GetComponent<Image>().color = Color.white;
        }
    }
}
