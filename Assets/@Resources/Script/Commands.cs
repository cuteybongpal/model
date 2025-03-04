using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
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
public class SetImageMaterial : ICommand
{
    public void Execute()
    {
        throw new System.NotImplementedException();
    }
}

public class ChangeColor : ICommand
{
    Color color;
    public void Execute()
    {
        UserManager.Instance.CurrentColor = color;
    }

    public ChangeColor(ref Color color)
    {
        this.color = color;
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