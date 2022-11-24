using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DailyContent : MonoBehaviour
{
    public TextMeshProUGUI dateText;
    public int dayId; //index on the array
    public List<Task> tasks = new List<Task>();
    public Task taskPrefab;
    public Transform taskGroup;

    public References.DailyContentInfo dailyContent;
    public void UpdateTaskList (References.DailyContentInfo _dailyContent)
    {
        CleanTaskList();
        BuildTaskList(_dailyContent);

        References.instance.loadControl.UpdatePercentage();
    }

    void CleanTaskList()
    {
        int taskListSize = tasks.Count;

        for (int i = taskListSize - 1; i >= 0; i--)
        {
            Destroy(tasks[i].gameObject);
        }

        tasks.Clear();
    }

    void BuildTaskList (References.DailyContentInfo _dailyContent)
    {
        int taskAmount = _dailyContent.tasksList.Count;

        for (int i = 0; i < taskAmount; i++)
        {
            bool lastTask = false;
            if (i == taskAmount-1)
                lastTask = true;
            tasks.Add(Instantiate(taskPrefab, taskGroup));
            bool taskIsDone = _dailyContent.tasksList[i].done;
            string taskContent = _dailyContent.tasksList[i].content;
            tasks[i].Init(taskContent, lastTask, i, taskIsDone);
        }
    }

    public void MontDailyContentInformation(References.DailyContentInfo _dailyContent)
    {
        dailyContent = _dailyContent;
        dayId = _dailyContent.index;
        References.instance.currentShowingDayIndex = dayId;
        string month = References.instance.monthsNames[_dailyContent.month];
        string day = _dailyContent.day + 1 <= 9? $"0{_dailyContent.day + 1}" : $"{_dailyContent.day + 1}";
        string name = $"{month} {day}";
        dateText.text = $"{name}";

        CleanTaskList();
        BuildTaskList(_dailyContent);
    }
}
