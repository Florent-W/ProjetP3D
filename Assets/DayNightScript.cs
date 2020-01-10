using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightScript : MonoBehaviour
{
    Light Lumiere;
    float Vitesse = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Lumiere = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        Lumiere.transform.Rotate(Vector3.right * Vitesse * Time.deltaTime);
    }
}
