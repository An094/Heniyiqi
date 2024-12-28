using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SignInScreen : ScreenBase
{
    [SerializeField] TMPro.TMP_InputField NameInputField;
    [SerializeField] TMPro.TMP_InputField CodeInputField;
    [SerializeField] Button PlayBtn;

    private string Name;
    private string Code;
    private bool IsMale;

    void OnEnable()
    {
        PlayBtn.onClick.AddListener(OnPlayBtnClicked);
    }

    void OnDisable()
    {
        PlayBtn.onClick.RemoveListener(OnPlayBtnClicked);
    }

    private void OnPlayBtnClicked()
    {
        Name = NameInputField.text;
        Code = CodeInputField.text;
        if (Code.Length > 0)
        {
            GameManager.instance.MyName = Name;
            DataPersistenceManager.instance.Initialize(Code, true);
            ScreenManager.Push(ScreenType.MainScreen);
        }
    }

}
