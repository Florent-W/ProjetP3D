using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class MonterSurPokemonVolant : MonoBehaviour
{
    public GameObject joueurGameObject;
    public GameObject VthirdPersonController;
    public GameObject dialogueManager;
    public GameObject emplacementJoueurs;
    [SerializeField]
    private bool enCheval = false;
    public Rigidbody rbJoueur;
    public PonytaController ponytaControllerScript;
    public bool declencherDialogue = false;
    private bool test = false;
    public Cinemachine.CinemachineFreeLook cmVcamMonture;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "vThirdPersonCamera")
        {
            declencherDialogue = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "vThirdPersonCamera")
        {
            declencherDialogue = false;
        }
    }

    // Ajoute les listeners de la conversation
    public void changerConversationAction()
    {
        dialogueManager.GetComponent<PixelCrushers.DialogueSystem.DialogueSystemTrigger>().selectedDatabase.GetConversation("Approche Monture").dialogueEntries[1].onExecute.RemoveAllListeners();
        dialogueManager.GetComponent<PixelCrushers.DialogueSystem.DialogueSystemTrigger>().selectedDatabase.GetConversation("Approche Monture").dialogueEntries[1].onExecute.AddListener(() => { this.gameObject.GetComponent<MonterSurPokemonVolant>().monterSurMonture(); });
        dialogueManager.GetComponent<PixelCrushers.DialogueSystem.DialogueSystemTrigger>().selectedDatabase.GetConversation("Enlever Monture").dialogueEntries[2].onExecute.RemoveAllListeners();
        dialogueManager.GetComponent<PixelCrushers.DialogueSystem.DialogueSystemTrigger>().selectedDatabase.GetConversation("Enlever Monture").dialogueEntries[2].onExecute.AddListener(() => { this.gameObject.GetComponent<MonterSurPokemonVolant>().descendreMonture(); });
    }

    public void declencherDialogueNouveauStatut(bool declencherDialogueStatut)
    {
        declencherDialogue = declencherDialogueStatut;
    }

    public void activerDialogueNouveauStatut(bool activerDialogueStatut)
    {
        this.gameObject.GetComponent<PixelCrushers.DialogueSystem.DialogueSystemTrigger>().enabled = activerDialogueStatut;
    }

    public void HandleConversationStart()
    {
        enabled = false;
        // StartCoroutine(DisableAfterOneFrame());
    }

    public void HandleConversationEnd()
    {
        // Wait one frame to allow input to clear:
        StartCoroutine(EnableAfterOneFrame());
    }
    IEnumerator EnableAfterOneFrame()
    {
        yield return new WaitForSeconds(1);
        enabled = true;
    }

    IEnumerator DisableAfterOneFrame()
    {
        yield return new WaitForSeconds(1);
        enabled = false;
    }

    void OnConversationStart()
    {
        HandleConversationStart();
    }

    void OnConversationEnd()
    {
        HandleConversationEnd();
        
    }

    public void monterSurMonture()
    {
            joueurGameObject.GetComponent<Invector.CharacterController.vThirdPersonInput>().enabled = false;
            joueurGameObject.GetComponent<Animator>().enabled = false;
            joueurGameObject.GetComponent<Rigidbody>().detectCollisions = false;
            // joueurGameObject.GetComponent<CapsuleCollider>().enabled = false;
            joueurGameObject.GetComponent<InputJoueurScript>().enabled = false;
            joueurGameObject.GetComponent<Invector.CharacterController.vThirdPersonController>().enabled = false;
            joueurGameObject.transform.parent = this.gameObject.transform.parent.transform; // Met le joueur sur le pokemon
            joueurGameObject.transform.position = new Vector3(this.gameObject.transform.position.x + 5, this.gameObject.transform.position.y, this.gameObject.transform.position.z);

            string nomPokemon = this.gameObject.name;

        if (nomPokemon == "Ponyta")
        {
            this.gameObject.transform.parent.gameObject.AddComponent<Rigidbody>();
            ponytaControllerScript.rb = this.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>();
            Rigidbody rbPokemon = this.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>();
            rbPokemon.mass = 30;
            rbPokemon.freezeRotation = true;
        }
        else if(nomPokemon == "Charizard")
        {
            this.gameObject.transform.parent.gameObject.AddComponent<Rigidbody>();
            this.gameObject.transform.parent.gameObject.GetComponent<PokemonVolantController>().rb = this.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>();
            Rigidbody rbPokemon = this.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>();
            rbPokemon.mass = 30;
            rbPokemon.freezeRotation = true;
            this.gameObject.transform.parent.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }

            enCheval = true;
            this.gameObject.GetComponent<PixelCrushers.DialogueSystem.DialogueSystemTrigger>().conversation = "Enlever Monture";

        // VthirdPersonController.GetComponent<vThirdPersonCamera>().rightOffset = 0.59f;

        if (nomPokemon == "Ponyta")
        {
            PonytaController ponytaController = this.gameObject.transform.parent.gameObject.GetComponent<PonytaController>();
            ponytaController.enabled = true;
        }
        else if(nomPokemon == "Charizard")
        {
            PokemonVolantController pokemonVolantController = this.gameObject.transform.parent.gameObject.GetComponent<PokemonVolantController>();
            pokemonVolantController.enabled = true;
        }

            Animator animatorPokemon = this.gameObject.transform.parent.GetComponent<Animator>(); // Récupération de l'animator
            animatorPokemon.Rebind();

        if (nomPokemon == "Ponyta")
        {
            animatorPokemon.Play("PretAMonterPonyta");
            animatorPokemon.SetInteger("New Int", 1);
        }
        else if(nomPokemon == "Charizard")
        {
            animatorPokemon.Play("DracaufeuPretAVoler");
            animatorPokemon.SetInteger("Vol", 1);
        }

        cmVcamMonture.Follow = this.gameObject.transform.parent.gameObject.transform; // La caméra va suivre le pokemon
        cmVcamMonture.LookAt = this.gameObject.transform.parent.gameObject.transform; // La caméra va regarder le pokemon
        cmVcamMonture.GetRig(0).LookAt = this.gameObject.transform.parent.gameObject.transform; // La caméra va regarder le pokemon
        cmVcamMonture.GetRig(1).LookAt = this.gameObject.transform.parent.gameObject.transform;
        cmVcamMonture.GetRig(2).LookAt = this.gameObject.transform.parent.gameObject.transform;

        VthirdPersonController.GetComponent<Cinemachine.CinemachineBrain>().enabled = true;
    }

    public void descendreMonture()
    {
        string nomPokemon = this.gameObject.name;

        // joueurGameObject.AddComponent<Rigidbody>();
        joueurGameObject.GetComponent<Invector.CharacterController.vThirdPersonInput>().enabled = true;

        // joueurGameObject.GetComponent<CapsuleCollider>().enabled = false;
        joueurGameObject.transform.SetParent(emplacementJoueurs.transform, true);        
        this.gameObject.transform.parent.gameObject.GetComponent<Animator>().Rebind();

        Destroy(this.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>());

      //  Debug.Log(new Vector3(this.gameObject.transform.parent.transform.position.x + 1, this.gameObject.transform.parent.transform.position.y, this.gameObject.transform.parent.transform.position.z));
        joueurGameObject.transform.position = new Vector3(this.gameObject.transform.parent.transform.position.x + 3, this.gameObject.transform.parent.transform.position.y, this.gameObject.transform.parent.transform.position.z);
        joueurGameObject.GetComponent<Animator>().Rebind();
        joueurGameObject.GetComponent<Animator>().enabled = true;

        joueurGameObject.GetComponent<InputJoueurScript>().enabled = true;
        joueurGameObject.GetComponent<Invector.CharacterController.vThirdPersonController>().enabled = true;

        Rigidbody joueurRigibody = joueurGameObject.GetComponent<Rigidbody>();
        joueurRigibody.velocity = Vector3.zero;
        joueurGameObject.GetComponent<Rigidbody>().detectCollisions = true;

        enCheval = false;
        this.gameObject.GetComponent<PixelCrushers.DialogueSystem.DialogueSystemTrigger>().conversation = "Approche Monture";
        declencherDialogue = false;

        // VthirdPersonController.GetComponent<vThirdPersonCamera>().rightOffset = 0.59f;
        if (nomPokemon == "Ponyta")
        {
            PonytaController ponytaController = this.gameObject.transform.parent.gameObject.GetComponent<PonytaController>();
            ponytaController.enabled = false;
        }
        else if(nomPokemon == "Charizard")
        {
            PokemonVolantController pokemonVolantController = this.gameObject.transform.parent.gameObject.GetComponent<PokemonVolantController>();
            pokemonVolantController.enabled = false;
        }

       // Animator animatorPokemon = this.gameObject.transform.parent.GetComponent<Animator>(); // Récupération de l'animator
       // animatorPokemon.Rebind();

       // animatorPokemon.Play("PretAMonterPonyta");
       // animatorPokemon.SetInteger("New Int", 1);

        VthirdPersonController.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator activerDialogue()
    {
        yield return new WaitForSeconds(4f);
        this.gameObject.GetComponent<PixelCrushers.DialogueSystem.DialogueSystemTrigger>().OnUse();
    }

    void activeDialogue()
    {
        // yield return new WaitForSeconds(1f);
        if (joueurGameObject.GetComponent<PlayerInput>().actions["Action1"].ReadValue<float>() == 1 && declencherDialogue == true && dialogueManager.GetComponent<PixelCrushers.DialogueSystem.DialogueSystemController>().IsConversationActive == false)
        {
            this.gameObject.GetComponent<PixelCrushers.DialogueSystem.DialogueSystemTrigger>().OnUse();
            // StartCoroutine(activeDialogue());
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (joueurGameObject.GetComponent<PlayerInput>().actions["Action1"].ReadValue<float>() == 1 && declencherDialogue == true && dialogueManager.GetComponent<PixelCrushers.DialogueSystem.DialogueSystemController>().IsConversationActive == false)
        {
            changerConversationAction();
            this.gameObject.GetComponent<PixelCrushers.DialogueSystem.DialogueSystemTrigger>().OnUse();
           // StartCoroutine(activeDialogue());
        }
    }
}
