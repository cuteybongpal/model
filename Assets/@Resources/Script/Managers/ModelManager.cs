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

            bool isFilled = true;
            for (int i = 0; i < UsedColor.Length; i++)
            {
                if (UsedColor[i] == null)
                {
                    isFilled &= true;
                    UsedColor[i] = value;
                }
                else
                    isFilled &= false;
            }
            if (isFilled)
            {

            }
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
    }
    public void LoadModel()
    {

    }
}
