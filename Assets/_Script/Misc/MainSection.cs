using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainSection : MonoBehaviour
{
    protected MainScreen MainScreen;
    public virtual void Initialize(MainScreen mainScreen)
    {
        MainScreen = mainScreen;
    }
}
