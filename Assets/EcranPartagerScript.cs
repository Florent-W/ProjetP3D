using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class EcranPartagerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject VthirdPersonControllerJoueur1GameObject;
    [SerializeField]
    private ProjetP3DScene1.main Main;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject CanvasGameObject;
    [SerializeField]
    private GameObject CameraCombatJoueur;

    public void activerEcranPartager()
    {
    }

    public void activerEcranPartagerInstance()
    {
        GameObject VthirdPersonControllerJoueur2GameObject = Instantiate(VthirdPersonControllerJoueur1GameObject);
        PlayerInput Joueur2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Gamepad", pairWithDevices: new InputDevice[] { Gamepad.all[1] });
        VthirdPersonControllerJoueur2GameObject.GetComponent<vThirdPersonCamera>().SetMainTarget(Joueur2.gameObject.transform);
          
        Main.JoueurManager.JoueursGameObject[0].GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard and Gamepad 1", Keyboard.current, Mouse.current, Gamepad.all[0]);

        VthirdPersonControllerJoueur1GameObject.GetComponent<Camera>().rect = new Rect(0, 0.5f, 1, 0.5f);
        VthirdPersonControllerJoueur2GameObject.GetComponent<Camera>().rect = new Rect(0, 0, 1, 0.5f);
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        if (Main.JoueurManager.Joueurs.Count == 1) // Si le deuxième joueur vient de rejoindre la partie
        {
            ClassLibrary.Dresseur nouveauJoueur = new ClassLibrary.Dresseur(); 
            Main.JoueurManager.Joueurs.Add(nouveauJoueur); // Ajout du joueur dans le joueurManager

            playerInput.gameObject.transform.position = Main.JoueurManager.JoueursGameObject[0].transform.position; // Position du nouveau joueur
            playerInput.gameObject.name = "vThirdPersonCameraJoueur2";
            playerInput.gameObject.transform.parent = Main.JoueurManager.JoueursGameObject[0].transform.parent; // Le gameobject du nouveau joueur se retrouve dans la même famille que les autres joueurs

            GameObject VthirdPersonControllerJoueur2GameObject = Instantiate(VthirdPersonControllerJoueur1GameObject);
            VthirdPersonControllerJoueur2GameObject.name = "vThirdPersonControllerJoueur2";
            VthirdPersonControllerJoueur2GameObject.GetComponent<vThirdPersonCamera>().SetMainTarget(playerInput.gameObject.transform);

            VthirdPersonControllerJoueur1GameObject.GetComponent<Camera>().rect = new Rect(0, 0.5f, 1, 0.5f); // Camera du haut
            VthirdPersonControllerJoueur2GameObject.GetComponent<Camera>().rect = new Rect(0, 0, 1, 0.5f); // Camera du bas
            CanvasGameObject.GetComponent<CanvasScaler>().referenceResolution = new Vector2(2930, 1080); // Mise au point de la résolution de l'interface

            GameObject CanvasInstanceGameObject = Instantiate(CanvasGameObject); // Interface nouveau joueur
            CanvasInstanceGameObject.name = "CanvasJoueur2";

            CanvasInstanceGameObject.transform.GetChild(0).gameObject.GetComponent<MenuStartScript>().Main = Main;

            CanvasInstanceGameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<Button>().onClick = new Button.ButtonClickedEvent(); // Les listeners du bouton pour ouvrir le menu des pokemon sont enlevés car il faut ouvrir l'inventaire de l'autre joueur
            CanvasInstanceGameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => { Main.btn_ouvrir_menu_pokemon_click(1); }); // Ajout du listener avec le joueur en paramètre

            CanvasInstanceGameObject.transform.GetChild(4).gameObject.transform.GetChild(3).gameObject.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            CanvasInstanceGameObject.transform.GetChild(4).gameObject.transform.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(() => { Main.btn_retour_apres_menu_pokemon_click(1); });

            CanvasInstanceGameObject.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(4).gameObject.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            CanvasInstanceGameObject.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(() => { Main.btn_retour_menu_combat_click(1); });

            CanvasInstanceGameObject.transform.GetChild(1).gameObject.transform.GetChild(5).gameObject.transform.GetChild(0).gameObject.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            CanvasInstanceGameObject.transform.GetChild(1).gameObject.transform.GetChild(5).gameObject.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(() => { Main.btn_attaque_click(1); });

            CanvasInstanceGameObject.transform.GetChild(1).gameObject.transform.GetChild(5).gameObject.transform.GetChild(3).gameObject.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            CanvasInstanceGameObject.transform.GetChild(1).gameObject.transform.GetChild(5).gameObject.transform.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(() => { Main.btn_fuite_click(1); });

            Canvas CanvasInstance = CanvasInstanceGameObject.GetComponent<Canvas>();

            CanvasInstance.worldCamera = VthirdPersonControllerJoueur2GameObject.GetComponent<Camera>(); // Le canvas pointe vers la camera du nouveau joueur

            playerInput.gameObject.GetComponent<Invector.CharacterController.vThirdPersonInput>().canvasGameObject = CanvasInstanceGameObject;

            GameObject CameraCombatJoueurInstance = Instantiate(CameraCombatJoueur);
            CameraCombatJoueurInstance.name = "CameraCombatJoueur2";

            Main.JoueurManager.JoueursGameObject[1] = playerInput.gameObject; // Ajout du gameobject du nouveau joueur dans le joueurManager
            Main.cameraCombatJoueurs[1] = CameraCombatJoueurInstance;
            Main.controllerJoueurs[1] = VthirdPersonControllerJoueur2GameObject;
            Main.canvasGameObject[1] = CanvasInstanceGameObject;
        }

    }
}
