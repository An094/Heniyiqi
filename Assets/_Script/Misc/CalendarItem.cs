using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarItem : MonoBehaviour
{
    [SerializeField] private Text DayOfMonth;
    [SerializeField] private Image PartnerEmote;
    [SerializeField] private Image MyEmote;
    [SerializeField] private CalendarItemConfig ItemConfig;
    public void Inititalize(int dayOfMonth, int PartnerEmoteId, int MyEmoteId)
    {
        if(dayOfMonth > 0)
        {
            DayOfMonth.text = dayOfMonth.ToString();
            PartnerEmote.sprite = ItemConfig.spriteConfigs[PartnerEmoteId].Sprite;
            MyEmote.sprite = ItemConfig.spriteConfigs[MyEmoteId].Sprite;
        }
        else
        {
            DayOfMonth.text = "";
            PartnerEmote.gameObject.SetActive(false);
            MyEmote.gameObject.SetActive(false);
        }
    }
}
