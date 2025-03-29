using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    ResourceManager resourceManager;
    void Start()
    {
        resourceManager = new ResourceManager();
        resourceManager.LoadAllAsync<GameObject>("Prefab", () =>
        {
            resourceManager.LoadAllAsync<Material>("Material", () =>
            {
                resourceManager.LoadAllAsync<RenderTexture>("RenderTexture", () =>
                {
                    UserManager.Instance.Init();
                    SceneManager.LoadScene(1);
                });
            });
        });

    }
}
