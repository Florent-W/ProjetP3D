using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SauvegardeJson : MonoBehaviour
{
    string chemin, jsonString;

    // Start is called before the first frame update
    void Start()
    {
        chemin = Application.streamingAssetsPath + "/Joueur.sav";
        jsonString = File.ReadAllText(chemin);
        ClassLibrary.Personnage Joueur = JsonUtility.FromJson<ClassLibrary.Personnage>(jsonString); // Lire personnage dans le json
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
