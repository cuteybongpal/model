using System;
using System.Runtime.InteropServices;
using UnityEngine;

public static class WebGLBridge
{

    [DllImport("__Internal")]
    private static extern void ExecuteMethodToJS(string MethodName, string data);

    [DllImport("__Internal")]
    private static extern void SendImagesToJS(string Base64ImagesJson, string fileNamesJson);
    [DllImport("__Internal")]
    private static extern void Submit();
    [DllImport("__Internal")]
    private static extern void SendThumbnailToJS(string thumbnail);

    public static void SendObjFileToJS(string objFile)
    {
        ExecuteMethodToJS("FillInputObj", objFile);
    }
    public static void SendMtlFileToJs(string mtlFile)
    {
        ExecuteMethodToJS("FillInputMtl", mtlFile);
    }
    public static void SendBdFileToJs(string bdFile)
    {
        ExecuteMethodToJS("FillInputBd", bdFile);
    }
    public static void SendImagesToJs(string[] Base64Images, string[] fileNames)
    {
        Wrapper base64Wrapper = new Wrapper();
        Wrapper fileNameWrapper = new Wrapper();
        base64Wrapper.items = Base64Images;
        fileNameWrapper.items = fileNames;

        string imagesJson = JsonUtility.ToJson(base64Wrapper);
        string fileNamesJson = JsonUtility.ToJson(fileNameWrapper);

        SendImagesToJS(imagesJson, fileNamesJson);
    }
    public static void SendThumbnailToJs(byte[] thumbnail)
    {
        string base64Image = Convert.ToBase64String(thumbnail);
        SendThumbnailToJS(base64Image);
    }
    public static void Send()
    {
        Submit();
    }
    private class Wrapper
    {
        public string[] items;
        public Wrapper(){ }
    }
}
