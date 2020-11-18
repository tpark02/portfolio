using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Michsky.UI.ModernUIPack;

public class PrefabManager : Singleton<PrefabManager>
{
    public GameObject optionPanel;
    public GameObject titlePanel;
    private GameObject title;

    // OX Scene prefabs list
    public GameObject card;
    public GameObject background;
    public GameObject titlepanelForGame;
    //public GameObject questionCount;
    public GameObject resultpanel;
    [HideInInspector] GameObject rpanel;
    public GameObject tutorialpanel;

    [HideInInspector] public GameObject destroyCard = null;
    [HideInInspector] public Stack<GameObject> dpanel = new Stack<GameObject>();
    [HideInInspector] public GameObject uicanvasResult;
    [HideInInspector] public GameObject uicanvas;
    [HideInInspector] public GameObject bg;
    [HideInInspector] public GameObject tbar;
    [HideInInspector] public QuestionCountController count;
    //[HideInInspector] public GameObject questionCnt;
    [HideInInspector] public GameObject tutorial_panel;

    // Sentence Scene prefabs list
    public GameObject sentenceQuestionPanel;
    public GameObject sentenceChoicePanel;
    public GameObject sentenceChoice;
    public int startSentenceIndex = 0;
    //public Sprite[] alphabet = new Sprite[4];

    [HideInInspector] public GameObject destroyQuestionPanel;
    [HideInInspector] public GameObject destroyChoicePanel;
    [HideInInspector] public GameObject choicepanel;

    public static float waitforseconds = 0.2f;
    public static float cardAnimationInterval = 0.5f;

    // sentence result panel
    public GameObject SentenceDescPanel;

    //[HideInInspector] public GameObject sDescPanel;
    // result panel
    public GameObject oxresultItem;
    public GameObject sentenceResultItem;
    public GameObject sentenceVocabItem;
    public GameObject dynGameBGTextBox;
    public GameObject titleForResult;
    // Menu Scene
    public GameObject menuTitlePanel;
    public GameObject menuButtonPanel;
    public GameObject mainMenuButtonPanel;
    [HideInInspector] public GameObject mainmenubtnpanel;
    [HideInInspector] public GameObject menubtnpanel;
    public GameObject menuBackground;
    public GameObject DaySelectPanel;
    [HideInInspector] public GameObject dselectpanel;
    public GameObject DayButton;
    // Study Vocab Scene
    public GameObject studyVocabPanel;
    public int ox_testSize = 0;
    public int sentence_testSize = 0;
    // Study List
    public GameObject myListPanel;
    // Study List
    public GameObject mySentenceListPanel;
    [HideInInspector] GameObject mysentencelistpanel;
    // card desc item
    public GameObject cardDescItem;
    // sentence card desc item
    public GameObject sentenceCardDescItem;
    // sentence DGbox
    public GameObject DGBoxforSentencedef;
    [HideInInspector] public string backButtonStr = string.Empty;
    void Awake()
    {
        SceneManager.activeSceneChanged -= OnSceneLoaded;
        SceneManager.activeSceneChanged += OnSceneLoaded;
        var r = Instance;        
    }

