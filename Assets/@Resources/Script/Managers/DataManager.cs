using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.AddressableAssets.HostingServices;
using UnityEngine;

//�����͸� �ҷ����� �����͸� �����ϴ� ������ �Ѵ�.
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
        string objContent = "";
        
        for (int j = 0; j < blocks.Count; j++)
        {
            Block block = blocks[j];
            string vertex = "";
            //���� �߰�
            for (int i = 0; i <8; i++)
            {
                
                float x = ExportFormat.vertexXPos[i] + block.Pos.x;
                float y = ExportFormat.vertexYPos[i] + block.Pos.y;
                float z = ExportFormat.vertexZPos[i] + block.Pos.z;
                vertex += String.Format(ExportFormat.vertexFormat, x, y, z);
            }
            string face = "";
            //�� �߰�
            for (int i = 0;i < 12; i++)
            {
                face += String.Format(ExportFormat.faceFormat, ExportFormat.face1[i] + j * 8, ExportFormat.face2[i] + j * 8, ExportFormat.face3[i] + j * 8, ExportFormat.face4[i] + j * 8, ExportFormat.face5[i] + j * 8, ExportFormat.face6[i] + j * 8);
            }
            objContent += String.Format(ExportFormat.objFormat, vertex, face);
        }
        File.WriteAllText("Assets/@Resources/Output/output.obj", objContent);
        Debug.Log("����");
    }
    public void Export2bd()
    {

    } 

}
