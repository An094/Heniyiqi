using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionItem : MonoBehaviour
{
    [SerializeField] private Text Question;
    [SerializeField] private Image LockIcon;
    public void Initialize(string inQuestion, bool isLock)
    {
        if(isLock)
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
}
