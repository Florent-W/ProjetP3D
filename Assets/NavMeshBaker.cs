using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    [SerializeField]
    //  NavMeshSurface[] navMeshSurfaces;
    public NavMeshSurface navMeshSurface;
    // Start is called before the first frame update
    void Awake()
    {
        navMeshSurface.BuildNavMesh();
        /*
        for(int i = 0; i < navMeshSurfaces.Length; i++)
        {
            navMeshSurfaces[i].BuildNavMesh();
        } */
    }

}
