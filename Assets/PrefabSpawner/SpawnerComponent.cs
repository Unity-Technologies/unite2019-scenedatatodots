using Unity.Entities;

public struct SpawnerComponent : IComponentData
{
    public Entity Prefab;
    public float SecondsBetweenSpawns;
    public float SecondsToNextSpawn;
}
