using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterQuestAnswerSection : MainSection
{

    [SerializeField] private Button CloseBtn;
    [SerializeField] private Text Question;
    [SerializeField] private Text MyName;
    [SerializeField] private Text PartnerName;
    [SerializeField] private Text PartnerAnswer;
    [SerializeField] private TMPro.TMP_InputField MyAnswer;

    private int CurrentQuestId;
    private bool AmIMale;
    public override void Initialize(MainScreen mainScreen)
    {
        base.Initialize(mainScreen);

        CurrentQuestId = MainScreen.QuestId;

        QuestData currentQuest = GameManager.instance.QuestData[CurrentQuestId];
        Question.text = currentQuest.Quest;
        MyName.text = GameManager.instance.MyName;
        PartnerName.text = GameManager.instance.MyParterName;
        AmIMale = GameManager.instance.AmIMale;
        if (AmIMale)
        {
            PartnerAnswer.text = currentQuest.FemaleFeeling;
        }
        else
        {
            PartnerAnswer.text = currentQuest.MaleFeeling;
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
        if (myAnswer != null && myAnswer.Length > 0)
        {
            QuestData Quest = GameManager.instance.QuestData[CurrentQuestId];

            if (AmIMale)
            {
                Quest.MaleFeeling = myAnswer;
            }
            else
            {
                Quest.FemaleFeeling = myAnswer;
            }

            Quest.IsLocked = false;
            if (Quest.MaleFeeling.Length > 0 && Quest.FemaleFeeling.Length > 0)
            {
                GameManager.instance.ChangeCatFood(1);
            }
            DataPersistenceManager.instance.SaveGame();
            MainScreen.IsShowQuestNotification = false;
            MainScreen.Push(MainSectionType.CatInteraction);
        }
        else
        {
            MainScreen.Push(MainSectionType.CatInteraction);
        }
    }
}
