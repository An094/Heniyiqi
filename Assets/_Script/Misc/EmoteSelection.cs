using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteSelection : MainSection
{
    [SerializeField] private Emote Emote;
    [SerializeField] private CalendarItemConfig calendarItemConfig;
    [SerializeField] private Transform EmotesParentTransform;

    public override void Initialize(MainScreen mainScreen)
    {
        base.Initialize(mainScreen);

        foreach(var config in calendarItemConfig.spriteConfigs)
        {
            Emote emote = Instantiate(Emote, EmotesParentTransform);
            emote.Initialize(config.Index, config.Sprite);
        }
    }

}