    void Start()
    {
        FileReadWrite.Instance.PrepareUserDataJson();
        SetCanvas();
    }
    public void OnSceneLoaded(Scene scene, Scene s)
    {        
        SetCanvas();
    }
    public void SetCanvas()
    {
        var s = SceneManager.GetActiveScene();
        if (s.name.Equals("OXScene"))
        {
            uicanvas = GameObject.Find("Canvas");
            GameModeManager.SetGameType("OXGame");
            GameModeManager.SetGameFinished(false);
            GameModeManager.SetQuestionSize(ox_testSize);
            SetBackButton("MainScene");
            InitOXScene();
        }
        else if (s.name.Equals("SentenceScene"))
        {
            uicanvas = GameObject.Find("Canvas");
            GameModeManager.SetGameType("SentenceGame");
            GameModeManager.SetGameFinished(false);
            GameModeManager.SetQuestionSize(sentence_testSize);
            SetBackButton("MainScene");
            InitSentenceScene();
        }
        else if (s.name.Equals("ResultScene"))
        {
            uicanvasResult = GameObject.Find("Canvas");
            InitResultScene();
        }
        else if (s.name.Equals("MenuScene"))
        {
            uicanvas = GameObject.Find("Canvas");
            InitMenuScene();
        }
        else if (s.name.Equals("StudyVocabScene"))
        {
            uicanvasResult = GameObject.Find("Canvas");
            InitStudyVocabScene();
        }
        else if (s.name.Equals("MyListScene"))
        {
            GameModeManager.SetGameType("MyList");
            uicanvasResult = GameObject.Find("Canvas");
            InitMyListScene();
        }
        else if (s.name.Equals("MySentenceListScene"))
        {
            GameModeManager.SetGameType("MySentenceList");
            uicanvasResult = GameObject.Find("Canvas");
            InitMySentenceListScene();
        }
    }
    //////////////////////////////////////////////////////////////////
    ///////////////////////// My Sentence List Scene /////////////////
    //////////////////////////////////////////////////////////////////
    void InitMySentenceListScene()
    {
        rpanel = Instantiate(mySentenceListPanel);
        rpanel.transform.SetParent(uicanvasResult.transform, false);
        rpanel.GetComponent<ResultPanel>().SetTitle("파트5 즐겨찾기");
        title = rpanel.GetComponent<ResultPanel>().GetTitle();
    }
    public void ShowMySentenceListPanel()
    {
        SceneManager.LoadScene("Scenes/MySentenceListScene");
    }
    //////////////////////////////////////////////////////////////////
    ///////////////////////// My List Scene //////////////////////////
    //////////////////////////////////////////////////////////////////
    void InitMyListScene()
    {
        rpanel = Instantiate(myListPanel);
        rpanel.transform.SetParent(uicanvasResult.transform, false);
        rpanel.GetComponent<ResultPanel>().SetTitle("단어 즐겨찾기");
        title = rpanel.GetComponent<ResultPanel>().GetTitle();
    }
    public void ShowMyListPanel()
    {
        SceneManager.LoadScene("Scenes/MyListScene");
    }
    //////////////////////////////////////////////////////////////////
    ///////////////////////// Study Vocab Scene //////////////////////
    //////////////////////////////////////////////////////////////////
    void InitStudyVocabScene()
    {
        rpanel = Instantiate(studyVocabPanel);
        rpanel.transform.SetParent(uicanvasResult.transform, false);
        rpanel.GetComponent<ResultPanel>().SetTitle("단어 리스트");
        title = rpanel.GetComponent<ResultPanel>().GetTitle();
        SetBackButton("MainScene");
    }
    //////////////////////////////////////////////////////////////////
    ///////////////////////// Result Scene ///////////////////////////
    //////////////////////////////////////////////////////////////////
    void InitResultScene()
    {
        rpanel = Instantiate(resultpanel);
        rpanel.transform.SetParent(uicanvasResult.transform, false);
        title = rpanel.GetComponent<ResultPanel>().GetTitle();
    }
    //////////////////////////////////////////////////////////////////
    ///////////////////////// Menu Scene /////////////////////////////
    //////////////////////////////////////////////////////////////////
    private void InitMenuScene()
    {
        var r = Instantiate(menuBackground);
        r.transform.SetParent(uicanvas.transform, false);

        title = Instantiate(menuTitlePanel);
        title.GetComponent<TitlePanelController>().SetTitle("토익 단어 & 파트5");
        title.transform.SetParent(r.transform, false);

        mainmenubtnpanel = Instantiate(mainMenuButtonPanel);
        mainmenubtnpanel.transform.SetParent(r.transform, false);

        menubtnpanel = Instantiate(menuButtonPanel);
        menubtnpanel.transform.SetParent(r.transform, false);
        menubtnpanel.gameObject.SetActive(false);

        dselectpanel = Instantiate(DaySelectPanel);
        dselectpanel.transform.SetParent(r.transform, false);
        dselectpanel.gameObject.SetActive(false);

        ActivateBackButton(false);
    }
    public void ShowMenuButtonPanel(string s)
    {
        dselectpanel.SetActive(false);
        mainmenubtnpanel.SetActive(false);
        menubtnpanel.gameObject.SetActive(true);

        if (s.Equals("test") || s.Equals("OXGame") || s.Equals("SentenceGame"))
        {
            title.GetComponent<TitlePanelController>().SetTitle("퀴즈");
            menubtnpanel.GetComponent<MenuButtonPanelController>().ShowTestButtons();
        }
        else if (s.Equals("favorite"))
        {
            title.GetComponent<TitlePanelController>().SetTitle("즐겨찾기");
            menubtnpanel.GetComponent<MenuButtonPanelController>().ShowFavoriteButtons();
        }
        else if (s.Equals("study") || s.Equals("StudyVocab"))
        {
            title.GetComponent<TitlePanelController>().SetTitle("단어");
            menubtnpanel.GetComponent<MenuButtonPanelController>().ShowStudyButtons();

        }

        SetBackButton("MenuPanel");
        ActivateBackButton(true);
    }
    public void BackToMainMenu()
    {
        DOTween.KillAll();

        title.GetComponent<TitlePanelController>().SetTitle("토익 단어 & 파트5");
        ActivateBackButton(false);
        dselectpanel.SetActive(false);
        menubtnpanel.SetActive(false);
        mainmenubtnpanel.SetActive(true);
    }
    public void BackToPreviousMenu()
    {
        DOTween.KillAll();

        string gametype = GameModeManager.GetGameType();
        ShowMenuButtonPanel(gametype);
    }
    //////////////////////////////////////////////////////////////////
    ///////////////////////// Sentence Scene /////////////////////////
    //////////////////////////////////////////////////////////////////
    private void InitSentenceScene()
    {
        bg = Instantiate(background) as GameObject;
        bg.transform.SetParent(uicanvas.transform, false);

        bg.GetComponent<BackgroundController>().timeup.gameObject.SetActive(false);
        bg.GetComponent<BackgroundController>().finish.gameObject.SetActive(false);

        title = bg.GetComponent<BackgroundController>().titlePanelForGame;
        title.GetComponent<TitlePanelController>().title.GetComponent<Text>().text = "Sentence Game";
        //title.transform.SetParent(bg.transform, false);

        //mselect = Instantiate(modeSelectPanel) as GameObject;
        //mselect.transform.SetParent(bg.transform, false);
        //mselect.GetComponent<ModeSelectController>().SetCallBack(StartSentenceGame);
        StartSentenceGame();
    }

