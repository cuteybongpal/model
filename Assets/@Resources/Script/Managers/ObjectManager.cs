using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class ObjectManager :  Singleton<ObjectManager>, BaseManager
{
    public enum MethodNum
    {
        BlockSpawn = 0,
        BlockDeSpawn = 1
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public K GetMethod<K>(int methodNum) where K : Delegate
    {
        switch ((MethodNum)methodNum)
        {
            case MethodNum.BlockSpawn:
                return new Func<Block>(blockManage.Spawn) as K;
            case MethodNum.BlockDeSpawn:
                return new Action<Block>(blockManage.DeSpawn) as K;
        }
        
        return default(K);
    }
    ManagedObject<Block> blockManage = new ManagedObject<Block>("Block.prefab");
}

public class ManagedObject<T> where T : Component
{
    ResourceManager resourceManager;
    private string key;

    public HashSet<T> SpawnedObject = new HashSet<T>();
    public List<T> DeSpawnedObject = new List<T>();


    public T Spawn()
    {
        //리스트 속 아이템이 null일 경우 리스트를 클리어해줌
        DeSpawnedObject.RemoveAll(item => item == null);

        if (DeSpawnedObject.Count != 0)
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
