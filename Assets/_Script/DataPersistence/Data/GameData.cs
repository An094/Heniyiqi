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
    public int CurrentScore;
    public int CurrentQuestionId;
    public PersonData Male;
    public PersonData Female;

    public GameData()
    {
        FirstDay = new SerializableDateTime(DateTime.MinValue);
        AccCreationDay = new SerializableDateTime(DateTime.MinValue);
        CurrentScore = 0;
        CurrentQuestionId = 0;
        Male = new();
        Female = new();
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
    public List<QuestionAndAnswer> QuestionAndAnswers;
    public List<Emotion> Emotions;

    public PersonData()
    {
        Name = "";
        QuestionAndAnswers = new();
        Emotions = new();
    }
}

[Serializable]
public class QuestionAndAnswer
{
    public string Question;
    public string Answer;
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