    public void StartSentenceGame()
    {
        //DestroyImmediate(title);
        
        //var t = Instantiate(titlepanelForGame) as GameObject;
        //title.transform.SetParent(bg.transform, false);
        tbar = title.transform.Find("Timer").gameObject;
        count = title.transform.Find("Count").GetComponent<QuestionCountController>();
        count.SetCallBack(GameFinishFunc);
#if TEST
        //Sentence_DataLoader.SetSentenceIndex(startSentenceIndex);
        Sentence_DataLoader.ClearData();
        Sentence_DataLoader.PrepareOriginalData();
        Sentence_DataLoader.Sentence_InitList();
#else        
        Sentence_DataLoader.ClearData();
        Sentence_DataLoader.PrepareOriginalData();
        Sentence_DataLoader.Sentence_Shuffle();
        Sentence_DataLoader.Sentence_InitList();
#endif
        
#if TEST
        Sentence_DataLoader.CheckSymbol();
#endif
        // 2020-08-25 여기서 기획 변경. Time Attack 모드로 OX game 일괄 통일.
        // 아래 부분 주석처리함.
        GameModeManager.SetTimeAttackMode();

        CreateSentenceCard();        
    }
    public void CreateSentenceCard()
    {
        if (destroyQuestionPanel)
        {
            Destroy(destroyQuestionPanel);
        }

        if (destroyChoicePanel)
        {
            Destroy(destroyChoicePanel);
        }
        if (bg.GetComponent<BackgroundController>().timeup.gameObject.activeSelf) // 타임 업이 보이면, 더 이상 함수를 진행하지않는다.
        {
            return;
        }

        if (GameModeManager.IsGameFinished()) // 다 풀었다! 게임 끝!
        {
            return;
        }

        var answerchoices = Sentence_DataLoader.GetAnswerChoices();
#if !TEST
        Sentence_DataLoader.Sentence_Choice_Shuffle(answerchoices);
#endif
        string questionstr = Sentence_DataLoader.GetCurrentQuestion();

        var q = Instantiate(sentenceQuestionPanel);
        {
            q.GetComponent<SentenceQuestionPanelController>().question.text = questionstr;
            q.GetComponent<SentenceQuestionPanelController>().SetFontSize(GameModeManager.fontsize);
            q.transform.SetParent(bg.transform, false);
            destroyQuestionPanel = q;
        }

        choicepanel = Instantiate(sentenceChoicePanel);
        choicepanel.transform.SetParent(bg.transform, false);
        destroyChoicePanel = choicepanel;

        for (int i = 0; i < answerchoices.Count; i++)
        {
            var o = Instantiate(sentenceChoice);
            {
                o.GetComponent<SentenceChoiceController>().choice.text = ((char) ('A' + i)).ToString();
                o.GetComponent<SentenceChoiceController>().desc.text = answerchoices[i];
                o.GetComponent<SentenceChoiceController>().SetFontSize(GameModeManager.fontsize);
                o.transform.SetParent(choicepanel.transform, false);
                o.GetComponent<SentenceChoiceController>().SetCallBack(DisableAllChoiceButtons);
            }
        }
        tbar.GetComponent<TimeController>().StartTimerSentence();
        

        bg.GetComponent<BackgroundController>().finish.transform.SetAsLastSibling();
        bg.GetComponent<BackgroundController>().timeup.transform.SetAsLastSibling();
    }

    public void DisableAllChoiceButtons()
    {
        foreach (Transform btn in choicepanel.transform)
        {
            btn.GetComponent<Button>().enabled = false;
        }
    }
    //////////////////////////////////////////////////////////////////
    /////////////////////////// OX Scene /////////////////////////////
    //////////////////////////////////////////////////////////////////
    private void InitOXScene()
    {
        bg = Instantiate(background) as GameObject;
        bg.transform.SetParent(uicanvas.transform, false);

        bg.GetComponent<BackgroundController>().timeup.gameObject.SetActive(false);
        bg.GetComponent<BackgroundController>().finish.gameObject.SetActive(false);

        title = bg.GetComponent<BackgroundController>().titlePanelForGame;
        title.GetComponent<TitlePanelController>().title.GetComponent<Text>().text = "Vocab OX Game";
        //title.transform.SetParent(bg.transform, false);

        StartOXGame();
    }

