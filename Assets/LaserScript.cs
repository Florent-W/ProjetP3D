using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script pour lancer un laser
public class LaserScript : MonoBehaviour
{
    private LineRenderer lr;
    private ProjetP3DScene1.main Main;

    void Start()
    {
        Main = GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>();
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        lr.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
                if (hit.transform.gameObject.transform.parent != null)
                {
                    if (hit.transform.gameObject.transform.parent.gameObject.tag == "PokemonAdverse") // On regarde si c'est bien un pokémon
                    {
                       if (Main.modeCombat == "Temps réel")
                         {
                            hit.transform.gameObject.transform.parent.gameObject.GetComponent<StatistiquesPokemon>().GetPokemon().attaqueAvecDegat(3);
                        }
                    }
                }
            }
        }
        else
        {
            lr.SetPosition(1, transform.position + (transform.forward * 5000));
        }
    }
}
