using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    [SerializeField] Text MyName;
    [SerializeField] Text PartnerName;
    [SerializeField] Text TogetherDays;
    [SerializeField] Text CurrentCatFood;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        UpdateData();
        GameManager.UpdateData += UpdateData;
    }

    private void OnDisable()
    {
        GameManager.UpdateData -= UpdateData;
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