    public void GameFinishFunc()
    {
        bg.GetComponent<BackgroundController>().finish.gameObject.SetActive(true);
        bg.GetComponent<BackgroundController>().finish.StartShow();
    }
    public void StartOXGame()
    {
        //DestroyImmediate(title);

        //var t = Instantiate(titlepanelForGame) as GameObject;
        //t.transform.SetParent(bg.transform, false);

        tbar = title.GetComponent<TitlePanelController>().timer;
        count = title.GetComponent<TitlePanelController>().count.GetComponent<QuestionCountController>();
        count.SetCallBack(GameFinishFunc);
        //bg.GetComponent<BackgroundController>().count.gameObject.SetActive(false);

        //combo = Instantiate(combopanel) as GameObject;
        //combo.transform.SetParent(bg.transform, false);
        //combo.GetComponent<ComboController>().HideCombo();

        bg.GetComponent<BackgroundController>().finish.transform.SetAsLastSibling();
        bg.GetComponent<BackgroundController>().timeup.transform.SetAsLastSibling();
#if TEST
        OX_DataLoader.ClearData();
        OX_DataLoader.PrepareOriginalData();
        OX_DataLoader.OX_InitWordList();
        CreateCard();
#else
        OX_DataLoader.ClearData();
        OX_DataLoader.PrepareOriginalData();
        OX_DataLoader.OX_Shuffle();
        OX_DataLoader.OX_InitWordList();
        CreateCard();
#endif
        if (GameModeManager.IsTutorialFirstTime())
        {
            tutorial_panel = Instantiate(tutorialpanel);
            tutorial_panel.transform.SetParent(uicanvas.transform, false);
            tutorial_panel.GetComponent<TutorialController>().CradlePanel();
            destroyCard.transform.Find("Panel").GetComponent<CardMove>().TouchEnable(false);
        }

        // 2020-08-25 여기서 기획 변경. Time Attack 모드로 OX game 일괄 통일.
        // 아래 부분 주석처리함.
        GameModeManager.SetTimeAttackMode();
    }

    public void CreateCard()
    {
        if (destroyCard)
        {
            Destroy(destroyCard);
        }

        if (OX_DataLoader.IsIndexOutOfRange())
        {
            Debug.Log("<color=red> DataLoader.cs : CreateCard : index out of range.</color>");
            return;
        }

        if (bg.GetComponent<BackgroundController>().timeup.gameObject.activeSelf) // 타임 업이 보이면, 더 이상 함수를 진행하지않는다.
        {
            return;
        }
        
        if (GameModeManager.IsGameFinished()) // 다 풀었다! 게임 끝!
        {
            return;
        }

        var s = OX_DataLoader.GetCurrentOXData();
        var o = Instantiate(card);
        {
            o.transform.position = Vector3.zero;
            o.transform.localPosition = Vector3.zero;
            o.transform.SetParent(bg.transform, false);
            o.transform.Find("Panel").GetComponent<CardFormatter>().SetVocab(s.Key);
            o.transform.SetAsFirstSibling();
        }

        bg.transform.Find("X").SetSiblingIndex(1);
        bg.transform.Find("O").SetSiblingIndex(2);

        GetComponent<CanvasGroup>().DOFade(0f, 0f);

        if (s.Value.Value[(int) OX_DataLoader.Index.isTrick].Equals("true"))      // set trick
        {
            string trick = OX_DataLoader.OX_GetTrick(s.Key);
            var dic = UIStaticManager.TrimDesc(trick, s.Value.Value[(int) OX_DataLoader.Index.isTrick]);
            o.transform.Find("Panel").GetComponent<CardMove>().isTrickCard = true;
            o.transform.Find("Panel").GetComponent<CardFormatter>().SetDescList(dic);   // set trick desc
        }
        else
        {
            var dic = UIStaticManager.TrimDesc(s.Value.Value[(int)OX_DataLoader.Index.answer], s.Value.Value[(int)OX_DataLoader.Index.isTrick]);
            o.transform.Find("Panel").GetComponent<CardMove>().isTrickCard = false;
            o.transform.Find("Panel").GetComponent<CardFormatter>().SetDescList(dic);
        }

        //if (OX_DataLoader.IsBonusTurn())
        //{
        //    o.transform.Find("Panel").transform.Find("Bonus").gameObject.SetActive(true);
        //    o.transform.Find("Panel").GetComponent<CardMove>().isBonusTimeCard = true;
        //}
#if TEST
        if (o.transform.Find("Panel").transform.Find("Number").gameObject.activeSelf == false)
        {
            o.transform.Find("Panel").transform.Find("Number").gameObject.SetActive(true);
        }

        o.transform.Find("Panel").transform.Find("Number").GetComponent<Text>().text = OX_DataLoader.GetVocabIndex(s.Key).ToString();
#endif
        tbar.GetComponent<TimeController>().StartOXTimer(o);
        if (GameModeManager.IsTurnBaseMode())
        {
            tbar.GetComponent<TimeController>().timeLeft = 0;
        }

        destroyCard = o;
        
        OX_DataLoader.NextOXCard();
        
        int currentIndex = OX_DataLoader.GetCurrentOXIndex();
        GameModeManager.SetGameFinished(currentIndex);
    }
    public void OnCloseDescPanel()
    {
        OnClickClose(dpanel);
    }
    private void OnClickClose(Stack<GameObject> s)
    {
        var g = s.Pop();
        g.SetActive(false);
        Destroy(g);
    }

