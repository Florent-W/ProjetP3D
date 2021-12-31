using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script qui s'occuper de quand le personnage est dans l'eau
public class PersonnageDansEau : MonoBehaviour
{
    private GameObject personnage;

    private void Start()
    {
        personnage = GameObject.Find("vThirdPersonCamera");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "vThirdPersonCamera")
        {
            personnage.GetComponent<Animator>().SetBool("dansEau", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "vThirdPersonCamera")
        {
            personnage.GetComponent<Animator>().SetBool("dansEau", false);
        }
    }
}
