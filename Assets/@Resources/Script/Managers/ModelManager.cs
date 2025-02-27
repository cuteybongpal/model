using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : Singleton<ModelManager>
{
    public enum MethodNum
    {
        Add,
        Remove,
        CompleteModel,
    }

    List<Block> blocks = new List<Block>();
    DataManager dataManager = new DataManager();
    Color[] UsedColor = new Color[5];
    private Color currentColor;
    public Color CurrentColor 
    {
        get { return currentColor; }
        set
        {
            currentColor = value;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            CurrentColor = Color.white;
        }
        else
        {
            Destroy(this);
        }
    }
    public void Add(Block block)
    {
        blocks.Add(block);
    }
    public void Remove(Block block)
    {
        blocks.Remove(block);
    }
    public void BuildComplete()
    {
        dataManager.Export2obj(blocks);
        dataManager.Export2bd(blocks);
        dataManager.Export2mtl(blocks);
    }
    public void LoadModel()
    {

    }
}
