using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    ResourceManager resourceManager;
    async void Start()
    {
        Debug.Log("권한 No");
        WebGLBridge.getUserAuthority();
        while (UserManager.Instance.Authority == UserManager.UserAuthority.None)
        {
            await UniTask.Yield();
        }
        Debug.Log("권한 On");
        resourceManager = new ResourceManager();
        resourceManager.LoadAllAsync<GameObject>("Prefab", () =>
        {
            Debug.Log("로딩1 완료");
            resourceManager.LoadAllAsync<Material>("Material", () =>
            {
                Debug.Log("로딩2 완료");
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
    }
}
