using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalendarController : MonoBehaviour
{
    public TextMeshProUGUI dateText;
    public DailyCalendarController dailyCalendarController;
    public int currentMonth;
    public int currentYear;

    public Button[] daysButtons = new Button[35];
    public List<References.DaysInCalendar> daysIndex = new List<References.DaysInCalendar>();

    void Start()
    {
        Init();
    }

    void Init() {
        GetCurrentMonthAndYear();
        ClearAndRedraw();
    }

    void GetCurrentMonthAndYear()
    {
        currentMonth = System.DateTime.UtcNow.ToLocalTime().Month - 1;
        currentYear = System.DateTime.UtcNow.ToLocalTime().Year;
    }

    void UpdateDateText(int _currentMonth, int _currentYear)
    {
        string monthName = References.instance.monthsNames[_currentMonth];

        dateText.text = $"{monthName} {_currentYear}";
    }

    public void NextPage()
    {
        if (currentMonth >= 11) // Next year
        {
            currentYear++;
            currentMonth = 0;
        } else     //Same year
            currentMonth++;

        ClearAndRedraw();
    }
    public void PreviousPage()
    {
        if (currentMonth <= 0) // previous year
        {
            currentYear--;
            currentMonth = 11;
        }
        else//Same year
            currentMonth--;

        ClearAndRedraw();
    }

    void ClearAndRedraw() {
        CleardaysIndex();
        UpdateDateText(currentMonth, currentYear);
        DrawCalendar();
    }

    void CleardaysIndex() {
        daysIndex.Clear();
    }

    public void ClickOnTheDay(GameObject buttonHasClicked) {
        int day = int.Parse(buttonHasClicked.name);
        int daysOfMonths = 0;
        for (int i = 0; i < currentMonth; i++)
        {
            daysOfMonths += dailyCalendarController.daysInMonths[i];
        }
        
        int dayIndex = daysOfMonths + day;

        dailyCalendarController.SelectADayInCalendar(dayIndex);
    }

    void DrawCalendar() {
        int firstDay = dailyCalendarController.firstDay[currentMonth];
        int j = 0;

        for (int i = 0; i < daysButtons.Length; i++)
        {
            if (i >= firstDay && i < dailyCalendarController.daysInMonths[currentMonth] + firstDay)
            {
                daysButtons[i].gameObject.GetComponent<TextMeshProUGUI>().text = $"{i + 1 - firstDay}";
                daysButtons[i].gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
                daysButtons[i].interactable = true;

                daysIndex.Add(new References.DaysInCalendar(j, daysButtons[i].gameObject.GetComponent<TextMeshProUGUI>()));
                daysButtons[i].gameObject.name = $"{j}";
                j++;
            }
            else
            {
                daysButtons[i].gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
                daysButtons[i].interactable = true;     //fix a bug on unity, but I want to be false here
                daysButtons[i].interactable = false;    
            }
        }
    }
}
