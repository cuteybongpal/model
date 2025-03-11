using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

//데이터를 불러오고 데이터를 저장하는 역할을 한다.
public class DataManager
{
    TextureColorChanger colorChanger = new TextureColorChanger();
    public enum DataExtention {
        xml,
        bd,
    }

    ResourceManager resourceManager;

    public DataManager()
    {
        //todo : intialize
    }

    private T ReadData<T>(string data, string extention)
    {
        DataExtention ex = (DataExtention)Enum.Parse(typeof(DataExtention), extention);
        switch (ex)
        {
            case DataExtention.xml:
                break;
            case DataExtention.bd:
                break;
        }
        return default(T);
    }


    public string Export2obj(List<Block> blocks)
    {
        string objContent = "" + ExportFormat.UseMtl;

        for (int j = 0; j < blocks.Count; j++)
        {
            Block block = blocks[j];
            string vertex = "";
            //정점 추가
            for (int i = 0; i < 8; i++)
            {

                float x = ExportFormat.VertexXPos[i] + block.Pos.x;
                float y = ExportFormat.VertexYPos[i] + block.Pos.y;
                float z = ExportFormat.VertexZPos[i] + block.Pos.z;
                vertex += String.Format(ExportFormat.VertexFormat, x, y, z);
            }
            string face = "";
            //면 추가
            for (int i = 0; i < 12; i++)
            {
                face += String.Format(ExportFormat.FaceFormat, ExportFormat.Face1[i] + j * 8, ExportFormat.Face2[i] + j * 8, ExportFormat.Face3[i] + j * 8, ExportFormat.Face4[i] + j * 8, ExportFormat.Face5[i] + j * 8, ExportFormat.Face6[i] + j * 8, ExportFormat.Vt1[i] + j * 4, ExportFormat.Vt2[i] + j * 4, ExportFormat.Vt3[i] + j * 4);
            }
            string mtlName = String.Format(ExportFormat.MtlName, blocks[j].Color.r, blocks[j].Color.g, blocks[j].Color.b, blocks[j].Material.name);
            objContent += String.Format(ExportFormat.ObjFormat, vertex, face, mtlName);
        }
        return objContent;
    }
    public string Export2bd(List<Block> blocks)
    {
        string bdContent = "";
        for (int i = 0; i < blocks.Count; i++)
        {
            Block block = blocks[i];
            bdContent += String.Format(ExportFormat.BdFormat, block.Pos.x, block.Pos.y, block.Pos.z, block.Material.name, Mathf.RoundToInt(block.Color.r * 255), Mathf.RoundToInt(block.Color.g * 255), Mathf.RoundToInt(block.Color.b * 255));
        }
        return bdContent;
    }
    public string Export2mtl(List<Block> blocks)
    {
        List<Mtl> materials = new List<Mtl>();
        foreach (Block block in blocks)
        {
            Mtl mtl = new Mtl();
            mtl.Material = block.Material;
            mtl.Color = block.Color;
            bool isNotOverlap = true;
            foreach (Mtl mat in materials)
            {
                if (mat.Material == mtl.Material && mat.Color == mtl.Color)
                {
                    isNotOverlap = false;
                    break;
                }
                else
                {
                    isNotOverlap &= true;
                }
            }

            if (isNotOverlap)
            {
                materials.Add(mtl);
            }
        }
        string mtlContent = "";

        foreach (Mtl mtl in materials)
        {
            string mtlName = String.Format(ExportFormat.MtlName, mtl.Color.r, mtl.Color.g, mtl.Color.b, mtl.Material.name);
            mtlContent += String.Format(ExportFormat.MtlFormat, mtlName, $"{mtl.Material.mainTexture.name}_{mtl.Color.r}_{mtl.Color.g}_{mtl.Color.b}", mtl.Color.r, mtl.Color.g, mtl.Color.b);
        }
        return mtlContent;
    }

    public List<AssetImage> GetImages(List<Block> blocks)
    {
        List<Mtl> materials = new List<Mtl>();
        foreach (Block block in blocks)
        {
            Mtl mtl = new Mtl();
            mtl.Material = block.Material;
            mtl.Color = block.Color;
            bool isNotOverlap = true;
            foreach (Mtl mat in materials)
            {
                if (mat.Material == mtl.Material && mat.Color == mtl.Color)
                {
                    isNotOverlap = false;
                    break;
                }
                else
                {
                    isNotOverlap &= true;
                }
            }

            if (isNotOverlap)
            {
                materials.Add(mtl);
            }
        }
        List<AssetImage> images = new List<AssetImage>();
        foreach (Mtl mtl in materials)
        {
            images.Add(new AssetImage(colorChanger.ChangeTextureColor(mtl.Material, mtl.Color), $"{mtl.Material.mainTexture.name}_{mtl.Color.r}_{mtl.Color.g}_{mtl.Color.b}"));
        }
        return images;
    }
    struct Mtl
    {
        public Material Material;
        public Color Color;
    }
    public struct AssetImage
    {
        public byte[] Image;
        public string Name;

        public AssetImage(byte[] bytes, string Name)
        {
            this.Image = bytes;
            this.Name = Name;
        }
    }
}