    public void OXTurnBaseTimesUp()
    {
        if (destroyCard)
        {
            var card = destroyCard.transform.Find("Panel").GetComponent<CardMove>();

            if (card)
            {
                card.TurnBaseTimesUp();
            }
        }
    }
    public void SentenceTurnBaseTimesUp()
    {
        StartCoroutine(TurnBaseDisappearSentence());
    }
    private IEnumerator TurnBaseDisappearSentence()
    {
        tbar.GetComponent<TimeController>().PauseTimer();
        destroyQuestionPanel.GetComponent<CanvasGroup>().alpha = 0f;
        destroyChoicePanel.GetComponent<CanvasGroup>().alpha = 0f;
        bg.GetComponent<BackgroundController>().SetBothInvisible();
        bg.GetComponent<BackgroundController>().ShowX();
        //if (combo)
        //{
        //    OX_DataLoader.combocount = 0;
        //    //combo.GetComponent<ComboController>().HideCombo();
        //}
        yield return new WaitForSeconds(cardAnimationInterval);
        Sentence_DataLoader.NextQuestion();
        CreateSentenceCard();
    }
    //public void OXPenaltyTimer()
    //{
    //    if (GameModeManager.IsTimeAttackMode())
    //    {
    //        tbar.GetComponent<TimeController>().timeLeft += 6f;     // if the answer is wrong, add 4 seconds
    //    }
    //    else if (GameModeManager.IsTurnBaseMode())
    //    {
    //        int timelimit = tbar.GetComponent<TimeController>().timeLimit;
    //        tbar.GetComponent<TimeController>().timeLeft +=  timelimit + 1f;
    //    }
    //}

    public void ShowOptiopnPanel()
    {
        tbar.GetComponent<TimeController>().PauseTimer();
        var o = Instantiate(optionPanel);
        o.transform.SetParent(bg.transform, false);
        o.transform.SetAsLastSibling();
    }

