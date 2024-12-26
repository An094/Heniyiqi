using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScreen : MonoBehaviour
{
    [SerializeField] private Button CreateAccoutBtn;
    [SerializeField] private Button EnterCodeBtn;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        CreateAccoutBtn.onClick.AddListener(OnCreateAccountBtnClicked);
        EnterCodeBtn.onClick.AddListener(OnEnterCodeBtnClicked);
    }

    private void OnDisable()
    {
        CreateAccoutBtn.onClick.RemoveListener(OnCreateAccountBtnClicked);
        EnterCodeBtn.onClick.RemoveListener(OnEnterCodeBtnClicked);
    }

    private void OnEnterCodeBtnClicked()
    {
        GameManager.instance.ShowEnterCodeScreen();
    }

    private void OnCreateAccountBtnClicked()
    {
        GameManager.instance.ShowSignUpScreen();
    }
}
