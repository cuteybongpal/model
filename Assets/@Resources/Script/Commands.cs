using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMode : ICommand
{
    int modeNumber;
    public void Execute()
    {
        UserManager.Instance.UserMode = (UserManager.UserState)modeNumber;
    }
    public ChangeMode(int _modeNumber)
    {
        this.modeNumber = _modeNumber;
    }
}

public class ChangeMaterial : ICommand
{
    ResourceManager resourceManager;
    int materialNumber;
    public void Execute()
    {
        Material SelectedMaterial = resourceManager.Load<Material>($"Material{materialNumber}.mat");
        UserManager.Instance.CurrentMaterial = SelectedMaterial;
    }
    public ChangeMaterial(int _materialNumber)
    {
        resourceManager = new ResourceManager();
        this.materialNumber = _materialNumber;
    }
}

public class SetColor : ICommand
{
    Image image;
    public Color color;

    public void Execute()
    {
        image.color = color;
    }
    public SetColor(Image image)
    {
        this.image = image;
    }
}

public class ChangeColor : ICommand
{
    Func<Color> getColor;

    public void Execute()
    {
        Debug.Log(getColor());
        UserManager.Instance.CurrentColor = getColor();
    }

    public ChangeColor(Func<Color> getColor)
    {
        this.getColor = getColor;
    }
}
public class Save : ICommand
{
    public void Execute()
    {
        ModelManager.Instance.BuildComplete();
    }
}
public class DeleteAll : ICommand
{
    public void Execute()
    {
        ModelManager.Instance.RemoveAll();
    }
}

public class Undo : ICommand
{
    public void Execute()
    {
        ModelManager.Instance.Undo();;
    }
}
public class Redo : ICommand
{
    public UI_ImageController controller;

    public void Execute()
    {
        ModelManager.Instance.Redo();
    }
}
