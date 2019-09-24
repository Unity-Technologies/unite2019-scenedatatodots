using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public struct MoveForward : IComponentData
{
    public float SpeedInUnitsPerSecond;
}

public class MoveForwardSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref MoveForward moveForward, ref Translation translation, ref LocalToWorld localToWorld) =>
        {
            translation.Value += localToWorld.Forward * moveForward.SpeedInUnitsPerSecond * Time.deltaTime;
        });
    }
}
