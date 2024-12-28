using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatInteractionSection : MainSection
{
    [SerializeField] private Cat Cat;
    [SerializeField] private Button Notification;
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
        GameManager.instance.CurrentCatFood -= 1;
        Cat.Eat();
    }
}
