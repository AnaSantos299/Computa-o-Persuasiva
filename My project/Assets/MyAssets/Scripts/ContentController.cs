using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentController : MonoBehaviour  //Control the section that is appearing on the screen
{
    int currentScreen;

    public RectTransform contentMaster;
    public List<Button> buttons = new List<Button>();
    public ScrollGrid scrollGrid;

    void Start()
    {
        ChangeScreen(0);
    }

    public void InstantMoveScreen(int contentScreenToMove)
    {
        int screenPosition = (contentScreenToMove - 2) * -1;

        contentMaster.localPosition = new Vector3 (screenPosition * 1080f, contentMaster.localPosition.y, contentMaster.localPosition.z);
    }

    void CurrentUpdatedScreen(int _currentScreen)
    {
        ControlEnabledButtons(_currentScreen);
    }

    void ControlEnabledButtons (int _currentScreen)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;

            if (i == _currentScreen)
                buttons[i].interactable = false;
        }
    }

    public void SetCurrentScreen (int _currentScreen)
    {
        currentScreen = _currentScreen;

        CurrentUpdatedScreen(_currentScreen);
    }

    public void ChangeScreen(int targetScreen)
    {
        scrollGrid.ChangeScreen(targetScreen);
        InstantMoveScreen(targetScreen);
    }
}
