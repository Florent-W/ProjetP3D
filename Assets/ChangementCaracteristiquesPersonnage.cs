using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.ParticleSystem;

public class ChangementCaracteristiquesPersonnage : MonoBehaviour
{
    private Invector.CharacterController.vThirdPersonController vThirdPersonController;
    // Va servir à lancer une musique
    [SerializeField]
    private AudioClip musique;
    [SerializeField]
    private AudioSource musiquePlayer;
    [SerializeField]
    private GameObject trailGameObject;
    [SerializeField]
    private GameObject laserSupermanGameObject;
    private GameObject laserSupermanGameObjectInstance;

    // Start is called before the first frame update
    void Start()
    {
        /*
        if(DonneesChargement.nomGameObjectModel == "Flash")  // Selon le personnages, les caractéristiques seront différentes
        {
            vThirdPersonController = this.gameObject.GetComponent<Invector.CharacterController.vThirdPersonController>();
            vThirdPersonController.freeWalkSpeed = 3;
            vThirdPersonController.freeRunningSpeed = 15;
            vThirdPersonController.freeSprintSpeed = 31;
            GameObject shapeParticuleFlash = Instantiate(trailGameObject, vThirdPersonController.transform);
            ShapeModule shapeParticule = shapeParticuleFlash.GetComponent<ParticleSystem>().shape;
            shapeParticule.skinnedMeshRenderer = vThirdPersonController.gameObject.transform.GetChild(1).gameObject.transform.GetChild(4).gameObject.transform.GetChild(4).gameObject.GetComponent<SkinnedMeshRenderer>();

           // musiquePlayer.clip = musique; // On lance la musique
           // musiquePlayer.Play();
        } */

        StartCoroutine(miseAJourCaracteristiquesChangementPersonnage());
    }

    // Quand on change le personnage, on change les caractéristiques, pareil pour les dresseurs
    public IEnumerator miseAJourCaracteristiquesChangementPersonnage()
    {
        yield return new WaitForSeconds(0.1f);
        vThirdPersonController = this.gameObject.GetComponent<Invector.CharacterController.vThirdPersonController>();

        string nomPrefab = this.gameObject.name;

        if (nomPrefab == "vThirdPersonCamera" || nomPrefab == "vThirdPersonCamera2") // Si c'est le joueur, on ne regardera pas le nom directement du personnagePrefab mais dans le personnage
        {
            changementCaracteristiques(this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(4).gameObject);
        }
        else // Si c'est un dresseur
        {
            changementCaracteristiques(this.gameObject);
        }
    }

    // Méthode pour ne pas avoir à répeter les caractéristiques que ce soit si c'est un joueur ou un dresseur
    private void changementCaracteristiques(GameObject personnage)
    {
        if (personnage.name == "Flash")  // Selon le personnages, les caractéristiques seront différentes
        {
            vThirdPersonController.freeWalkSpeed = 3;
            vThirdPersonController.freeRunningSpeed = 15;
            vThirdPersonController.freeSprintSpeed = 31;

            vThirdPersonController.stepSmooth = -3;
            GameObject shapeParticuleFlash = Instantiate(trailGameObject, vThirdPersonController.transform);
            ShapeModule shapeParticule = shapeParticuleFlash.GetComponent<ParticleSystem>().shape;
            shapeParticule.skinnedMeshRenderer = vThirdPersonController.gameObject.transform.GetChild(1).gameObject.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>();
        }
        else
        {
            Transform trailFlash = this.gameObject.transform.Find("trail_character_flash(Clone)");

            if (trailFlash != null) // Si il y a une trail de Flash et que c'est un autre personnage, on l'enlève
            {
                Destroy(trailFlash.gameObject);
            }

            vThirdPersonController.freeWalkSpeed = 2.5f; // On remet les bonnes caractéristiques
            vThirdPersonController.freeRunningSpeed = 3;
            vThirdPersonController.freeSprintSpeed = 4f;

            vThirdPersonController.stepSmooth = 4;
        }
    }

    // Si on veut lancer des laser comme superman
    private void OnLaserSuperman()
    {
        if (this.GetComponent<PlayerInput>().actions["LaserSuperman"].ReadValue<float>() == 1) 
        {
            if (!this.gameObject.transform.Find("Laser(Clone)")) { // Si il n'y a pas déjà de laser
                laserSupermanGameObjectInstance = Instantiate(laserSupermanGameObject, this.gameObject.transform); // On met le laser si on appuie sur la touche
                laserSupermanGameObjectInstance.transform.localPosition = new Vector3(0, 1.99f, 0.23f);
            }
            else
            {
                Destroy(laserSupermanGameObjectInstance); // Sinon on le détruit
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
