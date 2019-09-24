using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PrefabListAuthoring : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
    public GameObject[] Prefabs;

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        foreach (var prefab in Prefabs)
        {
            referencedPrefabs.Add(prefab);
        }
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var buffer = dstManager.AddBuffer<SpawnerListComponent>(entity);

        foreach (var prefab in Prefabs)
        {
            buffer.Add(new SpawnerListComponent { Prefab = conversionSystem.GetPrimaryEntity(prefab) });
        }
    }
}
