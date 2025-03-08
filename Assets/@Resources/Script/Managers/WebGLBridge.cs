using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class WebGLBridge : Singleton<WebGLBridge>
{
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SendObjFileToJS(string objFile)
    {
        Application.ExternalCall("receiveMessageFromUnity", objFile);
    }
    public void SendMtlFileToJs(string mtlFile)
    {
        Application.ExternalCall("receiveMessageFromUnity", mtlFile);
    }
    public void SendBdFileToJs(string bdFile)
    {
        Application.ExternalCall("receiveMessageFromUnity", bdFile);
    }
}
