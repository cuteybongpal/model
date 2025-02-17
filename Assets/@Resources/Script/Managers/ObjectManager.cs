using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager :  Singleton<ObjectManager>, BaseManager
{
    
    public T GetMethod<T>(int methodNum) where T : Delegate
    {
        return default(T);
    }
}

public class ManagedObject<T> where T : MonoBehaviour
{
    ResourceManager resourceManager;
    private string key;

    public HashSet<T> SpawnedObject = new HashSet<T>();
    public List<T> DeSpawnedObject = new List<T>();


    public T Spawn()
    {
        //����Ʈ �� �������� null�� ��� ����Ʈ�� Ŭ��������
        DeSpawnedObject.RemoveAll(item => item == null);

        if (DeSpawnedObject.Count == 0)
        {
            T pooledObject = DeSpawnedObject[0];
            DeSpawnedObject.Remove(pooledObject);
            pooledObject.gameObject.SetActive(true);
            pooledObject.gameObject.transform.position = Vector3.zero;
            SpawnedObject.Add(pooledObject);
            return pooledObject;
        }
        else
        {
            GameObject SpawnObject = resourceManager.Load<GameObject>(key);
            T spawnObject = GameObject.Instantiate(SpawnObject).GetComponent<T>();
            SpawnedObject.Add(spawnObject);
            return spawnObject;
        }
    }
    public void DeSpawn(T DeSpawnObject)
    {
        SpawnedObject.Remove(DeSpawnObject);
        DeSpawnedObject.Add(DeSpawnObject);
        DeSpawnObject.transform.position = Vector3.zero;
        DeSpawnObject.transform.parent = null;
        DeSpawnObject.gameObject.SetActive(false);
    }
    public ManagedObject(string key)
    {
        resourceManager = new ResourceManager();
        this.key = key;
    }
}
