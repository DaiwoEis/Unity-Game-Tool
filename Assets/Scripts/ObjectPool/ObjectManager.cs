using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoSingleton<ObjectManager>
{
    private Dictionary<GameObject, ObjectPool> _objectPoolDic;

    protected override void OnCreate()
    {
        base.OnCreate();

        _objectPoolDic = new Dictionary<GameObject, ObjectPool>();
    }

    public void CreatePool(GameObject prefab, int initSize)
    {
        _objectPoolDic[prefab] = ObjectPool.CreateObjectPool(prefab, initSize);
        _objectPoolDic[prefab].transform.SetParent(transform);
    }

    public GameObject Create(GameObject prefab, Vector3 pos, Quaternion rotation)
    {
        if (_objectPoolDic.ContainsKey(prefab))
            return _objectPoolDic[prefab].Instantiate(pos, rotation);

        return Instantiate(prefab, pos, rotation);
    }
}
