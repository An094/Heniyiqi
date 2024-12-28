using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignUpSelectionScreen : ScreenBase
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
        ScreenManager.Push(ScreenType.EnterCode);
    }

    private void OnCreateAccountBtnClicked()
    {
        ScreenManager.Push(ScreenType.CreateAcc);
    }
}
