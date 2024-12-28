using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionAndAnswersSection : MainSection
{
    [SerializeField] private GameObject QnATemplate;
    [SerializeField] private Transform Content;
    public override void Initialize(MainScreen mainScreen)
    {
        base.Initialize(mainScreen);
        List<QuestionAndAnswer> QnAs = GameManager.instance.QuestionAndAnswers;
        foreach (QuestionAndAnswer QnA in QnAs)
        {
            GameObject QnAObj = GameObject.Instantiate(QnATemplate, Content);
            QuestionItem questionItem = QnAObj.GetComponent<QuestionItem>();
            questionItem.Initialize(QnA.Question, QnA.IsLocked);
        }
    }
}
