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
        Debug.Log("���� No");
        WebGLBridge.getUserAuthority();
        while (UserManager.Instance.Authority == UserManager.UserAuthority.None)
        {
            await UniTask.Yield();
        }
        Debug.Log("���� On");
        resourceManager = new ResourceManager();
        resourceManager.LoadAllAsync<GameObject>("Prefab", () =>
        {
            Debug.Log("�ε�1 �Ϸ�");
            resourceManager.LoadAllAsync<Material>("Material", () =>
            {
                Debug.Log("�ε�2 �Ϸ�");
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
