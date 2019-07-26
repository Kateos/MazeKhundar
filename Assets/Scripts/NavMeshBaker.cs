using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    private NavMeshSurface[] navMeshSurfaces;

    // Start is called before the first frame update
    void Start()
    {
        navMeshSurfaces = FindObjectsOfType<NavMeshSurface>();
        print(navMeshSurfaces.Length);
        for (int i = 0; i < navMeshSurfaces.Length; i++)
        {
            navMeshSurfaces[i].BuildNavMesh();
            print("I bake");
        }
    }
}
