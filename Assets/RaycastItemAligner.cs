using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawn des objets en fonction du raycast
public class RaycastItemAligner : MonoBehaviour
{
    public float raycastDistance = 100f;
    public GameObject objectToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void PositionRaycast()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            GameObject clone = Instantiate(objectToSpawn, hit.point, spawnRotation);
        }
    }
}
