using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CalendarItemConfig", menuName = "SO/CalendarItemConfig")]
public class CalendarItemConfig : ScriptableObject
{
    public List<SpriteConfig> spriteConfigs;
}

[Serializable]
public class SpriteConfig
{
    public int Index;
    public Sprite Sprite;
}