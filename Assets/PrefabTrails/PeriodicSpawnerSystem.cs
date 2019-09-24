using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public struct PeriodicSpawner : IComponentData
{
    public Entity Prefab;
    public float SecondsBetweenSpawns;
    public float SecondsToNextSpawn;
}

public class PeriodicSpawnerSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref PeriodicSpawner spawner, ref Translation translation, ref RotationEulerXYZ rotation) =>
        {
            spawner.SecondsToNextSpawn -= Time.deltaTime;
            if (spawner.SecondsToNextSpawn < 0)
            {
                spawner.SecondsToNextSpawn = spawner.SecondsBetweenSpawns;
                var instance = EntityManager.Instantiate(spawner.Prefab);
                EntityManager.SetComponentData(instance, translation);
                EntityManager.SetComponentData(instance, rotation);
            }
        });
    }
}
