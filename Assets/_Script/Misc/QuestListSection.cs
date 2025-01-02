using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListSection : MainSection
{
    [SerializeField] private GameObject QuestTemplate;
    [SerializeField] private Transform Content;

    public override void Initialize(MainScreen mainScreen)
    {
        base.Initialize(mainScreen);
        List<QuestData> Quests = GameManager.instance.QuestData;
        foreach(QuestData quest in Quests)
        {
            GameObject questObj = Instantiate(QuestTemplate, Content);
            QuestItem questItem = questObj.GetComponent<QuestItem>();
            questItem.Initialize(quest.Quest, quest.IsLocked, quest.QuestId, MainScreen);
        }
    }
}
