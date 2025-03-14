using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonController : MonoBehaviour
{
    Button button;
    public enum OnClickEvent
    {
        ChangeMode,
        ChangeMaterial,
        ChangeColor,
        //테스트용도로 집어넣은 것 
        Save,
        DelateAll,
        Undo,
        Redo,
        None
    }
    public OnClickEvent ClickEvent = OnClickEvent.None;
    public int num;
    void Start()
    {
        button = GetComponent<Button>();
    }
    public void AddOnClickEvent(Action action)
    {
        if (button == null)
            button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            action.Invoke();
        });
    }
}
