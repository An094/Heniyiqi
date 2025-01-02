using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Text Quest;
    [SerializeField] private Image LockIcon;
    private int QuestId;
    private MainScreen MainScreen;
    private bool IsLocked;
    public void Initialize(string inQuest, bool isLock, int questionId, MainScreen mainScreen)
    {
        MainScreen = mainScreen;
        QuestId = questionId;
        IsLocked = isLock;
        if (isLock)
        {
            Quest.gameObject.SetActive(false);
            LockIcon.gameObject.SetActive(true);
        }
        else
        {
            Quest.text = inQuest;
            Quest.gameObject.SetActive(true);
            LockIcon.gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsLocked)
        {
            MainScreen.ShowAnswerOfQuest(QuestId);
        }
    }
}
