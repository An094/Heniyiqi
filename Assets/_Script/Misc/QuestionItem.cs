using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestionItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Text Question;
    [SerializeField] private Image LockIcon;
    private int QuestionId;
    private MainScreen MainScreen;
    private bool IsLocked;
    public void Initialize(string inQuestion, bool isLock, int questionId, MainScreen mainScreen)
    {
        MainScreen = mainScreen;
        QuestionId = questionId;
        IsLocked = isLock;
        if (isLock)
        {
            Question.gameObject.SetActive(false);
            LockIcon.gameObject.SetActive(true);
        }
        else
        {
            Question.text = inQuestion;
            Question.gameObject.SetActive(true);
            LockIcon.gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsLocked)
        {
            MainScreen.ShowAnswerOfQuestion(QuestionId);
        }
    }
}
