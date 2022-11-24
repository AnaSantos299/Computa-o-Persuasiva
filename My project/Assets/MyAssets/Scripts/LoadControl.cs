using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadControl : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI text;
    public float percentage;
    public Button giftButton;

    public void TryToGetReward() {
        References.instance.contentController.ChangeScreen(3);
    }
    public void UpdatePercentage () {
        int today = References.instance.today;
        int amountOfTasks = References.instance.dailyCalendarController.dailyContents[today].tasksList.Count - 1;
        int tasksdone = 0;

        for (int i = 0; i < amountOfTasks; i++)
        {
            tasksdone += References.instance.dailyCalendarController.dailyContents[today].tasksList[i].done ? 1 : 0;
        }

        percentage = amountOfTasks > 0f ? (float)tasksdone / (float)amountOfTasks : 0f;
        UpdateLoadAmount(percentage);

        giftButton.interactable = percentage >= 1f;
    }
    public void UpdateLoadAmount (float amount) {
        image.fillAmount = amount;

        text.text = $"{Mathf.Round(amount *100f)}%";
    }
}
