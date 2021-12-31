using System.Collections;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Runtime.Serialization;
// using Doozy.Engine.UI;
using System.Xml;
using System.Xml.Serialization;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace ProjetP3DScene1
{
    public class main : MonoBehaviour
    {
        public GameObject pokeballInstance;
        public Transform destinationPokeball;
        public Animator animator;
        public string EnCombatContre = null;
        public bool jeuInitialiser = false;
        public ClassLibrary.Dresseur dresseurAdverse;
        public GameObject pokemonJoueurGameObject, pokemonAdverseGameObject;
        public JoueurManagerScript JoueurManager;

        public GameObject uiPokemonGameObject; // Ui info du pokémon

        [SerializeField]
        private GameObject canvasGameObjectJoueur;

        public ClassLibrary.Jeu jeu = new ClassLibrary.Jeu(); // Parametres du jeu (attaque, pokemon)
        public ClassLibrary.Pokemon pokemon = new ClassLibrary.Pokemon();
        public GameObject[] canvasGameObject = new GameObject[2];
        public GameObject[] cameraCombatJoueurs = new GameObject[2];
        public GameObject[] controllerJoueurs = new GameObject[2];

        public GameObject GameObjectMusique;
        public GameObject GameObjectJoueur;

        ClassLibrary.Pokemon pokemonStarter = new ClassLibrary.Pokemon();

        public ClassLibrary.Attaque attaqueLancerPokemon = new ClassLibrary.Attaque();
        ClassLibrary.Attaque attaqueLancerAdversaire = new ClassLibrary.Attaque();

        public int nbDegats = 0, nbDegatsContreAdversaire = 0;
        public int positionPokemonMenuPokemon = 0;
        public bool jeuEnPause = false;

        public string modeCombat;

        public GameObject mapGameObject;

        public GameObject DialogueManagerGameObject;

        bool gagnePvPokemonJoueur = false, pokemonJoueurAttaquePremier = true, changement_pokemon = false, changementPokemonPokemonKo = false, changementStatutPokemon = false, changementStatutAdversaire = false, showStatistiquesAugmenter = false, showStatistiquesNiveauSuivant = false, reussiteAttaque = false, reussiteAttaqueParalyse = false, reussiteAttaqueParalyseAdversaire = false, reussiteAttaqueGel = false, reussiteAttaqueGelAdversaire = false, coroutineBarreVieJoueurLancer = false, coroutineBarreVieAdversaireLancer = false, messageBoitePCActif = false, OuverturePC = false;
        int compteur = 0, compteurAdversaire = 0, compteurExperience = 0, nombreMouvementsBall = -1, statutPokemonPerdPvJoueur = 0, statutPokemonPerdPvAdversaire = 0, nombreTourStatut = 0, nombreTourStatutAdversaire = 0, nombreTourStatutAEffectuer = 0, nombreTourStatutAEffectuerAdversaire = 0, nombreTourSommeil = 0, nombreTourSommeilAdversaire = 0;
        double bonusCritique = 1;

        GameObject boutons_attaque, boutons_combat, scroll_objets, listeObjetsContent, menu, menuPokemonStatistiques, menuStart, BoiteDialogue, barViePokemonJoueur, barViePokemonAdversaire, barExperiencePokemonJoueur, UICombat, UIJoueur, UIAdversaire, StatistiquesChangementNiveau, TableStarter, BoitePC, PokeballBulbizarre, PokeballSalameche, PokeballCarapuce, menuPC, cameraCombat, cameraCombatUI, cameraMapGameObject, ObjetProcheGameObject, sceneBuilder;
        Image barViePokemonJoueurImage, barViePokemonAdversaireImage, barExperiencePokemonJoueurImage;
        DialogueTrigger dialogueCombat;
        Text LabelPvPokemonJoueurUI, LabelPvPokemonAdversaireUI, LabelNiveauPokemonJoueurUI, BoiteDialogueTexte, LabelPvChangementNiveau, LabelAttaqueChangementNiveau, LabelDefenseChangementNiveau, LabelVitesseChangementNiveau, LabelAttaqueSpecialeChangementNiveau, LabelDefenseSpecialeChangementNiveau, LabelObjetProche;
        int[] positionPokemonMenu = new int[6];
        Animator BoiteDialogueAnimator;
        PlayableDirector TimelineAnimationLancerPokeball, TimelinePokemonAccompagner, TimelineCameraCombat, TimelineAnimationCapture;
        List<AudioClip> MusiquesCombat = new List<AudioClip>();        
 
        /*
        public void SauvegardeJoueur()
        {
            SystemeSauvegarde.sauvegardeJoueur(Joueur);
        }

        public void ChargementJoueur()
        {
            JoueurDonnees donnees = SystemeSauvegarde.chargementJoueur();
            Debug.Log(donnees.Joueur.getNom());

            Joueur.setJoueur(donnees.Joueur.getNom(), donnees.Joueur.getAge(), donnees.Joueur.getPokemonEquipe(), donnees.Joueur.getPokemonPc(), donnees.Joueur.getObjetsSac());
        } */

        /// <summary>
        /// Cette méthode permet de gérer la barre d'expérience du pokémon du joueur en combat
        /// </summary>
        public void rafraichirBarreExperiencePokemonJoueur()
        {
            /*
            btn_attaque1.Enabled = false;
            btn_attraper.Enabled = false;
            btn_soigner.Enabled = false;
            btn_changement_pokemon.Enabled = false; */

            if (pokemon.getGainEvPv() > 0)
            {
                JoueurManager.Joueurs[0].pokemonSelectionner.setEvPv(JoueurManager.Joueurs[0].pokemonSelectionner.getEvPv() + pokemon.getGainEvPv());
            }
            if (pokemon.getGainEvAttaque() > 0)
            {
                JoueurManager.Joueurs[0].pokemonSelectionner.setEvAttaque(JoueurManager.Joueurs[0].pokemonSelectionner.getEvAttaque() + pokemon.getGainEvAttaque());
            }
            if (pokemon.getGainEvDefense() > 0)
            {
                JoueurManager.Joueurs[0].pokemonSelectionner.setEvDefense(JoueurManager.Joueurs[0].pokemonSelectionner.getEvDefense() + pokemon.getGainEvDefense());
            }
            if (pokemon.getGainEvVitesse() > 0)
            {
                JoueurManager.Joueurs[0].pokemonSelectionner.setEvVitesse(JoueurManager.Joueurs[0].pokemonSelectionner.getEvVitesse() + pokemon.getGainEvVitesse());
            }
            if (pokemon.getGainEvAttaqueSpeciale() > 0)
            {
                JoueurManager.Joueurs[0].pokemonSelectionner.setEvAttaqueSpeciale(JoueurManager.Joueurs[0].pokemonSelectionner.getEvAttaqueSpeciale() + pokemon.getGainEvAttaqueSpeciale());
            }
            if (pokemon.getGainEvDefenseSpeciale() > 0)
            {
                JoueurManager.Joueurs[0].pokemonSelectionner.setEvDefenseSpeciale(JoueurManager.Joueurs[0].pokemonSelectionner.getEvDefenseSpeciale() + pokemon.getGainEvDefenseSpeciale());
            }

            compteurExperience = (100 * (JoueurManager.Joueurs[0].pokemonSelectionner.getExperience() - JoueurManager.Joueurs[0].pokemonSelectionner.getExperiencePokemonReturn())) / (JoueurManager.Joueurs[0].pokemonSelectionner.getExperiencePokemonProchainNiveau() - JoueurManager.Joueurs[0].pokemonSelectionner.getExperiencePokemonReturn());
            double experienceGagner = JoueurManager.Joueurs[0].pokemonSelectionner.gainExperiencePokemonBattu(pokemon);

            dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " a obtenu " + experienceGagner + " points d'expérience");
            StartCoroutine(DialogueCombat());

          //  statutPokemonPerdPvAdversaire = 0;

            StartCoroutine("timerBarreExperienceStart");
        }

        public void DeclenchementCombat(int positionJoueur)
        {
            if (JoueurManager.Joueurs[positionJoueur].getPokemonEquipe().Count > 0 && JoueurManager.Joueurs[positionJoueur].enCombat == false)
            {
                JoueurManager.Joueurs[positionJoueur].enCombat = true;

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                if (EnCombatContre != "PokemonSauvage")
                {
                    EnCombatContre = "PokemonDresseur";
                }

                pokemonJoueurGameObject = GameObject.Find("PokemonJoueur");

                if (GameObjectJoueur != null)
                {
                    Destroy(pokemonJoueurGameObject);
                }

              // Destroy(GameObject.FindGameObjectWithTag("PokemonAdverse"));

               // GameObjectJoueur.GetComponent<PokeballLancer>().lancerPokeball();

                Vector3 destinationPositionPokeball = new Vector3(destinationPokeball.transform.position.x, destinationPokeball.transform.position.y, destinationPokeball.transform.position.z - 0.5f);
                GameObject pokeball = (GameObject)Instantiate(Resources.Load("Models/Pokeballs/Pokeball"), destinationPositionPokeball, destinationPokeball.rotation, GameObjectJoueur.transform);
                //  GameObject pokeball = Instantiate(pokeballInstance, destinationPositionPokeball, destinationPokeball.rotation);
                Animator animatorPokeball = pokeball.GetComponent<Animator>();
                SignalReceiver signalPokeball = pokeball.GetComponent<SignalReceiver>(); 

                int idPokedex = JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getNoIdPokedex();
                pokeball.GetComponent<AfterThrowingPokeball>().NumeroPokedexPokemon = idPokedex;
         
                AudioSource GameObjectMusiqueAudioSource = GameObjectMusique.GetComponent<AudioSource>();

                GameObjectMusiqueAudioSource.clip = MusiquesCombat[Random.Range(0, MusiquesCombat.Count)];

                GameObjectMusiqueAudioSource.Play();

                foreach (Transform child in UIJoueur.transform.GetChild(8).gameObject.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

                foreach (Transform child in UIAdversaire.transform.GetChild(6).gameObject.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

                for (int i = 0; i < JoueurManager.Joueurs[positionJoueur].getPokemonEquipe().Count; i++)
                {
                    try
                    {
                        Vector3 destinationPokeball = new Vector3(-350 + (90 * i), -40, 0);
                       // Quaternion rotationPokeball = Quaternion.Euler(0, 0, 0);
                        int nombrePokemon = i + 1;

                        GameObject pokeballMenu = new GameObject("Pokeball" + nombrePokemon + "Menu");
                        pokeballMenu.AddComponent<Image>();
                        pokeballMenu.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Pokeball/pokeball_3");

                        pokeballMenu.transform.SetParent(UIJoueur.transform.GetChild(8).gameObject.transform);
                        pokeballMenu.transform.localPosition = destinationPokeball;
                        pokeballMenu.transform.localRotation = Quaternion.identity;
                        pokeballMenu.transform.localScale = new Vector3(1, 1, 1);

                    }
                    catch
                    {

                    }
                }

                if (EnCombatContre == "PokemonSauvage")
                {
                    try
                    {
                        Vector3 destinationPokeball = new Vector3(-350, 0, 0);
                        // Quaternion rotationPokeball = Quaternion.Euler(0, 0, 0);
                        int nombrePokemon = 0 + 1;

                        GameObject pokeballMenu = new GameObject("Pokeball" + nombrePokemon + "Menu");
                        pokeballMenu.AddComponent<Image>();
                        pokeballMenu.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Pokeball/pokeball_3");

                        pokeballMenu.transform.SetParent(UIAdversaire.transform.GetChild(6).gameObject.transform);
                        pokeballMenu.transform.localPosition = destinationPokeball;
                        pokeballMenu.transform.localRotation = Quaternion.identity;
                        pokeballMenu.transform.localScale = new Vector3(1, 1, 1);
                    }
                    catch
                    {

                    }
                }

                else if (EnCombatContre == "PokemonDresseur")
                {
                    for (int i = 0; i < dresseurAdverse.getPokemonEquipe().Count; i++)
                    {
                        try
                        {
                            Vector3 destinationPokeball = new Vector3(-350 + (90 * i), 0, 0);
                            // Quaternion rotationPokeball = Quaternion.Euler(0, 0, 0);
                            int nombrePokemon = i + 1;

                            GameObject pokeballMenu = new GameObject("Pokeball" + nombrePokemon + "Menu");
                            pokeballMenu.AddComponent<Image>();
                            pokeballMenu.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Pokeball/pokeball_3");

                            pokeballMenu.transform.SetParent(UIAdversaire.transform.GetChild(6).gameObject.transform);
                            pokeballMenu.transform.localPosition = destinationPokeball;
                            pokeballMenu.transform.localRotation = Quaternion.identity;
                            pokeballMenu.transform.localScale = new Vector3(1, 1, 1);

                        }
                        catch
                        {

                        }
                    }
                }
                
                animator.Play("Throw pokeball");

                TimelineAsset timelineAsset = (TimelineAsset)TimelineAnimationLancerPokeball.playableAsset;
                TrackAsset trackAsset = timelineAsset.GetOutputTrack(2);
                TrackAsset trackAsset2 = timelineAsset.GetOutputTrack(3);
                TrackAsset trackAsset10 = timelineAsset.GetOutputTrack(11);

                TimelineAnimationLancerPokeball.SetGenericBinding(trackAsset, animatorPokeball);
                TimelineAnimationLancerPokeball.SetGenericBinding(trackAsset2, signalPokeball);
                TimelineAnimationLancerPokeball.SetGenericBinding(trackAsset10, signalPokeball);

                if (TimelineAnimationLancerPokeball.state == PlayState.Playing)
                {
                    TimelineAnimationLancerPokeball.Stop();
                }

               // pokeball.GetComponent<LancerObjetScript>().lancerObjet();

                TimelineAnimationLancerPokeball.Play();
                TimelineCameraCombat.Play();

                Destroy(pokeball, 15); 
                

                if (EnCombatContre == "PokemonRandom")
                {
                    pokemon = pokemon.setRandomPokemon(jeu);
                }
                else if(EnCombatContre == "PokemonDresseur")
                {
                    pokemon = dresseurAdverse.getPokemonEquipe()[0];
                }
                else if(EnCombatContre == "PokemonSauvage")
                {
                    // int.TryParse(GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().pokemonAdverseGameObject.name, out idPokedex);
                    pokemon = pokemonAdverseGameObject.GetComponent<StatistiquesPokemon>().GetPokemon();
                   // pokemon = pokemon.setChercherPokemonParNoId(idPokedex, jeu);
                }

               // pokemon.setPvRestant(pokemon.getPv());
                GameObject spawnPokemonGameObject = GameObject.Find("SpawnPokemon");
                idPokedex = pokemon.getNoIdPokedex();

                pokeball.GetComponent<AfterThrowingPokeball>().positionJoueur = positionJoueur;
                pokeball.GetComponent<AfterThrowingPokeball>().NumeroPokedexPokemonAdverse = idPokedex;

                rafraichirBarreViePokemonJoueur2(positionJoueur);
                rafraichirBarreViePokemonAdversaire2(positionJoueur);
                canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.SetActive(true);

                cameraCombatJoueurs[positionJoueur].SetActive(true); 
                controllerJoueurs[positionJoueur].SetActive(false);

                canvasGameObject[positionJoueur].GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera; // Passage du rendu du canvas en camera
                canvasGameObject[positionJoueur].GetComponent<Canvas>().worldCamera = cameraCombatJoueurs[positionJoueur].transform.GetChild(1).gameObject.GetComponent<Camera>();

                if (JoueurManager.Joueurs.Count == 2)
                {
                    if (positionJoueur == 0)
                    {
                        cameraCombatJoueurs[positionJoueur].transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0, 0.5f, 1, 0.5f);
                        cameraCombatJoueurs[positionJoueur].transform.GetChild(1).gameObject.GetComponent<Camera>().rect = new Rect(0, 0.5f, 1, 0.5f);
                    }
                    else if (positionJoueur == 1)
                    {
                        cameraCombatJoueurs[positionJoueur].transform.GetChild(0).gameObject.GetComponent<Camera>().rect = new Rect(0, 0, 1, 0.5f);
                        cameraCombatJoueurs[positionJoueur].transform.GetChild(1).gameObject.GetComponent<Camera>().rect = new Rect(0, 0, 1, 0.5f);
                    }

                    canvasGameObject[positionJoueur].GetComponent<Canvas>().planeDistance = 0.32f;
                    canvasGameObject[positionJoueur].GetComponent<CanvasScaler>().referenceResolution = new Vector2(3900, 1080);
                }

                StartDialogueCombat(positionJoueur);
            }
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
          //  DialogueManagerGameObject.GetComponent<DialogueManager>().DisplayNextSentence(DialogueManagerGameObject.GetComponent<DialogueTrigger>().getDialogue());
        } */
    }

        public void changementPokemon_click()
        {
            //  textBox1.Text += pokemonJoueurSelectionner.getNom() + " a " + pokemonJoueurSelectionner.getPvRestant() + " PV" + Environment.NewLine;

            //  cb_choix_objets.Visible = false;
            //  btn_choix_objet.Visible = false;

            if (JoueurManager.Joueurs[0].enCombat == true)
            {
                if (JoueurManager.Joueurs[0].pokemonSelectionner == JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon]) // Si le pokémon est déjà sur le terrain, il ne peut pas rentrer
                {
                    dialogueCombat.getDialogue().AddSentence("Le pokemon est déjà sur le terrain");
                    StartCoroutine(DialogueCombat());
                }
                else
                {

                    if (JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getPvRestant() <= 0) // Si le pokémon selectionné est K.O., il ne peut pas rentrer
                    {
                        dialogueCombat.getDialogue().AddSentence("Le pokémon est K.O.");
                        StartCoroutine(DialogueCombat());
                    }

                    else if (JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getPvRestant() > 0) // Si le pokémon selectionné n'est pas K.O et qu'il n'est pas sur le terrain, il peut rentrer
                    {
                        JoueurManager.Joueurs[0].pokemonSelectionner = JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon]; // Il prend la place du pokémon sur le terrain

                        int idPokedexImage = JoueurManager.Joueurs[0].pokemonSelectionner.getNoIdPokedex();

                        try
                        {
                            boutons_combat.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().preserveAspect = true;
                            boutons_combat.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Icones/" + JoueurManager.Joueurs[0].pokemonSelectionner.getNoIdPokedex());
                        }
                        catch
                        {

                        }

                        /*
                        try
                        {

                            Bitmap png = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + idPokedexImage + ".png");
                            pictureBoxPokemonCombatJoueur.Image = png;
                            pictureBoxPokemonCombatJoueur.Image.RotateFlip(RotateFlipType.Rotate180FlipY);

                        }
                        catch
                        {
                            MessageBox.Show("L'image du pokémon n'a pas pu être chargée. Veuillez vérifier que celle-ci est bien présente dans le répertoire.", "Vérification de l'image du pokémon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        pictureBoxBasePokemonJoueur.Controls.Add(pictureBoxPokemonCombatJoueur);
                        pictureBoxPokemonCombatJoueur.Location = new Point(26, 8);

                        label_nom_pokemon_combat_joueur.Text = pokemonJoueurSelectionner.getNom();

                        try
                        {
                            Bitmap pngIconePokemonMenuCombat = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\Icones\\" + pokemonJoueurSelectionner.getNoIdPokedex() + ".png");
                            pictureBoxIconePokemon.Image = pngIconePokemonMenuCombat;
                        }
                        catch
                        {
                            MessageBox.Show("L'icône du pokémon n'a pas pu être chargée. Veuillez vérifier que celle-ci est bien présente dans le répertoire.", "Vérification de l'icône du pokémon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        if (pokemonJoueurSelectionner.getSexe() == "Feminin")
                        {
                            label_sexe_pokemon_combat_joueur.Text = "♀";
                            label_sexe_pokemon_combat_joueur.ForeColor = Color.Pink;
                        }
                        else if (pokemonJoueurSelectionner.getSexe() == "Masculin")
                        {
                            label_sexe_pokemon_combat_joueur.Text = "♂";
                            label_sexe_pokemon_combat_joueur.ForeColor = Color.Blue;
                        }

                        panel_choix_pokemon_selection.Visible = false;
                        btn_attaque1.Enabled = true;
                        btn_attraper.Enabled = true;
                        btn_soigner.Enabled = true;
                        btn_changement_pokemon.Enabled = true;
                        */
                        compteur = JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant();

                        changement_pokemon = true;
                        nombreTourStatut = 0;
                        nombreTourSommeil = 0;
                        nombreTourStatutAEffectuer = 0;

                        rafraichirBarreViePokemonJoueur2(0); // Rafraichissement de l'interface de combat côté pokémon joueur avec les nouvelles données du pokémon entré

                        // rafraichirBarreViePokemonJoueur1();

                        // label_niveau_pokemon_combat_joueur.Text = "N. " + pokemonJoueurSelectionner.getNiveau().ToString();
                        rafraichirBarreExperiencePokemonJoueur(); // Rafraichissement de la barre d'expérience dans l'interface de combat avec l'expérience du pokémon entré

                        dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " est entré sur le terrain ");
                        StartCoroutine(DialogueCombat());

                        ClassLibrary.Attaque attaqueLancerAdversaire = pokemon.attaqueAdversaire(pokemon, JoueurManager.Joueurs[0].pokemonSelectionner); // On sélectionne l'attaque que va faire le pokémon adverse vu que c'est son tour d'attaquer
                        attaqueCombatAdversaire(attaqueLancerAdversaire); // Et on applique l'attaque de l'adversaire
                    }
                }
            }
            else
            {
                btn_retour_menu_pokemon_apres_menu_pokemon_options_click();
                btn_retour_apres_menu_pokemon_click(0);
                menuStart.GetComponent<MenuStartScript>().btn_retour_apres_menu_start_click();

                Vector3 destinationPositionPokeball = new Vector3(destinationPokeball.transform.position.x, destinationPokeball.transform.position.y, destinationPokeball.transform.position.z - 0.5f);
                GameObject pokeball = (GameObject)Instantiate(Resources.Load("Models/Pokeballs/Pokeball"), destinationPositionPokeball, destinationPokeball.rotation, GameObjectJoueur.transform);
                Animator animatorPokeball = pokeball.GetComponent<Animator>();
                SignalReceiver signalPokeball = pokeball.GetComponent<SignalReceiver>();

                int idPokedex = JoueurManager.Joueurs[0].pokemonSelectionner.getNoIdPokedex();
                pokeball.GetComponent<AfterThrowingPokeball>().NumeroPokedexPokemon = idPokedex;

                animator.Play("Throw pokeball");

                TimelineAsset timelineAsset = (TimelineAsset)TimelinePokemonAccompagner.playableAsset;
                TrackAsset trackAsset = timelineAsset.GetOutputTrack(2);
                TrackAsset trackAsset2 = timelineAsset.GetOutputTrack(3);

                TimelinePokemonAccompagner.SetGenericBinding(trackAsset, animatorPokeball);
                TimelinePokemonAccompagner.SetGenericBinding(trackAsset2, signalPokeball);

                TimelinePokemonAccompagner.Play();

                Destroy(pokeball, 15);
            }
        }

        public void rafraichirBarreViePokemonJoueur()
    {
            if (statutPokemonPerdPvJoueur == 0)
            {
                if (gagnePvPokemonJoueur != true)
                {
                    for (int i = 0; i <= JoueurManager.Joueurs[0].getPokemonEquipe().Count - 1; i++)
                    {
                        if (JoueurManager.Joueurs[0].pokemonSelectionner == JoueurManager.Joueurs[0].getPokemonEquipe()[i])
                        {
                            if (pokemon.getPvRestant() > 0)
                            {
                                if (pokemon.getListeAttaque().Count > 0 && pokemon.getAttaque1().getNom() != "default")
                                {
                                    if (nombreTourSommeilAdversaire > nombreTourStatutAEffectuerAdversaire && pokemon.getStatutPokemon() == "Sommeil")
                                    {
                                        dialogueCombat.getDialogue().AddSentence(pokemon.getNom() + " adverse se réveille");
                                      //  StartCoroutine(DialogueCombat());
                                        pokemon.setStatutPokemon("Normal");
                                      //  pictureBoxStatutPokemonCombatAdversaire.Image = null;
                                        nombreTourSommeilAdversaire = 0;
                                        nombreTourStatutAEffectuerAdversaire = 0;
                                    }
                                    attaqueLancerAdversaire = pokemon.attaqueAdversaire(pokemon, JoueurManager.Joueurs[0].pokemonSelectionner);

                                    if (attaqueLancerAdversaire != null)
                                    {
                                        changementStatutPokemon = pokemon.getAttaqueChangementStatutPokemonAdverseReussi(attaqueLancerAdversaire);
                                        reussiteAttaque = pokemon.getReussiteAttaque(JoueurManager.Joueurs[0].pokemonSelectionner.getProbabiliteReussiteAttaque(pokemon, JoueurManager.Joueurs[0].pokemonSelectionner, attaqueLancerAdversaire));
                                        bonusCritique = JoueurManager.Joueurs[0].pokemonSelectionner.getCoupCritique(pokemon.getProbabiliteCoupCritique(pokemon));
                                        nbDegats = pokemon.attaqueWithNomAttaque(pokemon, JoueurManager.Joueurs[0].pokemonSelectionner, attaqueLancerAdversaire, bonusCritique, changementStatutPokemon, ref nombreTourStatut, ref reussiteAttaqueParalyseAdversaire, ref reussiteAttaqueGelAdversaire, ref nombreTourSommeilAdversaire);

                                        if (pokemon.getStatutPokemon() != "Sommeil")
                                        {
                                            if (reussiteAttaque == true)
                                            {
                                                dialogueCombat.getDialogue().AddSentence(pokemon.getNom() + " adverse lance " + attaqueLancerAdversaire.getNom());
                                               // StartCoroutine(DialogueCombat());
                                                
                                                if (pokemon.getStatutPokemon() != "Paralysie" || (pokemon.getStatutPokemon() == "Paralysie" && reussiteAttaqueParalyseAdversaire == true))
                                                {
                                                    if (pokemon.getStatutPokemon() != "Gelé" || (pokemon.getStatutPokemon() == "Gelé" && reussiteAttaqueGelAdversaire == true))
                                                    {

                                                        if (bonusCritique == 1.5)
                                                        {
                                                            dialogueCombat.getDialogue().AddSentence("Coup Critique");
                                                          //  StartCoroutine(DialogueCombat());
                                                            bonusCritique = 1;
                                                        }

                                                        if (pokemon.getEfficaciteAttaque(attaqueLancerAdversaire, JoueurManager.Joueurs[0].pokemonSelectionner) != 1)
                                                        {
                                                            dialogueCombat.getDialogue().AddSentence(pokemon.getEfficaciteAttaqueTexte(pokemon.getEfficaciteAttaque(attaqueLancerAdversaire, JoueurManager.Joueurs[0].pokemonSelectionner)));
                                                         //   StartCoroutine(DialogueCombat());
                                                        }

                                                        if (reussiteAttaqueGelAdversaire == true)
                                                        {
                                                            dialogueCombat.getDialogue().AddSentence(pokemon.getNom() + " adverse redevient normal");
                                                        //    StartCoroutine(DialogueCombat());
                                                            pokemon.setStatutPokemon("Normal");
                                                         //   pictureBoxStatutPokemonCombatAdversaire.Image = null;
                                                        }



                                                        if (changementStatutPokemon == true)
                                                        {
                                                            string statutPokemon = JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon();
                                                            if (statutPokemon != "Normal")
                                                            {
                                                                try
                                                                {
                                                                 //   Bitmap pngChangementStatutPokemon = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\Statut\\" + statutPokemon + ".png");
                                                                 //   pictureBoxStatutPokemonCombatJoueur.Image = pngChangementStatutPokemon;
                                                                }
                                                                catch
                                                                {
                                                                 //   MessageBox.Show("L'image du statut du pokémon n'a pas pu être chargée. Veuillez vérifier que celle-ci est bien présente dans le répertoire.", "Vérification de l'image du statut du pokémon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                }
                                                            }

                                                            changementStatutPokemon = false;
                                                        }

                                                        dialogueCombat.getDialogue().AddSentence(pokemon.getNom() + " adverse a fait " + nbDegats + " dégâts ");
                                                        dialogueCombat.getDialogue().AddSentence(pokemon.getEfficaciteAttaqueTexte(pokemon.getEfficaciteAttaque(attaqueLancerAdversaire, JoueurManager.Joueurs[0].pokemonSelectionner)));
                                                      //  StartCoroutine(DialogueCombat());
                                                    }
                                                    else
                                                    {
                                                        dialogueCombat.getDialogue().AddSentence(pokemon.getNom() + " adverse est gelé. Il ne peut pas attaquer");
                                                      //  StartCoroutine(DialogueCombat());
                                                    }
                                                }

                                                else
                                                {
                                                    dialogueCombat.getDialogue().AddSentence(pokemon.getNom() + " adverse est paralysé. Il ne peut pas attaquer");
                                                  //  StartCoroutine(DialogueCombat());
                                                }
                                            }
                                            else
                                            {
                                                dialogueCombat.getDialogue().AddSentence(pokemon.getNom() + " rate son attaque");
                                               // StartCoroutine(DialogueCombat());
                                            }
                                        }
                                        else
                                        {
                                            if (nombreTourStatutAEffectuerAdversaire == 0)
                                            {
                                                nombreTourStatutAEffectuerAdversaire = pokemon.getNombreTourSommeilAEffectuer();
                                            }

                                            dialogueCombat.getDialogue().AddSentence(pokemon.getNom() + " adverse est endormi. Il ne peut pas attaquer");
                                           // StartCoroutine(DialogueCombat());
                                            nombreTourSommeilAdversaire++;
                                        }
                                    }
                                }
                                else
                                {
                                   // MessageBox.Show("Le pokemon n'a aucune attaque", "Vérification des attaques", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }


                            }
                        }
                    }
                }
                else
                {
                    pokemonJoueurAttaquePremier = true;
                }
            }

            else if (statutPokemonPerdPvJoueur == 1)
            {
                statutPokemonPerdPvJoueur = 2;
            }

            if (pokemonJoueurAttaquePremier == true)
            {
                /*
                btn_attaque1.Enabled = false;
                btn_attraper.Enabled = false;
                btn_soigner.Enabled = false;
                btn_changement_pokemon.Enabled = false; */
            }

            //  StartCoroutine("timerBarreJoueurStart");
           // tourCombatPokemon();
        }

        public void rafraichirBarreViePokemonAdversaire()
        {
            if (statutPokemonPerdPvAdversaire == 0)
            {
                if (JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() > 0)
                {
                    if (nombreTourSommeil > nombreTourStatutAEffectuer && JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Sommeil")
                    {
                        dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " se réveille");
                        StartCoroutine(DialogueCombat());
                        JoueurManager.Joueurs[0].pokemonSelectionner.setStatutPokemon("Normal");
                        nombreTourSommeil = 0;
                        nombreTourStatutAEffectuer = 0;
                    }

                    bool changementStatutAdversaire = JoueurManager.Joueurs[0].pokemonSelectionner.getAttaqueChangementStatutPokemonAdverseReussi(attaqueLancerPokemon);
                    bool reussiteAttaque = JoueurManager.Joueurs[0].pokemonSelectionner.getReussiteAttaque(JoueurManager.Joueurs[0].pokemonSelectionner.getProbabiliteReussiteAttaque(JoueurManager.Joueurs[0].pokemonSelectionner, pokemon, attaqueLancerPokemon));
                    double bonusCritique = JoueurManager.Joueurs[0].pokemonSelectionner.getCoupCritique(JoueurManager.Joueurs[0].pokemonSelectionner.getProbabiliteCoupCritique(JoueurManager.Joueurs[0].pokemonSelectionner));
                    nbDegatsContreAdversaire = JoueurManager.Joueurs[0].pokemonSelectionner.attaqueWithNomAttaque(JoueurManager.Joueurs[0].pokemonSelectionner, pokemon, attaqueLancerPokemon, bonusCritique, changementStatutAdversaire, ref nombreTourStatutAdversaire, ref reussiteAttaqueParalyse, ref reussiteAttaqueGel, ref nombreTourSommeil);

                    if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() != "Sommeil")
                    {
                        if (reussiteAttaque == true)
                        {
                            dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " lance " + attaqueLancerPokemon.getNom());
                          //  StartCoroutine(DialogueCombat());

                            if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() != "Paralysie" || (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Paralysie" && reussiteAttaqueParalyse == true))
                            {
                                if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() != "Gelé" || (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Gelé" && reussiteAttaqueGel == true))
                                {

                                    if (bonusCritique == 1.5)
                                    {
                                        dialogueCombat.getDialogue().AddSentence("Coup Critique");
                                       // StartCoroutine(DialogueCombat());
                                        bonusCritique = 1;
                                    }

                                    if (JoueurManager.Joueurs[0].pokemonSelectionner.getEfficaciteAttaque(attaqueLancerPokemon, pokemon) != 1)
                                    {
                                        dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getEfficaciteAttaqueTexte(JoueurManager.Joueurs[0].pokemonSelectionner.getEfficaciteAttaque(attaqueLancerPokemon, pokemon)));
                                       // StartCoroutine(DialogueCombat());
                                    }

                                    if (reussiteAttaqueGel == true)
                                    {
                                        dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " redevient normal");
                                        //  StartCoroutine(DialogueCombat());
                                        JoueurManager.Joueurs[0].pokemonSelectionner.setStatutPokemon("Normal");
                                       // pictureBoxStatutPokemonCombatJoueur.Image = null;

                                    }

                                    if (changementStatutAdversaire == true)
                                    {
                                        string statutPokemonAdversaire = pokemon.getStatutPokemon();
                                        if (statutPokemonAdversaire != "Normal")
                                        {
                                            try
                                            {
                                             //   Bitmap pngStatutPokemonAdverse = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\Statut\\" + statutPokemonAdversaire + ".png");
                                            //    pictureBoxStatutPokemonCombatAdversaire.Image = pngStatutPokemonAdverse;
                                            }
                                            catch
                                            {
                                              //  MessageBox.Show("L'image du statut du pokémon adverse n'a pas pu être chargée. Veuillez vérifier que celle-ci est bien présente dans le répertoire.", "Vérification de l'image du statut du pokémon adverse", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                            // statutPokemonPerdPv = 1;
                                        }
                                        changementStatutAdversaire = false;
                                    }

                                }
                                else
                                {
                                    dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " est gelé. Il ne peut pas attaquer");
                                  //  StartCoroutine(DialogueCombat());
                                }
                            }
                            else
                            {
                                dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " est paralysé. Il ne peut pas attaquer");
                             //   StartCoroutine(DialogueCombat());
                            }
                        }
                        else
                        {
                            dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " rate son attaque");
                         //   StartCoroutine(DialogueCombat());
                        }

                    }
                    else
                    {
                        if (nombreTourStatutAEffectuer == 0)
                        {
                            nombreTourStatutAEffectuer = JoueurManager.Joueurs[0].pokemonSelectionner.getNombreTourSommeilAEffectuer();
                        }

                        dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " est endormi. Il ne peut pas attaquer");
                       // StartCoroutine(DialogueCombat());
                        nombreTourSommeil++;
                    }
                }
            }

            else if (statutPokemonPerdPvAdversaire == 1)
            {
                statutPokemonPerdPvAdversaire = 2;
            }

            if (pokemonJoueurAttaquePremier == false)
            {
                /*
                btn_attraper.Enabled = false;
                btn_soigner.Enabled = false;
                btn_attaque1.Enabled = false;
                btn_changement_pokemon.Enabled = false; */
            }
            // timerBarrePokemonAdversaire.Start();
            // StartCoroutine("timerBarreAdversaireStart");
         //   tourCombatPokemon();
        }

        /// <summary>
        /// Cette méthode permet de voir quel pokémon va attaquer en premier selon les différents paramètres
        /// </summary>
        public void attaqueCombatReecriture(ClassLibrary.Attaque attaqueLancerPokemonJoueur)
        {
            boutons_attaque.SetActive(false);

            ClassLibrary.Attaque attaqueLancerAdversaire = pokemon.attaqueAdversaire(pokemon, JoueurManager.Joueurs[0].pokemonSelectionner);

            if (attaqueLancerPokemonJoueur.getPrioriteAttaque() > attaqueLancerAdversaire.getPrioriteAttaque())
            {
                pokemonJoueurAttaquePremier = true;
                attaqueCombatPokemonJoueur(attaqueLancerPokemonJoueur);
                if (attaqueLancerAdversaire != null)
                {
                  //  attaqueCombatAdversaire(attaqueLancerAdversaire);
                    StartCoroutine("tourCombatPokemonStartTourContrePokemonAdversaireEnPremier");
                }
            }
            else if (attaqueLancerPokemonJoueur.getPrioriteAttaque() < attaqueLancerAdversaire.getPrioriteAttaque())
            {
                pokemonJoueurAttaquePremier = false;
                if (attaqueLancerAdversaire != null)
                {
                  //  attaqueCombatAdversaire(attaqueLancerAdversaire);
                    StartCoroutine("tourCombatPokemonStartTourContrePokemonJoueurEnPremier");
                }
                attaqueCombatPokemonJoueur(attaqueLancerPokemonJoueur);
            }
            else
            {
                if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatistiquesVitesse() > pokemon.getStatistiquesVitesse())
                {
                    pokemonJoueurAttaquePremier = true;
                    attaqueCombatPokemonJoueur(attaqueLancerPokemonJoueur);
                    if (attaqueLancerAdversaire != null)
                    {
                      //  attaqueCombatAdversaire(attaqueLancerAdversaire);
                        StartCoroutine("tourCombatPokemonStartTourContrePokemonAdversaireEnPremier");
                    }
                }
                else if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatistiquesVitesse() < pokemon.getStatistiquesVitesse())
                {
                    pokemonJoueurAttaquePremier = false;
                    if (attaqueLancerAdversaire != null)
                    {
                       // attaqueCombatAdversaire(attaqueLancerAdversaire);
                        StartCoroutine("tourCombatPokemonStartTourContrePokemonJoueurEnPremier");
                    }
                    attaqueCombatPokemonJoueur(attaqueLancerPokemonJoueur);
                }
                else
                {
                    List<ClassLibrary.Attaque> liste_attaque_vitesse = new List<ClassLibrary.Attaque>();

                    liste_attaque_vitesse.Add(attaqueLancerPokemonJoueur);
                    liste_attaque_vitesse.Add(attaqueLancerAdversaire);

                    int index = Random.Range(1, liste_attaque_vitesse.Count);

                    if (liste_attaque_vitesse[index] == attaqueLancerPokemonJoueur)
                    {
                        pokemonJoueurAttaquePremier = true;
                        attaqueCombatPokemonJoueur(attaqueLancerPokemonJoueur);
                        if (attaqueLancerAdversaire != null)
                        {
                          //  attaqueCombatAdversaire(attaqueLancerAdversaire);
                            StartCoroutine("tourCombatPokemonStartTourContrePokemonAdversaireEnPremier");
                        }
                    }
                    else if (liste_attaque_vitesse[index] == attaqueLancerAdversaire)
                    {
                        pokemonJoueurAttaquePremier = false;
                        if (attaqueLancerAdversaire != null)
                        {
                           // attaqueCombatAdversaire(attaqueLancerAdversaire);
                            StartCoroutine("tourCombatPokemonStartTourContrePokemonJoueurEnPremier");
                        }
                        attaqueCombatPokemonJoueur(attaqueLancerPokemonJoueur);
                    }
                }
            }
        }

        /// <summary>
        /// Cette méthode permet de voir quel pokémon va attaquer en premier selon les différents paramètres
        /// </summary>
        public void attaqueCombat(ClassLibrary.Attaque attaqueLancerPokemonJoueur)
        {
            boutons_attaque.SetActive(false);

            ClassLibrary.Attaque attaqueLancerAdversaire = pokemon.attaqueAdversaire(pokemon, JoueurManager.Joueurs[0].pokemonSelectionner);

            if (attaqueLancerPokemonJoueur.getPrioriteAttaque() > attaqueLancerAdversaire.getPrioriteAttaque())
            {
                pokemonJoueurAttaquePremier = true;
                attaqueCombatPokemonJoueur(attaqueLancerPokemonJoueur);
                if (attaqueLancerAdversaire != null)
                {
                    attaqueCombatAdversaire(attaqueLancerAdversaire);
                }
            }
            else if (attaqueLancerPokemonJoueur.getPrioriteAttaque() < attaqueLancerAdversaire.getPrioriteAttaque())
            {
                pokemonJoueurAttaquePremier = false;
                if (attaqueLancerAdversaire != null)
                {
                    attaqueCombatAdversaire(attaqueLancerAdversaire);
                }
                attaqueCombatPokemonJoueur(attaqueLancerPokemonJoueur);
            }
            else
            {
                if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatistiquesVitesse() > pokemon.getStatistiquesVitesse())
                {
                    pokemonJoueurAttaquePremier = true;
                    attaqueCombatPokemonJoueur(attaqueLancerPokemonJoueur);
                    if (attaqueLancerAdversaire != null)
                    {
                        attaqueCombatAdversaire(attaqueLancerAdversaire);
                    }
                }
                else if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatistiquesVitesse() < pokemon.getStatistiquesVitesse())
                {
                    pokemonJoueurAttaquePremier = false;
                    if (attaqueLancerAdversaire != null)
                    {
                        attaqueCombatAdversaire(attaqueLancerAdversaire);
                    }
                    attaqueCombatPokemonJoueur(attaqueLancerPokemonJoueur);
                }
                else
                {
                    List<ClassLibrary.Attaque> liste_attaque_vitesse = new List<ClassLibrary.Attaque>();

                    liste_attaque_vitesse.Add(attaqueLancerPokemonJoueur);
                    liste_attaque_vitesse.Add(attaqueLancerAdversaire);

                    int index = Random.Range(1, liste_attaque_vitesse.Count);

                    if (liste_attaque_vitesse[index] == attaqueLancerPokemonJoueur)
                    {
                        pokemonJoueurAttaquePremier = true;
                        attaqueCombatPokemonJoueur(attaqueLancerPokemonJoueur);
                        if (attaqueLancerAdversaire != null)
                        {
                            attaqueCombatAdversaire(attaqueLancerAdversaire);
                        }
                    }
                    else if (liste_attaque_vitesse[index] == attaqueLancerAdversaire)
                    {
                        pokemonJoueurAttaquePremier = false;
                        if (attaqueLancerAdversaire != null)
                        {
                            attaqueCombatAdversaire(attaqueLancerAdversaire);
                        }
                        attaqueCombatPokemonJoueur(attaqueLancerPokemonJoueur);
                    }
                }
            }
        }

        public void activerMenuCombatApresTour()
        {
            boutons_combat.SetActive(true);
            boutons_combat.transform.GetChild(0).gameObject.GetComponent<Button>().Select();
        }

        public void desactiverMenuCombat()
        {
            UIJoueur.SetActive(false);
            UIAdversaire.SetActive(false);
            boutons_combat.SetActive(false);
            BoiteDialogue.SetActive(false);
        }

        public void attaqueCombatPokemonJoueur(ClassLibrary.Attaque attaqueLancer)
        {
            if (JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() > 0)
            {
                attaqueLancerPokemon = attaqueLancer;
            }
        }

        public void attaqueCombatAdversaire(ClassLibrary.Attaque attaqueLancer)
        {
            if (JoueurManager.Joueurs[0].pokemonSelectionner == JoueurManager.Joueurs[0].getPokemonEquipe()[0])
            {

            }

            compteur = JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant();

            /*
            btn_attaque_une.Visible = false;
            btn_attaque_deux.Visible = false;
            btn_attaque_trois.Visible = false;
            btn_attaque_quatre.Visible = false;
            label_pp_attaque_une.Visible = false;
            label_pp_attaque_deux.Visible = false;
            label_pp_attaque_trois.Visible = false;
            label_pp_attaque_quatre.Visible = false;
            btn_retour_choix_attaque.Visible = false;

            pictureBoxTypeAttaque1.Visible = false;
            pictureBoxTypeAttaque2.Visible = false;
            pictureBoxTypeAttaque3.Visible = false;
            pictureBoxTypeAttaque4.Visible = false; */

            // pokemonJoueurAttaquePremier = true;
            attaqueLancerAdversaire = null;

            if (pokemonJoueurAttaquePremier == true && changement_pokemon == false)
            {
                rafraichirBarreViePokemonAdversaire();
            }
            else if (changement_pokemon == true && changementPokemonPokemonKo == false)
            {
                changement_pokemon = false;
                rafraichirBarreViePokemonJoueur();
            }
            else if (changement_pokemon == true && changementPokemonPokemonKo == true)
            {
                changement_pokemon = false;
                changementPokemonPokemonKo = false;

                boutons_combat.SetActive(true);
                /*
                btn_attaque1.Enabled = true;
                btn_soigner.Enabled = true;
                btn_attraper.Enabled = true;
                btn_changement_pokemon.Enabled = true;

                btn_attaque1.Visible = true;
                btn_soigner.Visible = true;
                btn_changement_pokemon.Visible = true;

                btn_attaque1.Focus();
                
                try
                {
                    Bitmap pngMenuCombat = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\Combat\\menu_combat.png");
                    pictureBoxMenuCombat.Image = pngMenuCombat;
                    pictureBoxMenuCombat.Visible = true;
                }
                catch
                {
                    MessageBox.Show("L'image du menu de combat n'a pas pu être chargée. Veuillez vérifier que celle-ci est bien présente dans le répertoire.", "Vérification de l'image du menu du combat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                */
            }
            else
            {
                rafraichirBarreViePokemonJoueur();
            }

        }

        /// <summary>
        /// Cette méthode permet de rafraîchir les pokémon et leurs statistiques présents dans le menu
        /// </summary>
        public void rafraichirEquipe(int positionJoueur, GameObject canvasGameObject)
        {
            for (int i = 0; i < JoueurManager.Joueurs[positionJoueur].getPokemonEquipe().Count; i++)
            {
                canvasGameObject.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.SetActive(true); // Activation de la partie qu'occuppe les pokémon présents dans l'équipe

                int idPokedexImage = JoueurManager.Joueurs[positionJoueur].getPokemonEquipe()[i].getNoIdPokedex();

                try
                {
                    // Chargement des sprites des pokémon
                    canvasGameObject.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    Sprite imagePokemon = Resources.Load<Sprite>("Images/" + idPokedexImage);
                    canvasGameObject.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = imagePokemon;
                }
                catch
                {

                }

                // Chargement des noms et du nombre du pv des pokémon
                canvasGameObject.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = JoueurManager.Joueurs[positionJoueur].getPokemonEquipe()[i].getNom();
                canvasGameObject.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = JoueurManager.Joueurs[positionJoueur].getPokemonEquipe()[i].getPvRestant() + " / " + JoueurManager.Joueurs[positionJoueur].getPokemonEquipe()[i].getPv() + " PV";           
            }

            JoueurManager.Joueurs[positionJoueur].pokemonSelectionner = JoueurManager.Joueurs[positionJoueur].getPokemonEquipe()[0];
        }
        /*
        public void OpeningMenuPokemon()
        {
            jeu.getUIPopUpMenu();
        }
        */
        /// <summary>
        /// Cette méthode s'active quand le joueur clique sur le bouton d'attaque, elle permet de passer au menu des différentes attaques du pokémon et va voir si il possède bien ses attaques
        /// </summary>
        public void btn_attaque_click(int positionJoueur)
        {
            if (JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getListeAttaque().Count > 0)
            {
                if (JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getAttaque1().getNom() != "default")
                {
                    // Chargement de l'interface de l'attaque et de ses propriétés
                    Text texteBoutonAttaque1 = canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>();
                    texteBoutonAttaque1.text = JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getAttaque1().getNom();
                    Text textePPAttaque1 = canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Text>();
                    textePPAttaque1.text = "PP " + JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getAttaque1().getPPRestant() + "/" + JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getAttaque1().getPP();

                    string typeAttaque1 = JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getAttaque1().getTypeAttaque();
                    if (typeAttaque1 != null)
                    {

                        try
                        {
                            Sprite pngAttaque1 = Resources.Load<Sprite>("Images/Combat/bouton_type_" + typeAttaque1);
                            canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = pngAttaque1;
                        }
                        catch
                        {

                        }

                        try
                        {
                            Sprite pngTypeAttaque1 = Resources.Load<Sprite>("Images/Types/" + typeAttaque1);
                            canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = pngTypeAttaque1;
                        }
                        catch
                        {

                        }
                    }
                }

                if (JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getListeAttaque().Count > 1)
                {
                    if (JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getAttaque2().getNom() != "default")
                    {
                        // Chargement de l'interface de l'attaque et de ses propriétés
                        Text texteBoutonAttaque2 = canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>();
                        texteBoutonAttaque2.text = JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getAttaque2().getNom();
                        Text textePPAttaque2 = canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<Text>();
                        textePPAttaque2.text = "PP " + JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getAttaque2().getPPRestant() + "/" + JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getAttaque2().getPP();

                        string typeAttaque2 = JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getAttaque2().getTypeAttaque();
                        if (typeAttaque2 != null)
                        {
                            try
                            {
                                Sprite pngAttaque2 = Resources.Load<Sprite>("Images/Combat/bouton_type_" + typeAttaque2);
                                canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = pngAttaque2;
                            }
                            catch
                            {

                            }

                            try
                            {
                                Sprite pngTypeAttaque2 = Resources.Load<Sprite>("Images/Types/" + typeAttaque2);
                                canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = pngTypeAttaque2;
                            }
                            catch
                            {

                            }
                        }
                    }

                    if (JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getListeAttaque().Count > 2)
                    {
                        if (JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getAttaque3().getNom() != "default")
                        {
                            // Chargement de l'interface de l'attaque et de ses propriétés
                            Text texteBoutonAttaque3 = canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<Text>();
                            texteBoutonAttaque3.text = JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getAttaque3().getNom();
                            Text textePPAttaque3 = canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).GetComponent<Text>();
                            textePPAttaque3.text = "PP " + JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getAttaque3().getPPRestant() + "/" + JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getAttaque3().getPP();

                            string typeAttaque3 = JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getAttaque3().getTypeAttaque();
                            if (typeAttaque3 != null)
                            {
                                try
                                {
                                    Sprite pngAttaque3 = Resources.Load<Sprite>("Images/Combat/bouton_type_" + typeAttaque3);
                                    canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = pngAttaque3;
                                }
                                catch
                                {

                                }

                                try
                                {
                                    Sprite pngTypeAttaque3 = Resources.Load<Sprite>("Images/Types/" + typeAttaque3);
                                    canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = pngTypeAttaque3;
                                }
                                catch
                                {

                                }
                            }
                        }

                        if (JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getListeAttaque().Count > 3)
                        {
                            if (JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getAttaque4().getNom() != "default")
                            {
                                // Chargement de l'interface de l'attaque et de ses propriétés
                                Text texteBoutonAttaque4 = canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).GetComponent<Text>();
                                texteBoutonAttaque4.text = JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getAttaque4().getNom();
                                Text textePPAttaque4 = canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(3).gameObject.transform.GetChild(1).GetComponent<Text>();
                                textePPAttaque4.text = "PP " + JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getAttaque4().getPPRestant() + "/" + JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getAttaque4().getPP();

                                string typeAttaque4 = JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getAttaque4().getTypeAttaque();
                                if (typeAttaque4 != null)
                                {
                                    try
                                    {
                                        Sprite pngAttaque4 = Resources.Load<Sprite>("Images/Combat/bouton_type_" + typeAttaque4);
                                        canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = pngAttaque4;
                                    }
                                    catch
                                    {

                                    }

                                    try
                                    {
                                        Sprite pngTypeAttaque4 = Resources.Load<Sprite>("Images/Types/" + typeAttaque4);
                                        canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(3).gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = pngTypeAttaque4;
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                        }
                    }
                }

                canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(5).gameObject.SetActive(false); // Boutons combat
                canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.SetActive(true);  // Boutons attaque
                canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<Button>().Select();
            }
        }

        /// <summary>
        /// Cette méthode s'active quand le joueur décide d'annuler une attaque pour revenir dans le menu de combat
        public void btn_retour_menu_combat_click(int positionJoueur)
        {
            canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.SetActive(false); // Desactivation bouton attaque
            canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(5).gameObject.SetActive(true); // Activation bouton combat
            canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(5).transform.GetChild(0).gameObject.GetComponent<Button>().Select();
        }

        /// <summary>
        /// Cette méthode s'active quand le joueur décide de ne pas utiliser d'objet et de revenir dans le menu de combat
        public void btn_retour_menu_combat_apres_objets_click()
        {
            // Désactivation de l'interface des objets
            scroll_objets.SetActive(false);

            // Ainsi que les boutons des objets présents dedans
            foreach (Transform contentChild in scroll_objets.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform)
            {
                GameObject.Destroy(contentChild.gameObject);
            }

            boutons_combat.SetActive(true);
            boutons_combat.transform.GetChild(0).gameObject.GetComponent<Button>().Select();
        }

        public void btn_retour_apres_menu_pokemon_click(int positionJoueur)
        { 
            if (JoueurManager.Joueurs[positionJoueur].enCombat == true)
            {
                menu.SetActive(false);
                UICombat.SetActive(true);
                boutons_combat.transform.GetChild(0).gameObject.GetComponent<Button>().Select();
            }            
            else if(JoueurManager.Joueurs[positionJoueur].enCombat == false && jeuEnPause == true && menu.transform.GetChild(0).gameObject.activeSelf == true && menuPokemonStatistiques.activeSelf == false)
            {
                Debug.Log(positionJoueur);
                menu.transform.GetChild(1).GetComponent<DeplacementPokemonMenuScript>().EnTrainDeDeplacerPokemon = false;
                menu.transform.GetChild(1).GetComponent<DeplacementPokemonMenuScript>().boutonConfirmerChangementPositionGameObject.SetActive(false);
                menu.SetActive(false);
                menuStart.transform.GetChild(2).gameObject.GetComponent<Button>().Select();
                JoueurManager.Joueurs[positionJoueur].menuPauseActuel = "MenuStart";
            }
            else if(JoueurManager.Joueurs[positionJoueur].enCombat == false && jeuEnPause == true && menu.transform.GetChild(0).gameObject.activeSelf == false && menuPokemonStatistiques.activeSelf == true)
            {
                btn_retour_menu_pokemon_apres_menu_statistiques_click();
            }
        }

        // On retourne au menu start
        public void btn_retour_menu_start_apres_menu_selection_personnages()
        {
            canvasGameObject[0].transform.GetChild(12).gameObject.SetActive(false); // On enlève le menu de selection
            menuStart.transform.GetChild(7).gameObject.GetComponent<Button>().Select();
            JoueurManager.Joueurs[0].menuPauseActuel = "MenuStart";
        }


        // On enlève le menu de selection des personnages
        public void btn_retour_apres_menu_selection_personnages()
        {
            canvasGameObject[0].transform.GetChild(12).gameObject.SetActive(false); // On enlève le menu de selection
        }

        /// <summary>
        /// Cette méthode va afficher les différents objets présents dans l'inventaire du personnage
        public void btn_objets_click()
        {
            boutons_combat.SetActive(false);
            // Activation de l'interface des objets
            scroll_objets.SetActive(true);

            int quantiteObjetSuperieurZero = 0;
            
            if (listeObjetsContent.transform.childCount > 0)
            {
                foreach (Transform child in listeObjetsContent.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }

            // Remplissage de l'interface des objets de soin
            for (int i = 0; i < JoueurManager.Joueurs[0].getObjetsSac().Count; i++)
            {
                if (JoueurManager.Joueurs[0].getObjetsSac()[i].getQuantiteObjet() > 0 && JoueurManager.Joueurs[0].getObjetsSac()[i].getTypeObjet() == "Soin")
                {
                    GameObject boutonObjetGameObject = this.gameObject.GetComponent<CreerComposantScript>().CreateButton(scroll_objets.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform, JoueurManager.Joueurs[0].getObjetsSac()[i].getNom(), 320, 160, JoueurManager.Joueurs[0].getObjetsSac()[i].getNom(), 38);
                    Button boutonObjet = boutonObjetGameObject.GetComponent<Button>();
                    boutonObjet.onClick.AddListener(() => { btn_all_objets_click(boutonObjet); });

                    // textNomObjets.Add(CreateText(scroll_objets.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform, Joueur.getObjetsSac()[i].getNom(), 25, -20 * i, Joueur.getObjetsSac()[i].getNom(), 12, Color.black));
                    // listeObjets.Add(boutonObjetGameObject);
                    quantiteObjetSuperieurZero++;
                }
            }

            scroll_objets.transform.GetChild(3).gameObject.GetComponent<Button>().Select();
        }

        /// <summary>
        /// Cette méthode va afficher les différentes pokéballs présents dans l'inventaire du personnage
        public void btn_attraper_click()
        {
            boutons_combat.SetActive(false);
            // Activation de l'interface des objets
            scroll_objets.SetActive(true);

            int quantiteObjetSuperieurZero = 0;

            if (listeObjetsContent.transform.childCount > 0)
            {
                foreach (Transform child in listeObjetsContent.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }

            // Remplissage de l'interface des objets de capture
            for (int i = 0; i < JoueurManager.Joueurs[0].getObjetsSac().Count; i++)
            {
                if (JoueurManager.Joueurs[0].getObjetsSac()[i].getQuantiteObjet() > 0 && JoueurManager.Joueurs[0].getObjetsSac()[i].getTypeObjet() == "Capture")
                {
                    GameObject boutonObjetGameObject = this.gameObject.GetComponent<CreerComposantScript>().CreateButton(scroll_objets.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform, JoueurManager.Joueurs[0].getObjetsSac()[i].getNom(), 320, 160, JoueurManager.Joueurs[0].getObjetsSac()[i].getNom(), 38);
                    Button boutonObjet = boutonObjetGameObject.GetComponent<Button>();
                    boutonObjet.onClick.AddListener(() => { btn_all_pokeball_click(boutonObjet); });

                    // textNomObjets.Add(CreateText(scroll_objets.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform, Joueur.getObjetsSac()[i].getNom(), 25, -20 * i, Joueur.getObjetsSac()[i].getNom(), 12, Color.black));
                   // listeObjets.Add(boutonObjetGameObject);
                    quantiteObjetSuperieurZero++;
                }
            }
        }

        /// <summary>
        /// Cette méthode va gérer la capture d'un pokémon suite à l'utilisation d'une pokéball
        public void btn_all_pokeball_click(Button boutonCliquer)
        {
            // Si le pokémon du joueur est actuellement endormi, on lui fait passer un tour de sommeil puisque l'utilisation d'un objet de soin compte comme un tour
            if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Sommeil")
            {
                nombreTourSommeil++;
            }
             
            /*
            try
            {
                Bitmap pngPokeball = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + "Pokeball\\" + cb_choix_pokeball.SelectedItem + "_1" + ".png");
                pictureBoxPokeball.Image = pngPokeball;
            }
            catch
            {
                MessageBox.Show("L'image de la ball n'a pas pu être chargée. Veuillez vérifier que celle-ci est bien présente dans le répertoire.", "Vérification de l'image de la ball", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */

            // Chargement et calcul du nombre de mouvement que va faire la ball
            ClassLibrary.Objet ball = new ClassLibrary.Objet();
            nombreMouvementsBall = pokemon.nombreMouvementsBall(pokemon.probabiliteMouvementsBall(pokemon.getTauxCaptureModifier(ball.setChercherObjet(boutonCliquer.name, jeu))));

            Vector3 destinationPositionPokeball = new Vector3(destinationPokeball.position.x, destinationPokeball.position.y, destinationPokeball.position.z - 0.5f);
            GameObject pokeballPrefab = (GameObject)(Resources.Load("Models/Pokeballs/" + boutonCliquer.name));

            GameObject pokeball = Instantiate(pokeballPrefab, destinationPositionPokeball, destinationPokeball.rotation);
            Animator animatorPokeball = pokeball.GetComponent<Animator>();
            animatorPokeball.applyRootMotion = true;
            animatorPokeball.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("PokeballLancerCapture");

            animatorPokeball.SetInteger("NombreMouvementsBall", nombreMouvementsBall);

            animator.SetTrigger("trThrowBall");
            animator.Play("Red throw pokeball");
            animatorPokeball.Play("Ball throw");

            dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].getNom() + " utilise " + boutonCliquer.name);

            StartCoroutine(DialogueCombat());

            // timerAnimationCapture();
            StartCoroutine("tourCapturePokemon");
        }

        /// <summary>
        /// Cette méthode va gérer l'utilisation des objets
        public void btn_all_objets_click(Button boutonCliquer)
       {            
            if (listeObjetsContent != null)
            {
                for (int i = 0; i < JoueurManager.Joueurs[0].getObjetsSac().Count; i++)
                {
                    if (boutonCliquer.name == JoueurManager.Joueurs[0].getObjetsSac()[i].getNom())
                    {
                      //  label_nb_potion.Text = Joueur.getObjetsSac()[i].getQuantiteObjet().ToString();

                      //  label_nb_potion.Visible = true;
                      //  label_potion.Visible = true;

                        if (JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() != JoueurManager.Joueurs[0].pokemonSelectionner.getPv())
                        {
                            int pvRestantAvantSoin = JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant();
                            gagnePvPokemonJoueur = true;
                            compteur = JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant();

                            JoueurManager.Joueurs[0].getObjetsSac()[i].Soin(JoueurManager.Joueurs[0].pokemonSelectionner, JoueurManager.Joueurs[0].getObjetsSac()[i]); // Soin du pokémon

                            if (JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() + JoueurManager.Joueurs[0].getObjetsSac()[i].getValeurObjet() < JoueurManager.Joueurs[0].pokemonSelectionner.getPv())
                            {
                                dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " regagne " + JoueurManager.Joueurs[0].getObjetsSac()[i].getValeurObjet() + " PV");
                                StartCoroutine(DialogueCombat());
                            }
                            else
                            {
                                int pvObtenu = JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() - pvRestantAvantSoin;
                                dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " regagne " + pvObtenu + " PV");
                                StartCoroutine(DialogueCombat());
                            }

                            dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " a " + JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() + " PV");
                            dialogueCombat.getDialogue().AddSentence(pokemon.getNom() + " sauvage a " + pokemon.getPvRestant() + " PV");
                            StartCoroutine(DialogueCombat());

                            StartCoroutine("rafraichirApresSoinPokemonJoueur");

                            dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " a " + JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() + " PV");
                            StartCoroutine(DialogueCombat());

                            scroll_objets.SetActive(false);

                            foreach (Transform contentChild in scroll_objets.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform)
                            {
                                GameObject.Destroy(contentChild.gameObject);
                            }
                        }
                        else
                        {
                       //     MessageBox.Show("Cela n'aura aucun effet", "Le pokemon a tous ses PV", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }

            }


          //  else
           // {
            //    MessageBox.Show("Choisissez un objet...", "Vérification de l'objet", MessageBoxButtons.OK, MessageBoxIcon.Error);
          //  }
        }

        /*
        if (panel_choix_attaque_selection.Visible == true)
        {
            panel_choix_attaque_selection.Visible = false;
        }

        if (cb_choix_objets.SelectedItem != null)
        {
            cb_choix_objets.Items.Remove(cb_choix_objets.SelectedItem);
        }
        cb_choix_objets.Items.Clear();

        for (int i = 0; i < Joueur.getObjetsSac().Count; i++)
        {
            if (Joueur.getObjetsSac()[i].getQuantiteObjet() > 0 && Joueur.getObjetsSac()[i].getTypeObjet() == "Soin")
            {
                cb_choix_objets.Items.Add(Joueur.getObjetsSac()[i].getNom());
                quantiteObjetSuperieurZero++;
            }
        }

        if (quantiteObjetSuperieurZero > 0)
        {
            btn_attaque1.Enabled = false;
            btn_attraper.Enabled = false;
            btn_soigner.Enabled = false;
            btn_changement_pokemon.Enabled = false;

            cb_choix_objets.Visible = true;
            btn_choix_objet.Visible = true;
            panel_choix_objets_selection.Visible = true;
        }

        else
        {
            MessageBox.Show("Il n'y aucun objet", "Vérification objet", MessageBoxButtons.OK, MessageBoxIcon.Error);
        } */

        public void btn_fuite_click(int positionJoueur)
        {
            // Désactivation de l'interface de combat
            UIJoueur.SetActive(false);
            UIAdversaire.SetActive(false);
            boutons_combat.SetActive(false);
            BoiteDialogue.SetActive(false);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            GameObjectMusique.GetComponent<AudioSource>().Stop();
            GameObjectMusique.GetComponent<AudioSource>().clip = null;

            Destroy(GameObject.FindGameObjectWithTag("PokemonJoueur"));

            if (EnCombatContre == "PokemonDresseur")
            {
                Destroy(pokemonAdverseGameObject);
            }
            else if (EnCombatContre == "PokemonSauvage")
            {
                foreach (Transform t in pokemonAdverseGameObject.transform)
                {
                    if (t.gameObject.name == "NomPokemon3D")
                    {
                        Destroy(t.gameObject);
                        break;
                    }
                }
            }

            TimelineCameraCombat.Stop();

            canvasGameObject[positionJoueur].GetComponent<Canvas>().worldCamera = controllerJoueurs[positionJoueur].GetComponent<Camera>();
            controllerJoueurs[0].SetActive(true);
            cameraCombatJoueurs[positionJoueur].SetActive(false);

            JoueurManager.Joueurs[positionJoueur].enCombat = false;
        }

        public void btn_changement_pokemon_click()
        {
            rafraichirEquipe(0, canvasGameObject[0]);
            UICombat.SetActive(false);
            menu.SetActive(true);
            menu.transform.GetChild(3).gameObject.GetComponent<Button>().Select();
        }

        public void btn_ouvrir_menu_options_pokemon_click(int positionPokemonCliquer)
        {
            GameObject menuOptionsPokemonGameobject = menu.transform.GetChild(2).gameObject;
            menuOptionsPokemonGameobject.transform.position = new Vector3(menuOptionsPokemonGameobject.transform.position.x, menu.transform.GetChild(1).gameObject.transform.GetChild(positionPokemonCliquer).gameObject.transform.position.y, menuOptionsPokemonGameobject.transform.position.z);

            if (JoueurManager.Joueurs[0].enCombat == true)
            {
                menuOptionsPokemonGameobject.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                menuOptionsPokemonGameobject.transform.GetChild(1).gameObject.SetActive(true);
            }

            menuOptionsPokemonGameobject.SetActive(true);
            menuOptionsPokemonGameobject.transform.GetChild(3).gameObject.GetComponent<Button>().Select();
            positionPokemonMenuPokemon = positionPokemonCliquer;
        }

        /// <summary>
        /// Cette méthode va permettre d'aller au menu principal depuis le menu start
        public void btn_ouvrir_menu_principal(int positionPokemonCliquer)
        {
            Time.timeScale = 1f; // On remet le temps normal
            DonneesChargement.nomSceneSuivante = "SceneMenu"; // On dit de charger le menu principal
            SceneManager.LoadScene("SceneChargement");
        }

        /// <summary>
        /// Cette méthode va permettre de quitter le jeu
        public void btn_quitter_jeu(int positionPokemonCliquer)
        {
            Time.timeScale = 1f; // On remet le temps normal
            Application.Quit();
        }

        public void changementPokemonAdversaire()
        {
            if (EnCombatContre == "PokemonDresseur")
            {
                bool pokemonEnVieTrouver = false; 

                for (int i = 0; i < dresseurAdverse.getPokemonEquipe().Count; i++)
                {
                    if (dresseurAdverse.getPokemonEquipe()[i].getPvRestant() > 0 && pokemonEnVieTrouver == false)
                    {
                        pokemon = dresseurAdverse.getPokemonEquipe()[i];
                        rafraichirBarreViePokemonAdversaire2(0);
                        compteurAdversaire = dresseurAdverse.getPokemonEquipe()[i].getPvRestant();
                        pokemonEnVieTrouver = true;
                    }
                }

                if (pokemonEnVieTrouver == false)
                {
                    dialogueCombat.getDialogue().AddSentence("Vous avez gagné le combat contre " + dresseurAdverse.getNom());
                    StartCoroutine(DialogueCombat());

                    btn_fuite_click(0);
                }
            }
        }


        /// <summary>
        /// Cette méthode va permettre d'ouvrir ou fermer la carte
        public void btn_carte(int positionJoueur)
        { 
            if (mapGameObject.activeSelf == false) // Si la carte n'est pas active, on la met
            {
                if (cameraMapGameObject.activeSelf == false)
                {
                    cameraMapGameObject.SetActive(true);
                }

                /*
                if (jeuEnPause == false) // On met le jeu en pause
                {
                    menuStart.GetComponent<MenuStartScript>().Pause(positionJoueur);
                    jeuEnPause = true;
                }
                */
              //  mapGameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.InverseTransformPoint(JoueurManager.JoueursGameObject[0].transform.GetChild(4).gameObject.transform.position);
                mapGameObject.SetActive(true);
            }

            else // Sinon on l'enlève
            {
                if (cameraMapGameObject.activeSelf == true)
                {
                    cameraMapGameObject.SetActive(false);
                }
                mapGameObject.SetActive(false);

                /*
                    if (JoueurManager.Joueurs[positionJoueur].menuPauseActuel == "MenuStart")
                    {
                        menuStart.GetComponent<MenuStartScript>().Resume();
                        JoueurManager.Joueurs[positionJoueur].menuPauseActuel = "";
                        jeuEnPause = false;
                    }
                    */
                }
        }

        /// <summary>
        /// Cette méthode gère l'appui sur le bouton de la première attaque du pokémon
        public void btn_attaque1_click()
        {
            // attaqueCombat(pokemonJoueurSelectionner.getAttaque1());
            attaqueCombatReecriture(JoueurManager.Joueurs[0].pokemonSelectionner.getAttaque1());
        }

        /// <summary>
        /// Cette méthode gère l'appui sur le bouton de la deuxième attaque du pokémon
        public void btn_attaque2_click()
        {
            // attaqueCombat(pokemonJoueurSelectionner.getAttaque2());
            attaqueCombatReecriture(JoueurManager.Joueurs[0].pokemonSelectionner.getAttaque2());
        }

        /// <summary>
        /// Cette méthode gère l'appui sur le bouton de la troisième attaque du pokémon
        public void btn_attaque3_click()
        {
            attaqueCombatReecriture(JoueurManager.Joueurs[0].pokemonSelectionner.getAttaque3());
        }

        /// <summary>
        /// Cette méthode gère l'appui sur le bouton de la quatrième attaque du pokémon
        public void btn_attaque4_click()
        {
            attaqueCombatReecriture(JoueurManager.Joueurs[0].pokemonSelectionner.getAttaque4());
        }

        public void btn_ouvrir_menu_statistiques_click()
        {
            menu.transform.GetChild(1).gameObject.SetActive(false);
            menuPokemonStatistiques.SetActive(true); // Activation de la page des statistiques

            int idPokedexImage = JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getNoIdPokedex();

            try
            {
                Sprite imagePokemon = Resources.Load<Sprite>("Images/" + idPokedexImage);
                menuPokemonStatistiques.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = imagePokemon;
            }
            catch
            {

            }

            menuPokemonStatistiques.transform.GetChild(1).gameObject.GetComponent<Text>().text = JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getNom();
            menuPokemonStatistiques.transform.GetChild(2).gameObject.GetComponent<Text>().text = "N°" + JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getNiveau().ToString();

            if (JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getSexe() == "Feminin")
            {
                menuPokemonStatistiques.transform.GetChild(3).gameObject.GetComponent<Text>().text = "♀";
                menuPokemonStatistiques.transform.GetChild(3).gameObject.GetComponent<Text>().color = new Color32(232, 0, 255, 1);
            }
            else if (JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getSexe() == "Masculin")
            {
                menuPokemonStatistiques.transform.GetChild(3).gameObject.GetComponent<Text>().text = "♂";
                menuPokemonStatistiques.transform.GetChild(3).gameObject.GetComponent<Text>().color = Color.blue;
            }

            menuPokemonStatistiques.transform.GetChild(4).gameObject.GetComponent<Text>().text = JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getPvRestant() + " / " + JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getPv() + " PV";
            menuPokemonStatistiques.transform.GetChild(5).gameObject.GetComponent<Text>().text = "Attaque : " + JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getStatistiquesAttaque();
            menuPokemonStatistiques.transform.GetChild(6).gameObject.GetComponent<Text>().text = "Defense : " + JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getStatistiquesDefense();
            menuPokemonStatistiques.transform.GetChild(7).gameObject.GetComponent<Text>().text = "Vitesse : " + JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getStatistiquesVitesse();
            menuPokemonStatistiques.transform.GetChild(8).gameObject.GetComponent<Text>().text = "Attaque Speciale : " + JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getStatistiquesAttaqueSpeciale();
            menuPokemonStatistiques.transform.GetChild(9).gameObject.GetComponent<Text>().text = "Defense Speciale : " + JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getStatistiquesDefenseSpeciale();
            menuPokemonStatistiques.transform.GetChild(10).gameObject.GetComponent<Text>().text = "Experience : " + JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getExperience();

            menuPokemonStatistiques.transform.GetChild(11).gameObject.GetComponent<Text>().text = "EV PV : " + JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getEvPv();
            menuPokemonStatistiques.transform.GetChild(12).gameObject.GetComponent<Text>().text = "EV Attaque : " + JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getEvAttaque();
            menuPokemonStatistiques.transform.GetChild(13).gameObject.GetComponent<Text>().text = "EV Defense : " + JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getEvDefense();
            menuPokemonStatistiques.transform.GetChild(14).gameObject.GetComponent<Text>().text = "EV Vitesse : " + JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getEvVitesse();
            menuPokemonStatistiques.transform.GetChild(15).gameObject.GetComponent<Text>().text = "EV Attaque Speciale : " + JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getEvAttaqueSpeciale();
            menuPokemonStatistiques.transform.GetChild(16).gameObject.GetComponent<Text>().text = "EV Defense Speciale : " + JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getEvDefenseSpeciale();

            if (JoueurManager.Joueurs[0].pokemonSelectionner.getListeAttaque().Count > 0)
            {
                if (JoueurManager.Joueurs[0].pokemonSelectionner.getAttaque1().getNom() != "default")
                {
                    menuPokemonStatistiques.transform.GetChild(17).gameObject.GetComponent<Text>().text = JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getAttaque1().getNom();
                }

                if (JoueurManager.Joueurs[0].pokemonSelectionner.getListeAttaque().Count > 1)
                {
                    if (JoueurManager.Joueurs[0].pokemonSelectionner.getAttaque2().getNom() != "default")
                    {
                        menuPokemonStatistiques.transform.GetChild(18).gameObject.GetComponent<Text>().text = JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getAttaque2().getNom();
                    }

                    if (JoueurManager.Joueurs[0].pokemonSelectionner.getListeAttaque().Count > 2)
                    {
                        if (JoueurManager.Joueurs[0].pokemonSelectionner.getAttaque3().getNom() != "default")
                        {
                            menuPokemonStatistiques.transform.GetChild(19).gameObject.GetComponent<Text>().text = JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getAttaque3().getNom();
                        }

                        if (JoueurManager.Joueurs[0].pokemonSelectionner.getListeAttaque().Count > 3)
                        {
                            if (JoueurManager.Joueurs[0].pokemonSelectionner.getAttaque4().getNom() != "default")
                            {
                                menuPokemonStatistiques.transform.GetChild(20).gameObject.GetComponent<Text>().text = JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getAttaque4().getNom();
                            }
                        }
                    }
                }
            }

            if (JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getType() != null)
            {
                string typePokemon = JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuPokemon].getType();
                Sprite imagePokemon = Resources.Load<Sprite>("Images/Types/" + typePokemon);
                menuPokemonStatistiques.transform.GetChild(21).gameObject.GetComponent<Image>().sprite = imagePokemon;
                menuPokemonStatistiques.transform.GetChild(21).gameObject.SetActive(true);
            }
        }

        public void btn_ouvrir_menu_pokemon_click(int positionJoueur)
        {
            Debug.Log(positionJoueur);

            if (JoueurManager.Joueurs[positionJoueur].getPokemonEquipe().Count > 0)
            {
                GameObject canvas = canvasGameObject[0];

                if (positionJoueur == 1)
                {
                    canvas = GameObject.Find("CanvasJoueur2");
                }

                rafraichirEquipe(positionJoueur, canvas);
                canvas.transform.GetChild(4).gameObject.SetActive(true);
                canvas.transform.GetChild(4).transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Button>().Select();
                JoueurManager.Joueurs[positionJoueur].menuPauseActuel = "MenuPokemon";
            }
        }

        // Si le menu n'est pas ouvert, on l'ouvre sinon on le ferme
        public void btn_ouvrir_menu_personnages_selection()
        {
            if (canvasGameObject[0].transform.GetChild(12).gameObject.activeSelf == false)
            {
                canvasGameObject[0].transform.GetChild(12).gameObject.SetActive(true);
            }
            else
            {
                canvasGameObject[0].transform.GetChild(12).gameObject.SetActive(false);
            }
        }

        // Si le menu n'est pas ouvert, on l'ouvre sinon on le ferme
        public void btn_ouvrir_menu_commandes()
        {
            if (canvasGameObject[0].transform.GetChild(10).gameObject.activeSelf == false)
            {
                canvasGameObject[0].transform.GetChild(10).gameObject.SetActive(true);
            }
            else
            {
                canvasGameObject[0].transform.GetChild(10).gameObject.SetActive(false);
            }
        }

        // Si le menu n'est pas ouvert, on l'ouvre sinon on le ferme
        public void btn_ouvrir_menu_montures()
        {
            if (canvasGameObject[0].transform.GetChild(13).gameObject.activeSelf == false)
            {
                canvasGameObject[0].transform.GetChild(13).gameObject.SetActive(true);
                canvasGameObject[0].transform.GetChild(0).gameObject.GetComponent<MenuStartScript>().btn_retour_apres_menu_start_click(); // On enlève le menu pause
            }
            else
            {
                canvasGameObject[0].transform.GetChild(13).gameObject.SetActive(false);
            }
        }

        public void btn_retour_menu_pokemon_apres_menu_statistiques_click()
        {
            menu.transform.GetChild(2).gameObject.SetActive(false);
            menu.transform.GetChild(0).gameObject.SetActive(true);
        }

        public void btn_retour_menu_pokemon_apres_menu_pokemon_options_click()
        {
            menu.transform.GetChild(2).gameObject.SetActive(false);
            menu.transform.GetChild(1).gameObject.transform.GetChild(positionPokemonMenuPokemon).gameObject.GetComponent<Button>().Select();
        }

        public void btn_retour_menu_pokemon_apres_menu_pc_click()
        {
            menuPC.SetActive(false);
            OuverturePC = false;
        }

        public void rafraichirBarreViePokemonJoueur2(int positionJoueur)
        {
            Text LabelNomPokemonJoueur = canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
            LabelPvPokemonJoueurUI = canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.GetComponent<Text>();
            Text LabelSexePokemonJoueur = canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.GetComponent<Text>();
            Text LabelNiveauPokemonJoueurUI = canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.GetComponent<Text>();
            barViePokemonJoueur = canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).transform.GetChild(3).gameObject;
            barViePokemonJoueurImage = barViePokemonJoueur.GetComponent<Image>();

            LabelNomPokemonJoueur.text = JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getNom();
            LabelPvPokemonJoueurUI.text = JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getPvRestant().ToString() + " / " + JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getPv().ToString() + " PV";
            LabelNiveauPokemonJoueurUI.text = "N. " + JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getNiveau();

            if (JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getSexe() == "Feminin")
            {
                LabelSexePokemonJoueur.text = "♀";
                LabelSexePokemonJoueur.color = new Color32(255, 130, 192, 255);
            }
            else if (JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getSexe() == "Masculin")
            {
                LabelSexePokemonJoueur.text = "♂";
                LabelSexePokemonJoueur.color = new Color32(0, 128, 255, 255);
            }

            barViePokemonJoueurImage.fillAmount = (float)JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getPvRestant() / (float)JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getPv();
        }

        public void rafraichirBarreViePokemonAdversaire2(int positionJoueur)
        {
            Text LabelNomPokemonAdversaire = canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
            Text LabelNiveauPokemonAdversaire = canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(4).gameObject.GetComponent<Text>();
            Text LabelSexePokemonAdversaire = canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(5).gameObject.GetComponent<Text>();
            LabelPvPokemonAdversaireUI = canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.GetComponent<Text>();

            LabelNomPokemonAdversaire.text = pokemon.getNom();
            LabelPvPokemonAdversaireUI.text = pokemon.getPvRestant().ToString() + " / " + pokemon.getPv().ToString() + " PV";
            LabelNiveauPokemonAdversaire.text = "N. " + pokemon.getNiveau();

            if (pokemon.getSexe() == "Feminin")
            {
                LabelSexePokemonAdversaire.text = "♀";
                LabelSexePokemonAdversaire.color = new Color32(255, 130, 192, 255);
            }
            else if (pokemon.getSexe() == "Masculin")
            {
                LabelSexePokemonAdversaire.text = "♂";
                LabelSexePokemonAdversaire.color = new Color32(0, 128, 255, 255);
            }

            GameObject barViePokemonAdversaire = canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).transform.GetChild(3).gameObject;
            Image barImagePokemonAdversaire = barViePokemonAdversaire.GetComponent<Image>();

            barViePokemonAdversaireImage.color = new Color32(81, 209, 39, 255);
            barImagePokemonAdversaire.fillAmount = (float)pokemon.getPvRestant() / (float)pokemon.getPv();
        }

        IEnumerator DialogueCombat()
        {
           // sceneBuilder.AddComponent<DialogueTrigger>();
           // DialogueTrigger dialogueCombat = sceneBuilder.GetComponent<DialogueTrigger>();

            dialogueCombat.TriggerDialogue();

           yield return new WaitUntil(() => BoiteDialogueAnimator.GetBool("IsOpen") == false);
        }

        IEnumerator DialogueCombatStart(int positionJoueur)
        {
            // sceneBuilder.AddComponent<DialogueTrigger>();
            // DialogueTrigger dialogueCombat = sceneBuilder.GetComponent<DialogueTrigger>();

            dialogueCombat.TriggerDialogue();

            yield return new WaitUntil(() => BoiteDialogueAnimator.GetBool("IsOpen") == false);

            try
            {
                canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(5).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().preserveAspect = true; // Changement icone pokemon sur l'interface du bouton attaque
                canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(5).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Icones/" + JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getNoIdPokedex());
            }
            catch
            {

            }

            canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(5).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
            canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(5).gameObject.SetActive(true); // Activation boutons combat
            canvasGameObject[positionJoueur].transform.GetChild(1).gameObject.transform.GetChild(5).gameObject.transform.GetChild(0).gameObject.GetComponent<Button>().Select(); // Selection du bouton attaque
        }

        void StartDialogueCombat(int positionJoueur)
        {
            dialogueCombat.getDialogue().AddSentence("Un " + pokemon.getNom() + " sauvage veut se battre");
            dialogueCombat.getDialogue().AddSentence("Il a " + pokemon.getPv() + " PV");

            StartCoroutine(DialogueCombatStart(positionJoueur));

            compteur = JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getPvRestant();
            compteurAdversaire = pokemon.getPvRestant();
        }

        private void Awake()
        {
            canvasGameObject[0] = canvasGameObjectJoueur;
        }
 /*
        private IEnumerator coroutineThreadInitialisation()
        {
            // Initialisation des attaques, des pokémon, des starters, des objets
           
            Thread threadInitialisationAttaque = new Thread(jeu.initialisationAttaques);
            threadInitialisationAttaque.Start();
            yield return new WaitForSeconds(1f);

            Thread threadInitialisationEspecePokemon = new Thread(jeu.initialisationEspecePokemon);
            threadInitialisationEspecePokemon.Start();
            yield return new WaitForSeconds(1f);

            Thread threadInitialisationPokemon = new Thread(jeu.initialisationPokemon);
            threadInitialisationPokemon.Start();
            Thread threadInitialisationPokemonStarter = new Thread(jeu.initialisationPokemonStarter);
            threadInitialisationPokemonStarter.Start();
            Thread threadInitialisationSort = new Thread(jeu.initialisationSorts);
            threadInitialisationSort.Start();
            Thread threadInitialisationObjet = new Thread(jeu.initialisationObjets);
            threadInitialisationObjet.Start();
            
            yield return null;
            
        } */

        // Start is called before the first frame update
        void Start()
        {
            jeu.initialisationAttaques();
            jeu.initialisationEspecePokemon();
            jeu.initialisationPokemon();
            jeu.initialisationPokemonStarter();
            jeu.initialisationSorts();
            jeu.initialisationObjets();

            jeu.initialisationPersonnage();

            JoueurManager.Joueurs.Add(PlayButton.Joueur);
            JoueurManager.Joueurs[0].pv = 50; // Initialisation des pv du joueur
            JoueurManager.Joueurs[0].pvRestants = JoueurManager.Joueurs[0].pv;

            JoueurManager.Joueurs[0].addSort(jeu.getListeSort()[0]); // On ajoute les sorts

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            jeuInitialiser = true;

            modeCombat = "Tour par tour";

            // Assignage des GameObjects
            boutons_attaque = GameObject.Find("/Canvas").transform.GetChild(1).gameObject.transform.GetChild(2).gameObject;
            boutons_combat = GameObject.Find("/Canvas").transform.GetChild(1).gameObject.transform.Find("BoutonsCombat").gameObject;
            scroll_objets = GameObject.Find("/Canvas").transform.GetChild(1).gameObject.transform.GetChild(4).gameObject;
            listeObjetsContent = scroll_objets.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
            menu = GameObject.Find("/Canvas").transform.GetChild(4).gameObject;
            menuPokemonStatistiques = menu.transform.GetChild(3).gameObject;
            menuStart = GameObject.Find("/Canvas").transform.GetChild(0).gameObject;
            mapGameObject = GameObject.Find("/Canvas").transform.GetChild(9).gameObject;
            UICombat = GameObject.Find("/Canvas").transform.GetChild(1).gameObject;
            UIJoueur = GameObject.Find("/Canvas").transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
            UIAdversaire = GameObject.Find("/Canvas").transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;
            barViePokemonJoueur = UIJoueur.transform.GetChild(1).transform.GetChild(3).gameObject;
            barViePokemonJoueurImage = barViePokemonJoueur.GetComponent<Image>();
            barExperiencePokemonJoueur = UIJoueur.transform.GetChild(6).transform.GetChild(1).gameObject;
            barExperiencePokemonJoueurImage = barExperiencePokemonJoueur.GetComponent<Image>();
            barViePokemonAdversaire = UIAdversaire.transform.GetChild(1).transform.GetChild(3).gameObject;
            barViePokemonAdversaireImage = barViePokemonAdversaire.GetComponent<Image>();
            StatistiquesChangementNiveau = UIJoueur.transform.GetChild(7).gameObject;
            TableStarter = GameObject.Find("TableStarter");
            BoitePC = GameObject.Find("BoitePC");
            menuPC = GameObject.Find("Canvas").transform.GetChild(3).gameObject;
            PokeballBulbizarre = TableStarter.transform.GetChild(0).gameObject;
            PokeballSalameche = TableStarter.transform.GetChild(1).gameObject;
            PokeballCarapuce = TableStarter.transform.GetChild(2).gameObject;
            BoiteDialogue = GameObject.Find("/Canvas").transform.GetChild(1).gameObject.transform.GetChild(3).gameObject;
            BoiteDialogueTexte = BoiteDialogue.transform.GetChild(0).gameObject.GetComponent<Text>();
            BoiteDialogueAnimator = BoiteDialogue.GetComponent<Animator>();
            DialogueManagerGameObject = GameObject.Find("DialogueManager");
            JoueurManager.JoueursGameObject[0] = GameObject.Find("Joueurs").gameObject.transform.GetChild(0).gameObject;
            controllerJoueurs[0] = GameObject.Find("vThirdPersonController");
            cameraCombatJoueurs[0] = GameObject.Find("CameraCombatJoueur");
            cameraCombat = GameObject.Find("CameraCombatJoueur").transform.GetChild(0).gameObject;
            cameraCombatUI = GameObject.Find("CameraCombatJoueur").transform.GetChild(1).gameObject;
            cameraMapGameObject = GameObject.Find("CameraMap");
            GameObjectMusique = GameObject.Find("GameObjectMusique");
            sceneBuilder = GameObject.Find("SceneBuilder");
            dialogueCombat = sceneBuilder.GetComponent<DialogueTrigger>();
            TimelineAnimationLancerPokeball = GameObject.Find("Timeline").GetComponent<PlayableDirector>();
            TimelinePokemonAccompagner = GameObject.Find("TimelinePokemonAccompagner").GetComponent<PlayableDirector>();
            TimelineCameraCombat = GameObject.Find("TimelineCamera").GetComponent<PlayableDirector>();
            GameObject VolumeGameObject = GameObject.Find("Global Volume");

            GameObject personnageModel = new GameObject();

            if (DonneesChargement.dimensionPersonnage == "3D") // Si le personnage est en 3D, on charge un gameobject
            {
                personnageModel = Instantiate(Resources.Load<GameObject>(DonneesChargement.nomGameObjectJoueur), JoueurManager.JoueursGameObject[0].transform.GetChild(1).gameObject.transform); // Le personnage selectionné dans le menu apparait
                JoueurManager.JoueursGameObject[0].transform.GetChild(1).gameObject.transform.GetChild(4).name = DonneesChargement.nomGameObjectModel; 
                JoueurManager.JoueursGameObject[0].GetComponent<Animator>().avatar = personnageModel.GetComponent<Animator>().avatar; // Avatar du personnage
                Destroy(personnageModel);
                JoueurManager.JoueursGameObject[0].GetComponent<Animator>().Rebind();
                personnageModel.transform.localScale = new Vector3(0.014f, 0.014f, 0.014f);
            }
            else if (DonneesChargement.dimensionPersonnage == "2D") // Si le personnage est un sprite, on charge un sprite 
            {
                // personnageModel = JoueurManager.JoueursGameObject[0].transform.GetChild(1).gameObject.transform.GetChild(4).gameObject;
                VolumeGameObject.GetComponent<Volume>().profile = Resources.Load<VolumeProfile>("Profiles/HD 2D");
                GameObject spriteGameObject = new GameObject("Sprite");
                spriteGameObject.transform.parent = JoueurManager.JoueursGameObject[0].transform.GetChild(1).gameObject.transform.GetChild(4).gameObject.transform;
                spriteGameObject.transform.localPosition = new Vector3(0, 41, 0);
                // spriteGameObject.transform.localScale = new Vector3(8, 8, 1);
                spriteGameObject.transform.localScale = new Vector3(210, 210, 1);
                SpriteRenderer spriteRenderer = spriteGameObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = Resources.Load<Sprite>(DonneesChargement.nomGameObjectJoueur);
                spriteRenderer.material = Resources.Load<Material>("Materials/HD 2D"); // On charge le bon material
                spriteGameObject.AddComponent<ShadowedSprite>(); // On ajoute les ombres HD 2D

                spriteGameObject.AddComponent<SpriteDeplacement3D>(); // On ajoute le script de changement des sprites

                // On lock la caméra pour qu'elle suive bien le joueur
                vThirdPersonCamera camera = controllerJoueurs[0].GetComponent<vThirdPersonCamera>();
                camera.lockCamera = true;
                camera.defaultDistance = 14;
                camera.height = 3;
            }

            if(DonneesChargement.musiquePersonnage != null) // Si le personnage selectionné à une musique
            {
                AudioSource GameObjectMusiqueAudioSource = GameObjectMusique.GetComponent<AudioSource>();
                GameObjectMusiqueAudioSource.clip = DonneesChargement.musiquePersonnage;
                GameObjectMusiqueAudioSource.Play(); // On lance la musique
            }

            for (int i = personnageModel.transform.childCount; i>0; i--) // On enlève les enfants du gameobject puisqu'on a besoin d'eux pour le personnage
            {
                personnageModel.transform.GetChild(0).gameObject.transform.parent = JoueurManager.JoueursGameObject[0].transform.GetChild(1).gameObject.transform.GetChild(4).gameObject.transform;
            }

            GameObjectJoueur = GameObject.Find("vThirdPersonCamera").transform.GetChild(1).gameObject.transform.GetChild(4).gameObject;

            this.destinationPokeball = GameObjectJoueur.transform.GetChild(0).gameObject.transform;

            cameraMapGameObject.SetActive(false);

            cameraCombatJoueurs[0].SetActive(false);

            // Thread threadInitialisationPersonnage = new Thread(jeu.initialisationPersonnage);
           // threadInitialisationPersonnage.Start();

            /*
            // Construction de l'interface du menu des pokémon
            for (int i = 0; i < 6; i++)
            {
                imagePokemonMenu[i] = new GameObject();
                imagePokemonMenu[i].name = "ImagePokemon";
                imagePokemonMenu[i].gameObject.transform.SetParent(menu.transform.GetChild(0).gameObject.transform.GetChild(i));
                imagePokemonMenu[i].gameObject.transform.localPosition = new Vector3(-220, 0, 0);
                imagePokemonMenu[i].gameObject.transform.localScale = new Vector3(2.3f, 2.3f, 2.3f);
                imagePokemonMenuImage[i] = imagePokemonMenu[i].AddComponent<Image>();

              //  Button imagePokemonMenuButton = imagePokemonMenu[i].AddComponent<Button>();
              //  EventTrigger imagePokemonMenuBoutonTrigger  = imagePokemonMenu[i].AddComponent<EventTrigger>();
              //  imagePokemonMenuButton.onClick.AddListener(() => { btn_all_objets_click(boutonStatistiquesPokemonMenuBouton[i]); });

                imagePokemonMenuImage[i].color = new Color32(255, 255, 255, 0);
                imagePokemonMenuImage[i].transform.Rotate(0, 180, 0);
              //  imagePokemonMenuImage[i].AddListener(() => { btn_all_objets_click(boutonStatistiquesPokemonMenuBouton[i]); });

                int numeroPokemon = i + 1;
                positionPokemonMenu[i] = i;

                textNomPokemonMenu[i] = this.gameObject.GetComponent<CreerComposantScript>().CreateText(menu.transform.GetChild(0).gameObject.transform.GetChild(i), "NomPokemon" + numeroPokemon, 300, 100, 113, 0, "", 43, 0, 0, Color.black);
                pvPokemonMenu[i] = this.gameObject.GetComponent<CreerComposantScript>().CreateText(menu.transform.GetChild(0).gameObject.transform.GetChild(i), "PvPokemon", 200, 100, 320, 0, "", 43, 0, 0, Color.black);
                pvPokemonMenuTexte[i] = pvPokemonMenu[i].GetComponent<Text>();
                //  textNomPokemonMenuBouton[i].onClick.AddListener(() => { btn_ouvrir_menu_statistiques_click(positionPokemonMenu[numeroPokemon - 1]); });

                boutonStatistiquesPokemonMenu[i] = this.gameObject.GetComponent<CreerComposantScript>().CreateButton(menu.transform.GetChild(0).gameObject.transform.GetChild(i), "BoutonStatisiques", 220, 90, "Statistiques", 38);
                boutonStatistiquesPokemonMenu[i].SetActive(false);
                boutonStatistiquesPokemonMenuBouton[i] = boutonStatistiquesPokemonMenu[i].GetComponent<Button>();
                
             
                pokemonMenu[i] = menu.transform.GetChild(0).gameObject.transform.GetChild(i).gameObject;
                pokemonMenu[i].SetActive(false);
            
                // boutonStatistiquesPokemonMenuBouton[i].onClick.AddListener(() => { btn_all_objets_click(boutonStatistiquesPokemonMenuBouton[i]); });
            }
            */
            // LabelPvChangementNiveau = StatistiquesChangementNiveau.transform.GetChild(0).gameObject.GetComponent<Text>();
            // LabelAttaqueChangementNiveau = StatistiquesChangementNiveau.transform.GetChild(1).gameObject.GetComponent<Text>();
            // LabelDefenseChangementNiveau = StatistiquesChangementNiveau.transform.GetChild(2).gameObject.GetComponent<Text>();
            //  LabelVitesseChangementNiveau = StatistiquesChangementNiveau.transform.GetChild(3).gameObject.GetComponent<Text>();
            //  LabelAttaqueSpecialeChangementNiveau = StatistiquesChangementNiveau.transform.GetChild(4).gameObject.GetComponent<Text>();
            //  LabelDefenseSpecialeChangementNiveau = StatistiquesChangementNiveau.transform.GetChild(5).gameObject.GetComponent<Text>();

            ObjetProcheGameObject = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
            LabelObjetProche = ObjetProcheGameObject.GetComponent<Text>();

            MusiquesCombat.Add(Resources.Load<AudioClip>("Musics/Combat/" + "normal-battle-pokemon-colosseum")); // Ajout des musiques de combats
            MusiquesCombat.Add(Resources.Load<AudioClip>("Musics/Combat/" + "Pokemon RedBlueYellow - Battle! Trainer Music (HQ)"));

            // BoiteDialogue.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => DialogueManagerGameObject.GetComponent<DialogueManager>().DisplayNextSentence(DialogueManagerGameObject.GetComponent<DialogueTrigger>().getDialogue()));

            JoueurManager.Joueurs[0].setObjetsSacOffline(jeu);

            if (Gamepad.all.Count > 0)
            {
                JoueurManager.JoueursGameObject[0].GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard and Gamepad 1", new InputDevice[] { Keyboard.current, Mouse.current, Gamepad.all[0] }); // Permet au joueur 1 de controller le clavier, la souris et la première manette
            }

            /*
            try
            {
                Bitmap png = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + idPokedexImage + ".png");
                pictureBoxPokemon[0].Image = png;
                pictureBoxPokemon[0].Image.RotateFlip(RotateFlipType.Rotate180FlipY);
            }
            catch
            {
                MessageBox.Show("L'image du pokémon n'a pas pu être chargée. Veuillez vérifier que celle-ci est bien présente dans le répertoire.", "Vérification de l'image du pokémon", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */

            // int idPokedexImage = Joueur.getPokemonEquipe()[0].getNoIdPokedex();

            // GameObject spawnPokemonGameObject = GameObject.Find("SpawnPokemon");
            // GameObject pokemonSelectionnerGameObject = (GameObject)Instantiate(Resources.Load("Models/Pokemon/" + idPokedexImage), spawnPokemonGameObject.transform.position, spawnPokemonGameObject.transform.rotation);

        }

        // Update is called once per frame
        void Update()
        {
            /*
            // Met le jeu en pause si le jeu n'est pas en cours de combat, si le jeu est déjà en pause, cela permet de reprendre la partie
            if (Input.GetKeyDown("i") && EnCombat == false)
            {
                if (jeuEnPause == false)
                {
                    Pause();
                    jeuEnPause = true;                    
                }
                else
                {
                    if (MenuPauseActuel == "MenuStart")
                    {
                        Resume();
                        MenuPauseActuel = "";
                        jeuEnPause = false;
                    }
                }
            } 
            */

            /*
            float DistanceBoitePCJoueur = (BoitePC.transform.position - GameObjectJoueur.transform.position).sqrMagnitude;

            // Permet d'afficher un message quand le joueur se retrouve proche des starters ou de la boîte et d'intéragir
            if (starterChoisi == false)
            {
                float DistancePokeballBulbizarreJoueur = (PokeballBulbizarre.transform.position - GameObjectJoueur.transform.position).sqrMagnitude;
                float DistancePokeballSalamecheJoueur = (PokeballSalameche.transform.position - GameObjectJoueur.transform.position).sqrMagnitude;
                float DistancePokeballCarapuceJoueur = (PokeballCarapuce.transform.position - GameObjectJoueur.transform.position).sqrMagnitude;

                if ((DistancePokeballBulbizarreJoueur < 3.0f) || (DistancePokeballSalamecheJoueur < 3.0f) || (DistancePokeballCarapuceJoueur < 3.0f) && ObjetProcheGameObject.activeSelf == false)
                {
                    ObjetProcheGameObject.SetActive(true);
                }
                else if ((DistancePokeballBulbizarreJoueur > 3.0f) && (DistancePokeballSalamecheJoueur > 3.0f) && (DistancePokeballCarapuceJoueur > 3.0f) && DistanceBoitePCJoueur > 3.0f && ObjetProcheGameObject.activeSelf == true)
                {
                    ObjetProcheGameObject.SetActive(false);

                    if (DistanceBoitePCJoueur > 3.0f && messageBoitePCActif == true)
                    {
                        messageBoitePCActif = false;
                    }
                }

                if (DistancePokeballBulbizarreJoueur <= DistancePokeballSalamecheJoueur && DistancePokeballBulbizarreJoueur <= DistancePokeballCarapuceJoueur && LabelObjetProche.text != "Choisir Bulbizarre" && starterChoisi == false && messageBoitePCActif == false)
                {
                    LabelObjetProche.text = "Choisir Bulbizarre";
                }
                else if (DistancePokeballSalamecheJoueur < DistancePokeballBulbizarreJoueur && DistancePokeballSalamecheJoueur <= DistancePokeballCarapuceJoueur && LabelObjetProche.text != "Choisir Salamèche" && starterChoisi == false && messageBoitePCActif == false)
                {
                    LabelObjetProche.text = "Choisir Salamèche";
                }
                else if (DistancePokeballCarapuceJoueur < DistancePokeballBulbizarreJoueur && DistancePokeballCarapuceJoueur < DistancePokeballSalamecheJoueur && LabelObjetProche.text != "Choisir Carapuce" && starterChoisi == false && messageBoitePCActif == false)
                {
                    LabelObjetProche.text = "Choisir Carapuce";
                }

                if (DistancePokeballBulbizarreJoueur <= DistancePokeballSalamecheJoueur && DistancePokeballBulbizarreJoueur <= DistancePokeballCarapuceJoueur && ObjetProcheGameObject.activeSelf == true && Input.GetKeyDown(KeyCode.Return) && starterChoisi == false && messageBoitePCActif == false)
                {
                    pokemonStarter = pokemon.setChercherPokemonStarter("Bulbizarre", jeu);

                    Joueur.ajouterPokemonEquipe(pokemonStarter);
                    pokemonJoueurSelectionner = Joueur.getPokemonEquipe()[0];
                    rafraichirBarreViePokemonJoueur2();

                    starterChoisi = true;
                    Destroy(PokeballBulbizarre);

                    menuStart.transform.GetChild(2).gameObject.SetActive(true);

                    LabelObjetProche.text = "Bulbizarre a été choisi !";

                    StartCoroutine("WaitBeforeMessageDisapear");
                }
                else if (DistancePokeballSalamecheJoueur < DistancePokeballBulbizarreJoueur && DistancePokeballSalamecheJoueur <= DistancePokeballCarapuceJoueur && ObjetProcheGameObject.activeSelf == true && Input.GetKeyDown(KeyCode.Return) && starterChoisi == false && messageBoitePCActif == false)
                {
                    pokemonStarter = pokemon.setChercherPokemonStarter("Salamèche", jeu);

                    Joueur.ajouterPokemonEquipe(pokemonStarter);
                    pokemonJoueurSelectionner = Joueur.getPokemonEquipe()[0];
                    rafraichirBarreViePokemonJoueur2();

                    starterChoisi = true;
                    Destroy(PokeballSalameche);

                    menuStart.transform.GetChild(2).gameObject.SetActive(true);

                    LabelObjetProche.text = "Salamèche a été choisi !";

                    StartCoroutine("WaitBeforeMessageDisapear");
                }
                else if (DistancePokeballCarapuceJoueur < DistancePokeballBulbizarreJoueur && DistancePokeballCarapuceJoueur < DistancePokeballSalamecheJoueur && ObjetProcheGameObject.activeSelf == true && Input.GetKeyDown(KeyCode.Return) && starterChoisi == false && messageBoitePCActif == false)
                {
                    pokemonStarter = pokemon.setChercherPokemonStarter("Carapuce", jeu);

                    Joueur.ajouterPokemonEquipe(pokemonStarter);
                    pokemonJoueurSelectionner = Joueur.getPokemonEquipe()[0];
                    rafraichirBarreViePokemonJoueur2();

                    starterChoisi = true;
                    Destroy(PokeballCarapuce);

                    menuStart.transform.GetChild(2).gameObject.SetActive(true);

                    LabelObjetProche.text = "Carapuce a été choisi !";

                    StartCoroutine("WaitBeforeMessageDisapear");
                }
            }

            if (DistanceBoitePCJoueur < 3.0f && ObjetProcheGameObject.activeSelf == false && messageBoitePCActif == false && OuverturePC == false)
            {
                LabelObjetProche.text = "Boîte PC";
                ObjetProcheGameObject.SetActive(true);
                messageBoitePCActif = true;
            }          
            */
            /*
            if (DistanceBoitePCJoueur < 3.0f  && ObjetProcheGameObject.activeSelf == true && messageBoitePCActif == true && Input.GetKeyDown(KeyCode.Return))
            {
                OuverturePC = true;

                if (Joueur.getPokemonPc().Count > 0)
                {
                    for (int i = 0; i < Joueur.getPokemonPc().Count; i++)
                    {
                        GameObject boutonPokemonGameObject = CreateButton(menuPC.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).transform, Joueur.getPokemonPc()[i].getNom(), 320, 160, Joueur.getPokemonPc()[i].getNom(), 38);
                        Destroy(boutonPokemonGameObject.transform.GetChild(0).gameObject);
                        Button boutonPokemon = boutonPokemonGameObject.GetComponent<Button>();
                        boutonPokemon.transform.Rotate(0, 180, 0);
                        int idPokedexImage = Joueur.getPokemonPc()[i].getNoIdPokedex();
                        boutonPokemon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/" + idPokedexImage);

                        /*
                                            boutonObjet.onClick.AddListener(() => { btn_all_pokeball_click(boutonObjet); });
                                                        btn_pokemon.Add(new Button());
                                                        btn_pokemon[(i * 5) + j].Size = new Size(89, 77);
                                                        // btn_pokemon[(i * 5) + j].SizeMode = PictureBoxSizeMode.CenterImage;
                                                        btn_pokemon[(i * 5) + j].Location = new System.Drawing.Point(40 + (120 * j), 50 + (120 * i));
                                                        pictureBoxBackgroundBoite.Controls.Add(btn_pokemon[(i * 5) + j]);

                                                        int idPokedexImage = Joueur.getPokemonPc()[(i * 5) + j].getNoIdPokedex();
                                                        Bitmap png = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\Icones\\" + idPokedexImage + ".png");
                                                        btn_pokemon[(i * 5) + j].Image = (Image)(new Bitmap(png, new Size(80, 60)));

                                                        btn_pokemon[(i * 5) + j].FlatStyle = FlatStyle.Flat;
                                                        btn_pokemon[(i * 5) + j].FlatAppearance.BorderSize = 0;
                                                        btn_pokemon[(i * 5) + j].BackColor = Color.Transparent;

                                                        btn_pokemon[(i * 5) + j].Click += btn_pokemon_Click;
                                                        btn_pokemon[(i * 5) + j].PreviewKeyDown += btn_pokemon_PreviewKeyDown;
                                                        btn_pokemon[(i * 5) + j].KeyDown += btn_pokemon_KeyDown;                            
                                            }

                                            pictureBoxCurseurChoixPokemon.Size = new Size(41, 41);
                                            pictureBoxCurseurChoixPokemon.SizeMode = PictureBoxSizeMode.StretchImage;
                                            pictureBoxCurseurChoixPokemon.BackColor = Color.Transparent;

                                            Bitmap pngCurseurChoixPokemon = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\Curseur.png");
                                            pictureBoxCurseurChoixPokemon.Image = pngCurseurChoixPokemon;
                                            pictureBoxBackgroundBoite.Controls.Add(pictureBoxCurseurChoixPokemon);

                                            pictureBoxCurseurChoixPokemon.Visible = true;

                                            btn_pokemon[0].Focus();

                                            pictureBoxCurseurChoixPokemon.Location = new System.Drawing.Point(68, 9);
                                        */
            /*
}
}
menuPC.SetActive(true);
} */


            if (Input.GetKeyDown("c"))
            {
                Vector3 destinationPositionPokeball = new Vector3(destinationPokeball.transform.position.x, destinationPokeball.transform.position.y, destinationPokeball.transform.position.z - 0.5f);
                GameObject pokeball = (GameObject) Instantiate(Resources.Load("Models/Pokeballs/Pokeball"), destinationPositionPokeball, destinationPokeball.rotation, GameObjectJoueur.transform);
                Animator animatorPokeball = pokeball.GetComponent<Animator>();
                SignalReceiver signalPokeball = pokeball.GetComponent<SignalReceiver>();

                int idPokedex = JoueurManager.Joueurs[0].pokemonSelectionner.getNoIdPokedex();
                pokeball.GetComponent<AfterThrowingPokeball>().NumeroPokedexPokemon = idPokedex;

                animator.Play("Throw pokeball");

                TimelineAsset timelineAsset = (TimelineAsset)TimelineAnimationLancerPokeball.playableAsset;
                TrackAsset trackAsset = timelineAsset.GetOutputTrack(2);
                TrackAsset trackAsset2 = timelineAsset.GetOutputTrack(3);

                TimelineAnimationLancerPokeball.SetGenericBinding(trackAsset, animatorPokeball);
                TimelineAnimationLancerPokeball.SetGenericBinding(trackAsset2, signalPokeball);

                TimelineAnimationLancerPokeball.Play();

                Destroy(pokeball, 15);

                /*
                GameObject spawnPokemonGameObject = GameObject.Find("SpawnPokemon");
                int idPokedexImage = pokemonJoueurSelectionner.getNoIdPokedex();
                GameObject pokemonSelectionnerGameObject = (GameObject)Instantiate(Resources.Load("Models/Pokemon/" + idPokedexImage), spawnPokemonGameObject.transform.position, spawnPokemonGameObject.transform.rotation);
                */
            }
            /*
            if (Input.GetKeyDown("y"))
            {
                Vector3 destinationPositionPokeball = new Vector3(destinationPokeball.position.x, destinationPokeball.position.y, destinationPokeball.position.z - 0.5f);
                GameObject pokeball = Instantiate(pokeballInstance, destinationPositionPokeball, destinationPokeball.rotation);
                Animator animatorPokeball = pokeball.GetComponent<Animator>();

                int idPokedex = JoueurManager.Joueurs[0].pokemonSelectionner.getNoIdPokedex();
                animatorPokeball.SetInteger("NumeroPokedexPokemon", idPokedex);

                animator.SetTrigger("trThrowBall");
                animator.Play("Red throw pokeball");
                animatorPokeball.Play("Ball throw");
            }
            */
            if (Input.GetKeyDown("n"))
            {
                // Déclenchement d'un combat
                DeclenchementCombat(0);
            }
            /*
            if (Input.GetKeyDown(KeyCode.Space))
            {
              //  DialogueManagerGameObject.GetComponent<DialogueManager>().DisplayNextSentence(DialogueManagerGameObject.GetComponent<DialogueTrigger>().getDialogue());
            } */
        }

        IEnumerator timerBarreExperienceStart()
        {
            while (true)
            {
                timerBarreExperiencePokemonJoueur();
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void tourCombatPokemon()
        {
            StartCoroutine("tourCombatPokemonStart");
        }

        IEnumerator rafraichirApresSoinPokemonJoueur()
        {
            coroutineBarreVieJoueurLancer = true;
            while (coroutineBarreVieJoueurLancer == true)
            {
                timerBarrePokemonJoueurReecriture(JoueurManager.Joueurs[0].pokemonSelectionner, barViePokemonJoueurImage, LabelPvPokemonJoueurUI, ref compteur, gagnePvPokemonJoueur, "PokemonJoueur");
                yield return new WaitForSeconds(0.1f);
            }

            coroutineBarreVieJoueurLancer = true;
            rafraichirBarreViePokemonJoueur();

            while (coroutineBarreVieJoueurLancer == true)
            {
                timerBarrePokemonJoueurReecriture(JoueurManager.Joueurs[0].pokemonSelectionner, barViePokemonJoueurImage, LabelPvPokemonJoueurUI, ref compteur, gagnePvPokemonJoueur, "PokemonJoueur");
                yield return new WaitForSeconds(0.1f);
            }

            activerMenuCombatApresTour();
        }

        IEnumerator tourCapturePokemon()
        {
            coroutineBarreVieAdversaireLancer = true;

            while (coroutineBarreVieAdversaireLancer == true)
            {
                timerAnimationCapture();
                yield return new WaitForSeconds(0.1f);
            }

            if (nombreMouvementsBall < 4)
            {
                coroutineBarreVieJoueurLancer = true;
                rafraichirBarreViePokemonJoueur();

                while (coroutineBarreVieJoueurLancer == true)
                {
                    timerBarrePokemonJoueurReecriture(JoueurManager.Joueurs[0].pokemonSelectionner, barViePokemonJoueurImage, LabelPvPokemonJoueurUI, ref compteur, gagnePvPokemonJoueur, "PokemonJoueur");
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else
            {
                yield return new WaitForSeconds(8f);

                scroll_objets.SetActive(false);
                btn_fuite_click(0);
            }

            if (nombreMouvementsBall >= 0)
            {
                nombreMouvementsBall = -1;
            }
        }


        IEnumerator tourCombatPokemonStartTourContrePokemonJoueurEnPremier()
        {
            rafraichirBarreViePokemonJoueur();
            this.gameObject.GetComponent<AttaqueMouvementCombatScript>().GoToNextPNJ(pokemonAdverseGameObject, pokemonJoueurGameObject);
            coroutineBarreVieJoueurLancer = true;
            //  while (coroutineBarreVieJoueurLancer == true)
            //  {

            //   yield return new WaitUntil(() => this.gameObject.GetComponent<AttaqueMouvementCombatScript>().animationAttaqueFiniContrePokemonAdversairePremierePartie == true);

            while (this.gameObject.GetComponent<AttaqueMouvementCombatScript>().animationStatutAttaqueContrePokemonAdversaire == "PremierDeplacement")
            {
                yield return new WaitForSeconds(0.1f);
            }

            GameObject textDegats3D = this.gameObject.GetComponent<CreerComposantScript>().CreateText3D(pokemonJoueurGameObject.transform, "NbDegats", 250, 250, 0, 1, nbDegats.ToString(), 12, Color.green, cameraCombat);
            Destroy(textDegats3D, 5);

            while (coroutineBarreVieJoueurLancer == true)
                { 
                    timerBarrePokemonJoueurReecriture(JoueurManager.Joueurs[0].pokemonSelectionner, barViePokemonJoueurImage, LabelPvPokemonJoueurUI, ref compteur, gagnePvPokemonJoueur, "PokemonJoueur");
                    //  timerBarrePokemonJoueurReecriture(pokemon, barViePokemonAdversaireImage, LabelPvPokemonAdversaireUI, ref compteurAdversaire, gagnePvPokemonJoueur);
                    yield return new WaitForSeconds(0.1f);
                }

            while (this.gameObject.GetComponent<AttaqueMouvementCombatScript>().animationStatutAttaqueContrePokemonAdversaire == "RevientPositionInitiale")
            {
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(1);

            rafraichirBarreViePokemonAdversaire();
            coroutineBarreVieAdversaireLancer = true;
            this.gameObject.GetComponent<AttaqueMouvementCombatScript>().GoToNextPNJ(pokemonJoueurGameObject, pokemonAdverseGameObject);

            while (this.gameObject.GetComponent<AttaqueMouvementCombatScript>().animationStatutAttaqueContrePokemonAdversaire == "PremierDeplacement")
            {
                yield return new WaitForSeconds(0.1f);
            }

            GameObject textDegats3DAdversaire = this.gameObject.GetComponent<CreerComposantScript>().CreateText3D(pokemonAdverseGameObject.transform, "NbDegats", 250, 250, 0, 1, nbDegatsContreAdversaire.ToString(), 12, Color.green, cameraCombat);
            Destroy(textDegats3DAdversaire, 5);

            while (coroutineBarreVieAdversaireLancer == true)
            {
                timerBarrePokemonJoueurReecriture(pokemon, barViePokemonAdversaireImage, LabelPvPokemonAdversaireUI, ref compteurAdversaire, gagnePvPokemonJoueur, "PokemonAdversaire");
                yield return new WaitForSeconds(0.1f);
            }

            while (this.gameObject.GetComponent<AttaqueMouvementCombatScript>().animationStatutAttaqueContrePokemonAdversaire == "RevientPositionInitiale")
            {
                yield return new WaitForSeconds(0.1f);
            }

            activerMenuCombatApresTour();
        }

        IEnumerator tourCombatPokemonStartTourContrePokemonAdversaireEnPremier()
        {
            rafraichirBarreViePokemonAdversaire();
            this.gameObject.GetComponent<AttaqueMouvementCombatScript>().GoToNextPNJ(pokemonJoueurGameObject, pokemonAdverseGameObject);
            coroutineBarreVieAdversaireLancer = true;
            //  while (coroutineBarreVieJoueurLancer == true)
            //  {
            while (this.gameObject.GetComponent<AttaqueMouvementCombatScript>().animationStatutAttaqueContrePokemonAdversaire == "PremierDeplacement")
            {
                yield return new WaitForSeconds(0.1f);
            }

            GameObject textDegats3DAdversaire = this.gameObject.GetComponent<CreerComposantScript>().CreateText3D(pokemonAdverseGameObject.transform, "NbDegats", 250, 250, 0, 1, nbDegatsContreAdversaire.ToString(), 12, Color.green, cameraCombat);
            Destroy(textDegats3DAdversaire, 5);

            while (coroutineBarreVieAdversaireLancer == true)
            {
                timerBarrePokemonJoueurReecriture(pokemon, barViePokemonAdversaireImage, LabelPvPokemonAdversaireUI, ref compteurAdversaire, gagnePvPokemonJoueur, "PokemonAdversaire");
                yield return new WaitForSeconds(0.1f);
            }

            while (this.gameObject.GetComponent<AttaqueMouvementCombatScript>().animationStatutAttaqueContrePokemonAdversaire == "RevientPositionInitiale")
            {
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(1);

            if (pokemon.getPvRestant() > 0)
            {

                rafraichirBarreViePokemonJoueur();
                this.gameObject.GetComponent<AttaqueMouvementCombatScript>().GoToNextPNJ(pokemonAdverseGameObject, pokemonJoueurGameObject);
                coroutineBarreVieJoueurLancer = true;

                while (this.gameObject.GetComponent<AttaqueMouvementCombatScript>().animationStatutAttaqueContrePokemonAdversaire == "PremierDeplacement")
                {
                    yield return new WaitForSeconds(0.1f);
                }

                GameObject textDegats3D = this.gameObject.GetComponent<CreerComposantScript>().CreateText3D(pokemonJoueurGameObject.transform, "NbDegats", 250, 250, 0, 1, nbDegats.ToString(), 12, Color.green, cameraCombat);
                Destroy(textDegats3D, 5);

                while (coroutineBarreVieJoueurLancer == true)
                {
                    timerBarrePokemonJoueurReecriture(JoueurManager.Joueurs[0].pokemonSelectionner, barViePokemonJoueurImage, LabelPvPokemonJoueurUI, ref compteur, gagnePvPokemonJoueur, "PokemonJoueur");
                    yield return new WaitForSeconds(0.1f);
                }

                while (this.gameObject.GetComponent<AttaqueMouvementCombatScript>().animationStatutAttaqueContrePokemonAdversaire == "RevientPositionInitiale")
                {
                    yield return new WaitForSeconds(0.1f);
                }

                activerMenuCombatApresTour();
            }
        }

        IEnumerator timerBarreJoueurStart()
        {
            coroutineBarreVieJoueurLancer = true;

            while (true)
            {
                timerBarrePokemonJoueurReecriture(JoueurManager.Joueurs[0].pokemonSelectionner, barViePokemonJoueurImage, LabelPvPokemonJoueurUI, ref compteur, gagnePvPokemonJoueur, "PokemonJoueur");
              //  timerBarrePokemonJoueurReecriture(pokemon, barViePokemonAdversaireImage, LabelPvPokemonAdversaireUI, ref compteurAdversaire, gagnePvPokemonJoueur);
                yield return new WaitForSeconds(0.1f);
            }
        }

        IEnumerator timerBarreAdversaireStart()
        {
            while (true)
            {
                timerBarrePokemonJoueurReecriture(pokemon, barViePokemonAdversaireImage, LabelPvPokemonAdversaireUI, ref compteurAdversaire, gagnePvPokemonJoueur, "PokemonAdversaire");
                // timerBarrePokemonAdversaire();
                yield return new WaitForSeconds(0.1f);
            }
        }

        IEnumerator showStatistiquesStart()
        {
            while (StatistiquesChangementNiveau.activeSelf)
            {
                ShowStatistiquesNext();
                yield return null;
            }
            StopCoroutine("showStatistiquesStart");
        }

       public IEnumerator WaitBeforeMessageDisapear()
        {
            yield return new WaitForSeconds(5);  
            ObjetProcheGameObject.SetActive(false);
        }

        private void ShowStatistiquesNext()
        {
            if (showStatistiquesAugmenter == true && Input.GetKeyDown("j"))
            {
                showStatistiquesAugmenter = false;
                showStatistiquesNiveauSuivant = true;

                LabelPvChangementNiveau.text = "PV :                           " + JoueurManager.Joueurs[0].pokemonSelectionner.getPv();
                LabelAttaqueChangementNiveau.text = "Attaque :                   " + JoueurManager.Joueurs[0].pokemonSelectionner.getStatistiquesAttaque();
                LabelDefenseChangementNiveau.text = "Défense :                  " + JoueurManager.Joueurs[0].pokemonSelectionner.getStatistiquesDefense();
                LabelVitesseChangementNiveau.text = "Vitesse :                   " + JoueurManager.Joueurs[0].pokemonSelectionner.getStatistiquesVitesse();
                LabelAttaqueSpecialeChangementNiveau.text = "Attaque Spéciale :    " + JoueurManager.Joueurs[0].pokemonSelectionner.getStatistiquesAttaqueSpeciale();
                LabelDefenseSpecialeChangementNiveau.text = "Défense Spéciale :   " + JoueurManager.Joueurs[0].pokemonSelectionner.getStatistiquesDefenseSpeciale();
            }

            else if(showStatistiquesAugmenter == false && showStatistiquesNiveauSuivant == true && Input.GetKeyDown("j"))
            {
                showStatistiquesNiveauSuivant = false;
                StatistiquesChangementNiveau.SetActive(false);
            }

            /*
            else if ((e.KeyCode == Keys.A || e.KeyCode == Keys.Enter) && showStatistiquesAugmenter == false && showStatistiquesNiveauSuivant == true)
            {
                showStatistiquesNiveauSuivant = false;
                groupBoxNiveauSuperieurStatistiques.Visible = false;
                combat_btn.Enabled = true;
                combat_btn.Focus();
            } */
        }

        /// <summary>
        /// Cette méthode gère la capture ou non d'un pokémon
        private void timerAnimationCapture()
        {
            // Si le nombre de mouvement d'une pokéball est 4, le pokémon est attrapé
            if (nombreMouvementsBall == 4)
            {               
                    LabelPvPokemonAdversaireUI.text = "Attrapé";

                    string pokemonEnvoi = JoueurManager.Joueurs[0].attraperPokemon(pokemon);

                    dialogueCombat.getDialogue().AddSentence("Et hop ! " + pokemon.getNom() + " est attrapé !");
                    StartCoroutine(DialogueCombat());

                    /*
                    combat_btn.Enabled = true;
                    btn_pc.Enabled = true;
                    btn_attaque1.Enabled = false;
                    btn_attraper.Enabled = false;
                    btn_soigner.Enabled = false;
                    btn_changement_pokemon.Enabled = false;
                    panel_choix_attaque_selection.Visible = false;
                    panel_choix_pokemon_selection.Visible = false;

                    Graphics gBarreAdversaire = Graphics.FromImage(DrawAreaPokemonAdversaire);
                    gBarreAdversaire.Clear(SystemColors.Control);
                    pictureBoxBarreViePokemonAdversaire.Image = DrawAreaPokemonAdversaire;
                    gBarreAdversaire.Dispose();
                    */

                    barViePokemonAdversaireImage.fillAmount = 0;
                    
                    if (pokemonEnvoi == "Equipe") // Si le nombre de pokémon dans l'équipe est inférieur à 6, il est envoyé dans l'équipe
                    {
                        // pokemon.setPvRestant(0);
                        // rafraichirBarreViePokemonAdversaire1();

                        int idPokedexImage = JoueurManager.Joueurs[0].getPokemonEquipe()[JoueurManager.Joueurs[0].getPokemonEquipe().Count - 1].getNoIdPokedex();                    
                    }
                    else // Sinon il sera envoyé dans le pc
                    {
                        dialogueCombat.getDialogue().AddSentence(pokemon.getNom() + " est envoyé dans le PC !");
                        StartCoroutine(DialogueCombat());
                    }

                    /*
                    compteurAnimationCapturePartie1 = 0;
                    compteurAnimationCapturePartie2 = 0;
                    compteurAnimationCapturePartie3 = 0;
                    compteurAnimationCapturePartie4 = 0;
                    compteurAnimationCapturePartie5 = 0; */
                   // nombreMouvementsBall = 0;
                    nombreTourStatutAdversaire = 0;
                    nombreTourSommeilAdversaire = 0;
                    nombreTourStatutAEffectuerAdversaire = 0;
                }
                else
                {
                //  pictureBoxPokemonCombatAdversaire.Visible = true;
                 //   StopCoroutine("timerAnimationCaptureStart");

                    // Si la pokéball fait entre 0 et 3 mouvements, le pokémon n'est pas attrapé
                    if (nombreMouvementsBall == 0)
                    {
                        dialogueCombat.getDialogue().AddSentence("Oh, non ! Le Pokémon s'est libéré");
                      //  StartCoroutine(DialogueCombat());
                    }
                    else if (nombreMouvementsBall == 1)
                    {
                        dialogueCombat.getDialogue().AddSentence("Raaah ça y était presque !");
                      //  StartCoroutine(DialogueCombat());
                    }
                    else if (nombreMouvementsBall == 2)
                    {
                        dialogueCombat.getDialogue().AddSentence("Aaaaaah ! Presque !");
                      //  StartCoroutine(DialogueCombat());
                    }
                    else if (nombreMouvementsBall == 3)
                    {
                        dialogueCombat.getDialogue().AddSentence("Mince ! ça y était presque !");
                      //  StartCoroutine(DialogueCombat());
                    }
                   // rafraichirBarreViePokemonJoueur();
            }
            coroutineBarreVieAdversaireLancer = false;
        }

        /// <summary>
        /// Cette méthode gère la barre d'expérience du pokémon en combat
        private void timerBarreExperiencePokemonJoueur()
        {
            int pourcentageBarreExperience = (100 * (JoueurManager.Joueurs[0].pokemonSelectionner.getExperience() - JoueurManager.Joueurs[0].pokemonSelectionner.getExperiencePokemonReturn())) / (JoueurManager.Joueurs[0].pokemonSelectionner.getExperiencePokemonProchainNiveau() - JoueurManager.Joueurs[0].pokemonSelectionner.getExperiencePokemonReturn());

            if (compteurExperience <= pourcentageBarreExperience)
            {

                barExperiencePokemonJoueurImage.fillAmount = (float)(((100 * (JoueurManager.Joueurs[0].pokemonSelectionner.getExperience() - JoueurManager.Joueurs[0].pokemonSelectionner.getExperiencePokemonReturn())) / (JoueurManager.Joueurs[0].pokemonSelectionner.getExperiencePokemonProchainNiveau() - JoueurManager.Joueurs[0].pokemonSelectionner.getExperiencePokemonReturn())) - (pourcentageBarreExperience - compteurExperience)) / 100;

                compteurExperience += (JoueurManager.Joueurs[0].pokemonSelectionner.getExperiencePokemonProchainNiveau() - JoueurManager.Joueurs[0].pokemonSelectionner.getExperiencePokemonReturn()) / (JoueurManager.Joueurs[0].pokemonSelectionner.getExperiencePokemonProchainNiveau() - JoueurManager.Joueurs[0].pokemonSelectionner.getExperiencePokemonReturn());

                // Si le pourcentage d'expérience est supérieur à 100, la taille de la barre, alors cela veut dire que le pokémon à gagné un niveau ainsi que des statistiques en plus
                if (compteurExperience >= 100)
                {
                    compteurExperience = 0;

                    int nbViePokemonAvant = JoueurManager.Joueurs[0].pokemonSelectionner.getPv();
                    int statistiquesAttaquePokemonAvant = JoueurManager.Joueurs[0].pokemonSelectionner.getStatistiquesAttaque();
                    int statistiquesDefensePokemonAvant = JoueurManager.Joueurs[0].pokemonSelectionner.getStatistiquesDefense();
                    int statistiquesVitessePokemonAvant = JoueurManager.Joueurs[0].pokemonSelectionner.getStatistiquesVitesse();
                    int statistiquesAttaqueSpecialePokemonAvant = JoueurManager.Joueurs[0].pokemonSelectionner.getStatistiquesAttaqueSpeciale();
                    int statistiquesDefenseSpecialePokemonAvant = JoueurManager.Joueurs[0].pokemonSelectionner.getStatistiquesDefenseSpeciale();

                    JoueurManager.Joueurs[0].pokemonSelectionner.setNiveau(JoueurManager.Joueurs[0].pokemonSelectionner.getNiveau() + 1);

                    int nbviePokemonApres = JoueurManager.Joueurs[0].pokemonSelectionner.getPv();
                    int statistiquesAttaquePokemonApres = JoueurManager.Joueurs[0].pokemonSelectionner.getStatistiquesAttaque();
                    int statistiquesDefensePokemonApres = JoueurManager.Joueurs[0].pokemonSelectionner.getStatistiquesDefense();
                    int statistiquesVitessePokemonApres = JoueurManager.Joueurs[0].pokemonSelectionner.getStatistiquesVitesse();
                    int statistiquesAttaqueSpecialePokemonApres = JoueurManager.Joueurs[0].pokemonSelectionner.getStatistiquesAttaqueSpeciale();
                    int statistiquesDefenseSpecialePokemonApres = JoueurManager.Joueurs[0].pokemonSelectionner.getStatistiquesDefenseSpeciale();

                    int nbPvGagner = nbviePokemonApres - nbViePokemonAvant;
                    int statistiquesAttaqueGagner = statistiquesAttaquePokemonApres - statistiquesAttaquePokemonAvant;
                    int statistiquesDefenseGagner = statistiquesDefensePokemonApres - statistiquesDefensePokemonAvant;
                    int statistiquesVitesseGagner = statistiquesVitessePokemonApres - statistiquesVitessePokemonAvant;
                    int statistiquesAttaqueSpecialeGagner = statistiquesAttaqueSpecialePokemonApres - statistiquesAttaqueSpecialePokemonAvant;
                    int statistiquesDefenseSpecialeGagner = statistiquesDefenseSpecialePokemonApres - statistiquesDefenseSpecialePokemonAvant;

                    JoueurManager.Joueurs[0].pokemonSelectionner.setPvRestant(JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() + nbPvGagner);
                    LabelNiveauPokemonJoueurUI.text = "N. " + JoueurManager.Joueurs[0].pokemonSelectionner.getNiveau();

                    LabelPvChangementNiveau.text = "PV :                           + " + nbPvGagner;
                    LabelAttaqueChangementNiveau.text = "Attaque :                   + " + statistiquesAttaqueGagner;
                    LabelDefenseChangementNiveau.text = "Défense :                  + " + statistiquesDefenseGagner;
                    LabelVitesseChangementNiveau.text = "Vitesse :                   + " + statistiquesVitesseGagner;
                    LabelAttaqueSpecialeChangementNiveau.text = "Attaque Spéciale :    + " + statistiquesAttaqueSpecialeGagner;
                    LabelDefenseSpecialeChangementNiveau.text = "Défense Spéciale :   + " + statistiquesDefenseSpecialeGagner;

                    for (int i = 0; i <= JoueurManager.Joueurs[0].getPokemonEquipe().Count - 1; i++)
                    {
                        if (JoueurManager.Joueurs[0].pokemonSelectionner == JoueurManager.Joueurs[0].getPokemonEquipe()[i])
                        {
                          //  label_pv_pokemon[i].Text = pokemonJoueurSelectionner.getPvRestant().ToString() + " / " + pokemonJoueurSelectionner.getPv().ToString() + " PV";
                        }
                    }
                    LabelPvPokemonJoueurUI.text = JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant().ToString() + " / " + JoueurManager.Joueurs[0].pokemonSelectionner.getPv().ToString() + " PV";

                    dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " passe au niveau " + JoueurManager.Joueurs[0].pokemonSelectionner.getNiveau());
                    StartCoroutine(DialogueCombat());
                    //  rafraichirBarreViePokemonJoueur1();

                    StatistiquesChangementNiveau.SetActive(true);

                    //  groupBoxNiveauSuperieurStatistiques.Visible = true;
                    showStatistiquesAugmenter = true;
                    StartCoroutine("showStatistiquesStart");
                }
            }
            else
            {
                StopCoroutine("timerBarreExperienceStart");
                btn_fuite_click(0);
                //    timerBarreExperiencePokemonJoueur.Stop();

                //  if (groupBoxNiveauSuperieurStatistiques.Visible == false)
                // {
                //   combat_btn.Enabled = true;

                //  combat_btn.Focus();
                // }
            }

            // gBarreExperience.Dispose();



        }

        private void timerBarrePokemonJoueurReecriture(ClassLibrary.Pokemon pokemonSelectionner, Image barViePokemon, Text LabelPVPokemonUI, ref int compteurPv, bool gagnePvPokemon, string tourContrePokemonJoueurouAdversaire)
        {
            if (compteurPv >= pokemonSelectionner.getPvRestant() && compteurPv >= 0 && compteurPv <= pokemonSelectionner.getPv() && gagnePvPokemon == false || compteurPv <= pokemonSelectionner.getPvRestant() && compteurPv <= pokemonSelectionner.getPv() && gagnePvPokemon == true)
            {
                // Pv entre toute la barre et 1/5
                if (compteurPv >= pokemonSelectionner.getPv() / 2)
                {
                    barViePokemon.color = new Color32(81, 209, 39, 255);
                    barViePokemon.fillAmount = (float)(pokemonSelectionner.getPv() - (pokemonSelectionner.getPv() - compteurPv)) / pokemonSelectionner.getPv() + 0.01f;
                    LabelPVPokemonUI.text = compteurPv + " / " + pokemonSelectionner.getPv() + " PV";

                }
                else if (compteurPv < pokemonSelectionner.getPv() / 2 && compteurPv >= pokemonSelectionner.getPv() / 5)
                {
                    barViePokemon.color = Color.yellow;
                    barViePokemon.fillAmount = (float)(pokemonSelectionner.getPv() - (pokemonSelectionner.getPv() - compteurPv)) / pokemonSelectionner.getPv();
                    LabelPVPokemonUI.text = compteurPv + " / " + pokemonSelectionner.getPv() + " PV";
                }
                else if (compteurPv < pokemonSelectionner.getPv() / 5 && compteurPv > 0)
                {
                    barViePokemon.color = Color.red;
                    barViePokemon.fillAmount = (float)(pokemonSelectionner.getPv() - (pokemonSelectionner.getPv() - compteurPv)) / pokemonSelectionner.getPv();
                    LabelPVPokemonUI.text = compteurPv + " / " + pokemonSelectionner.getPv() + " PV";
                }

                // Pv entre 0 et 1/5
                else
                {
                    if (pokemonSelectionner.getPv() - (pokemonSelectionner.getPv() - compteurPv) > 0)
                    {
                        barViePokemon.fillAmount = (float)(pokemonSelectionner.getPv() - (pokemonSelectionner.getPv() - compteurPv)) / pokemonSelectionner.getPv();
                        LabelPVPokemonUI.text = compteur + " / " + pokemonSelectionner.getPv() + " PV";
                    }
                    else
                    {
                        barViePokemon.fillAmount = 0f;
                        LabelPVPokemonUI.text = "K.O.";

                        dialogueCombat.getDialogue().AddSentence(pokemonSelectionner.getNom() + " est K.O.");
                        StartCoroutine(DialogueCombat());

                        if (pokemon.getPvRestant() <= 0 && tourContrePokemonJoueurouAdversaire == "PokemonAdversaire")
                        {
                            rafraichirBarreExperiencePokemonJoueur();
                        }
                    }
                }

                if (gagnePvPokemon == false)
                {
                    compteurPv--;
                }
                else if (gagnePvPokemon == true)
                {
                    compteurPv++;
                }

            }
            else
            {
                if (tourContrePokemonJoueurouAdversaire == "PokemonJoueur")
                {
                    if (gagnePvPokemonJoueur == true)
                    {
                        compteur = pokemonSelectionner.getPvRestant();
                        gagnePvPokemonJoueur = false;
                    }

                    coroutineBarreVieJoueurLancer = false;
                }
                else if(tourContrePokemonJoueurouAdversaire == "PokemonAdversaire")
                {
                    coroutineBarreVieAdversaireLancer = false;
                }
            }

            if (gagnePvPokemonJoueur == true)
            {
               // compteur = pokemonJoueurSelectionner.getPvRestant();
               // gagnePvPokemonJoueur = false;

             //  rafraichirBarreViePokemonJoueur();
              //  StartCoroutine("rafraichirApresSoinPokemonJoueur");
            }

            /*
            if (statutPokemonPerdPvJoueur != 2)
            {
                if (pokemon.getPvRestant() > 0 && (pokemon.getStatutPokemon() != "Paralysie" && pokemon.getStatutPokemon() != "Gelé" && pokemon.getStatutPokemon() != "Sommeil") || (pokemon.getStatutPokemon() == "Paralysie" && reussiteAttaqueParalyseAdversaire == true) || (pokemon.getStatutPokemon() == "Gelé" && reussiteAttaqueGelAdversaire == true))
                {
                    dialogueCombat.getDialogue().AddSentence(pokemon.getNom() + " adverse a fait " + nbDegats + " dégâts ");
                    StartCoroutine(DialogueCombat());
                }

            }
            else
            {
                if (pokemonSelectionner.getStatutPokemon() == "Brulure")
                {
                    nbDegats = pokemonJoueurSelectionner.getPv() / 16;
                    dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " brule");
                    dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " perd " + nbDegats + " pv");
                    StartCoroutine(DialogueCombat());
                }
                else if (pokemonSelectionner.getStatutPokemon() == "Empoisonnement normal")
                {
                    nbDegats = pokemonJoueurSelectionner.getPv() / 8;
                    dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " souffre du poison");
                    dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " perd " + nbDegats + " pv");
                    StartCoroutine(DialogueCombat());
                }
                else if (pokemonSelectionner.getStatutPokemon() == "Empoisonnement grave")
                {
                    nbDegats = (pokemonJoueurSelectionner.getPv() / 16) * (nombreTourStatut - 1);
                    dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " souffre du poison");
                    dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " perd " + nbDegats + " pv");
                    StartCoroutine(DialogueCombat());
                }
            }

            if (gagnePvPokemon == true)
            {
                compteurPv = pokemonSelectionner.getPvRestant();
                gagnePvPokemon = false;

                rafraichirBarreViePokemonJoueur();
            }

            else if (pokemonJoueurAttaquePremier == false && nombreMouvementsBall < 0 && pokemonSelectionner.getPvRestant() > 0 && changement_pokemon == false && statutPokemonPerdPvJoueur == 0)
            {
                rafraichirBarreViePokemonAdversaire();
            }

            if (pokemonJoueurAttaquePremier == false && pokemon.getPvRestant() <= 0)
            {

            }
            else if ((pokemonJoueurAttaquePremier == true && pokemon.getPvRestant() > 0 && statutPokemonPerdPvAdversaire == 0 && pokemonSelectionner.getPvRestant() > 0 && (pokemon.getStatutPokemon() == "Normal" || pokemon.getStatutPokemon() == "Paralysie" || pokemon.getStatutPokemon() == "Gelé" || pokemon.getStatutPokemon() == "Sommeil")) || (pokemonJoueurAttaquePremier == false && nombreMouvementsBall >= 0 && nombreMouvementsBall < 4 && pokemonSelectionner.getPvRestant() > 0 && (pokemon.getStatutPokemon() == "Normal" || pokemon.getStatutPokemon() == "Paralysie" || pokemon.getStatutPokemon() == "Gelé" || pokemon.getStatutPokemon() == "Sommeil")))
            {
                try
                {
                    //   Bitmap pngMenuCombat = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\Combat\\menu_combat.png");
                    //   pictureBoxMenuCombat.Image = pngMenuCombat;
                }
                catch
                {
                    //   MessageBox.Show("L'image du menu de combat n'a pas pu être chargée. Veuillez vérifier que celle-ci est bien présente dans le répertoire.", "Vérification du menu de combat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                boutons_combat.SetActive(true);
                boutons_combat.transform.GetChild(0).gameObject.GetComponent<Button>().Select();
            }

            else if (pokemon.getPvRestant() > 0 && pokemonSelectionner.getPvRestant() <= 0)
            {
                // timerAnimationKo.Start();

                bool equipePokemonEncoreEnVie = false;

                for (int i = 0; i < Joueur.getPokemonEquipe().Count; i++)
                {
                    if (Joueur.getPokemonEquipe()[i].getPvRestant() > 0)
                    {
                        equipePokemonEncoreEnVie = true;
                    }
                }

                if (equipePokemonEncoreEnVie == false)
                {
                    //  MessageBox.Show("Votre équipe à perdu");
                }
                else
                {
                    btn_changement_pokemon_click();

                    changementPokemonPokemonKo = true;
                }
            }
            if ((pokemon.getStatutPokemon() == "Brulure" || pokemon.getStatutPokemon() == "Empoisonnement normal" || pokemon.getStatutPokemon() == "Empoisonnement grave") && statutPokemonPerdPvAdversaire == 0 && statutPokemonPerdPvJoueur == 0 && pokemonJoueurAttaquePremier == true && pokemon.getPvRestant() > 0)
            {
                statutPokemonPerdPvAdversaire = 1;
                compteurAdversaire = pokemon.getPvRestant();

                if (pokemon.getStatutPokemon() == "Brulure")
                {
                    pokemon.setPvRestant(pokemon.getPvRestant() - (pokemon.getPv() / 16));
                }
                else if (pokemon.getStatutPokemon() == "Empoisonnement normal")
                {
                    pokemon.setPvRestant(pokemon.getPvRestant() - (pokemon.getPv() / 8));
                }
                else if (pokemon.getStatutPokemon() == "Empoisonnement grave")
                {
                    pokemon.setPvRestant(pokemon.getPvRestant() - ((pokemon.getPv() / 16) * nombreTourStatutAdversaire));
                    if (nombreTourStatutAdversaire < 16)
                    {
                        nombreTourStatutAdversaire++;
                    }
                }
                rafraichirBarreViePokemonAdversaire();
            }
            else if ((pokemon.getStatutPokemon() == "Normal" || pokemon.getStatutPokemon() == "Paralysie" || pokemon.getStatutPokemon() == "Gelé" || pokemon.getStatutPokemon() == "Sommeil") && (pokemonSelectionner.getStatutPokemon() == "Brulure" || pokemonSelectionner.getStatutPokemon() == "Empoisonnement normal" || pokemonSelectionner.getStatutPokemon() == "Empoisonnement grave") && statutPokemonPerdPvAdversaire == 0 && statutPokemonPerdPvJoueur == 0 && pokemonJoueurAttaquePremier == true && pokemon.getPvRestant() > 0)
            {
                compteurPv = pokemonSelectionner.getPvRestant();
                if (pokemonSelectionner.getStatutPokemon() == "Brulure")
                {
                    pokemonSelectionner.setPvRestant(pokemonSelectionner.getPvRestant() - (pokemonSelectionner.getPv() / 16));
                }
                else if (pokemonSelectionner.getStatutPokemon() == "Empoisonnement normal")
                {
                    pokemonSelectionner.setPvRestant(pokemonSelectionner.getPvRestant() - (pokemonSelectionner.getPv() / 8));
                }
                else if (pokemonSelectionner.getStatutPokemon() == "Empoisonnement grave")
                {
                    pokemonSelectionner.setPvRestant(pokemonSelectionner.getPvRestant() - ((pokemonSelectionner.getPv() / 16) * nombreTourStatut));
                    if (nombreTourStatut < 16)
                    {
                        nombreTourStatut++;
                    }
                }
                statutPokemonPerdPvJoueur = 1;
                rafraichirBarreViePokemonJoueur();
            }

            else if (statutPokemonPerdPvJoueur == 2 && (pokemonSelectionner.getStatutPokemon() == "Brulure" || pokemonSelectionner.getStatutPokemon() == "Empoisonnement normal" || pokemonSelectionner.getStatutPokemon() == "Empoisonnement grave"))
            {
                statutPokemonPerdPvJoueur = 0;

                if (pokemonSelectionner.getPvRestant() > 0 && pokemon.getPvRestant() > 0)
                {
                    boutons_combat.SetActive(true);
                    boutons_combat.transform.GetChild(0).gameObject.GetComponent<Button>().Select();   
                }
            }

            if (nombreMouvementsBall >= 0)
            {
                nombreMouvementsBall = -1;
            } */
        }

        /// <summary>
        /// Cette méthode gère la vie et la barre du pokémon du joueur
        private void timerBarrePokemonJoueur()
        {

            if (compteur >= JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() && compteur >= 0 && compteur <= JoueurManager.Joueurs[0].pokemonSelectionner.getPv() && gagnePvPokemonJoueur == false || compteur <= JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() && compteur <= JoueurManager.Joueurs[0].pokemonSelectionner.getPv() && gagnePvPokemonJoueur == true)
            {

                if (compteur >= JoueurManager.Joueurs[0].pokemonSelectionner.getPv() / 2)
                {
                    barViePokemonJoueurImage.color = new Color32(81, 209, 39, 255);
                    barViePokemonJoueurImage.fillAmount = (float)(JoueurManager.Joueurs[0].pokemonSelectionner.getPv() - (JoueurManager.Joueurs[0].pokemonSelectionner.getPv() - compteur)) / JoueurManager.Joueurs[0].pokemonSelectionner.getPv() + 0.01f;
                    LabelPvPokemonJoueurUI.text = compteur + " / " + JoueurManager.Joueurs[0].pokemonSelectionner.getPv() + " PV";

                }
                else if (compteur < JoueurManager.Joueurs[0].pokemonSelectionner.getPv() / 2 && compteur >= JoueurManager.Joueurs[0].pokemonSelectionner.getPv() / 5)
                {
                    barViePokemonJoueurImage.color = Color.yellow;
                    barViePokemonJoueurImage.fillAmount = (float)(JoueurManager.Joueurs[0].pokemonSelectionner.getPv() - (JoueurManager.Joueurs[0].pokemonSelectionner.getPv() - compteur)) / JoueurManager.Joueurs[0].pokemonSelectionner.getPv();
                    LabelPvPokemonJoueurUI.text = compteur + " / " + JoueurManager.Joueurs[0].pokemonSelectionner.getPv() + " PV";
                }
                else if (compteur < JoueurManager.Joueurs[0].pokemonSelectionner.getPv() / 5 && compteur > 0)
                {
                    barViePokemonJoueurImage.color = Color.red;
                    barViePokemonJoueurImage.fillAmount = (float)(JoueurManager.Joueurs[0].pokemonSelectionner.getPv() - (JoueurManager.Joueurs[0].pokemonSelectionner.getPv() - compteur)) / JoueurManager.Joueurs[0].pokemonSelectionner.getPv();
                    LabelPvPokemonJoueurUI.text = compteur + " / " + JoueurManager.Joueurs[0].pokemonSelectionner.getPv() + " PV";
                }

                else
                {


                    if (JoueurManager.Joueurs[0].pokemonSelectionner.getPv() - (JoueurManager.Joueurs[0].pokemonSelectionner.getPv() - compteur) > 0)
                    {
                        barViePokemonJoueurImage.fillAmount = (float)(JoueurManager.Joueurs[0].pokemonSelectionner.getPv() - (JoueurManager.Joueurs[0].pokemonSelectionner.getPv() - compteur)) / JoueurManager.Joueurs[0].pokemonSelectionner.getPv();
                        LabelPvPokemonJoueurUI.text = compteur + " / " + JoueurManager.Joueurs[0].pokemonSelectionner.getPv() + " PV";


                    }
                    else
                    {
                        barViePokemonJoueurImage.fillAmount = 0f;
                        LabelPvPokemonJoueurUI.text = "K.O.";

                        dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " est K.O.");
                        StartCoroutine(DialogueCombat());
                    }
                }


                if (gagnePvPokemonJoueur == false)
                {
                    compteur--;
                }

                if (gagnePvPokemonJoueur == true)
                {
                    compteur++;
                }

                // g.Dispose();


            }
            else
            {
                StopCoroutine("timerBarreJoueurStart");

                if (statutPokemonPerdPvJoueur != 2)
                {
                    if (pokemon.getPvRestant() > 0 && (pokemon.getStatutPokemon() != "Paralysie" && pokemon.getStatutPokemon() != "Gelé" && pokemon.getStatutPokemon() != "Sommeil") || (pokemon.getStatutPokemon() == "Paralysie" && reussiteAttaqueParalyseAdversaire == true) || (pokemon.getStatutPokemon() == "Gelé" && reussiteAttaqueGelAdversaire == true))
                    {
                        dialogueCombat.getDialogue().AddSentence(pokemon.getNom() + " adverse a fait " + nbDegats + " dégâts ");
                        StartCoroutine(DialogueCombat());
                    }

                }
                else
                {
                    if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Brulure")
                    {
                        nbDegats = JoueurManager.Joueurs[0].pokemonSelectionner.getPv() / 16;
                        dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " brule");
                        dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " perd " + nbDegats + " pv");
                        StartCoroutine(DialogueCombat());
                    }
                    else if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Empoisonnement normal")
                    {
                        nbDegats = JoueurManager.Joueurs[0].pokemonSelectionner.getPv() / 8;
                        dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " souffre du poison");
                        dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " perd " + nbDegats + " pv");
                        StartCoroutine(DialogueCombat());
                    }
                    else if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Empoisonnement grave")
                    {
                        nbDegats = (JoueurManager.Joueurs[0].pokemonSelectionner.getPv() / 16) * (nombreTourStatut - 1);
                        dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " souffre du poison");
                        dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " perd " + nbDegats + " pv");
                        StartCoroutine(DialogueCombat());
                    }
                }

                if (gagnePvPokemonJoueur == true)
                {
                    compteur = JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant();
                    gagnePvPokemonJoueur = false;

                    rafraichirBarreViePokemonJoueur();
                }

                else if (pokemonJoueurAttaquePremier == false && nombreMouvementsBall < 0 && JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() > 0 && changement_pokemon == false && statutPokemonPerdPvJoueur == 0)
                {
                    rafraichirBarreViePokemonAdversaire();
                }

                if (pokemonJoueurAttaquePremier == false && pokemon.getPvRestant() <= 0)
                {

                }
                else if ((pokemonJoueurAttaquePremier == true && pokemon.getPvRestant() > 0 && statutPokemonPerdPvAdversaire == 0 && JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() > 0 && (pokemon.getStatutPokemon() == "Normal" || pokemon.getStatutPokemon() == "Paralysie" || pokemon.getStatutPokemon() == "Gelé" || pokemon.getStatutPokemon() == "Sommeil")) || (pokemonJoueurAttaquePremier == false && nombreMouvementsBall >= 0 && nombreMouvementsBall < 4 && JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() > 0 && (pokemon.getStatutPokemon() == "Normal" || pokemon.getStatutPokemon() == "Paralysie" || pokemon.getStatutPokemon() == "Gelé" || pokemon.getStatutPokemon() == "Sommeil")))
                {
                    try
                    {
                     //   Bitmap pngMenuCombat = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\Combat\\menu_combat.png");
                     //   pictureBoxMenuCombat.Image = pngMenuCombat;
                    }
                    catch
                    {
                     //   MessageBox.Show("L'image du menu de combat n'a pas pu être chargée. Veuillez vérifier que celle-ci est bien présente dans le répertoire.", "Vérification du menu de combat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    boutons_combat.SetActive(true);
                    boutons_combat.transform.GetChild(0).gameObject.GetComponent<Button>().Select();

                    /*

                    btn_attaque1.Enabled = true;
                    btn_soigner.Enabled = true;
                    btn_attraper.Enabled = true;
                    btn_changement_pokemon.Enabled = true;

                    btn_attaque1.Visible = true;
                    btn_soigner.Visible = true;
                    btn_changement_pokemon.Visible = true;

                    btn_attaque1.Focus(); */

                    //  MessageBox.Show("marche");

                }

                else if (pokemon.getPvRestant() > 0 && JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() <= 0)
                {
                    // timerAnimationKo.Start();

                    bool equipePokemonEncoreEnVie = false;

                    for (int i = 0; i < JoueurManager.Joueurs[0].getPokemonEquipe().Count; i++)
                    {
                        if (JoueurManager.Joueurs[0].getPokemonEquipe()[i].getPvRestant() > 0)
                        {
                            equipePokemonEncoreEnVie = true;
                        }
                    }

                    if (equipePokemonEncoreEnVie == false)
                    {
                      //  MessageBox.Show("Votre équipe à perdu");
                    }
                    else
                    {
                        btn_changement_pokemon_click();

                        /*
                        btn_attaque1.Enabled = false;
                        btn_soigner.Enabled = false;
                        btn_attraper.Enabled = false;
                        
                        panel_choix_pokemon_selection.Visible = true;

                        btn_changement_pokemon.Enabled = false;
                        btn_changement_pokemon.Focus(); */

                        changementPokemonPokemonKo = true;
                    }
                }
                if ((pokemon.getStatutPokemon() == "Brulure" || pokemon.getStatutPokemon() == "Empoisonnement normal" || pokemon.getStatutPokemon() == "Empoisonnement grave") && statutPokemonPerdPvAdversaire == 0 && statutPokemonPerdPvJoueur == 0 && pokemonJoueurAttaquePremier == true && pokemon.getPvRestant() > 0)
                {
                    statutPokemonPerdPvAdversaire = 1;
                    compteurAdversaire = pokemon.getPvRestant();

                    if (pokemon.getStatutPokemon() == "Brulure")
                    {
                        pokemon.setPvRestant(pokemon.getPvRestant() - (pokemon.getPv() / 16));
                    }
                    else if (pokemon.getStatutPokemon() == "Empoisonnement normal")
                    {
                        pokemon.setPvRestant(pokemon.getPvRestant() - (pokemon.getPv() / 8));
                    }
                    else if (pokemon.getStatutPokemon() == "Empoisonnement grave")
                    {
                        pokemon.setPvRestant(pokemon.getPvRestant() - ((pokemon.getPv() / 16) * nombreTourStatutAdversaire));
                        if (nombreTourStatutAdversaire < 16)
                        {
                            nombreTourStatutAdversaire++;
                        }
                    }
                    rafraichirBarreViePokemonAdversaire();
                }
                else if ((pokemon.getStatutPokemon() == "Normal" || pokemon.getStatutPokemon() == "Paralysie" || pokemon.getStatutPokemon() == "Gelé" || pokemon.getStatutPokemon() == "Sommeil") && (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Brulure" || JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Empoisonnement normal" || JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Empoisonnement grave") && statutPokemonPerdPvAdversaire == 0 && statutPokemonPerdPvJoueur == 0 && pokemonJoueurAttaquePremier == true && pokemon.getPvRestant() > 0)
                {
                    compteur = JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant();
                    if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Brulure")
                    {
                        JoueurManager.Joueurs[0].pokemonSelectionner.setPvRestant(JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() - (JoueurManager.Joueurs[0].pokemonSelectionner.getPv() / 16));
                    }
                    else if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Empoisonnement normal")
                    {
                        JoueurManager.Joueurs[0].pokemonSelectionner.setPvRestant(JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() - (JoueurManager.Joueurs[0].pokemonSelectionner.getPv() / 8));
                    }
                    else if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Empoisonnement grave")
                    {
                        JoueurManager.Joueurs[0].pokemonSelectionner.setPvRestant(JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() - ((JoueurManager.Joueurs[0].pokemonSelectionner.getPv() / 16) * nombreTourStatut));
                        if (nombreTourStatut < 16)
                        {
                            nombreTourStatut++;
                        }
                    }
                    statutPokemonPerdPvJoueur = 1;
                    rafraichirBarreViePokemonJoueur();
                }

                else if (statutPokemonPerdPvJoueur == 2 && (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Brulure" || JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Empoisonnement normal" || JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Empoisonnement grave"))
                {
                    statutPokemonPerdPvJoueur = 0;                  

                    if (JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() > 0 && pokemon.getPvRestant() > 0)
                    {
                        try
                        {
                            // Bitmap pngMenuCombat = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\Combat\\menu_combat.png");
                           // pictureBoxMenuCombat.Image = pngMenuCombat;
                        }
                        catch
                        {
                          //  MessageBox.Show("L'image du menu de combat n'a pas pu être chargée. Veuillez vérifier que celle-ci est bien présente dans le répertoire.", "Vérification de l'image du menu de combat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        boutons_combat.SetActive(true);
                        boutons_combat.transform.GetChild(0).gameObject.GetComponent<Button>().Select();
                        /*
                        btn_attaque1.Enabled = true;
                        btn_soigner.Enabled = true;
                        btn_attraper.Enabled = true;
                        btn_changement_pokemon.Enabled = true;

                        btn_attaque1.Visible = true;
                        btn_soigner.Visible = true;
                        btn_changement_pokemon.Visible = true;

                        btn_attaque1.Focus(); */
                    }
                }

                if (nombreMouvementsBall >= 0)
                {
                    nombreMouvementsBall = -1;
                }

            }

        }

        /// <summary>
        /// Cette méthode gère la barre de vie du pokémon adverse
        private void timerBarrePokemonAdversaire()
        {

            if (compteurAdversaire >= pokemon.getPvRestant() && compteurAdversaire >= 0 && compteurAdversaire <= pokemon.getPv())
            {

                if (compteurAdversaire >= pokemon.getPv() / 2)
                {
                    barViePokemonAdversaireImage.color = new Color32(81, 209, 39, 255);
                    barViePokemonAdversaireImage.fillAmount = (float)(pokemon.getPv() - (pokemon.getPv() - compteurAdversaire)) / pokemon.getPv() + 0.01f;
                    LabelPvPokemonAdversaireUI.text = compteurAdversaire + " / " + pokemon.getPv() + " PV";
                }
                else if (compteurAdversaire < pokemon.getPv() / 2 && compteurAdversaire >= pokemon.getPv() / 5)
                {
                    barViePokemonAdversaireImage.color = Color.yellow;
                    barViePokemonAdversaireImage.fillAmount = (float)(pokemon.getPv() - (pokemon.getPv() - compteurAdversaire)) / pokemon.getPv();
                    LabelPvPokemonAdversaireUI.text = compteurAdversaire + " / " + pokemon.getPv() + " PV";
                }
                else if (compteurAdversaire < pokemon.getPv() / 5 && compteurAdversaire > 0)
                {
                    barViePokemonAdversaireImage.color = Color.red;
                    barViePokemonAdversaireImage.fillAmount = (float)(pokemon.getPv() - (pokemon.getPv() - compteurAdversaire)) / pokemon.getPv();
                    LabelPvPokemonAdversaireUI.text = compteurAdversaire + " / " + pokemon.getPv() + " PV";
                }
                else
                {

                    if (pokemon.getPv() - (pokemon.getPv() - compteurAdversaire) > 0)
                    {
                        barViePokemonAdversaireImage.fillAmount = (float)(pokemon.getPv() - (pokemon.getPv() - compteurAdversaire)) / pokemon.getPv();
                        LabelPvPokemonAdversaireUI.text = compteurAdversaire + " / " + pokemon.getPv() + " PV";
                    }
                }

                compteurAdversaire--;
            }
            else
            {
                //  barViePokemonAdversaireImage.fillAmount = (float) (pokemon.getPv() - (pokemon.getPv() - compteurAdversaire)) / pokemon.getPv();
                //   LabelPvPokemonAdversaireUI.text = compteurAdversaire + " / " + pokemon.getPv() + " PV";

                StopCoroutine("timerBarreAdversaireStart");

                if (statutPokemonPerdPvAdversaire != 2)
                {
                    if ((JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() != "Paralysie" && JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() != "Gelé" && JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() != "Sommeil") || (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Paralysie" && reussiteAttaqueParalyse == true) || (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Gelé" && reussiteAttaqueGel == true))
                    {
                        dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " a fait " + nbDegats + " dégâts ");
                        StartCoroutine(DialogueCombat());
                    }
                }
                else
                {
                    if (pokemon.getStatutPokemon() == "Brulure")
                    {
                        nbDegats = pokemon.getPv() / 16;
                        dialogueCombat.getDialogue().AddSentence(pokemon.getNom() + " adverse brule");
                        dialogueCombat.getDialogue().AddSentence(pokemon.getNom() + " adverse perd " + nbDegats + " pv");
                        StartCoroutine(DialogueCombat());
                    }
                    else if (pokemon.getStatutPokemon() == "Empoisonnement normal")
                    {
                        nbDegats = pokemon.getPv() / 8;
                        dialogueCombat.getDialogue().AddSentence(pokemon.getNom() + " adverse souffre du poison");
                        dialogueCombat.getDialogue().AddSentence(pokemon.getNom() + " adverse perd " + nbDegats + " pv");
                        StartCoroutine(DialogueCombat());
                    }
                    else if (pokemon.getStatutPokemon() == "Empoisonnement grave")
                    {
                        nbDegats = (pokemon.getPv() / 16) * (nombreTourStatutAdversaire - 1);
                        dialogueCombat.getDialogue().AddSentence(pokemon.getNom() + " adverse souffre du poison");
                        dialogueCombat.getDialogue().AddSentence(pokemon.getNom() + " adverse perd " + nbDegats + " pv");
                        StartCoroutine(DialogueCombat());
                    }

                }

                if (pokemon.getPvRestant() <= 0)
                {
                    barViePokemonAdversaireImage.fillAmount = 0f;
                    LabelPvPokemonAdversaireUI.text = "K.O.";

                    dialogueCombat.getDialogue().AddSentence(pokemon.getNom() + " sauvage est K.O.");
                    StartCoroutine(DialogueCombat());

                    //  timerAnimationAdversaireKo.Start();

                    nombreTourStatutAdversaire = 0;
                    nombreTourStatutAEffectuerAdversaire = 0;
                    nombreTourSommeilAdversaire = 0;

                    boutons_combat.SetActive(false);
                   // btn_attaque1.Visible = false;
                   // pictureBoxMenuCombat.Visible = false;


                    if (pokemon.getGainEvPv() > 0)
                    {
                        JoueurManager.Joueurs[0].pokemonSelectionner.setEvPv(JoueurManager.Joueurs[0].pokemonSelectionner.getEvPv() + pokemon.getGainEvPv());
                    }
                    if (pokemon.getGainEvAttaque() > 0)
                    {
                        JoueurManager.Joueurs[0].pokemonSelectionner.setEvAttaque(JoueurManager.Joueurs[0].pokemonSelectionner.getEvAttaque() + pokemon.getGainEvAttaque());
                    }
                    if (pokemon.getGainEvDefense() > 0)
                    {
                        JoueurManager.Joueurs[0].pokemonSelectionner.setEvDefense(JoueurManager.Joueurs[0].pokemonSelectionner.getEvDefense() + pokemon.getGainEvDefense());
                    }
                    if (pokemon.getGainEvVitesse() > 0)
                    {
                        JoueurManager.Joueurs[0].pokemonSelectionner.setEvVitesse(JoueurManager.Joueurs[0].pokemonSelectionner.getEvVitesse() + pokemon.getGainEvVitesse());
                    }
                    if (pokemon.getGainEvAttaqueSpeciale() > 0)
                    {
                        JoueurManager.Joueurs[0].pokemonSelectionner.setEvAttaqueSpeciale(JoueurManager.Joueurs[0].pokemonSelectionner.getEvAttaqueSpeciale() + pokemon.getGainEvAttaqueSpeciale());
                    }
                    if (pokemon.getGainEvDefenseSpeciale() > 0)
                    {
                        JoueurManager.Joueurs[0].pokemonSelectionner.setEvDefenseSpeciale(JoueurManager.Joueurs[0].pokemonSelectionner.getEvDefenseSpeciale() + pokemon.getGainEvDefenseSpeciale());
                    }

                    compteurExperience = (100 * (JoueurManager.Joueurs[0].pokemonSelectionner.getExperience() - JoueurManager.Joueurs[0].pokemonSelectionner.getExperiencePokemonReturn())) / (JoueurManager.Joueurs[0].pokemonSelectionner.getExperiencePokemonProchainNiveau() - JoueurManager.Joueurs[0].pokemonSelectionner.getExperiencePokemonReturn());
                    double experienceGagner = JoueurManager.Joueurs[0].pokemonSelectionner.gainExperiencePokemonBattu(pokemon);

                    dialogueCombat.getDialogue().AddSentence(JoueurManager.Joueurs[0].pokemonSelectionner.getNom() + " a obtenu " + experienceGagner + " points d'expérience");
                    StartCoroutine(DialogueCombat());

                    statutPokemonPerdPvAdversaire = 0;

                    rafraichirBarreExperiencePokemonJoueur();

                    changementPokemonAdversaire();

                    /*
                    if (EnCombatContre == "PokemonDresseur")
                    {
                    
                    } 
                    */
                  //  combat_btn.Focus();

                }
                if (pokemonJoueurAttaquePremier == true && statutPokemonPerdPvAdversaire == 0)
                {
                    rafraichirBarreViePokemonJoueur();
                }
                else if (pokemonJoueurAttaquePremier == false && pokemon.getPvRestant() > 0 && statutPokemonPerdPvAdversaire == 0 && (pokemon.getStatutPokemon() == "Normal" || pokemon.getStatutPokemon() == "Paralysie" || pokemon.getStatutPokemon() == "Gelé" || pokemon.getStatutPokemon() == "Sommeil") && (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Normal" || JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Paralysie" || JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Gelé" || JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Sommeil"))
                {
                    try
                    {
                       // Bitmap pngMenuCombat = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\Combat\\menu_combat.png");
                      //  pictureBoxMenuCombat.Image = pngMenuCombat;
                    }
                    catch
                    {
                      //  MessageBox.Show("L'image du menu de combat n'a pas pu être chargée. Veuillez vérifier que celle-ci est bien présente dans le répertoire.", "Vérification de l'image du menu de combat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    boutons_combat.SetActive(true);
                    boutons_combat.transform.GetChild(0).gameObject.GetComponent<Button>().Select();
                    /*
                    btn_attaque1.Enabled = true;
                    btn_soigner.Enabled = true;
                    btn_attraper.Enabled = true;
                    btn_changement_pokemon.Enabled = true;

                    btn_attaque1.Visible = true;
                    btn_soigner.Visible = true;
                    btn_changement_pokemon.Visible = true;

                    btn_attaque1.Focus(); 
                    */

                }
                else if (pokemonJoueurAttaquePremier == false && statutPokemonPerdPvAdversaire == 0 && (pokemon.getStatutPokemon() == "Brulure" || pokemon.getStatutPokemon() == "Empoisonnement normal" || pokemon.getStatutPokemon() == "Empoisonnement grave") && pokemon.getPvRestant() > 0)
                {
                    statutPokemonPerdPvAdversaire = 1;
                    compteurAdversaire = pokemon.getPvRestant();

                    if (pokemon.getStatutPokemon() == "Brulure")
                    {
                        pokemon.setPvRestant(pokemon.getPvRestant() - (pokemon.getPv() / 16));
                    }
                    else if (pokemon.getStatutPokemon() == "Empoisonnement normal")
                    {
                        pokemon.setPvRestant(pokemon.getPvRestant() - (pokemon.getPv() / 8));
                    }
                    else if (pokemon.getStatutPokemon() == "Empoisonnement grave")
                    {
                        pokemon.setPvRestant(pokemon.getPvRestant() - ((pokemon.getPv() / 16) * nombreTourStatutAdversaire));
                        if (nombreTourStatutAdversaire < 16)
                        {
                            nombreTourStatutAdversaire++;
                        }
                    }

                    rafraichirBarreViePokemonAdversaire();
                }
                else if (pokemonJoueurAttaquePremier == false && statutPokemonPerdPvAdversaire == 0 && (pokemon.getStatutPokemon() == "Normal" || pokemon.getStatutPokemon() == "Paralysie" || pokemon.getStatutPokemon() == "Gelé" || pokemon.getStatutPokemon() == "Sommeil") && (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Brulure" || JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Empoisonnement normal" || JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Empoisonnement grave") && pokemon.getPvRestant() > 0)
                {
                    compteur = JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant();
                    if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Brulure")
                    {
                        JoueurManager.Joueurs[0].pokemonSelectionner.setPvRestant(JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() - (JoueurManager.Joueurs[0].pokemonSelectionner.getPv() / 16));
                    }
                    else if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Empoisonnement normal")
                    {
                        JoueurManager.Joueurs[0].pokemonSelectionner.setPvRestant(JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() - (JoueurManager.Joueurs[0].pokemonSelectionner.getPv() / 8));
                    }
                    else if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Empoisonnement grave")
                    {
                        JoueurManager.Joueurs[0].pokemonSelectionner.setPvRestant(JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() - ((JoueurManager.Joueurs[0].pokemonSelectionner.getPv() / 16) * nombreTourStatut));
                        if (nombreTourStatut < 16)
                        {
                            nombreTourStatut++;
                        }
                    }
                    statutPokemonPerdPvJoueur = 1;
                    rafraichirBarreViePokemonJoueur();
                }
                else if (statutPokemonPerdPvAdversaire == 2)
                {
                    statutPokemonPerdPvAdversaire = 0;

                    if (pokemon.getPvRestant() > 0)
                    {
                        if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Normal" || JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Paralysie" || JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Gelé" || JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Sommeil")
                        {
                            try
                            {
                                // Bitmap pngMenuCombat = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\Combat\\menu_combat.png");
                               // pictureBoxMenuCombat.Image = pngMenuCombat;
                            }
                            catch
                            {
                               // MessageBox.Show("L'image du menu de combat n'a pas pu être chargée. Veuillez vérifier que celle-ci est bien présente dans le répertoire.", "Vérification de l'image du menu de combat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            boutons_combat.SetActive(true);
                            boutons_combat.transform.GetChild(0).gameObject.GetComponent<Button>().Select();

                            /*
                            btn_attaque1.Enabled = true;
                            btn_soigner.Enabled = true;
                            btn_attraper.Enabled = true;
                            btn_changement_pokemon.Enabled = true;

                            btn_attaque1.Visible = true;
                            btn_soigner.Visible = true;
                            btn_changement_pokemon.Visible = true;

                            btn_attaque1.Focus(); */
                        }
                        else
                        {
                            compteur = JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant();
                            if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Brulure")
                            {
                                JoueurManager.Joueurs[0].pokemonSelectionner.setPvRestant(JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() - (JoueurManager.Joueurs[0].pokemonSelectionner.getPv() / 16));
                            }
                            else if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Empoisonnement normal")
                            {
                                JoueurManager.Joueurs[0].pokemonSelectionner.setPvRestant(JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() - (JoueurManager.Joueurs[0].pokemonSelectionner.getPv() / 8));
                            }
                            else if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Empoisonnement grave")
                            {
                                JoueurManager.Joueurs[0].pokemonSelectionner.setPvRestant(JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() - ((JoueurManager.Joueurs[0].pokemonSelectionner.getPv() / 16) * nombreTourStatut));
                                if (nombreTourStatut < 16)
                                {
                                    nombreTourStatut++;
                                }
                            }
                            statutPokemonPerdPvJoueur = 1;
                            rafraichirBarreViePokemonJoueur();
                        }
                    }

                }

                if (pokemonJoueurAttaquePremier == false && statutPokemonPerdPvJoueur == 1 && (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Brulure" || JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Empoisonnement normal" || JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Empoisonnement grave") && JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() > 0)
                {
                    compteur = JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant();

                    if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Brulure")
                    {
                        JoueurManager.Joueurs[0].pokemonSelectionner.setPvRestant(JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() - (JoueurManager.Joueurs[0].pokemonSelectionner.getPv() / 16));
                    }
                    else if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Empoisonnement normal")
                    {
                        JoueurManager.Joueurs[0].pokemonSelectionner.setPvRestant(JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() - (JoueurManager.Joueurs[0].pokemonSelectionner.getPv() / 8));
                    }
                    else if (JoueurManager.Joueurs[0].pokemonSelectionner.getStatutPokemon() == "Empoisonnement grave")
                    {
                        JoueurManager.Joueurs[0].pokemonSelectionner.setPvRestant(JoueurManager.Joueurs[0].pokemonSelectionner.getPvRestant() - ((JoueurManager.Joueurs[0].pokemonSelectionner.getPv() / 16) * nombreTourStatut));
                        if (nombreTourStatut < 16)
                        {
                            nombreTourStatut++;
                        }
                    }

                    rafraichirBarreViePokemonJoueur();
                }
            }
        }
    }
}
