using Unity.Entities;
using UnityEngine;

public class MoveForwardAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float SpeedInUnitsPerSecond;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new MoveForward { SpeedInUnitsPerSecond = SpeedInUnitsPerSecond });
    }
}
