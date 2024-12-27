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
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
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
        int day = GameManager.instance.GetTogetherDays();
        TogetherDays.text = "Together for " + day + "days";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
