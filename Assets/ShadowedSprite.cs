using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Pour mettre des ombres sur les personnages comme Octopath
[RequireComponent(typeof(SpriteRenderer))]
public class ShadowedSprite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On; 
    }
}
