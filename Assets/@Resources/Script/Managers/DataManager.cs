using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public void Export2obj()
    {

    }
    public void Export2bd()
    {

    } 

}
