using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public enum MainSectionType
{
    CatInteraction,
    QnAList,
    EnterAnswer
}

[Serializable]
public struct MainSectionConfig
{
    public MainSectionType MainSectionType;
    public GameObject MainSectionPrefab;
}

public class MainScreen : ScreenBase
{
    [SerializeField] List<MainSectionConfig> MainSections;
    [SerializeField] Text MyName;
    [SerializeField] Text PartnerName;
    [SerializeField] Text TogetherDays;
    [SerializeField] Text CurrentCatFood;
    [SerializeField] Button FoodBtn;
    [SerializeField] TabGroup TabGroup;
    [SerializeField] Transform MainSectionTransform;

    public bool IsShowNotification { get; set; } = false;
    public int QuestionId { get; set; } = 0;

    private GameObject CurrentSection;
    public void Push(MainSectionType mainSectionType)
    {
        if (MainSectionTransform.childCount > 0)
        {
            var child = MainSectionTransform.GetChild(MainSectionTransform.childCount - 1);
            Destroy(child.gameObject);
        }
        var sectionConfig = MainSections.FirstOrDefault(p => p.MainSectionType == mainSectionType);
        CurrentSection = Instantiate(sectionConfig.MainSectionPrefab, MainSectionTransform);
        CurrentSection.GetComponent<MainSection>()?.Initialize(this);
    }

    private void OnEnable()
    {
        UpdateData();
        GameManager.instance.ShowNotifcation += ShowNotification;
        GameManager.UpdateData += UpdateData;
        FoodBtn.onClick.AddListener(FeedTheCat);
        TabGroup.OnTabChanged += OnTabChanged;

        TabGroup.Initialize();
        Push(MainSectionType.CatInteraction);

        GameManager.instance.BeginGamePlay();
    }

    private void OnDisable()
    {
        GameManager.UpdateData -= UpdateData;
        TabGroup.OnTabChanged -= OnTabChanged;
        FoodBtn.onClick.RemoveListener(FeedTheCat);
    }
    private void OnTabChanged(int tabIndex)
    {
        switch (tabIndex)
        {
            case 0:
                {
                    Push(MainSectionType.CatInteraction);
                }
                break;
            case 1:
                {
                    Push(MainSectionType.QnAList);
                }
                break;
        }
    }

    private void FeedTheCat()
    {
        CatInteractionSection catInteractionSection = CurrentSection.GetComponent<CatInteractionSection>();
        if (catInteractionSection)
        {
            catInteractionSection.FeedTheCat();
            int catFood = GameManager.instance.CurrentCatFood;
            CurrentCatFood.text = $"x{catFood}";
        }
    }

    private void UpdateData()
    {
        MyName.text = GameManager.instance.MyName;
        PartnerName.text = GameManager.instance.MyParterName;
        int catFood = GameManager.instance.CurrentCatFood;
        CurrentCatFood.text = $"x{catFood}";
        int day = GameManager.instance.GetTogetherDays();
        TogetherDays.text = day.ToString();
    }

    public void ShowNotification(int questionId)
    {
        TabGroup.SetSelectionTab(0);
        IsShowNotification = true;
        QuestionId = questionId;
        Push(MainSectionType.CatInteraction);
    }
}
