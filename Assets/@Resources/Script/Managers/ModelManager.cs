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
    public Stack<List<BlockData>> WorkStack = new Stack<List<BlockData>>();
    public Stack<List<BlockData>> UndoStack = new Stack<List<BlockData>>();
    bool canUndo = false;
    bool canRedo = false;

    WebGLBridge WebGLBridge = null;
    public bool CanUndo 
    { 
        get { return canUndo; }
        set
        {
            canUndo = value;
            UserUI userUI = UIManager.Instance.CurrentMainUI as UserUI;
            userUI?.CanUndo?.Invoke(canUndo);
        }
    }

    public bool CanRedo
    {
        get { return canRedo; }
        set
        {
            canRedo = value;
            UserUI userUI = UIManager.Instance.CurrentMainUI as UserUI;
            userUI?.CanRedo?.Invoke(canRedo);
        }
    }
    //�̱��� �ڵ�
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

    private void Start()
    {
        CanRedo = false;
        CanUndo = false;
    }
    //blocks ����Ʈ�� �� �߰�
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
        userUI.ChangeColorHistory(usedColor);

        Push(blocks);
    }
    //blocks ����Ʈ�� �� ����
    public void Remove(Block block)
    {
        blocks.Remove(block);
        Push(blocks);
    }
    //���� ����
    public void RemoveAll()
    {
        for (int i = 0; i < blocks.Count;)
        {
            Debug.Log(blocks[0].name);
            ObjectManager.Instance.blockManager.DeSpawn(blocks[0]);
            blocks.RemoveAt(0);
        }
        Push(blocks);
    }
    //���� �ٲ��ִ� �Լ�
    public void ChangeColor(Block block, Color color)
    {
        block.Color = color;
        Push(blocks);
    }
    //���� ���� ���� �����Ŵ
    public void BuildComplete()
    {
        string objContent = dataManager.Export2obj(blocks);
        string bdContent = dataManager.Export2bd(blocks);
        string mtlContent = dataManager.Export2mtl(blocks);
        List<DataManager.AssetImage> Images = dataManager.GetImages(blocks);
        List<byte[]> images = new List<byte[]>();
        List<string> fileNames = new List<string>();

        foreach(DataManager.AssetImage image in Images)
        {
            images.Add(image.Image);
            fileNames.Add(image.Name);
        }

        WebGLBridge = new WebGLBridge();

        WebGLBridge.SendMtlFileToJs(mtlContent);
        WebGLBridge.SendBdFileToJs(bdContent);
        WebGLBridge.SendObjFileToJS(objContent);

        WebGLBridge.SendImagesToJs(images, fileNames);
    }
    //bd������ �ε���
    
    public void LoadModel()
    {

    }
    
    //�ǵ�����
    public void Undo()
    {
        if (WorkStack.Count < 1)
            return;
        UndoStack.Push(WorkStack.Pop());
        foreach (Block block in blocks)
        {
            ObjectManager.Instance.blockManager.DeSpawn(block);
        }
        blocks.Clear();
        CanRedo = true;
        if (WorkStack.Count == 0)
        {
            CanUndo = false;
            return;
        }
        foreach (BlockData data in WorkStack.Peek())
        {
            Block block = ObjectManager.Instance.blockManager.Spawn();
            block.Pos = data.Pos;
            block.Material = data.Material;
            block.Color = data.Color;
            blocks.Add(block);
        }
    }
    //�ǵ����� ���
    public void Redo()
    {
        if (UndoStack.Count == 0)
            return;
        foreach (Block block in blocks)
        {
            ObjectManager.Instance.blockManager.DeSpawn(block);
        }
        blocks.Clear();
        List<BlockData> popedWork = UndoStack.Pop();
        foreach (BlockData data in popedWork)
        {
            Block block = ObjectManager.Instance.blockManager.Spawn();
            block.Pos = data.Pos;
            block.Material = data.Material;
            block.Color = data.Color;
            blocks.Add(block);
        }
        popedWork = null;

        if (UndoStack.Count == 0)
        {
            CanRedo = false;
        }
    }
    
    //WorkStack�� �ߴ� �� �߰� ����
    public void Push(List<Block> blockList)
    {
        UndoStack.Clear();
        List<BlockData> blockDataList = new List<BlockData>();
        foreach (Block block in blocks)
        {
            BlockData data = new BlockData();
            Material mat = new Material(block.Material);
            Vector3Int Pos = block.Pos;
            Color color = block.Color;

            data.Material = mat;
            data.Pos = Pos;
            data.Color = color;
            blockDataList.Add(data);
        }
        WorkStack.Push(blockDataList);
        CanRedo = false;
        CanUndo = true;
    }
}
public struct BlockData
{
    public Color Color;
    public Material Material;
    public Vector3Int Pos;
}