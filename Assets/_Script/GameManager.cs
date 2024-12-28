using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private ScreenManager ScreenManager;
    [SerializeField] public List<QuestionAndAnswer> QuestionAndAnswers;

    public bool AmIMale { get; set; }
    public string MyName { get; set; }
    public string MyParterName { get; set; }
    public SerializableDateTime FirstDay { get; set; }
    public SerializableDateTime AccCreationDay { get; set; }
    public int CurrentCatFood { get; set; }
    
    public static GameManager instance { get; private set; }

    public static event Action UpdateData;
    public event Action<int> ShowNotifcation;

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
        ShowQuestionNotification();
    }

    public void ShowQuestionNotification()
    {
        //TODO
        ShowNotifcation.Invoke(0);
    }

    void Start()
    {
        ScreenManager.Initialize(ScreenType.Selection);
    }
}
