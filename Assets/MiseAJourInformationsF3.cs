using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiseAJourInformationsF3 : MonoBehaviour
{
    [SerializeField]
    private GameObject JoueurGameObject;
    [SerializeField]
    private TerrainGenerator terrainGenerator; // Pour récuperer le seed dans la heightmap
    [SerializeField]
    private ProjetP3DScene1.main main;

    private void Start()
    {
        this.gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = terrainGenerator.heightMapSettings.noiseSettings.seed.ToString(); // Indique le seed de la génération de la map grâce a la heightmap
        this.gameObject.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = main.modeCombat; // On récupère le mode de combat pour l'afficher (Tour par tour, Temps réel)
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = JoueurGameObject.transform.position.x.ToString("N0") + " / " + JoueurGameObject.transform.position.y.ToString("N0") + " / " + JoueurGameObject.transform.position.z.ToString("N0"); // On écrit la position du joueur dans menu
        this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = JoueurGameObject.transform.rotation.eulerAngles.x.ToString("N0") + " / " + JoueurGameObject.transform.rotation.eulerAngles.y.ToString("N0") + " / " + JoueurGameObject.transform.rotation.eulerAngles.z.ToString("N0"); // On écrit la rotation du joueur dans menu
    }

    // On met à jour l'affichage du mode de combat
    public void miseAJourModeCombat()
    {
         this.gameObject.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = main.modeCombat;
    }
}