    public List<GameObject> CreatePreSentenceVocab(int id, Transform p)
    {
        string explain = Sentence_DataLoader.GetSentenceListDataById(id).Value[(int)Sentence_DataLoader.Index.explain];
        string answer = Sentence_DataLoader.GetSentenceListDataById(id).Value[(int)Sentence_DataLoader.Index.ans];
        string translate = Sentence_DataLoader.GetSentenceListDataById(id).Value[(int)Sentence_DataLoader.Index.translate];

        GameObject a = null;
        GameObject e = null;
        GameObject t = null;

        if (answer.Equals("") == false)
        {
            a = Instantiate(dynGameBGTextBox);
            answer = "\n" + answer + "\n";
            a.transform.GetComponent<DynamicBGTextBoxController>().SetText(answer);
        }

        if (explain.Equals("") == false)
        {
            e = Instantiate(dynGameBGTextBox);
            explain = "\n" + explain + "\n";
            e.transform.GetComponent<DynamicBGTextBoxController>().SetText(explain);
        }

        if (translate.Equals("") == false)
        {
            t = Instantiate(dynGameBGTextBox);
            translate = "\n" + translate + "\n";
            t.transform.GetComponent<DynamicBGTextBoxController>().SetText(translate);
        }

        if (a)
        {
            a.transform.SetParent(p, false);
            a.GetComponent<CanvasGroup>().alpha = 0f;
        }

        if (e)
        {
            e.transform.SetParent(p, false);
            e.GetComponent<CanvasGroup>().alpha = 0f;
        }

        if (t)
        {
            t.transform.SetParent(p, false);
            t.GetComponent<CanvasGroup>().alpha = 0f;
        }
        return new List<GameObject>() { a, e, t };
    }
    public void CreateSetenceVocab(int id, Transform p, GameObject answerPrefab, GameObject explainPrefab, GameObject translatePrefab) // create setence vocab before so can get the size.
    {
        string explain = Sentence_DataLoader.GetSentenceListDataById(id).Value[(int)Sentence_DataLoader.Index.explain];
        string answer = Sentence_DataLoader.GetSentenceListDataById(id).Value[(int)Sentence_DataLoader.Index.ans];
        string translate = Sentence_DataLoader.GetSentenceListDataById(id).Value[(int)Sentence_DataLoader.Index.translate];

        GameObject a = null;
        GameObject e = null;
        GameObject t = null;
        //float titleHeight = answerPrefab.GetComponent<DynamicBGTextBoxController>().titleTransform.rect.height;
        if (answer.Equals("") == false)
        {
            var title = Instantiate(titleForResult);
            {
                title.transform.GetChild(0).GetComponent<Text>().text = "정     답";
                title.transform.SetParent(p, false);
            }

            a = Instantiate(dynGameBGTextBox);

            answer = "\n" + answer + "\n";
            //a.transform.GetComponent<DynamicBGTextBoxController>().SetTitleText("정답");
            a.transform.GetComponent<DynamicBGTextBoxController>().SetText(answer);

            var answerSize = answerPrefab.GetComponent<DynamicBGTextBoxController>().rTransform.rect;
            //a.GetComponent<RectTransform>().sizeDelta = new Vector2(answerSize.width, answerSize.height + titleHeight);
            a.GetComponent<RectTransform>().sizeDelta = new Vector2(answerSize.width, answerSize.height);
            a.GetComponent<CanvasGroup>().alpha = 1f;
            a.transform.SetParent(p, false);
        }
        if (translate.Equals("") == false)
        {
            var title = Instantiate(titleForResult);
            {
                title.transform.GetChild(0).GetComponent<Text>().text = "번     역";
                title.transform.SetParent(p, false);
            }

            t = Instantiate(dynGameBGTextBox);
            translate = "\n" + translate + "\n";

            //t.transform.GetComponent<DynamicBGTextBoxController>().SetTitleText("번역");
            t.transform.GetComponent<DynamicBGTextBoxController>().SetText(translate);

            var translateSize = translatePrefab.GetComponent<DynamicBGTextBoxController>().rTransform.rect;
            //t.GetComponent<RectTransform>().sizeDelta = new Vector2(translateSize.width, translateSize.height + titleHeight);
            t.GetComponent<RectTransform>().sizeDelta = new Vector2(translateSize.width, translateSize.height);
            t.GetComponent<CanvasGroup>().alpha = 1f;
            t.transform.SetParent(p, false);
        }
        if (explain.Equals("") == false)
        {
            var title = Instantiate(titleForResult);
            {
                title.transform.GetChild(0).GetComponent<Text>().text = "해     설";
                title.transform.SetParent(p, false);
            }

            e = Instantiate(dynGameBGTextBox);
            explain = "\n" + explain + "\n";

            //e.transform.GetComponent<DynamicBGTextBoxController>().SetTitleText("해설");
            e.transform.GetComponent<DynamicBGTextBoxController>().SetText(explain);

            var explainSize = explainPrefab.GetComponent<DynamicBGTextBoxController>().rTransform.rect;
            //e.GetComponent<RectTransform>().sizeDelta = new Vector2(explainSize.width, explainSize.height + titleHeight);
            e.GetComponent<RectTransform>().sizeDelta = new Vector2(explainSize.width, explainSize.height);
            e.GetComponent<CanvasGroup>().alpha = 1f;
            e.transform.SetParent(p, false);
        }
        rpanel.GetComponent<ResultPanel>().TurnOffDGBox();
    }
    public List<GameObject> CreatePreVocabDesc(string v, Transform p)
    {
        string def = string.Empty;
        string e1 = string.Empty;
        string t1 = string.Empty;
        string e2 = string.Empty;
        string t2 = string.Empty;
        string sym = string.Empty;
        string aym = string.Empty;
        string gametype = GameModeManager.GetGameType();
        
        if (gametype.Equals("OXGame") || gametype.Equals("MyList") || gametype.Equals("StudyVocab"))
        {
            def = OX_DataLoader.GetOXDataByVocab(v).Value[(int)OX_DataLoader.Index.answer];
            e1 = OX_DataLoader.GetOXDataByVocab(v).Value[(int)OX_DataLoader.Index.e1];
            t1 = OX_DataLoader.GetOXDataByVocab(v).Value[(int)OX_DataLoader.Index.t1];
            //e2 = OX_DataLoader.GetOXDataByVocab(v).Value[(int)OX_DataLoader.Index.e2];
            //t2 = OX_DataLoader.GetOXDataByVocab(v).Value[(int)OX_DataLoader.Index.t2];
            sym = OX_DataLoader.GetOXDataByVocab(v).Value[(int)OX_DataLoader.Index.sym];
            aym = OX_DataLoader.GetOXDataByVocab(v).Value[(int)OX_DataLoader.Index.aym];
        }
        else if (gametype.Equals("SentenceGame") || gametype.Equals("MySentenceList"))
        {
            def = Sentence_DataLoader.GetVocabDatabyVocab(v).Value[(int)OX_DataLoader.Index.answer];
            e1 = Sentence_DataLoader.GetVocabDatabyVocab(v).Value[(int)OX_DataLoader.Index.e1];
            t1 = Sentence_DataLoader.GetVocabDatabyVocab(v).Value[(int)OX_DataLoader.Index.t1];
            //e2 = Sentence_DataLoader.GetVocabDatabyVocab(v).Value[(int)OX_DataLoader.Index.e2];
            //t2 = Sentence_DataLoader.GetVocabDatabyVocab(v).Value[(int)OX_DataLoader.Index.t2];
            sym = Sentence_DataLoader.GetVocabDatabyVocab(v).Value[(int)OX_DataLoader.Index.sym];
            aym = Sentence_DataLoader.GetVocabDatabyVocab(v).Value[(int)OX_DataLoader.Index.aym];
        }

        GameObject d = null;
        GameObject e = null;
        GameObject s = null;
        GameObject a = null;

        if (def.Equals("") == false)
        {
            d = Instantiate(DGBoxforSentencedef);
            var panel = d.GetComponent<DGBoxForSentenceDefController>().panel;
            var deflist = UIStaticManager.FormatDescDef(def);
            foreach (var s1 in deflist)
            {
                if (s1.Equals(""))
                {
                    continue;
                }

                var item = Instantiate(sentenceCardDescItem);
                {
                    item.GetComponent<SentenceCardDescController>().SetPsType(s1.Key);
                    item.GetComponent<SentenceCardDescController>().SetDesc(s1.Value);
                    item.GetComponent<SentenceCardDescController>().SetAlpah(0f);
                }
                item.transform.SetParent(panel.transform, false);
            }
        }

        if (e1.Equals("") == false 
            || t1.Equals("") == false
            || e2.Equals("") == false
            || t2.Equals("") == false )
        {
            e = Instantiate(dynGameBGTextBox);
            string exampleSentence = "\n" + e1 + "\n" + t1 + "\n" + e2 + "\n" + t2 + "\n";
            e.transform.GetComponent<DynamicBGTextBoxController>().SetText(exampleSentence);
        }

        if (sym.Equals("") == false)
        {
            s = Instantiate(dynGameBGTextBox);
            sym = "\n" + sym + "\n";
            s.transform.GetComponent<DynamicBGTextBoxController>().SetText(sym);
        }

        if (aym.Equals("") == false)
        {
            a = Instantiate(dynGameBGTextBox);
            aym = "\n" + aym + "\n";
            a.transform.GetComponent<DynamicBGTextBoxController>().SetText(aym);
        }

        if (d)
        {
            d.transform.SetParent(p, false);
            d.GetComponent<CanvasGroup>().alpha = 0f;
        }

        if (e)
        {
            e.transform.SetParent(p, false);
            e.GetComponent<CanvasGroup>().alpha = 0f;
        }
        if (s)
        {
            s.transform.SetParent(p, false);
            s.GetComponent<CanvasGroup>().alpha = 0f;
        }
        if (a)
        {
            a.transform.SetParent(p, false);
            a.GetComponent<CanvasGroup>().alpha = 0f;
        }
        return new List<GameObject>() { d, e, s, a };
    }

