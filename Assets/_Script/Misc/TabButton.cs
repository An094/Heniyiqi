using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum TabBtnState
{
    //Hover,
    Select,
    Normal
}

public class TabButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Color NormalColor;

    public TabGroup TabGroup { get; set; }
    private Image TabImg;
    public event Action<TabButton> OnTabClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnTabClicked(this);
    }

    public void SetState(TabBtnState tabBtnState)
    {
        TabImg = gameObject.GetComponent<Image>();
        switch (tabBtnState)
        {
            case TabBtnState.Select:
                {
                    TabImg.color = Color.white;
                }
                break;
            case TabBtnState.Normal:
                {
                    TabImg.color = NormalColor;
                }
                break;
        }
    }
}
