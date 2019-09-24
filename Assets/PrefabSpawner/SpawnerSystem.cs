using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Time = UnityEngine.Time;

public class SpawnerSystem : ComponentSystem
{
    Random m_Rand;

    protected override void OnCreate()
    {
        m_Rand = new Random(1234);
    }

    protected override void OnUpdate()
    {
        Entities.ForEach((ref SpawnerComponent spawner, ref LocalToWorld localToWorld) =>
        {
            spawner.SecondsToNextSpawn -= Time.deltaTime;
            if (spawner.SecondsToNextSpawn < 0)
            {
                spawner.SecondsToNextSpawn += spawner.SecondsBetweenSpawns;

                var prefabs = EntityManager.GetBuffer<SpawnerListComponent>(spawner.Prefab);
                var pick = m_Rand.NextInt(prefabs.Length);
                var instance = EntityManager.Instantiate(prefabs[pick].Prefab);

                EntityManager.SetComponentData(instance, new Translation { Value = localToWorld.Position });
            }
        });
    }
}
