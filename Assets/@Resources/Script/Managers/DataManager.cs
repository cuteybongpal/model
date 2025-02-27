using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEditor.AddressableAssets.HostingServices;
using UnityEngine;
using UnityEngine.AI;

//데이터를 불러오고 데이터를 저장하는 역할을 한다.
public class DataManager
{

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


    public void Export2obj(List<Block> blocks)
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
                face += String.Format(ExportFormat.FaceFormat, ExportFormat.Face1[i] + j * 8, ExportFormat.Face2[i] + j * 8, ExportFormat.Face3[i] + j * 8, ExportFormat.Face4[i] + j * 8, ExportFormat.Face5[i] + j * 8, ExportFormat.Face6[i] + j * 8);
            }
            string mtlName = String.Format(ExportFormat.MtlName, blocks[j].Color.r, blocks[j].Color.g, blocks[j].Color.b, blocks[j].Material.name);
            objContent += String.Format(ExportFormat.ObjFormat, vertex, face, mtlName);
        }
        File.WriteAllText("Assets/@Resources/Output/output.obj", objContent);
        Debug.Log("성공");
    }
    public void Export2bd(List<Block> blocks)
    {
        string bdContent = "";
        for (int i = 0; i < blocks.Count; i++)
        {
            Block block = blocks[i];
            bdContent += String.Format(ExportFormat.BdFormat, block.Pos.x, block.Pos.y, block.Pos.z, block.Material.name, Mathf.RoundToInt(block.Color.r * 255), Mathf.RoundToInt(block.Color.g * 255), Mathf.RoundToInt(block.Color.b * 255));
        }
        File.WriteAllText("Assets/@Resources/Output/output.bd", bdContent);
    }
    public void Export2mtl(List<Block> blocks)
    {
        List<Mtl> materials = new List<Mtl>();
        foreach (Block block in blocks)
        {
            Mtl mtl = new Mtl();
            mtl.material = block.Material.name;
            mtl.color = block.Color;
            bool isNotOverlap = true;
            foreach (Mtl mat in materials)
            {
                if (mat.material == mtl.material && mat.color == mtl.color)
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
            string mtlName = String.Format(ExportFormat.MtlName, mtl.color.r, mtl.color.g, mtl.color.b, mtl.material);
            mtlContent += String.Format(ExportFormat.MtlFormat, mtlName, mtl.material, mtl.color.r, mtl.color.g, mtl.color.b);
        }
        Debug.Log(mtlContent);
        File.WriteAllText("Assets/@Resources/Output/output.mtl", mtlContent);
    }
    struct Mtl
    {
        public string material;
        public Color color;
    }
}
