using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class References : MonoBehaviour //Database
{
    public static References instance { get; private set; }
    [System.Serializable]
    public class DaysInCalendar
    {
        public int index;
        public TextMeshProUGUI text;

        public DaysInCalendar(int _index, TextMeshProUGUI _text)
        {
            index = _index;
            text = _text;
        }
    }
    [System.Serializable]
    public class TasksInfo
    {
        public int index;
        public string content;
        public bool done;

        public TasksInfo(int _index, string _content, bool _done) {
            index = _index;
            content = _content;
            done = _done;
        }
    }
    public class DailyContentInfo
    {
        public int index;
        public int day;
        public int month;
        public int year;
        public List<TasksInfo> tasksList = new List<TasksInfo>();

        public DailyContentInfo(int _index, int _day, int _month, int _year)
        {
            index = _index;
            day = _day;
            month = _month;
            year = _year;
            tasksList.Add(new TasksInfo(0, "", false));
        }

        public DailyContentInfo()
        {
            index = 0;
            day = 0;
            month = 0;
            year = 0;
            tasksList.Add(new TasksInfo(0, "Não há oque fazer", false));
        }
    }
    public List<string> monthsNames = new List<string>();
    public DailyCalendarController dailyCalendarController;
    public int currentShowingDayIndex;
    public int today;
    public LoadControl loadControl;
    public ContentController contentController;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}