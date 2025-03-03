using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : Singleton<UserManager>
{
    public enum UserState
    {
        PlaceMode,
        RemoveMode,
        PaintMode
    }
    public UserState UserMode = UserState.PlaceMode;
    public Material CurrentMaterial = null;
    public Color CurrentColor = Color.white;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
}
