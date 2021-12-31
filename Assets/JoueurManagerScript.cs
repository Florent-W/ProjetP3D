using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoueurManagerScript : MonoBehaviour
{
    public List<ClassLibrary.Dresseur> Joueurs = new List<ClassLibrary.Dresseur>();
    public GameObject[] JoueursGameObject = new GameObject[2];

    public int ChercherJoueurGameObjectPosition(GameObject joueurGameObject)
    {
        return Array.IndexOf(JoueursGameObject, joueurGameObject, 0);
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
