using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WebGLBridge 
{

    [DllImport("__Internal")]
    private static extern void ExecuteMethodToJS(string MethodName, string data);

    [DllImport("__Internal")]
    private static extern void SendImagesToJS(List<byte[]> binaryImages, List<string> fileNames);

    public void SendObjFileToJS(string objFile)
    {
        ExecuteMethodToJS("FillInputObj", objFile);
    }
    public void SendMtlFileToJs(string mtlFile)
    {
        ExecuteMethodToJS("FillInputMtl", mtlFile);
    }
    public void SendBdFileToJs(string bdFile)
    {
        ExecuteMethodToJS("FillInputBd", bdFile);
    }
    public void SendImagesToJs(List<byte[]> binaryImages, List<string> fileNames)
    {
        SendImagesToJS(binaryImages, fileNames);
    }
}
