using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Base : MonoBehaviour
{
    protected virtual void Start()
    {
        UIManager.Instance.CurrentMainUI = this;
    }
}
