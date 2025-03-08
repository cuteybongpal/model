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
    public UserState UserMode;
    public Material CurrentMaterial;
    public Color CurrentColor = Color.white;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            ResourceManager resourceManager = new ResourceManager();
            CurrentMaterial = resourceManager.Load<Material>("Material9.mat");
        }
        else
        {
            Destroy(instance);
        }
    }
}
