using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ScreenType
{
    Selection,
    SignUpSelection,
    SignIn,
    CreateAcc,
    EnterCode,
    MainScreen
}

[Serializable]
public struct ScreenConfig
{
    public ScreenType ScreenType;
    public GameObject Screen;
}

public class ScreenManager : MonoBehaviour
{
    public List<ScreenConfig> ScreenConfigs;

    public void Initialize(ScreenType screenType)
    {
        AddScreen(screenType);
    }

    public void Push(ScreenType screenType)
    {
        var lastChild = transform.GetChild(transform.childCount - 1);
        Destroy(lastChild.gameObject);
        AddScreen(screenType);
    }

    private void AddScreen(ScreenType screenType)
    {
        var screenConfig = ScreenConfigs.FirstOrDefault(p => p.ScreenType == screenType);
        GameObject screenObj = Instantiate(screenConfig.Screen, transform);
        screenObj.GetComponent<ScreenBase>().Initialize(this);
    }
}
