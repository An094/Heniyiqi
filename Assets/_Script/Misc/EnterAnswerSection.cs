using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterAnswerSection : MainSection
{
    [SerializeField] private Button CloseBtn;
    [SerializeField] private Text Question;
    [SerializeField] private Text MyName;
    [SerializeField] private Text PartnerName;
    [SerializeField] private Text PartnerAnswer;
    [SerializeField] private TMPro.TMP_InputField MyAnswer;

    private int CurrentQuestionId;
    private bool AmIMale;
    public override void Initialize(MainScreen mainScreen)
    {
        base.Initialize(mainScreen);

        CurrentQuestionId = MainScreen.QuestionId;

        Question.text = GameManager.instance.QuestionAndAnswers[CurrentQuestionId].Question;
        MyName.text = GameManager.instance.MyName;
        PartnerName.text = GameManager.instance.MyParterName;
        AmIMale  =  GameManager.instance.AmIMale;
        if (AmIMale)
        {
            PartnerAnswer.text = GameManager.instance.QuestionAndAnswers[CurrentQuestionId].FemaleAnswer;
        }
        else
        {
            PartnerAnswer.text = GameManager.instance.QuestionAndAnswers[CurrentQuestionId].MaleAnswer;
        }
    }

    private void OnEnable()
    {
        CloseBtn.onClick.AddListener(OnCloseClicked);
    }

    private void OnDisable()
    {
        CloseBtn.onClick.RemoveListener(OnCloseClicked);
    }

    private void OnCloseClicked()
    {
        string myAnswer = MyAnswer.text;
        if(myAnswer != null && myAnswer.Length > 0)
        {
            QuestionAndAnswer QnA = GameManager.instance.QuestionAndAnswers[CurrentQuestionId];

            if(AmIMale)
            {
                QnA.MaleAnswer = myAnswer;
            }
            else
            {
                QnA.FemaleAnswer = myAnswer;
            }

            QnA.IsLocked = false;
            if(QnA.MaleAnswer.Length > 0 && QnA.FemaleAnswer.Length > 0)
            {
                GameManager.instance.ChangeCatFood(1);
            }
            DataPersistenceManager.instance.SaveGame();
            MainScreen.IsShowNotification = false;
            MainScreen.Push(MainSectionType.CatInteraction);
        }
        else
        {
            MainScreen.Push(MainSectionType.CatInteraction);
        }
    }
}
