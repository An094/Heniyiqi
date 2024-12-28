using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScreen : ScreenBase
{
    [SerializeField] Button SignUpButton;
    [SerializeField] Button SignInButton;
    private void OnEnable()
    {
        SignUpButton.onClick.AddListener(OnSignUpBtnClicked);
        SignInButton.onClick.AddListener(OnSignInBtnClicked);
    }

    private void OnDisable()
    {
        SignUpButton.onClick.RemoveListener(OnSignUpBtnClicked);
        SignInButton.onClick.RemoveListener(OnSignInBtnClicked);
    }

    private void OnSignInBtnClicked()
    {
        ScreenManager.Push(ScreenType.SignIn);
    }

    private void OnSignUpBtnClicked()
    {
        ScreenManager.Push(ScreenType.SignUpSelection);
    }

}
