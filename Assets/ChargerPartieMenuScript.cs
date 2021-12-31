using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using UnityEngine;

public class ChargerPartieMenuScript : MonoBehaviour
{
    [SerializeField]
    private EcranDeChargementScript ecranDeChargement;

    // Charge une sauvegarde d'un emplacement dans le menu charger
    public void Chargement_emplacement_sauvegarde_click()
    {
        ecranDeChargement.emplacementNumeroSauvegardeCharger = this.gameObject.name;
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
