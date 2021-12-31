using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerTerrain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Terrain>().preserveTreePrototypeLayers = true; // Permet de garder le même layer que dans un prefab, cela va permettre de cacher la végétation sur la map
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
