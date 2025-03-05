using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    List<Color> usedColor = new List<Color>() { Color.white };

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



        bool isDuplicate = false;
        foreach (Color blockColor in usedColor)
        {
            if (blockColor == block.Color)
            {
                isDuplicate = true;
            }
            else
            {
                isDuplicate |= false;
            }
        }
        if (!isDuplicate)
        {
            usedColor.Add(block.Color);
        }
        UserUI userUI = UIManager.Instance.CurrentMainUI as UserUI;
        userUI.ChagneColorHistory(usedColor);
    }

    public void ChangeColor(Block block, Color color)
    {
        block.Color = color;
    }
    public void Remove(Block block)
    {
        blocks.Remove(block);
    }
    public void RemoveAll()
    {
        for (int i = 0; i < blocks.Count;)
        {
            Debug.Log(blocks[0].name);
            ObjectManager.Instance.blockManager.DeSpawn(blocks[0]);
            blocks.RemoveAt(0);
        }
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
