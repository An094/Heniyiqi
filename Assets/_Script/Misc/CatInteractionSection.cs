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

        Cat.Init();
        CurrentHappyPoint = GameManager.instance.CurrentHappyPoint;
        HappyBarFill.fillAmount = Mathf.Clamp01((float)CurrentHappyPoint / 100);
    }

    private void OnEnable()
    {
        Notification.onClick.AddListener(ShowEnterAnswerSection);
    }

    private void OnDisable()
    {
        Notification.onClick.RemoveListener(ShowEnterAnswerSection);
    }

    private void ShowEnterAnswerSection()
    {
        MainScreen.Push(MainSectionType.EnterAnswer);
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
