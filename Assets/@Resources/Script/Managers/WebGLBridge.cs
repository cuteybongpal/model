using System.Runtime.InteropServices;
using UnityEngine;

public class WebGLBridge
{

    [DllImport("__Internal")]
    private static extern void ExecuteMethodToJS(string MethodName, string data);

    [DllImport("__Internal")]
    private static extern void SendImagesToJS(string Base64ImagesJson, string fileNamesJson);

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
    public void SendImagesToJs(string[] Base64Images, string[] fileNames)
    {
        Wrapper base64Wrapper = new Wrapper();
        Wrapper fileNameWrapper = new Wrapper();
        base64Wrapper.items = Base64Images;
        fileNameWrapper.items = fileNames;

        string imagesJson = JsonUtility.ToJson(base64Wrapper);
        string fileNamesJson = JsonUtility.ToJson(fileNameWrapper);

        SendImagesToJS(imagesJson, fileNamesJson);
    }
    private class Wrapper
    {
        public string[] items;
        public Wrapper(){ }
    }
}