    public void CreateVocabDesc(string v, Transform p, GameObject defPrefab, GameObject examplePrefab,
        GameObject symPrefab, GameObject aymPrefab)
    {
        string def = string.Empty;
        string e1 = string.Empty;
        string t1 = string.Empty;
        string e2 = string.Empty;
        string t2 = string.Empty;
        string sym = string.Empty;
        string aym = string.Empty;

        string gametype = GameModeManager.GetGameType();
        if (gametype.Equals("OXGame") || gametype.Equals("MyList") || gametype.Equals("StudyVocab"))
        {
             def = OX_DataLoader.GetOXDataByVocab(v).Value[(int)OX_DataLoader.Index.answer];
             e1 = OX_DataLoader.GetOXDataByVocab(v).Value[(int)OX_DataLoader.Index.e1];
             t1 = OX_DataLoader.GetOXDataByVocab(v).Value[(int)OX_DataLoader.Index.t1];
             //e2 = OX_DataLoader.GetOXDataByVocab(v).Value[(int)OX_DataLoader.Index.e2];
             //t2 = OX_DataLoader.GetOXDataByVocab(v).Value[(int)OX_DataLoader.Index.t2];
             sym = OX_DataLoader.GetOXDataByVocab(v).Value[(int)OX_DataLoader.Index.sym];
             aym = OX_DataLoader.GetOXDataByVocab(v).Value[(int)OX_DataLoader.Index.aym];
        }
        else if (gametype.Equals("SentenceGame") || gametype.Equals("MySentenceList"))
        {
             def = Sentence_DataLoader.GetVocabDatabyVocab(v).Value[(int)OX_DataLoader.Index.answer];
             e1 = Sentence_DataLoader.GetVocabDatabyVocab(v).Value[(int)OX_DataLoader.Index.e1];
             t1 = Sentence_DataLoader.GetVocabDatabyVocab(v).Value[(int)OX_DataLoader.Index.t1];
             //e2 = Sentence_DataLoader.GetVocabDatabyVocab(v).Value[(int)OX_DataLoader.Index.e2];
             //t2 = Sentence_DataLoader.GetVocabDatabyVocab(v).Value[(int)OX_DataLoader.Index.t2];
             sym = Sentence_DataLoader.GetVocabDatabyVocab(v).Value[(int)OX_DataLoader.Index.sym];
             aym = Sentence_DataLoader.GetVocabDatabyVocab(v).Value[(int)OX_DataLoader.Index.aym];
        }
        

        GameObject d = null;
        GameObject e = null;
        GameObject s = null;
        GameObject a = null;
        if (def.Equals("") == false)
        {
            var title = Instantiate(titleForResult);
            {
                title.transform.GetChild(0).GetComponent<Text>().text = "정     의";
                title.transform.SetParent(p, false);
            }

            d = Instantiate(DGBoxforSentencedef);
            var panel = d.GetComponent<DGBoxForSentenceDefController>().panel;
            var deflist = UIStaticManager.FormatDescDef(def);
            foreach (var s1 in deflist)
            {
                if (s1.Equals(""))
                {
                    continue;
                }

                var item = Instantiate(sentenceCardDescItem);
                {
                    item.GetComponent<SentenceCardDescController>().SetPsType(s1.Key);
                    item.GetComponent<SentenceCardDescController>().SetDesc(s1.Value);
                    item.GetComponent<SentenceCardDescController>().SetAlpah(1f);
                }
                item.transform.SetParent(panel.transform, false);
            }
            var defsize = defPrefab.GetComponent<DGBoxForSentenceDefController>().panel.GetComponent<RectTransform>()
                .rect;
            d.GetComponent<RectTransform>().sizeDelta = new Vector2(defsize.width, defsize.height);
            d.GetComponent<CanvasGroup>().alpha = 1f;
            d.transform.SetParent(p, false);
        }

        if (e1.Equals("") == false
            || t1.Equals("") == false
            || e2.Equals("") == false
            || t2.Equals("") == false)
        {
            var title = Instantiate(titleForResult);
            {
                title.transform.GetChild(0).GetComponent<Text>().text = "예     문";
                title.transform.SetParent(p, false);
            }

            e = Instantiate(dynGameBGTextBox);

            string exampleSentence = "\n" + e1 + "\n" + "<color=#9d9d9d>" + t1 +"</color>"+ "\n\n" + e2 + "\n" + "<color=#9d9d9d>" + t2 + "</color>" + "\n";
            //a.transform.GetComponent<DynamicBGTextBoxController>().SetTitleText("정답");
            e.transform.GetComponent<DynamicBGTextBoxController>().SetText(exampleSentence);

            var exampleSize = examplePrefab.GetComponent<DynamicBGTextBoxController>().rTransform.rect;
            e.GetComponent<RectTransform>().sizeDelta = new Vector2(exampleSize.width, exampleSize.height);
            e.GetComponent<CanvasGroup>().alpha = 1f;
            e.transform.SetParent(p, false);
        }

        if (sym.Equals("") == false)
        {
            var title = Instantiate(titleForResult);
            {
                title.transform.GetChild(0).GetComponent<Text>().text = "유  의  어";
                title.transform.SetParent(p, false);
            }

            s = Instantiate(dynGameBGTextBox);

            sym = "\n" + sym + "\n";
            //a.transform.GetComponent<DynamicBGTextBoxController>().SetTitleText("정답");
            s.transform.GetComponent<DynamicBGTextBoxController>().SetText(sym);

            var symsize = symPrefab.GetComponent<DynamicBGTextBoxController>().rTransform.rect;
            s.GetComponent<RectTransform>().sizeDelta = new Vector2(symsize.width, symsize.height);
            s.GetComponent<CanvasGroup>().alpha = 1f;
            s.transform.SetParent(p, false);
        }

        if (aym.Equals("") == false)
        {
            var title = Instantiate(titleForResult);
            {
                title.transform.GetChild(0).GetComponent<Text>().text = "반  의  어";
                title.transform.SetParent(p, false);
            }

            a = Instantiate(dynGameBGTextBox);

            aym = "\n" + aym + "\n";
            //a.transform.GetComponent<DynamicBGTextBoxController>().SetTitleText("정답");
            a.transform.GetComponent<DynamicBGTextBoxController>().SetText(aym);

            var aymsize = aymPrefab.GetComponent<DynamicBGTextBoxController>().rTransform.rect;
            a.GetComponent<RectTransform>().sizeDelta = new Vector2(aymsize.width, aymsize.height);
            a.GetComponent<CanvasGroup>().alpha = 1f;
            a.transform.SetParent(p, false);
        }
        rpanel.GetComponent<ResultPanel>().TurnOffDGBox();
    }
    public GameObject InstantiateSentenceDescPanel()
    {
        //sDescPanel = Instantiate(SentenceDescPanel);
        //return sDescPanel;
        return Instantiate(SentenceDescPanel);
    }
    
