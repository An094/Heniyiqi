using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterCodeScreen : MonoBehaviour
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
        if(Code.Length > 0)
        {

            IsMale = Code[Code.Length - 1].Equals("M");
            GameManager.instance.AmIMale = IsMale;

            GameManager.instance.MyName = Name;

            DataPersistenceManager.instance.Initialize(Code, IsMale);

            DataPersistenceManager.instance.SaveGame();
            GameManager.instance.ShowMainScreen();
        }
    }
}
