using Unity.Entities;
using UnityEngine;

public struct Lifetime : IComponentData
{
    public float TimeToLiveInSeconds;
}

public class LifetimeSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, ref Lifetime lifetime) =>
        {
            lifetime.TimeToLiveInSeconds -= Time.deltaTime;
            if (lifetime.TimeToLiveInSeconds < 0)
            {
                EntityManager.DestroyEntity(entity);
            }
        });
    }
}
