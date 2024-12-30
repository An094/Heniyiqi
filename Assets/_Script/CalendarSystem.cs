using System;
using UnityEngine;

public class CalendarSystem : MonoBehaviour
{
    public int day;
    public int month;
    public int year;

    public DateTime CurrentDate;

    void OnEnable()
    {
        CurrentDate = DateTime.Now;
        UpdateDate();
    }

    void UpdateDate()
    {
        day = CurrentDate.Day;
        month = CurrentDate.Month;
        year = CurrentDate.Year;
    }

    public void NextDay()
    {
        CurrentDate = CurrentDate.AddDays(1);
        UpdateDate();
    }

    public void PreviousDay()
    {
        CurrentDate = CurrentDate.AddDays(-1);
        UpdateDate();
    }

    public void NextMonth()
    {
        CurrentDate = CurrentDate.AddMonths(1);
        UpdateDate();
    }

    public void PreviousMonth()
    {
        CurrentDate = CurrentDate.AddMonths(-1);
        UpdateDate();
    }

    public string GetFormattedDate()
    {
        return CurrentDate.ToString("dd/MM/yyyy");
    }

    public string GetMonthYear()
    {
        return CurrentDate.ToString("MMMM yyyy");
    }
}
