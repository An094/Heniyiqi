using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestAnswerSection : MainSection
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

    public void SetData(int questId)
    {
        QuestData quest = GameManager.instance.QuestData[questId];
        Question.text = quest.Quest;
        MyName.text = GameManager.instance.MyName;
        PartnerName.text = GameManager.instance.MyParterName;
        if (GameManager.instance.AmIMale)
        {
            MyAnswer.text = quest.MaleFeeling;
            PartnerAnswer.text = quest.FemaleFeeling;
        }
        else
        {
            MyAnswer.text = quest.FemaleFeeling;
            PartnerAnswer.text = quest.MaleFeeling;
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
        MainScreen.Push(MainSectionType.QuestList);
    }
}
