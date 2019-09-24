using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpawnerConversion : GameObjectConversionSystem
{
    protected override void OnUpdate()
    {
        var spawners = GetEntityQuery(typeof(SpawnerAuthoring)).ToComponentArray<SpawnerAuthoring>();
        for (int i = 0; i < spawners.Length; i++)
        {
            DstEntityManager.AddComponentData(GetPrimaryEntity(spawners[i]), new SpawnerComponent
            {
                Prefab = GetPrimaryEntity(spawners[i].PrefabList),
                SecondsBetweenSpawns = spawners.Length / 3f,
                SecondsToNextSpawn = i / 3f
            });
        }
    }
}

public class SpawnerAuthoring : MonoBehaviour, IDeclareReferencedPrefabs
{
    public PrefabListAuthoring PrefabList;
    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        => referencedPrefabs.Add(PrefabList.gameObject);
}
