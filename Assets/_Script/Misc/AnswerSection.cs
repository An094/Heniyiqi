using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerSection : MainSection
{
    [SerializeField] private Button CloseBtn;
    [SerializeField] private Text Question;
    [SerializeField] private Text MyName;
    [SerializeField] private Text MyAnswer;
    [SerializeField] private Text PartnerName;
    [SerializeField] private Text PartnerAnswer;
    public override void Initialize(MainScreen mainScreen)
    {
        base.Initialize(mainScreen);
    }

    public void SetData(int questionId)
    {
        QuestionAndAnswer questionAndAnswer = GameManager.instance.QuestionAndAnswers[questionId];
        Question.text = questionAndAnswer.Question;
        MyName.text = GameManager.instance.MyName;
        PartnerName.text = GameManager.instance.MyParterName;
        if(GameManager.instance.AmIMale)
        {
            MyAnswer.text = questionAndAnswer.MaleAnswer;
            PartnerAnswer.text = questionAndAnswer.FemaleAnswer;
        }
        else
        {
            MyAnswer.text = questionAndAnswer.FemaleAnswer;
            PartnerAnswer.text = questionAndAnswer.MaleAnswer;
        }
    }

    private void OnEnable()
    {
        CloseBtn.onClick.AddListener(OnCloseBtnClicked);
    }

    private void OnDisable()
    {
        CloseBtn.onClick.RemoveListener(OnCloseBtnClicked);
    }

    private void OnCloseBtnClicked()
    {
        MainScreen.Push(MainSectionType.QnAList);
    }
}
