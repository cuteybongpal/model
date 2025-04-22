using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BdLoader : MonoBehaviour
{
    void Start()
    {
        string bd = WebGLBridge.getBd();

        ModelManager.Instance.LoadModel(bd);
    }
}
