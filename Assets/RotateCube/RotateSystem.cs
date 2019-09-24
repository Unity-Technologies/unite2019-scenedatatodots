using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class RotateSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Rotate rotate, ref RotationEulerXYZ euler) =>
        {
            euler.Value.y += rotate.radiansPerSecond * Time.deltaTime;
        });
    }
}
