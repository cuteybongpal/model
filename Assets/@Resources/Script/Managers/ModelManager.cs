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
    public void RemoveAll()
    {
        foreach (Block block in blocks)
        {
            blocks.Remove(block);
            ObjectManager.Instance.blockManager.DeSpawn(block);
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
