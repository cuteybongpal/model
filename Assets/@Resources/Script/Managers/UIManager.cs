using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    public UI_Base CurrentMainUI;

    public Stack<UI_Popup> Popup;


    public void PopupDown()
    {
        UI_Popup popup = Popup.Pop();


        popup.PopUpDown();
    }
}