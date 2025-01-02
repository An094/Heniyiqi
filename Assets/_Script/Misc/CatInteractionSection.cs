using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatInteractionSection : MainSection
{
    [SerializeField] private Cat Cat;
    [SerializeField] private Button Notification;
    [SerializeField] private Button QuestNotification;
    [SerializeField] private Image HappyBarFill;

    private int CurrentHappyPoint;
    public override void Initialize(MainScreen mainScreen)
    {
        base.Initialize(mainScreen);

        if(MainScreen.IsShowNotification)
        {
            Notification.gameObject.SetActive(true);
        }
        else
        {
            Notification.gameObject.SetActive(false);
        }

        if(MainScreen.IsShowQuestNotification)
        {
            QuestNotification.gameObject.SetActive(true);
        }
        else
        {
            QuestNotification.gameObject.SetActive(false);
        }

        Cat.Init();
        CurrentHappyPoint = GameManager.instance.CurrentHappyPoint;
        HappyBarFill.fillAmount = Mathf.Clamp01((float)CurrentHappyPoint / 100);
    }

    private void OnEnable()
    {
        Notification.onClick.AddListener(ShowEnterAnswerSection);
        QuestNotification.onClick.AddListener(ShowEnterQuestAnswerSection);
    }

    private void OnDisable()
    {
        Notification.onClick.RemoveListener(ShowEnterAnswerSection);
        QuestNotification.onClick.AddListener(ShowEnterQuestAnswerSection);
    }

    private void ShowEnterAnswerSection()
    {
        MainScreen.Push(MainSectionType.EnterAnswer);
    }

    private void ShowEnterQuestAnswerSection()
    {
        MainScreen.Push(MainSectionType.EnterQuestAnswer);
    }

    public void FeedTheCat()
    {
        GameManager.instance.ChangeCatFood(-1);
        Cat.Eat();
        int NewHappyPoint = GameManager.instance.CurrentHappyPoint;
        DOVirtual.Int(CurrentHappyPoint, NewHappyPoint, 1f,
            (int value) =>
            {
                HappyBarFill.fillAmount = Mathf.Clamp01((float)value / 100);
            }).OnComplete(() => CurrentHappyPoint = NewHappyPoint);
    }
}
