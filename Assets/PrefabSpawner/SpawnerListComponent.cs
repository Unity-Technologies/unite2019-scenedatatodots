using Unity.Entities;
using UnityEngine;

public struct SpawnerListComponent : IBufferElementData
{
    public Entity Prefab;
}
