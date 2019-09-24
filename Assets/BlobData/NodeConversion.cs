using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

struct Node
{
    public BlobArray<int> Links;
    public float3 Position;
}

struct NodeGraph
{
    public BlobArray<Node> Nodes;
}

struct NodeGraphSpawner : IComponentData
{
    public BlobAssetReference<NodeGraph> Graph;
    public float NextSpawnInSeconds;
    public Entity Prefab;
}

public class NodeConversion : GameObjectConversionSystem
{
    BlobAssetReference<NodeGraph> BuildNodeGraph(NodeAuthoring[] authoringNodes)
    {
        using (var builder = new BlobBuilder(Allocator.Temp))
        {
            ref var root = ref builder.ConstructRoot<NodeGraph>();
            var nodeArray = builder.Allocate(ref root.Nodes, authoringNodes.Length);
            for (int i = 0; i < nodeArray.Length; i++)
            {
                nodeArray[i].Position = authoringNodes[i].transform.position;
                var links = builder.Allocate(ref nodeArray[i].Links, authoringNodes[i].links.Length);
                for (int j = 0; j < authoringNodes[i].links.Length; j++)
                {
                    links[j] = Array.IndexOf(authoringNodes, authoringNodes[i].links[j]);
                }
            }
            return builder.CreateBlobAssetReference<NodeGraph>(Allocator.Persistent);
        }
    }

    private EntityQuery m_Query;
    
    protected override void OnCreate()
    {
        base.OnCreate();
        m_Query = GetEntityQuery(typeof(NodeAuthoring));
    }

    protected override void OnUpdate()
    {
        var authoringNodes = m_Query.ToComponentArray<NodeAuthoring>();
        var nodes = Array.FindAll(authoringNodes, node => node.links.Length > 0);

        var nodeGraph = BuildNodeGraph(nodes);

        Entities.ForEach((GraphAuthoring network) =>
        {
            DstEntityManager.AddComponentData(GetPrimaryEntity(network), new NodeGraphSpawner
            {
                Graph = nodeGraph,
                Prefab = GetPrimaryEntity(network.Prefab),
            });
        });
    }
}
