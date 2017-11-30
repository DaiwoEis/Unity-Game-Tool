using UnityEngine;

public interface IPooled
{
    GameObject OwnerPool { get; set; }

    void OnRetrieveFromPool();

    void OnReturnToPool();
}
