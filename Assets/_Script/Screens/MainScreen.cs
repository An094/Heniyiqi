using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : ScreenBase
{
    [SerializeField] Text MyName;
    [SerializeField] Text PartnerName;
    [SerializeField] Text TogetherDays;
    [SerializeField] Text CurrentCatFood;
    [SerializeField] Button FoodBtn;

    [SerializeField] private Cat Cat;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        UpdateData();
        GameManager.UpdateData += UpdateData;
        FoodBtn.onClick.AddListener(FeedTheCat);
    }

    private void OnDisable()
    {
        GameManager.UpdateData -= UpdateData;
        FoodBtn.onClick.RemoveListener(FeedTheCat);
    }

    private void FeedTheCat()
    {
        GameManager.instance.CurrentCatFood -= 1;
        Cat.Eat();
        int catFood = GameManager.instance.CurrentCatFood;
        CurrentCatFood.text = $"x{catFood}";
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
