using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScreenBase : MonoBehaviour
{
    protected ScreenManager ScreenManager;
    public void Initialize(ScreenManager screenManager)
    {
        ScreenManager = screenManager;
    }
}
