using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuMontures : MonoBehaviour
{
    [SerializeField]
    private ProjetP3DScene1.main main;
    [SerializeField]
    private GameObject CMVCamMonture;
    [SerializeField]
    private GameObject dialogueManager;
    private MonterSurPokemonVolant monterSurPokemonVolant;

    // instancie une monture
    public void ApparaitreMontures()
    {
        GameObject boutonCliquerGameObject = EventSystem.current.currentSelectedGameObject;

        Vector3 positionSpawnMonture = new Vector3(main.JoueurManager.JoueursGameObject[0].transform.position.x + 3, main.JoueurManager.JoueursGameObject[0].transform.position.y, main.JoueurManager.JoueursGameObject[0].transform.position.z);
        GameObject monture = Instantiate(Resources.Load<GameObject>("Models/Pokemon/" + boutonCliquerGameObject.name), positionSpawnMonture, Quaternion.Euler(Vector3.zero)); // On instancie la monture

        if (boutonCliquerGameObject.name == "Monture vol")
        {
            PokemonVolantController pokemonVolantController = monture.GetComponent<PokemonVolantController>();
            pokemonVolantController.vthirdPersonCamera = main.JoueurManager.JoueursGameObject[0];
            pokemonVolantController.VthirdPersonController = main.controllerJoueurs[0];
            pokemonVolantController.vCamMonture = CMVCamMonture;
            pokemonVolantController.rb = monture.GetComponent<Rigidbody>();
            monture.GetComponent<Animator>().applyRootMotion = true;

            monterSurPokemonVolant = monture.transform.GetChild(0).gameObject.GetComponent<MonterSurPokemonVolant>();

            pokemonVolantController.monterSurPokemonVolantScript = monterSurPokemonVolant;
        }
        else if (boutonCliquerGameObject.name == "Monture ponyta")
        {
            PonytaController ponytaController = monture.GetComponent<PonytaController>();
            ponytaController.vthirdPersonCamera = main.JoueurManager.JoueursGameObject[0];
            ponytaController.VthirdPersonController = main.controllerJoueurs[0];
            ponytaController.vCamMonture = CMVCamMonture;

            monterSurPokemonVolant = monture.transform.GetChild(1).gameObject.GetComponent<MonterSurPokemonVolant>();
            monterSurPokemonVolant.ponytaControllerScript = ponytaController;
        }

        monterSurPokemonVolant.joueurGameObject = main.JoueurManager.JoueursGameObject[0];
        monterSurPokemonVolant.VthirdPersonController = main.controllerJoueurs[0];
        monterSurPokemonVolant.dialogueManager = dialogueManager;
        monterSurPokemonVolant.emplacementJoueurs = main.JoueurManager.gameObject;
        monterSurPokemonVolant.rbJoueur = main.JoueurManager.JoueursGameObject[0].GetComponent<Rigidbody>();
        monterSurPokemonVolant.cmVcamMonture = CMVCamMonture.GetComponent<Cinemachine.CinemachineFreeLook>();

        RetirerMenuMontures();
    }

    // Pour enlever le menu des montures
    public void RetirerMenuMontures()
    {
        this.gameObject.SetActive(false);
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
