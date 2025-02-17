using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class BaseData
{

}

public class GameSettingData : BaseData
{
    public static string extention = "xml";
}

public class StructureData : BaseData
{
    /// <summary>
    /// sd : structureData¿« ¡ÿ∏ª
    /// </summary>
    public static string extention = "sd";
}