using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private ScreenManager ScreenManager;
    [SerializeField] public List<QuestionAndAnswer> QuestionAndAnswers;
    [SerializeField] public List<QuestData> QuestData;

    public bool AmIMale { get; set; }
    public string MyName { get; set; }
    public string MyParterName { get; set; }
    public SerializableDateTime FirstDay { get; set; }
    public SerializableDateTime AccCreationDay { get; set; }
    public int CurrentCatFood { get; private set; }
    public int CurrentHappyPoint { get; set; }
    public ECatState CatState { get; set; }

    public static GameManager instance { get; private set; }

    public static event Action UpdateData;
    public event Action<int> ShowNotifcation;
    public event Action<int> QuestNotification;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one GameManager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        FileMonitor.FileModified += OnFileChanged;
    }

    private void OnDisable()
    {
        FileMonitor.FileModified -= OnFileChanged;
    }

    private void OnFileChanged()
    {
        DataPersistenceManager.instance.LoadGame();
        UpdateData?.Invoke();
    }

    public void LoadData(GameData data)
    {
        FirstDay = data.FirstDay;
        AccCreationDay = data.AccCreationDay;
        CurrentCatFood = data.CurrentCatFood;

        if(data.Male.Name == MyName && MyName != null)
        {
            AmIMale = true;
            MyParterName = data.Female.Name;
        }
        else if(data.Female.Name == MyName && MyName != null)
        {
            AmIMale = false;
            MyParterName = data.Male.Name;
        }
        else
        {
            if (MyName != null && MyName.Length > 0)
            {
                MyParterName = AmIMale ? data.Female.Name : data.Male.Name;
            }
        }

        if(data.QuestionsAndAnswers.Count > 0)
        {
            foreach(var question in data.QuestionsAndAnswers)
            {
                QuestionAndAnswer QnA = QuestionAndAnswers.First(qna => qna.Question == question.Question);
                if(QnA != null)
                {
                    QnA.IsLocked = question.IsLocked;
                    QnA.MaleAnswer = question.MaleAnswer;
                    QnA.FemaleAnswer = question.FemaleAnswer;
                    QnA.Question = question.Question;
                    QnA.QuestionId = question.QuestionId;
                }
            }
        }

        if(data.Quests.Count > 0)
        {
            foreach(var quest in data.Quests)
            {
                QuestData Quest = QuestData.First(q => q.Quest == quest.Quest);
                if(Quest != null)
                {
                    Quest.IsLocked = quest.IsLocked;
                    Quest.QuestId = quest.QuestId;
                    Quest.Quest = quest.Quest;
                    Quest.MaleFeeling = quest.MaleFeeling;
                    Quest.FemaleFeeling = quest.FemaleFeeling;
                }
            }
        }

        if (data.HappyPoint == 0 && data.CatState == 0)
        {
            CurrentHappyPoint = 50;
        }
        else
        {
            CurrentHappyPoint = data.HappyPoint;
        }

        CatState = data.CatState;
    }

    public void SaveData(GameData data)
    {
        if (AmIMale)
        {
            data.Male.Name = MyName;
            data.Female.Name = MyParterName;
        }
        else
        {
            data.Female.Name = MyName;
            data.Male.Name = MyParterName;
        }

        data.FirstDay = FirstDay;
        data.AccCreationDay = AccCreationDay;
        data.CurrentCatFood = CurrentCatFood;

        data.QuestionsAndAnswers.Clear();
        foreach (var qna in QuestionAndAnswers)
        {
            data.QuestionsAndAnswers.Add(qna);
        }

        data.Quests.Clear();
        foreach (var quest in QuestData)
        {
            data.Quests.Add(quest);
        }

        data.CatState = CatState;
        data.HappyPoint = CurrentHappyPoint;
    }

    public void ChangeCatFood(int changedValue)
    {
        if(CurrentCatFood + changedValue >= 0)
        {
            CurrentCatFood += changedValue;
        }
        UpdateData?.Invoke();
    }

    public int GetTogetherDays()
    {
        DateTime now = DateTime.Now;
        DateTime firstDay = FirstDay.ToDateTime();
        TimeSpan duration = now - firstDay;
        return duration.Days;
    }

    public void BeginGamePlay()
    {
        int currentQuestionId = GetCurrentQuestionId();
        int currentQuestId = GetCurrentQuestId();

        DateTime now = DateTime.Now;
        DateTime currentDay = now.Date;
        DateTime accCreationDay = AccCreationDay.ToDateTime().Date;
        TimeSpan difference = currentDay - accCreationDay;
        int differenceDays = difference.Days;

        if(differenceDays >= currentQuestionId)
        {
            bool hasAnswered = false;
            if(AmIMale)
            {
                hasAnswered = QuestionAndAnswers[currentQuestionId].MaleAnswer.Length > 0;
            }
            else
            {
                hasAnswered = QuestionAndAnswers[currentQuestionId].FemaleAnswer.Length > 0;
            }

            if(!hasAnswered)
            {
                ShowQuestionNotification(currentQuestionId);
            }
        }

        if(differenceDays >= currentQuestId)
        {
            bool hasAnswered = false;
            if (AmIMale)
            {
                hasAnswered = QuestData[currentQuestId].MaleFeeling.Length > 0;
            }
            else
            {
                hasAnswered = QuestData[currentQuestId].FemaleFeeling.Length > 0;
            }

            if (!hasAnswered)
            {
                ShowQuestNotification(currentQuestId);
            }
        }
    }

    public int GetCurrentQuestionId()
    {
        QuestionAndAnswer questionAndAnswer = QuestionAndAnswers.First(p => p.MaleAnswer.Length == 0 || p.FemaleAnswer.Length == 0);
        if(questionAndAnswer != null)
        {
            return questionAndAnswer.QuestionId;
        }
        return 0;
    }

    public int GetCurrentQuestId()
    {
        QuestData quest = QuestData.First(p => p.MaleFeeling.Length == 0 || p.FemaleFeeling.Length == 0);
        if(quest != null)
        {
            return quest.QuestId;
        }
        return 0;
    }

    public void ShowQuestionNotification(int QuestionId)
    {
        ShowNotifcation.Invoke(QuestionId);
    }

    public void ShowQuestNotification(int QuestId)
    {
        QuestNotification.Invoke(QuestId);
    }

    void Start()
    {
        ScreenManager.Initialize(ScreenType.Selection);
    }
}
