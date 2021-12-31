using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cache le GameObject au lancement
public class HideOnPlay : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }
}
