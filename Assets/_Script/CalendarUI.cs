using System;
using UnityEngine;
using UnityEngine.UI;

public class CalendarUI : MainSection
{
    public Text monthYearText;
    public CalendarSystem calendarSystem;
    public GameObject dayButtonPrefab;
    public Transform daysContainer;

    void Start()
    {
        UpdateCalendarUI();
    }

    public void OnNextMonthButtonClicked()
    {
        calendarSystem.NextMonth();
        UpdateCalendarUI();
    }

    public void OnPreviousMonthButtonClicked()
    {
        calendarSystem.PreviousMonth();
        UpdateCalendarUI();
    }

    void UpdateCalendarUI()
    {
        monthYearText.text = calendarSystem.GetMonthYear();
        GenerateDays();
    }

    void GenerateDays()
    {
        foreach (Transform child in daysContainer)
        {
            Destroy(child.gameObject);
        }

        DateTime firstDayOfMonth = new DateTime(calendarSystem.year, calendarSystem.month, 1);
        int daysInMonth = DateTime.DaysInMonth(calendarSystem.year, calendarSystem.month);
        DateTime today = calendarSystem.CurrentDate;
        int daysOfMonth = today.Day;

        int startDay = (int)firstDayOfMonth.DayOfWeek;
        for (int i = 0; i < startDay; i++)
        {
            Instantiate(dayButtonPrefab, daysContainer);
        }

        for (int i = 1; i <= daysInMonth; i++)
        {
            GameObject dayButton = Instantiate(dayButtonPrefab, daysContainer);
            //dayButton.GetComponentInChildren<Text>().text = i.ToString();
            int day = i;
            dayButton.GetComponent<Button>().onClick.AddListener(() => OnDayButtonClicked(day));
            if (i <= daysOfMonth)
            {
                int partnerEmote = UnityEngine.Random.Range(0, 12);
                int myEmote = UnityEngine.Random.Range(0, 12);
                dayButton.GetComponent<CalendarItem>().Inititalize(i, partnerEmote, myEmote);
            }
            else
            {
                dayButton.GetComponent<CalendarItem>().Inititalize(i, -1, -1);
            }
        }
    }

    void OnDayButtonClicked(int day)
    {
        //calendarSystem.CurrentDate = new DateTime(calendarSystem.year, calendarSystem.month, day);
        //UpdateCalendarUI();
        MainScreen.ShowEmoteSelection();
    }
}
