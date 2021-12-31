using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerObjetScript : MonoBehaviour
{
    Rigidbody rb;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    public void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.E))
        {
            lancerObjet();
        }
        */
    }

    public void lancerObjet()
    {
        rb.AddForce(this.gameObject.transform.forward * 1400);
      //  rb.AddForceAtPosition(this.gameObject.transform.forward * force, this.gameObject.transform.forward * -5);
    }
}
