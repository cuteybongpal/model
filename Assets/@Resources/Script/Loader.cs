using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    ResourceManager resourceManager;
    void Start()
    {
        Debug.Log("Ω√¿€");
        Debug.Log(WebGLBridge.getUserAuthority());

        UserManager.Instance.SetUserAuthority(WebGLBridge.getUserAuthority());
                                
        resourceManager = new ResourceManager();
        resourceManager.LoadAllAsync<GameObject>("Prefab", () =>
        {
            resourceManager.LoadAllAsync<Material>("Material", () =>
            {
                resourceManager.LoadAllAsync<RenderTexture>("RenderTexture", () =>
                {
                    UserManager.Instance.Init();
                    switch (UserManager.Instance.Authority)
                    {
                        case UserManager.UserAuthority.Create:
                            SceneManager.LoadScene((int)Scenes.Create);
                            break;
                        case UserManager.UserAuthority.Specte:
                            SceneManager.LoadScene((int)Scenes.Specte);
                            break;
                    }
                });
            });
        });

    }
}
