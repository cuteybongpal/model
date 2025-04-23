using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager
{
    
    private static Dictionary<string, UnityEngine.Object> resources = new Dictionary<string, UnityEngine.Object>();

    //키값으로 resources 안에 값을 가져옴
    public T Load<T>(string key) where T : UnityEngine.Object
    {
        if (resources.ContainsKey(key))
        {
            return resources[key] as T;
        }
        return default(T);
    }

    //키값을 이용해 오브젝트를 비동기로 로딩해줌
    public void LoadAsync<T>(string key, Action<bool> callback = null) where T : UnityEngine.Object
    {
        if (resources.ContainsKey(key))
        {
            callback.Invoke(true);
            return;
        }
        else
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
            handle.Completed += (result) =>
            {
                if (result.Status == AsyncOperationStatus.Succeeded)
                {
                    //Debug.Log(key);
                    resources.Add(key, (UnityEngine.Object)handle.Result);
                    callback?.Invoke(true);
                }
                else
                {
                    callback?.Invoke(false);
                }
            };
        }
    }
    //라벨값으로 오브젝트들을 전부 비동기로 로딩해줌
    public void LoadAllAsync<T>(string label, Action callback = null) where T : UnityEngine.Object
    {
        var handle = Addressables.LoadResourceLocationsAsync(label, typeof(T));

        handle.Completed += (loadedAsset) =>
        {
            if (loadedAsset.Status == AsyncOperationStatus.Succeeded)
            {
                bool[] LoadedArray = new bool[loadedAsset.Result.Count];
                for (int i = 0; i < LoadedArray.Length; i++)
                {
                    int a = i;
                    LoadAsync<T>(loadedAsset.Result[a].PrimaryKey, (bool isload) =>
                    {
                        LoadedArray[a] = isload;
                        bool isLoadAll = true;
                        for (int j = 0; j < LoadedArray.Length; j++)
                            isLoadAll &= LoadedArray[j];
                        if (isLoadAll)
                        {
                            callback?.Invoke();
                        }
                    });
                }
            }
            else
            {
                Debug.Log("AsyncLoadingFailed");
            }
        };
    }
}
