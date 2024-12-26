using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterCodeScreen : MonoBehaviour, IDataPersistence  
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
            DataPersistenceManager.instance.Initialize(Code, false);
            DataPersistenceManager.instance.SaveGame();
        }
    }

    public void LoadData(GameData data)
    {
        if (data.Male.Name.Length > 0)
        {
            IsMale = false;
        }
    }

    public void SaveData(GameData data)
    {
        if (gameObject.activeSelf)
        {
            if (IsMale)
            {
                data.Male.Name = Name;
            }
            else
            {
                data.Female.Name = Name;
            }
        }    

    }
}
