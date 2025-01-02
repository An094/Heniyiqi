using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarItem : MonoBehaviour
{
    [SerializeField] private Text DayOfMonth;
    [SerializeField] private Image PartnerEmote;
    [SerializeField] private Image MyEmote;
    [SerializeField] private Image Background;
    [SerializeField] private CalendarItemConfig ItemConfig;

    private void Awake()
    {
        DayOfMonth.text = "";
        PartnerEmote.gameObject.SetActive(false);
        MyEmote.gameObject.SetActive(false);
        Background.enabled = false;
    }

    public void Inititalize(int dayOfMonth, int PartnerEmoteId, int MyEmoteId)
    {
        Background.enabled = true;
        DayOfMonth.text = dayOfMonth.ToString();
        if (PartnerEmoteId >= 0)
        {
            PartnerEmote.gameObject.SetActive(true);
            MyEmote.gameObject.SetActive(true);
            PartnerEmote.sprite = ItemConfig.spriteConfigs[PartnerEmoteId].Sprite;
            MyEmote.sprite = ItemConfig.spriteConfigs[MyEmoteId].Sprite;
        }
    }
}
