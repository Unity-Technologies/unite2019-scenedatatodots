using Unity.Entities;
using UnityEngine;

public class LifetimeAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float TimeToLiveInSeconds;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new Lifetime { TimeToLiveInSeconds = TimeToLiveInSeconds });
    }
}
