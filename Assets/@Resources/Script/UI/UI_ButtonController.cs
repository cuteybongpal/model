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
        button.onClick.AddListener(() =>
        {
            action.Invoke();
        });
    }
}
