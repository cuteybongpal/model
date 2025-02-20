using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : Singleton<ModelManager>, BaseManager
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

    public T GetMethod<T>(int methodNum) where T : Delegate
    {
        switch ((MethodNum)methodNum)
        {
            case MethodNum.Add:
                return new Action<Block>(Add) as T;
            case MethodNum.Remove:
                return new Action<Block>(Remove) as T;
            case MethodNum.CompleteModel:
                return new Action(BuildComplete) as T;
            default:
                return default(T);
        }
    }
    private void Add(Block block)
    {
        blocks.Add(block);
    }
    private void Remove(Block block)
    {
        blocks.Remove(block);
    }
    private void BuildComplete()
    {
        dataManager.Export2obj(blocks);
        dataManager.Export2bd();
    }

    private void LoadModel()
    {

    }
}
