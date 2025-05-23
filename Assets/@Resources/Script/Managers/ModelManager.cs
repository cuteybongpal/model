using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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
    ResourceManager ResourceManager = new ResourceManager();
    List<Color> usedColor = new List<Color>() { Color.white };
    public Stack<List<BlockData>> WorkStack = new Stack<List<BlockData>>();
    public Stack<List<BlockData>> UndoStack = new Stack<List<BlockData>>();
    bool canUndo = false;
    bool canRedo = false;

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
    //싱글톤 코드
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
    //blocks 리스트에 블럭 추가
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
        if (UIManager.Instance.CurrentMainUI != null)
        {
            UserUI userUI = UIManager.Instance.CurrentMainUI as UserUI;
            userUI?.ChangeColorHistory(usedColor);
        }
        Push(blocks);
    }
    //blocks 리스트에 블럭 삭제
    public void Remove(Block block)
    {
        blocks.Remove(block);
        Push(blocks);
    }
    //전부 삭제
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
    //색깔 바꿔주는 함수
    public void ChangeColor(Block block, Color color)
    {
        block.Color = color;
        Push(blocks);
    }
    //만들 었던 것을 저장시킴
    public void BuildComplete()
    {
        string objContent = dataManager.Export2obj(blocks);
        string bdContent = dataManager.Export2bd(blocks);
        string mtlContent = dataManager.Export2mtl(blocks);
        List<DataManager.AssetImage> Images = dataManager.GetImages(blocks);
        string[] images = new string[Images.Count];
        string[] fileNames = new string[Images.Count];
        int i = 0;
        foreach(DataManager.AssetImage image in Images)
        {
            images[i] = image.Image;
            fileNames[i] = image.Name;
            i++;
        }
        ResourceManager resourceManager = new ResourceManager();
        //현재 todo : 썸네일 만들기 완료 버튼을 누를 당시의 화면을 썸네일로 만들것임
        RenderTexture renderTexture = resourceManager.Load<RenderTexture>("Thumbnail");
        
        WebGLBridge.SendMtlFileToJs(mtlContent);
        WebGLBridge.SendBdFileToJs(bdContent);
        WebGLBridge.SendObjFileToJS(objContent);
        WebGLBridge.SendImagesToJs(images, fileNames);

        WebGLBridge.Send();
    }

    //bd파일을 로드함
    
    public void LoadModel(string bdFile)
    {
        string[] blockData = bdFile.Split("//", StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < blockData.Length; i++)
        {
            string block = blockData[i];
            string[] datas = block.Split(new[] { "position:", "texture:", "color:" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string data in datas)
            {
                Debug.Log(data);
            }
            string[] vectorData = datas[0].Split(',');
            string textureKey = datas[1];
            string[] ColorData = datas[2].Split(",");

            Vector3Int blockPosition = new Vector3Int(int.Parse(vectorData[0]), int.Parse(vectorData[1]), int.Parse(vectorData[2]));
            Material blockMaterial = ResourceManager.Load<Material>(textureKey+".mat");
            Color blockColor = new Color(int.Parse(ColorData[0]) / 255f, int.Parse(ColorData[1]) / 255f, int.Parse(ColorData[2]) / 255f);

            Block _block = ObjectManager.Instance.blockManager.Spawn();

            _block.Material = blockMaterial;
            _block.Color = blockColor;
            _block.Pos = blockPosition;
            Add(_block);
        }

    }
    
    //되돌리기
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
    //되돌리기 취소
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
    
    //WorkStack에 했던 걸 추가 해줌
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