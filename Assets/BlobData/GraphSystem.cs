using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

struct Packet : IComponentData
{
    public BlobAssetReference<NodeGraph> Graph;
    public int DestinationNode; 
    public float Speed;
    public int TurnCounter;
}

public class GraphSystem : ComponentSystem
{
    private int m_SpawnCounter = 0;
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, ref Packet packet, ref Translation translation) =>
        {
            ref var nodes = ref packet.Graph.Value.Nodes;
            ref var dstNode = ref nodes[packet.DestinationNode];
            float3 dir = dstNode.Position  - translation.Value;
            float distance = Time.deltaTime * packet.Speed;
            if (math.length(dir) < distance)
            {
                translation.Value = dstNode.Position;
                packet.DestinationNode = dstNode.Links[packet.TurnCounter++ % dstNode.Links.Length];
            }
            else
                translation.Value += math.normalize(dir) * distance;
        });

        Entities.ForEach((ref NodeGraphSpawner graph) =>
        {
            graph.NextSpawnInSeconds -= Time.deltaTime;
            if (graph.NextSpawnInSeconds < 0)
            {
                graph.NextSpawnInSeconds = 0.1f;
                ref var nodes = ref graph.Graph.Value.Nodes;

                for(int startNode = 0; startNode<nodes.Length; ++startNode)
                {
                    var entity = EntityManager.Instantiate(graph.Prefab);
                    var endNode = nodes[startNode].Links[m_SpawnCounter++ % nodes[startNode].Links.Length];

                    EntityManager.SetComponentData(entity, new Translation {Value = nodes[startNode].Position});
                    EntityManager.AddComponentData(entity, new Packet {Graph = graph.Graph, Speed = 5.0f, DestinationNode = endNode});
                }
            }
        });
    }
}
