using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfficherNomVilleScript : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI texteUINouvelleVille;
    [SerializeField]
    private TMPro.TextMeshProUGUI texteUIRegion;
 

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "vThirdPersonCamera" || other.name == "vThirdPersonCameraJoueur2") // Déclenchement de l'affichage si un des joueurs rentre dans une ville
        {
            texteUINouvelleVille.text = this.gameObject.GetComponent<Text>().text;

            if (texteUIRegion.text != this.gameObject.transform.parent.gameObject.name) // Si la région a changé, on met à jour
            {
                texteUIRegion.text = this.gameObject.transform.parent.gameObject.name;
            }
            texteUINouvelleVille.gameObject.transform.parent.GetComponent<Animator>().SetTrigger("AffichageVille");
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