    public void ShowDaySelectPanel(string gameType)
    {
        GameModeManager.SetGameType(gameType);

        menubtnpanel.SetActive(false);

        int daybtnsize = GameModeManager.dayButtonSize;
        dselectpanel.GetComponent<DaySelectPanelController>().ResetDayButtons();
        dselectpanel.GetComponent<DaySelectPanelController>().ResetDayButtonCheck();
        dselectpanel.GetComponent<DaySelectPanelController>().ResetUnlockBGColor();
        string gametype = GameModeManager.GetGameType();
        if (gametype.Equals("StudyVocab"))
        {
            title.GetComponent<TitlePanelController>().SetTitle("단어 리스트");
            dselectpanel.GetComponent<DaySelectPanelController>().SetDayButtonsForStudyVocab();
        }        
        else
        {
            if (gametype.Equals("OXGame"))
            {
                title.GetComponent<TitlePanelController>().SetTitle("OX 퀴즈");
            }
            else if (gametype.Equals("SentenceGame"))
            {
                title.GetComponent<TitlePanelController>().SetTitle("Sentence");
            }
            dselectpanel.GetComponent<DaySelectPanelController>().SetDayButtonUnlock(daybtnsize, gametype);
            dselectpanel.GetComponent<DaySelectPanelController>().SetDayButtonAlphaValue(daybtnsize);
            dselectpanel.GetComponent<DaySelectPanelController>().SetDayButtonCheck(daybtnsize, gametype);
        }
              
        dselectpanel.gameObject.SetActive(true);
    }

    public void SetBackButton(string s)
    {
        backButtonStr = s;
    }
    public void ClickBackButton()
    {
        ActivateBackButton(true);
        if (backButtonStr.Equals("DaySelectMenu"))
        {
            BackToPreviousMenu();
        }
        else if (backButtonStr.Equals("MenuPanel"))
        {
            BackToMainMenu();
        }
        else if (backButtonStr.Equals("MainMenu"))
        {
            ActivateBackButton(false);
        }
        else if (backButtonStr.Equals("MainScene"))
        {
            DOTween.KillAll();
            SceneManager.LoadScene("Scenes/MenuScene");
        }
    }

    public void ActivateBackButton(bool b)
    {
        title.GetComponent<TitlePanelController>().goBackbutton.gameObject.SetActive(b);
    }
}
