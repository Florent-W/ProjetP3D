using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VilleInteraction : MonoBehaviour
{
    [SerializeField]
    private ProjetP3DScene1.main main;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "PositionPersonnage") // Si le curseur touche un point
        {
            collider.gameObject.GetComponent<SpriteRenderer>().color = Color.green; // Change la couleur pour indiquer que il y a un point
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.name == "PositionPersonnage" && main.JoueurManager.JoueursGameObject[0].GetComponent<PlayerInput>().actions["Action1"].ReadValue<float>() > 0)
        {
            main.JoueurManager.JoueursGameObject[0].transform.position = collider.gameObject.transform.position;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "PositionPersonnage")
        {
            collider.gameObject.GetComponent<SpriteRenderer>().color = Color.white; // Change la couleur
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
