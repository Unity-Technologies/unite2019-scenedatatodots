using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PeriodicSpawnerAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    public GameObject Prefab;
    public float SpawnsPerSecond;

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) => referencedPrefabs.Add(Prefab);

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new PeriodicSpawner
        {
            Prefab = conversionSystem.GetPrimaryEntity(Prefab),
            SecondsBetweenSpawns = 1 / SpawnsPerSecond
        });
    }
}
