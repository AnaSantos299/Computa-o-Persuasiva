using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyCalendarController : MonoBehaviour    //Control the infomation and flow of the days and its infos
{
    public References.DailyContentInfo[] dailyContents = new References.DailyContentInfo[800];

    public int[] daysInMonths = new int[12];    //daysAmountInEachMonth
    public int[] firstDay = new int[24];        //first day of the month is on wich day of the week

    public DailyContent dailyContent;

    public ContentController contentController;

    public GameObject textDone;     //Texts on the corners fo the daily tasks page
    public GameObject textDelete;
    void Start()
    {
        Init();
    }

    void Init()
    {
        CreateArrayOfDays();

        int dayOfYear = System.DateTime.UtcNow.ToLocalTime().DayOfYear - 1;

        References.instance.currentShowingDayIndex = dayOfYear;
        References.instance.today = dayOfYear;

        BuildTodaysContent();
    }

    void CreateArrayOfDays()
    {
        int l = 0; //Id

        for (int k = 0; k < 2; k++)    //Year   year is 0 as base
        {
            for (int j = 0; j < 12; j++)    //Month
            {
                for (int i = 0; i < daysInMonths[j]; i++)  //Day
                {
                    dailyContents[l] = new References.DailyContentInfo(l, i, j, k); //all in zero based
                    l++;
                }
            }
        }
    }

    public void NextDay() {
        SelectADay(References.instance.currentShowingDayIndex + 1);
    }
    public void PreviousDay()
    {
        SelectADay(References.instance.currentShowingDayIndex - 1);
    }
    void BuildTodaysContent()
    {
        SelectADay(References.instance.currentShowingDayIndex); //Send info to build the daily task page
    }
    public void SelectADay(int day)
    {
        dailyContent.MontDailyContentInformation(dailyContents[day]);

        CheckIfThereIsAnyTask();
    }
    public void SelectADayInCalendar(int day)
    {
        ChangeScreen(1);
        SelectADay(day);
    }
    void ChangeScreen(int targetScreen) {
        contentController.ChangeScreen(targetScreen);
    }

    void CheckIfThereIsAnyTask() {
        bool thereIsAnyTasks = dailyContents[References.instance.currentShowingDayIndex].tasksList.Count > 1;
        textDone.SetActive(thereIsAnyTasks);
        textDelete.SetActive(thereIsAnyTasks);
    }

    public void MarkTaskAsDone(int taskIndex, bool isDone)
    {
        dailyContents[References.instance.currentShowingDayIndex].tasksList[taskIndex].done = isDone;

        UpdateTasksList();
    }
    public void DeleteTask(int taskIndex)
    {
        dailyContents[References.instance.currentShowingDayIndex].tasksList.RemoveAt(taskIndex);

        UpdateTasksList();
    }

    public void AddTask(string content)
    {
        int newTaskId = dailyContents[References.instance.currentShowingDayIndex].tasksList.Count + 1;

        dailyContents[References.instance.currentShowingDayIndex].tasksList.Add(new References.TasksInfo (newTaskId, content, false));

        UpdateTasksList();
    }

    public void ChangeTasks(string content, int taskIndex)  //Change content
    {
        dailyContents[References.instance.currentShowingDayIndex].tasksList[taskIndex].content = content;

        UpdateTasksList();
    }

    void UpdateTasksList()
    {
        dailyContent.UpdateTaskList(dailyContents[References.instance.currentShowingDayIndex]);

        CheckIfThereIsAnyTask();
    }
}
