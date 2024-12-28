using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private List<TabButton> TabButtons;
    private TabButton SelectedTab;

    private void Start()
    {
        foreach (TabButton tab in TabButtons)
        {
            tab.TabGroup = this;
            tab.SetState(TabBtnState.Normal);
            tab.OnTabClicked += OnTabClicked;
        }
        SelectedTab = TabButtons.FirstOrDefault();
        SelectedTab.SetState(TabBtnState.Select);
    }

    private void OnDestroy()
    {
        foreach (TabButton tab in TabButtons)
        {
            tab.OnTabClicked -= OnTabClicked;
        }
    }

    private void ResetTabs()
    {
        TabButtons.ForEach(p => p.SetState(TabBtnState.Normal));
    }

    private void OnTabClicked(TabButton tab)
    {
        ResetTabs();
        TabButtons.Find(tabBtn => tabBtn == tab)?.SetState(TabBtnState.Select);
    }
}
