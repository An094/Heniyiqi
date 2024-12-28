using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public SerializableDateTime FirstDay;
    public SerializableDateTime AccCreationDay;
    public ECatState CatState;
    public int CurrentCatFood;
    public int CurrentQuestionId;
    public PersonData Male;
    public PersonData Female;
    public List<QuestionAndAnswer> QuestionsAndAnswers;

    public GameData()
    {
        FirstDay = new SerializableDateTime(DateTime.MinValue);
        AccCreationDay = new SerializableDateTime(DateTime.MinValue);
        CatState = ECatState.InBox;
        CurrentCatFood = 0;
        CurrentQuestionId = 0;
        Male = new();
        Female = new();
        QuestionsAndAnswers = new();
    }
}

[Serializable]
public class SerializableDateTime
{
    public string DateTimeString;

    public SerializableDateTime(DateTime dateTime)
    {
        DateTimeString = dateTime.ToString("o");
    }

    public DateTime ToDateTime()
    {
        return DateTime.Parse(DateTimeString, null, System.Globalization.DateTimeStyles.RoundtripKind);
    }
}

[Serializable]
public class PersonData
{
    public string Name;
    public List<Emotion> Emotions;

    public PersonData()
    {
        Name = "";
        Emotions = new();
    }
}

[Serializable]
public class QuestionAndAnswer
{
    public int QuestionId;
    public bool IsLocked;
    public string Question;
    public string MaleAnswer;
    public string FemaleAnswer;

    public QuestionAndAnswer()
    {
        QuestionId = 0;
        IsLocked = true;
        Question = null;
        MaleAnswer = null;
        FemaleAnswer = null;
    }
}

public enum Emotion
{ 
    Happy,
    Tired,
    Angry,
    Sad,
    Sick,
    Lonely
}

public enum ECatState
{
    InBox,
    Eating,
    Cry,
    Dance,
    Sleep
}