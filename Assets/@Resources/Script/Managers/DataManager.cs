using System;
using System.Collections;
using System.Collections.Generic;
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


    public void Export2obj()
    {

    }
    public void Export2bd()
    {

    } 

}
