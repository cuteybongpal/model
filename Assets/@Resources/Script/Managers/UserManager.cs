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
    ResourceManager resourceManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            resourceManager = new ResourceManager();
        }
        else
        {
            Destroy(instance);
        }
    }
    public void Init()
    {
        CurrentMaterial = resourceManager.Load<Material>("Material9.mat");
    }
}
