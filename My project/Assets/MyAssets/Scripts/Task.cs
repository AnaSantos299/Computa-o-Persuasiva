using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    public TMP_InputField taskContent;
    public bool lastTask;
    public int taskIndex;
    public bool taskDone;
    public string content;
    public GameObject doneButton;
    public Sprite notDone;
    public Sprite done;
    public GameObject deleteButton;
    public void Init(string _content, bool _lastTask, int _taskIndex, bool _taskDone) {
        lastTask = _lastTask;
        taskIndex = _taskIndex;
        content = _content;
        taskDone = _taskDone;

        WriteNewText(_content);

        CheckTask(_lastTask);
    }

    void CheckTask(bool _lastTask) {
        deleteButton.SetActive(!_lastTask);
        doneButton.SetActive(!_lastTask);
        doneButton.GetComponent<Image>().sprite = taskDone ? done : notDone;
    }

    void WriteNewText(string content) {
        if (taskDone)
            taskContent.text = $"<s>{content}</s>";
        else
            taskContent.text = $"{content}";
    }

    public void Delete ()
    {
        References.instance.dailyCalendarController.DeleteTask(taskIndex);
    }

    public void MarkAsDone ()
    {
        taskDone = !taskDone;
        References.instance.dailyCalendarController.MarkTaskAsDone(taskIndex, taskDone);

        WriteNewText(content);
    }

    public void EditTask()
    {
        if (taskContent.text != null && taskContent.text != "")
            ChangeTask();
        else
            Delete();

        if (lastTask)
            CreateNewTask();
    }

    void ChangeTask() 
    {
        References.instance.dailyCalendarController.ChangeTasks(taskContent.text, taskIndex);
    }

    void CreateNewTask()
    {
        References.instance.dailyCalendarController.AddTask("");
    }
}
