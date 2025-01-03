using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SignUpScreen : ScreenBase
{
    [Header("Login")]
    [SerializeField] TMPro.TMP_InputField NameInputField;
    [SerializeField] Button GenerateBtn;
    [SerializeField] Toggle ToggleMale;
    [SerializeField] TMPro.TMP_Text Id;
    [SerializeField] Button PlayBtn;
    [SerializeField] TMPro.TMP_Dropdown DropDownDay;
    [SerializeField] TMPro.TMP_Dropdown DropDownMonth;
    [SerializeField] TMPro.TMP_Dropdown DropDownYear;

    private bool IsMale;

    private string HashedName;

    private string Name;
    private SerializableDateTime FirstDay;
    private SerializableDateTime AccCreationDay;

    private void OnEnable()
    {
        GenerateBtn.onClick.AddListener(OnBtnSubmitClicked);
        PlayBtn.onClick.AddListener(OnPlayBtnClicked);
    }

    private void OnDisable()
    {
        GenerateBtn.onClick.RemoveListener(OnBtnSubmitClicked);
        PlayBtn.onClick.RemoveListener(OnPlayBtnClicked);
    }

    private void OnPlayBtnClicked()
    {
        DataPersistenceManager.instance.SaveGame();
        ScreenManager.Push(ScreenType.MainScreen);
    }

    private void OnBtnSubmitClicked()
    {
        Name = NameInputField.text;

        if (Name.Length > 0)
        {
            HashedName = GenerateHash(Name);
            IsMale = ToggleMale.isOn;
            HashedName += IsMale ? "M" : "F";

            Id.text = HashedName;
            Id.transform.gameObject.SetActive(true);
            GenerateBtn.gameObject.SetActive(false);
            PlayBtn.gameObject.SetActive(true);


            int day = DropDownDay.value + 1;
            int month = DropDownMonth.value + 1;
            int year = DropDownYear.value + 2016;

            DateTime firstDay = new DateTime(year, month, day);
            FirstDay = new SerializableDateTime(firstDay);
            AccCreationDay = new SerializableDateTime(DateTime.Now);

            DataPersistenceManager.instance.Initialize(HashedName, IsMale);

            GameManager.instance.AmIMale = IsMale;
            GameManager.instance.FirstDay = FirstDay;
            GameManager.instance.AccCreationDay = AccCreationDay;
            GameManager.instance.MyName = Name;
        }

    }

    private string GenerateHash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            string base64String = Convert.ToBase64String(hashBytes);
            return base64String.Substring(0, 3);
        }
    }
}
