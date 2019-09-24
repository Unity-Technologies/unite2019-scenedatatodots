using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class GraphAuthoring : MonoBehaviour, IDeclareReferencedPrefabs
{
    public GameObject Prefab;

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(Prefab);
    }
}
