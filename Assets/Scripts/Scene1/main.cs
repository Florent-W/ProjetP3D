using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Runtime.Serialization;
using Doozy.Engine.UI;
using System.Xml;
using System.Xml.Serialization;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProjetP3DScene1
{
    public class main : MonoBehaviour
    {
        public GameObject pokeballInstance;
        public Transform destinationPokeball;
        public Animator animator;
        public string EnCombatContre = null;
        public bool EnCombat = false, jeuInitialiser = false;
        public ClassLibrary.Personnage dresseurAdverse;
        public GameObject pokemonAdverseGameObject;

        public ClassLibrary.Jeu jeu = new ClassLibrary.Jeu(); // Parametres du jeu (attaque, pokemon)
        ClassLibrary.Personnage Joueur = new ClassLibrary.Personnage();
        ClassLibrary.Pokemon pokemon = new ClassLibrary.Pokemon();

        ClassLibrary.Pokemon pokemonStarter = new ClassLibrary.Pokemon();
        ClassLibrary.Pokemon pokemonJoueurSelectionner = new ClassLibrary.Pokemon();

        ClassLibrary.Attaque attaqueLancerPokemon = new ClassLibrary.Attaque();
        ClassLibrary.Attaque attaqueLancerAdversaire = new ClassLibrary.Attaque();

        bool jeuEnPause = false, starterChoisi = false,  gagnePvPokemonJoueur = false, pokemonJoueurAttaquePremier = true, changement_pokemon = false, changementPokemonPokemonKo = false, changementStatutPokemon = false, changementStatutAdversaire = false, showStatistiquesAugmenter = false, showStatistiquesNiveauSuivant = false, reussiteAttaque = false, reussiteAttaqueParalyse = false, reussiteAttaqueParalyseAdversaire = false, reussiteAttaqueGel = false, reussiteAttaqueGelAdversaire = false, messageBoitePCActif = false, OuverturePC = false;
        int compteur = 0, compteurAdversaire = 0, compteurExperience = 0, positionPokemonMenuPokemon = 0, nbDegats = 0, nombreMouvementsBall = -1, statutPokemonPerdPvJoueur = 0, statutPokemonPerdPvAdversaire = 0, nombreTourStatut = 0, nombreTourStatutAdversaire = 0, nombreTourStatutAEffectuer = 0, nombreTourStatutAEffectuerAdversaire = 0, nombreTourSommeil = 0, nombreTourSommeilAdversaire = 0;
        string MenuPauseActuel;
        double bonusCritique = 1;

        GameObject[] textNomPokemonMenu = new GameObject[6], pokemonMenu = new GameObject[6], numeroPokemonMenu = new GameObject[6], pvPokemonMenu = new GameObject[6], imagePokemonMenu = new GameObject[6], boutonStatistiquesPokemonMenu = new GameObject[6], boutonChoisirPokemonMenu = new GameObject[6];
        GameObject boutons_attaque, boutons_combat, scroll_objets, listeObjetsContent, menu, menuPokemonStatistiques, menuStart, BoiteDialogue, barViePokemonJoueur, barViePokemonAdversaire, barExperiencePokemonJoueur, UICombat, UIJoueur, UIAdversaire, StatistiquesChangementNiveau, TableStarter, BoitePC, PokeballBulbizarre, PokeballSalameche, PokeballCarapuce, GameObjectJoueurCamera, GameObjectJoueur, pokemonJoueurGameObject, menuPC, cameraCombat, GameObjectMusique, DialogueManagerGameObject, ObjetProcheGameObject, sceneBuilder;
        Image[] imagePokemonMenuImage = new Image[6];
        Image barViePokemonJoueurImage, barViePokemonAdversaireImage, barExperiencePokemonJoueurImage;
        DialogueTrigger dialogueCombat;
        Text LabelPvPokemonJoueurUI, LabelPvPokemonAdversaireUI, LabelNiveauPokemonJoueurUI, BoiteDialogueTexte, LabelPvChangementNiveau, LabelAttaqueChangementNiveau, LabelDefenseChangementNiveau, LabelVitesseChangementNiveau, LabelAttaqueSpecialeChangementNiveau, LabelDefenseSpecialeChangementNiveau, LabelObjetProche;
        Text[] pvPokemonMenuTexte = new Text[6];
        int[] positionPokemonMenu = new int[6];
        Button[] boutonStatistiquesPokemonMenuBouton = new Button[6], textNomPokemonMenuBouton = new Button[6];
        Button[] boutonChoisirPokemonMenuBouton2 = new Button[6], boutonChoisirPokemonMenuBouton = new Button[6];
        Animator BoiteDialogueAnimator;
        PlayableDirector TimelineAnimationLancerPokeball, TimelineCameraCombat;
        DirectoryInfo Chemin;
        FileInfo[] FichiersMusiqueCombat;

        /// <summary>
        /// Cette méthode permet de créer et de récupérer un GameObject Texte
        /// </summary>
        /// <returns>Récupère un GameObject de texte</returns>
        GameObject CreateText(Transform canvas_transform, string nameGameObject, float width, float height, float x, float y, string text_to_print, int font_size, int min_font_size, int max_size_font, Color text_color)
        {
            GameObject UItextGO = new GameObject(nameGameObject);
            UItextGO.transform.SetParent(canvas_transform);

            RectTransform transform = UItextGO.AddComponent<RectTransform>();
            transform.sizeDelta = new Vector2(width, height);
            transform.anchoredPosition = new Vector2(x, y);
            transform.localScale = new Vector3(1, 1, 1);

            Text text = UItextGO.AddComponent<Text>();
            text.text = text_to_print;

            if (min_font_size > 0 || max_size_font > 0)
            {
                text.resizeTextForBestFit = true;
                text.resizeTextMinSize = min_font_size;
                text.resizeTextMaxSize = max_size_font;
            }
            else
            {
                text.fontSize = font_size;
            }

            text.color = text_color;    
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

            return UItextGO;
        }

        /// <summary>
        /// Cette méthode permet de créer et de récupérer un GameObject de Bouton
        /// </summary>
        /// <returns>Récupère un GameObject de bouton</returns>
        GameObject CreateButton(Transform canvas_transform, string nameGameObject, int bouton_width, int bouton_height, string text_to_print, int font_size)
        {
            DefaultControls.Resources uiResources = new DefaultControls.Resources();

            GameObject uiButtonGameObject = DefaultControls.CreateButton(uiResources);
            uiButtonGameObject.name = nameGameObject;
            uiButtonGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(bouton_width, bouton_height);
            Text textUiButton = uiButtonGameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
            textUiButton.text = text_to_print;
            textUiButton.fontSize = font_size;
            uiButtonGameObject.transform.SetParent(canvas_transform.transform, false);

            return uiButtonGameObject;
        }

        GameObject CreateButton(Transform canvas_transform, string nameGameObject, int bouton_width, int bouton_height, string text_to_print, int font_size, int x, int y)
        {
            DefaultControls.Resources uiResources = new DefaultControls.Resources();

            GameObject uiButtonGameObject = DefaultControls.CreateButton(uiResources);
            uiButtonGameObject.name = nameGameObject;
            uiButtonGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(bouton_width, bouton_height);
            uiButtonGameObject.transform.localPosition = new Vector2(x, y);
            Text textUiButton = uiButtonGameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
            textUiButton.text = text_to_print;
            textUiButton.fontSize = font_size;
            uiButtonGameObject.transform.SetParent(canvas_transform.transform, false);

            return uiButtonGameObject;
        }

        public void Sauvegarde_Click()
        {
            // SaveFileDialog saveFileDialog = new SaveFileDialog();

           // if (saveFileDialog.ShowDialog() == DialogResult.OK)
           // {
               // var cheminFichier = saveFileDialog.FileName;
                string cheminFichier = Application.streamingAssetsPath + "/Joueur.xml";
                XmlWriter writer = XmlWriter.Create(cheminFichier);

                DataContractSerializer serializer = new DataContractSerializer(typeof(ClassLibrary.Personnage));

                try
                {
                    serializer.WriteObject(writer, Joueur);
                }
                catch
                {
                  //  MessageBox.Show("Impossible de serialiser : " + Environment.NewLine + erreur);
                }

                writer.Close();
            // }
        }

        public void Chargement_click()
        {
            // OpenFileDialog openFileDialog = new OpenFileDialog();

            //   if (openFileDialog.ShowDialog() == DialogResult.OK)
            //  {
            //   var cheminFichier = openFileDialog.FileName;

                string cheminFichier = Application.streamingAssetsPath + "/Joueur.xml";
                DataContractSerializer serializer = new DataContractSerializer(typeof(ClassLibrary.Personnage));
                FileStream fs = new FileStream(cheminFichier, FileMode.Open);

                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());

                try
                {
                 //   nombrePokemonAvantChargementSauvegarde = Joueur.getPokemonEquipe().Count;
                    Joueur = (ClassLibrary.Personnage)serializer.ReadObject(reader);
                   // rafraichirApresChargementSauvegarde();

                }
                catch
                {
                  //  MessageBox.Show("Impossible de deserialiser : " + erreur);
                }
                reader.Close();


           }

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
        /// Cette méthode permet d'enlever la pause
        /// </summary>
        void Resume()
        {
            //   jeu.getUIPopUpMenu().Hide();
            Time.timeScale = 1f;
           // Cursor.visible = false;
           // Cursor.lockState = CursorLockMode.Locked;
            menuStart.SetActive(false);
        }

        /// <summary>
        /// Cette méthode permet d'activer la pause et le menu
        /// </summary>
        public void Pause()
        {
            rafraichirEquipe();
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            menuStart.SetActive(true);
            EventSystem eventSystem = GameObject.Find("EventSystem").gameObject.GetComponent<EventSystem>();
            eventSystem.SetSelectedGameObject(null);
            eventSystem.SetSelectedGameObject(menuStart.transform.GetChild(0).gameObject);
            MenuPauseActuel = "MenuStart";
        }

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

            StartCoroutine("timerBarreExperienceStart");
        }

        public void DeclenchementCombat()
        {
            if (Joueur.getPokemonEquipe().Count > 0 && EnCombat == false)
            {
                EnCombat = true;

                if (EnCombatContre != "PokemonSauvage")
                {
                    EnCombatContre = "PokemonDresseur";
                }

                pokemonJoueurGameObject = GameObject.Find("PokemonJoueur");

                if (GameObjectJoueur != null)
                {
                    Destroy(pokemonJoueurGameObject);
                }

                Destroy(GameObject.FindGameObjectWithTag("PokemonAdverse"));

                Vector3 destinationPositionPokeball = new Vector3(destinationPokeball.transform.position.x, destinationPokeball.transform.position.y, destinationPokeball.transform.position.z - 0.5f);
                GameObject pokeball = (GameObject)Instantiate(Resources.Load("Models/Pokeballs/Pokeball"), destinationPositionPokeball, destinationPokeball.rotation, GameObjectJoueur.transform);
                //  GameObject pokeball = Instantiate(pokeballInstance, destinationPositionPokeball, destinationPokeball.rotation);
                Animator animatorPokeball = pokeball.GetComponent<Animator>();
                SignalReceiver signalPokeball = pokeball.GetComponent<SignalReceiver>();

                int idPokedex = pokemonJoueurSelectionner.getNoIdPokedex();
                pokeball.GetComponent<AfterThrowingPokeball>().NumeroPokedexPokemon = idPokedex;

                int NumeroMusiqueCombatChoisi = Random.Range(0, FichiersMusiqueCombat.Length);
                string NomMusiqueCombatChoisi = Path.GetFileNameWithoutExtension(FichiersMusiqueCombat[NumeroMusiqueCombatChoisi].Name);

                AudioSource GameObjectMusiqueAudioSource = GameObjectMusique.GetComponent<AudioSource>();

                GameObjectMusiqueAudioSource.clip = Resources.Load<AudioClip>("Musics/" + NomMusiqueCombatChoisi);
                GameObjectMusiqueAudioSource.Play();

                foreach (Transform child in UIJoueur.transform.GetChild(7).gameObject.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

                foreach (Transform child in UIAdversaire.transform.GetChild(5).gameObject.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

                for (int i = 0; i < Joueur.getPokemonEquipe().Count; i++)
                {
                    try
                    {
                        Vector3 destinationPokeball = new Vector3(-350 + (90 * i), -40, 0);
                        Quaternion rotationPokeball = Quaternion.Euler(0, 0, 0);
                        int nombrePokemon = i + 1;

                        GameObject pokeballMenu = new GameObject("Pokeball" + nombrePokemon + "Menu");
                        pokeballMenu.AddComponent<Image>();
                        pokeballMenu.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Pokeball/pokeball_3");

                        pokeballMenu.transform.SetParent(UIJoueur.transform.GetChild(7).gameObject.transform);
                        pokeballMenu.transform.localPosition = destinationPokeball;
                        pokeballMenu.transform.rotation = rotationPokeball;
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
                        Quaternion rotationPokeball = Quaternion.Euler(0, 0, 0);
                        int nombrePokemon = 0 + 1;

                        GameObject pokeballMenu = new GameObject("Pokeball" + nombrePokemon + "Menu");
                        pokeballMenu.AddComponent<Image>();
                        pokeballMenu.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Pokeball/pokeball_3");

                        pokeballMenu.transform.SetParent(UIAdversaire.transform.GetChild(5).gameObject.transform);
                        pokeballMenu.transform.localPosition = destinationPokeball;
                        pokeballMenu.transform.rotation = rotationPokeball;
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
                            Quaternion rotationPokeball = Quaternion.Euler(0, 0, 0);
                            int nombrePokemon = i + 1;

                            GameObject pokeballMenu = new GameObject("Pokeball" + nombrePokemon + "Menu");
                            pokeballMenu.AddComponent<Image>();
                            pokeballMenu.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Pokeball/pokeball_3");

                            pokeballMenu.transform.SetParent(UIAdversaire.transform.GetChild(5).gameObject.transform);
                            pokeballMenu.transform.localPosition = destinationPokeball;
                            pokeballMenu.transform.rotation = rotationPokeball;
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

                TimelineAnimationLancerPokeball.SetGenericBinding(trackAsset, animatorPokeball);
                TimelineAnimationLancerPokeball.SetGenericBinding(trackAsset2, signalPokeball);

                if (TimelineAnimationLancerPokeball.state == PlayState.Playing)
                {
                    TimelineAnimationLancerPokeball.Stop();
                }

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
                    int.TryParse(GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().pokemonAdverseGameObject.name, out idPokedex);
                    pokemon = pokemon.setChercherPokemonParNoId(idPokedex, jeu);
                }

                pokemon.setPvRestant(pokemon.getPv());
                GameObject spawnPokemonGameObject = GameObject.Find("SpawnPokemon");
                idPokedex = pokemon.getNoIdPokedex();

                pokeball.GetComponent<AfterThrowingPokeball>().NumeroPokedexPokemonAdverse = idPokedex;

                rafraichirBarreViePokemonAdversaire2();
                UIJoueur.SetActive(true);
                UIAdversaire.SetActive(true);
                BoiteDialogue.SetActive(true);

                cameraCombat.SetActive(true);
                GameObjectJoueurCamera.SetActive(false);

                StartDialogueCombat();
            }
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
          //  DialogueManagerGameObject.GetComponent<DialogueManager>().DisplayNextSentence(DialogueManagerGameObject.GetComponent<DialogueTrigger>().getDialogue());
        } */
    }

        public void changementPokemon_click(int positionPokemon)
        {
            //  textBox1.Text += pokemonJoueurSelectionner.getNom() + " a " + pokemonJoueurSelectionner.getPvRestant() + " PV" + Environment.NewLine;

            //  cb_choix_objets.Visible = false;
            //  btn_choix_objet.Visible = false;
            if (pokemonJoueurSelectionner == Joueur.getPokemonEquipe()[positionPokemon]) // Si le pokémon est déjà sur le terrain, il ne peut pas rentrer
            {
                dialogueCombat.getDialogue().AddSentence("Le pokemon est déjà sur le terrain");
                StartCoroutine(DialogueCombat());
            }
            else
            {

                if (Joueur.getPokemonEquipe()[positionPokemon].getPvRestant() <= 0) // Si le pokémon selectionné est K.O., il ne peut pas rentrer
                {
                    dialogueCombat.getDialogue().AddSentence("Le pokémon est K.O."); 
                    StartCoroutine(DialogueCombat());
                }

                else if (Joueur.getPokemonEquipe()[positionPokemon].getPvRestant() > 0) // Si le pokémon selectionné n'est pas K.O et qu'il n'est pas sur le terrain, il peut rentrer
                {
                    pokemonJoueurSelectionner = Joueur.getPokemonEquipe()[positionPokemon]; // Il prend la place du pokémon sur le terrain

                    int idPokedexImage = pokemonJoueurSelectionner.getNoIdPokedex();
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
                    compteur = pokemonJoueurSelectionner.getPvRestant();

                    changement_pokemon = true;
                    nombreTourStatut = 0;
                    nombreTourSommeil = 0;
                    nombreTourStatutAEffectuer = 0;

                    rafraichirBarreViePokemonJoueur2(); // Rafraichissement de l'interface de combat côté pokémon joueur avec les nouvelles données du pokémon entré

                    // rafraichirBarreViePokemonJoueur1();

                    // label_niveau_pokemon_combat_joueur.Text = "N. " + pokemonJoueurSelectionner.getNiveau().ToString();
                    rafraichirBarreExperiencePokemonJoueur(); // Rafraichissement de la barre d'expérience dans l'interface de combat avec l'expérience du pokémon entré

                    dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " est entré sur le terrain ");
                    StartCoroutine(DialogueCombat());

                    ClassLibrary.Attaque attaqueLancerAdversaire = pokemon.attaqueAdversaire(pokemon, pokemonJoueurSelectionner); // On sélectionne l'attaque que va faire le pokémon adverse vu que c'est son tour d'attaquer
                    attaqueCombatAdversaire(attaqueLancerAdversaire); // Et on applique l'attaque de l'adversaire
                }
            }
        }

        public void rafraichirBarreViePokemonJoueur()
    {
            if (statutPokemonPerdPvJoueur == 0)
            {
                if (gagnePvPokemonJoueur != true)
                {
                    for (int i = 0; i <= Joueur.getPokemonEquipe().Count - 1; i++)
                    {
                        if (pokemonJoueurSelectionner == Joueur.getPokemonEquipe()[i])
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
                                    attaqueLancerAdversaire = pokemon.attaqueAdversaire(pokemon, pokemonJoueurSelectionner);

                                    if (attaqueLancerAdversaire != null)
                                    {
                                        changementStatutPokemon = pokemon.getAttaqueChangementStatutPokemonAdverseReussi(attaqueLancerAdversaire);
                                        reussiteAttaque = pokemon.getReussiteAttaque(pokemonJoueurSelectionner.getProbabiliteReussiteAttaque(pokemon, pokemonJoueurSelectionner, attaqueLancerAdversaire));
                                        bonusCritique = pokemonJoueurSelectionner.getCoupCritique(pokemon.getProbabiliteCoupCritique(pokemon));
                                        nbDegats = pokemon.attaqueWithNomAttaque(pokemon, pokemonJoueurSelectionner, attaqueLancerAdversaire, bonusCritique, changementStatutPokemon, ref nombreTourStatut, ref reussiteAttaqueParalyseAdversaire, ref reussiteAttaqueGelAdversaire, ref nombreTourSommeilAdversaire);

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

                                                        if (pokemon.getEfficaciteAttaque(attaqueLancerAdversaire, pokemonJoueurSelectionner) != 1)
                                                        {
                                                            dialogueCombat.getDialogue().AddSentence(pokemon.getEfficaciteAttaqueTexte(pokemon.getEfficaciteAttaque(attaqueLancerAdversaire, pokemonJoueurSelectionner)));
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
                                                            string statutPokemon = pokemonJoueurSelectionner.getStatutPokemon();
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
                                                        dialogueCombat.getDialogue().AddSentence(pokemon.getEfficaciteAttaqueTexte(pokemon.getEfficaciteAttaque(attaqueLancerAdversaire, pokemonJoueurSelectionner)));
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

            StartCoroutine("timerBarreJoueurStart");
        }

        public void rafraichirBarreViePokemonAdversaire()
        {
            if (statutPokemonPerdPvAdversaire == 0)
            {
                if (pokemonJoueurSelectionner.getPvRestant() > 0)
                {
                    if (nombreTourSommeil > nombreTourStatutAEffectuer && pokemonJoueurSelectionner.getStatutPokemon() == "Sommeil")
                    {
                        dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " se réveille");
                        StartCoroutine(DialogueCombat());
                        pokemonJoueurSelectionner.setStatutPokemon("Normal");
                        nombreTourSommeil = 0;
                        nombreTourStatutAEffectuer = 0;
                    }

                    bool changementStatutAdversaire = pokemonJoueurSelectionner.getAttaqueChangementStatutPokemonAdverseReussi(attaqueLancerPokemon);
                    bool reussiteAttaque = pokemonJoueurSelectionner.getReussiteAttaque(pokemonJoueurSelectionner.getProbabiliteReussiteAttaque(pokemonJoueurSelectionner, pokemon, attaqueLancerPokemon));
                    double bonusCritique = pokemonJoueurSelectionner.getCoupCritique(pokemonJoueurSelectionner.getProbabiliteCoupCritique(pokemonJoueurSelectionner));
                    int nbDegats = pokemonJoueurSelectionner.attaqueWithNomAttaque(pokemonJoueurSelectionner, pokemon, attaqueLancerPokemon, bonusCritique, changementStatutAdversaire, ref nombreTourStatutAdversaire, ref reussiteAttaqueParalyse, ref reussiteAttaqueGel, ref nombreTourSommeil);

                    if (pokemonJoueurSelectionner.getStatutPokemon() != "Sommeil")
                    {
                        if (reussiteAttaque == true)
                        {
                            dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " lance " + attaqueLancerPokemon.getNom());
                          //  StartCoroutine(DialogueCombat());

                            if (pokemonJoueurSelectionner.getStatutPokemon() != "Paralysie" || (pokemonJoueurSelectionner.getStatutPokemon() == "Paralysie" && reussiteAttaqueParalyse == true))
                            {
                                if (pokemonJoueurSelectionner.getStatutPokemon() != "Gelé" || (pokemonJoueurSelectionner.getStatutPokemon() == "Gelé" && reussiteAttaqueGel == true))
                                {

                                    if (bonusCritique == 1.5)
                                    {
                                        dialogueCombat.getDialogue().AddSentence("Coup Critique");
                                       // StartCoroutine(DialogueCombat());
                                        bonusCritique = 1;
                                    }

                                    if (pokemonJoueurSelectionner.getEfficaciteAttaque(attaqueLancerPokemon, pokemon) != 1)
                                    {
                                        dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getEfficaciteAttaqueTexte(pokemonJoueurSelectionner.getEfficaciteAttaque(attaqueLancerPokemon, pokemon)));
                                       // StartCoroutine(DialogueCombat());
                                    }

                                    if (reussiteAttaqueGel == true)
                                    {
                                        dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " redevient normal");
                                      //  StartCoroutine(DialogueCombat());
                                        pokemonJoueurSelectionner.setStatutPokemon("Normal");
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
                                    dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " est gelé. Il ne peut pas attaquer");
                                  //  StartCoroutine(DialogueCombat());
                                }
                            }
                            else
                            {
                                dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " est paralysé. Il ne peut pas attaquer");
                             //   StartCoroutine(DialogueCombat());
                            }
                        }
                        else
                        {
                            dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " rate son attaque");
                         //   StartCoroutine(DialogueCombat());
                        }

                    }
                    else
                    {
                        if (nombreTourStatutAEffectuer == 0)
                        {
                            nombreTourStatutAEffectuer = pokemonJoueurSelectionner.getNombreTourSommeilAEffectuer();
                        }

                        dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " est endormi. Il ne peut pas attaquer");
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
            StartCoroutine("timerBarreAdversaireStart");
        }

        /// <summary>
        /// Cette méthode permet de voir quel pokémon va attaquer en premier selon les différents paramètres
        /// </summary>
        public void attaqueCombat(ClassLibrary.Attaque attaqueLancerPokemonJoueur)
        {
            boutons_attaque.SetActive(false);

            ClassLibrary.Attaque attaqueLancerAdversaire = pokemon.attaqueAdversaire(pokemon, pokemonJoueurSelectionner);

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
                if (pokemonJoueurSelectionner.getStatistiquesVitesse() > pokemon.getStatistiquesVitesse())
                {
                    pokemonJoueurAttaquePremier = true;
                    attaqueCombatPokemonJoueur(attaqueLancerPokemonJoueur);
                    if (attaqueLancerAdversaire != null)
                    {
                        attaqueCombatAdversaire(attaqueLancerAdversaire);
                    }
                }
                else if (pokemonJoueurSelectionner.getStatistiquesVitesse() < pokemon.getStatistiquesVitesse())
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

        public void attaqueCombatPokemonJoueur(ClassLibrary.Attaque attaqueLancer)
        {
            if (pokemonJoueurSelectionner.getPvRestant() > 0)
            {
                attaqueLancerPokemon = attaqueLancer;
            }
        }

        public void attaqueCombatAdversaire(ClassLibrary.Attaque attaqueLancer)
        {
            if (pokemonJoueurSelectionner == Joueur.getPokemonEquipe()[0])
            {

            }

            compteur = pokemonJoueurSelectionner.getPvRestant();

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
        public void rafraichirEquipe()
        {
            for (int i = 0; i < Joueur.getPokemonEquipe().Count; i++)
            {
                pokemonMenu[i].SetActive(true); // Activation de la partie qu'occuppe les pokémon présents dans l'équipe

                int idPokedexImage = Joueur.getPokemonEquipe()[i].getNoIdPokedex();

                try
                {
                    // Chargement des sprites des pokémon
                    imagePokemonMenuImage[i].color = new Color32(255, 255, 255, 255);
                    Sprite imagePokemon = Resources.Load<Sprite>("Images/" + idPokedexImage);
                    imagePokemonMenuImage[i].sprite = imagePokemon;
                }
                catch
                {

                }

                // Chargement des noms et du nombre du pv des pokémon
                textNomPokemonMenu[i].GetComponent<Text>().text = Joueur.getPokemonEquipe()[i].getNom();
                pvPokemonMenuTexte[i].text = Joueur.getPokemonEquipe()[i].getPvRestant() + " / " + Joueur.getPokemonEquipe()[i].getPv() + " PV";

                // Si le joueur est en combat, on permet le changement de pokémon
                if (EnCombat == true && boutonChoisirPokemonMenu[i].activeSelf == false)
                {
                    boutonChoisirPokemonMenu[i].SetActive(true);
                }
                else if(EnCombat == false && boutonChoisirPokemonMenu[i].activeSelf == true)
                {
                    boutonChoisirPokemonMenu[i].SetActive(false);
                }             
            }
        }

        public void OpeningMenuPokemon()
        {
            jeu.getUIPopUpMenu();
        }

        /// <summary>
        /// Cette méthode s'active quand le joueur clique sur le bouton d'attaque, elle permet de passer au menu des différentes attaques du pokémon et va voir si il possède bien ses attaques
        /// </summary>
        public void btn_attaque_click()
        {
            if (pokemonJoueurSelectionner.getListeAttaque().Count > 0)
            {
                if (pokemonJoueurSelectionner.getAttaque1().getNom() != "default")
                {
                    // Chargement de l'interface de l'attaque et de ses propriétés
                    Text texteBoutonAttaque1 = boutons_attaque.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>();
                    texteBoutonAttaque1.text = pokemonJoueurSelectionner.getAttaque1().getNom();
                    Text textePPAttaque1 = boutons_attaque.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Text>();
                    textePPAttaque1.text = "PP " + pokemonJoueurSelectionner.getAttaque1().getPPRestant() + "/" + pokemonJoueurSelectionner.getAttaque1().getPP();

                    string typeAttaque1 = pokemonJoueurSelectionner.getAttaque1().getTypeAttaque();
                    if (typeAttaque1 != null)
                    {

                        try
                        {
                            Sprite pngAttaque1 = Resources.Load<Sprite>("Images/Combat/bouton_type_" + typeAttaque1);
                            boutons_attaque.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = pngAttaque1;
                        }
                        catch
                        {

                        }

                        try
                        {
                            Sprite pngTypeAttaque1 = Resources.Load<Sprite>("Images/Types/" + typeAttaque1);
                            boutons_attaque.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = pngTypeAttaque1;
                        }
                        catch
                        {

                        }
                    }
                }

                if (pokemonJoueurSelectionner.getListeAttaque().Count > 1)
                {
                    if (pokemonJoueurSelectionner.getAttaque2().getNom() != "default")
                    {
                        // Chargement de l'interface de l'attaque et de ses propriétés
                        Text texteBoutonAttaque2 = boutons_attaque.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>();
                        texteBoutonAttaque2.text = pokemonJoueurSelectionner.getAttaque2().getNom();
                        Text textePPAttaque2 = boutons_attaque.transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<Text>();
                        textePPAttaque2.text = "PP " + pokemonJoueurSelectionner.getAttaque2().getPPRestant() + "/" + pokemonJoueurSelectionner.getAttaque2().getPP();

                        string typeAttaque2 = pokemonJoueurSelectionner.getAttaque2().getTypeAttaque();
                        if (typeAttaque2 != null)
                        {
                            try
                            {
                                Sprite pngAttaque2 = Resources.Load<Sprite>("Images/Combat/bouton_type_" + typeAttaque2);
                                boutons_attaque.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = pngAttaque2;
                            }
                            catch
                            {

                            }

                            try
                            {
                                Sprite pngTypeAttaque2 = Resources.Load<Sprite>("Images/Types/" + typeAttaque2);
                                boutons_attaque.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = pngTypeAttaque2;
                            }
                            catch
                            {

                            }
                        }
                    }

                    if (pokemonJoueurSelectionner.getListeAttaque().Count > 2)
                    {
                        if (pokemonJoueurSelectionner.getAttaque3().getNom() != "default")
                        {
                            // Chargement de l'interface de l'attaque et de ses propriétés
                            Text texteBoutonAttaque3 = boutons_attaque.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<Text>();
                            texteBoutonAttaque3.text = pokemonJoueurSelectionner.getAttaque3().getNom();
                            Text textePPAttaque3 = boutons_attaque.transform.GetChild(2).gameObject.transform.GetChild(1).GetComponent<Text>();
                            textePPAttaque3.text = "PP " + pokemonJoueurSelectionner.getAttaque3().getPPRestant() + "/" + pokemonJoueurSelectionner.getAttaque3().getPP();

                            string typeAttaque3 = pokemonJoueurSelectionner.getAttaque3().getTypeAttaque();
                            if (typeAttaque3 != null)
                            {
                                try
                                {
                                    Sprite pngAttaque3 = Resources.Load<Sprite>("Images/Combat/bouton_type_" + typeAttaque3);
                                    boutons_attaque.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = pngAttaque3;
                                }
                                catch
                                {

                                }

                                try
                                {
                                    Sprite pngTypeAttaque3 = Resources.Load<Sprite>("Images/Types/" + typeAttaque3);
                                    boutons_attaque.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = pngTypeAttaque3;
                                }
                                catch
                                {

                                }
                            }
                        }

                        if (pokemonJoueurSelectionner.getListeAttaque().Count > 3)
                        {
                            if (pokemonJoueurSelectionner.getAttaque4().getNom() != "default")
                            {
                                // Chargement de l'interface de l'attaque et de ses propriétés
                                Text texteBoutonAttaque4 = boutons_attaque.transform.GetChild(3).gameObject.transform.GetChild(0).GetComponent<Text>();
                                texteBoutonAttaque4.text = pokemonJoueurSelectionner.getAttaque4().getNom();
                                Text textePPAttaque4 = boutons_attaque.transform.GetChild(3).gameObject.transform.GetChild(1).GetComponent<Text>();
                                textePPAttaque4.text = "PP " + pokemonJoueurSelectionner.getAttaque4().getPPRestant() + "/" + pokemonJoueurSelectionner.getAttaque4().getPP();

                                string typeAttaque4 = pokemonJoueurSelectionner.getAttaque4().getTypeAttaque();
                                if (typeAttaque4 != null)
                                {
                                    try
                                    {
                                        Sprite pngAttaque4 = Resources.Load<Sprite>("Images/Combat/bouton_type_" + typeAttaque4);
                                        boutons_attaque.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = pngAttaque4;
                                    }
                                    catch
                                    {

                                    }

                                    try
                                    {
                                        Sprite pngTypeAttaque4 = Resources.Load<Sprite>("Images/Types/" + typeAttaque4);
                                        boutons_attaque.transform.GetChild(3).gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = pngTypeAttaque4;
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                        }
                    }
                }

                boutons_combat.SetActive(false);
                boutons_attaque.SetActive(true);
                boutons_attaque.transform.GetChild(0).gameObject.GetComponent<Button>().Select();
            }
        }

        /// <summary>
        /// Cette méthode s'active quand le joueur décide d'annuler une attaque pour revenir dans le menu de combat
        public void btn_retour_menu_combat_click()
        {
            boutons_attaque.SetActive(false);
            boutons_combat.SetActive(true);
            boutons_combat.transform.GetChild(0).gameObject.GetComponent<Button>().Select();
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

        public void btn_retour_apres_menu_start_click()
        {
            Resume();
            jeuEnPause = false;
        }

        public void btn_retour_apres_menu_pokemon_click()
        {
            if (EnCombat == true)
            {
                menu.SetActive(false);
                UICombat.SetActive(true);
                boutons_combat.transform.GetChild(0).gameObject.GetComponent<Button>().Select();
            }            
            else if(EnCombat == false && jeuEnPause == true && menu.transform.GetChild(0).gameObject.activeSelf == true && menuPokemonStatistiques.activeSelf == false)
            {
                menu.SetActive(false);
                MenuPauseActuel = "MenuStart";
            }
            else if(EnCombat == false && jeuEnPause == true && menu.transform.GetChild(0).gameObject.activeSelf == false && menuPokemonStatistiques.activeSelf == true)
            {
                btn_retour_menu_pokemon_apres_menu_statistiques_click();
            }
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
            for (int i = 0; i < Joueur.getObjetsSac().Count; i++)
            {
                if (Joueur.getObjetsSac()[i].getQuantiteObjet() > 0 && Joueur.getObjetsSac()[i].getTypeObjet() == "Soin")
                {
                    GameObject boutonObjetGameObject = CreateButton(scroll_objets.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform, Joueur.getObjetsSac()[i].getNom(), 320, 160, Joueur.getObjetsSac()[i].getNom(), 38);
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
            for (int i = 0; i < Joueur.getObjetsSac().Count; i++)
            {
                if (Joueur.getObjetsSac()[i].getQuantiteObjet() > 0 && Joueur.getObjetsSac()[i].getTypeObjet() == "Capture")
                {
                    GameObject boutonObjetGameObject = CreateButton(scroll_objets.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform, Joueur.getObjetsSac()[i].getNom(), 320, 160, Joueur.getObjetsSac()[i].getNom(), 38);
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
            if (pokemonJoueurSelectionner.getStatutPokemon() == "Sommeil")
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

            dialogueCombat.getDialogue().AddSentence(Joueur.getNom() + " utilise " + boutonCliquer.name);

            StartCoroutine(DialogueCombat());

            timerAnimationCapture();
        }

        /// <summary>
        /// Cette méthode va gérer l'utilisation des objets
        public void btn_all_objets_click(Button boutonCliquer)
       {            
            if (listeObjetsContent != null)
            {
                for (int i = 0; i < Joueur.getObjetsSac().Count; i++)
                {
                    if (boutonCliquer.name == Joueur.getObjetsSac()[i].getNom())
                    {
                      //  label_nb_potion.Text = Joueur.getObjetsSac()[i].getQuantiteObjet().ToString();

                      //  label_nb_potion.Visible = true;
                      //  label_potion.Visible = true;

                        if (pokemonJoueurSelectionner.getPvRestant() != pokemonJoueurSelectionner.getPv())
                        {
                            int pvRestantAvantSoin = pokemonJoueurSelectionner.getPvRestant();
                            gagnePvPokemonJoueur = true;
                            compteur = pokemonJoueurSelectionner.getPvRestant();

                            Joueur.getObjetsSac()[i].Soin(pokemonJoueurSelectionner, Joueur.getObjetsSac()[i]); // Soin du pokémon

                            if (pokemonJoueurSelectionner.getPvRestant() + Joueur.getObjetsSac()[i].getValeurObjet() < pokemonJoueurSelectionner.getPv())
                            {
                                dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " regagne " + Joueur.getObjetsSac()[i].getValeurObjet() + " PV");
                                StartCoroutine(DialogueCombat());
                            }
                            else
                            {
                                int pvObtenu = pokemonJoueurSelectionner.getPvRestant() - pvRestantAvantSoin;
                                dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " regagne " + pvObtenu + " PV");
                                StartCoroutine(DialogueCombat());
                            }

                            dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " a " + pokemonJoueurSelectionner.getPvRestant() + " PV");
                            dialogueCombat.getDialogue().AddSentence(pokemon.getNom() + " sauvage a " + pokemon.getPvRestant() + " PV");
                            StartCoroutine(DialogueCombat());

                            rafraichirBarreViePokemonJoueur();

                            dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " a " + pokemonJoueurSelectionner.getPvRestant() + " PV");
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

        public void btn_fuite_click()
        {
            // Désactivation de l'interface de combat
            UIJoueur.SetActive(false);
            UIAdversaire.SetActive(false);
            boutons_combat.SetActive(false);
            BoiteDialogue.SetActive(false);

            GameObjectMusique.GetComponent<AudioSource>().Stop();
            GameObjectMusique.GetComponent<AudioSource>().clip = null;

            Destroy(GameObject.FindGameObjectWithTag("PokemonJoueur"));
            Destroy(GameObject.FindGameObjectWithTag("PokemonAdverse"));

            TimelineCameraCombat.Stop();

            GameObjectJoueurCamera.SetActive(true);
            cameraCombat.SetActive(false);
             
            EnCombat = false;
        }

        public void btn_changement_pokemon_click()
        {
            rafraichirEquipe();
            UICombat.SetActive(false);
            menu.SetActive(true);
            menu.transform.GetChild(2).gameObject.GetComponent<Button>().Select();
        }

        public void btn_ouvrir_menu_options_pokemon_click(int positionPokemonCliquer)
        {
            GameObject menuOptionsPokemonGameobject = menu.transform.GetChild(3).gameObject;
            menuOptionsPokemonGameobject.transform.position = new Vector3(menuOptionsPokemonGameobject.transform.position.x, menu.transform.GetChild(0).gameObject.transform.GetChild(positionPokemonCliquer).gameObject.transform.position.y, menuOptionsPokemonGameobject.transform.position.z);
            menuOptionsPokemonGameobject.SetActive(true);
            menuOptionsPokemonGameobject.transform.GetChild(1).gameObject.GetComponent<Button>().Select();
            positionPokemonMenuPokemon = positionPokemonCliquer;
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
                        rafraichirBarreViePokemonAdversaire2();
                        compteurAdversaire = dresseurAdverse.getPokemonEquipe()[i].getPvRestant();
                        pokemonEnVieTrouver = true;
                    }
                }

                if (pokemonEnVieTrouver == false)
                {
                    dialogueCombat.getDialogue().AddSentence("Vous avez gagné le combat contre " + dresseurAdverse.getNom());
                    StartCoroutine(DialogueCombat());

                    btn_fuite_click();
                }
            }
        }

        /// <summary>
        /// Cette méthode gère l'appui sur le bouton de la première attaque du pokémon
        public void btn_attaque1_click()
        {
            attaqueCombat(pokemonJoueurSelectionner.getAttaque1());
        }

        /// <summary>
        /// Cette méthode gère l'appui sur le bouton de la deuxième attaque du pokémon
        public void btn_attaque2_click()
        {
            attaqueCombat(pokemonJoueurSelectionner.getAttaque2());
        }

        /// <summary>
        /// Cette méthode gère l'appui sur le bouton de la troisième attaque du pokémon
        public void btn_attaque3_click()
        {
            attaqueCombat(pokemonJoueurSelectionner.getAttaque3());
        }

        /// <summary>
        /// Cette méthode gère l'appui sur le bouton de la quatrième attaque du pokémon
        public void btn_attaque4_click()
        {
            attaqueCombat(pokemonJoueurSelectionner.getAttaque4());
        }

        public void btn_ouvrir_menu_statistiques_click()
        {
            menu.transform.GetChild(0).gameObject.SetActive(false);
            menuPokemonStatistiques.SetActive(true); // Activation de la page des statistiques

            int idPokedexImage = Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getNoIdPokedex();

            try
            {
                Sprite imagePokemon = Resources.Load<Sprite>("Images/" + idPokedexImage);
                menuPokemonStatistiques.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = imagePokemon;
            }
            catch
            {

            }

            menuPokemonStatistiques.transform.GetChild(1).gameObject.GetComponent<Text>().text = Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getNom();
            menuPokemonStatistiques.transform.GetChild(2).gameObject.GetComponent<Text>().text = "N°" + Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getNiveau().ToString();

            if (Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getSexe() == "Feminin")
            {
                menuPokemonStatistiques.transform.GetChild(3).gameObject.GetComponent<Text>().text = "♀";
                menuPokemonStatistiques.transform.GetChild(3).gameObject.GetComponent<Text>().color = new Color32(232, 0, 255, 1);
            }
            else if (Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getSexe() == "Masculin")
            {
                menuPokemonStatistiques.transform.GetChild(3).gameObject.GetComponent<Text>().text = "♂";
                menuPokemonStatistiques.transform.GetChild(3).gameObject.GetComponent<Text>().color = Color.blue;
            }

            menuPokemonStatistiques.transform.GetChild(4).gameObject.GetComponent<Text>().text = Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getPvRestant() + " / " + Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getPv() + " PV";
            menuPokemonStatistiques.transform.GetChild(5).gameObject.GetComponent<Text>().text = "Attaque : " + Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getStatistiquesAttaque();
            menuPokemonStatistiques.transform.GetChild(6).gameObject.GetComponent<Text>().text = "Defense : " + Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getStatistiquesDefense();
            menuPokemonStatistiques.transform.GetChild(7).gameObject.GetComponent<Text>().text = "Vitesse : " + Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getStatistiquesVitesse();
            menuPokemonStatistiques.transform.GetChild(8).gameObject.GetComponent<Text>().text = "Attaque Speciale : " + Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getStatistiquesAttaqueSpeciale();
            menuPokemonStatistiques.transform.GetChild(9).gameObject.GetComponent<Text>().text = "Defense Speciale : " + Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getStatistiquesDefenseSpeciale();
            menuPokemonStatistiques.transform.GetChild(10).gameObject.GetComponent<Text>().text = "Experience : " + Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getExperience();

            menuPokemonStatistiques.transform.GetChild(11).gameObject.GetComponent<Text>().text = "EV PV : " + Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getEvPv();
            menuPokemonStatistiques.transform.GetChild(12).gameObject.GetComponent<Text>().text = "EV Attaque : " + Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getEvAttaque();
            menuPokemonStatistiques.transform.GetChild(13).gameObject.GetComponent<Text>().text = "EV Defense : " + Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getEvDefense();
            menuPokemonStatistiques.transform.GetChild(14).gameObject.GetComponent<Text>().text = "EV Vitesse : " + Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getEvVitesse();
            menuPokemonStatistiques.transform.GetChild(15).gameObject.GetComponent<Text>().text = "EV Attaque Speciale : " + Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getEvAttaqueSpeciale();
            menuPokemonStatistiques.transform.GetChild(16).gameObject.GetComponent<Text>().text = "EV Defense Speciale : " + Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getEvDefenseSpeciale();

            if (pokemonJoueurSelectionner.getListeAttaque().Count > 0)
            {
                if (pokemonJoueurSelectionner.getAttaque1().getNom() != "default")
                {
                    menuPokemonStatistiques.transform.GetChild(17).gameObject.GetComponent<Text>().text = Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getAttaque1().getNom();
                }

                if (pokemonJoueurSelectionner.getListeAttaque().Count > 1)
                {
                    if (pokemonJoueurSelectionner.getAttaque2().getNom() != "default")
                    {
                        menuPokemonStatistiques.transform.GetChild(18).gameObject.GetComponent<Text>().text = Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getAttaque2().getNom();
                    }

                    if (pokemonJoueurSelectionner.getListeAttaque().Count > 2)
                    {
                        if (pokemonJoueurSelectionner.getAttaque3().getNom() != "default")
                        {
                            menuPokemonStatistiques.transform.GetChild(19).gameObject.GetComponent<Text>().text = Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getAttaque3().getNom();
                        }

                        if (pokemonJoueurSelectionner.getListeAttaque().Count > 3)
                        {
                            if (pokemonJoueurSelectionner.getAttaque4().getNom() != "default")
                            {
                                menuPokemonStatistiques.transform.GetChild(20).gameObject.GetComponent<Text>().text = Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getAttaque4().getNom();
                            }
                        }
                    }
                }
            }

            if (Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getType() != null)
            {
                string typePokemon = Joueur.getPokemonEquipe()[positionPokemonMenuPokemon].getType();
                Sprite imagePokemon = Resources.Load<Sprite>("Images/Types/" + typePokemon);
                menuPokemonStatistiques.transform.GetChild(21).gameObject.GetComponent<Image>().sprite = imagePokemon;
                menuPokemonStatistiques.transform.GetChild(21).gameObject.SetActive(true);
            }
        }

        public void btn_ouvrir_menu_pokemon_click()
        {
            if (Joueur.getPokemonEquipe().Count > 0)
            {
                menu.SetActive(true);
                MenuPauseActuel = "MenuPokemon";
            }
        }

        public void btn_retour_menu_pokemon_apres_menu_statistiques_click()
        {
            menu.transform.GetChild(1).gameObject.SetActive(false);
            menu.transform.GetChild(0).gameObject.SetActive(true);
        }

        public void btn_retour_menu_pokemon_apres_menu_pokemon_options_click()
        {
            menu.transform.GetChild(3).gameObject.SetActive(false);
        }

        public void btn_retour_menu_pokemon_apres_menu_pc_click()
        {
            menuPC.SetActive(false);
            OuverturePC = false;
        }

        public void rafraichirBarreViePokemonJoueur2()
        {
            Text LabelNomPokemonJoueur = UIJoueur.transform.GetChild(1).gameObject.GetComponent<Text>();
            LabelPvPokemonJoueurUI = UIJoueur.transform.GetChild(2).gameObject.GetComponent<Text>();
            Text LabelSexePokemonJoueur = UIJoueur.transform.GetChild(4).gameObject.GetComponent<Text>();

            LabelNomPokemonJoueur.text = pokemonJoueurSelectionner.getNom();
            LabelPvPokemonJoueurUI.text = pokemonJoueurSelectionner.getPvRestant().ToString() + " / " + pokemonJoueurSelectionner.getPv().ToString() + " PV";
            LabelNiveauPokemonJoueurUI.text = "N. " + pokemonJoueurSelectionner.getNiveau();

            if (pokemonJoueurSelectionner.getSexe() == "Feminin")
            {
                LabelSexePokemonJoueur.text = "♀";
                LabelSexePokemonJoueur.color = new Color32(255, 130, 192, 255);
            }
            else if (pokemonJoueurSelectionner.getSexe() == "Masculin")
            {
                LabelSexePokemonJoueur.text = "♂";
                LabelSexePokemonJoueur.color = new Color32(0, 128, 255, 255);
            }

            barViePokemonJoueurImage.fillAmount = (100 * pokemonJoueurSelectionner.getPvRestant()) / pokemonJoueurSelectionner.getPv();
        }

        public void rafraichirBarreViePokemonAdversaire2()
        {
            Text LabelNomPokemonAdversaire = UIAdversaire.transform.GetChild(1).gameObject.GetComponent<Text>();
            Text LabelNiveauPokemonAdversaire = UIAdversaire.transform.GetChild(3).gameObject.GetComponent<Text>();
            Text LabelSexePokemonAdversaire = UIAdversaire.transform.GetChild(4).gameObject.GetComponent<Text>();

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

            GameObject barViePokemonAdversaire = UIAdversaire.transform.GetChild(0).transform.GetChild(2).gameObject;
            Image barImagePokemonAdversaire = barViePokemonAdversaire.GetComponent<Image>();

            barViePokemonAdversaireImage.color = new Color32(81, 209, 39, 255);
            barImagePokemonAdversaire.fillAmount = (100 * pokemon.getPvRestant()) / pokemon.getPv();
        }

        IEnumerator DialogueCombat()
        {
           // sceneBuilder.AddComponent<DialogueTrigger>();
           // DialogueTrigger dialogueCombat = sceneBuilder.GetComponent<DialogueTrigger>();

            dialogueCombat.TriggerDialogue();

           yield return new WaitUntil(() => BoiteDialogueAnimator.GetBool("IsOpen") == false);

            boutons_combat.SetActive(true);
            boutons_combat.transform.GetChild(0).gameObject.GetComponent<Button>().Select();
        }

        IEnumerator DialogueCombatStart()
        {
            // sceneBuilder.AddComponent<DialogueTrigger>();
            // DialogueTrigger dialogueCombat = sceneBuilder.GetComponent<DialogueTrigger>();

            dialogueCombat.TriggerDialogue();

            yield return new WaitUntil(() => BoiteDialogueAnimator.GetBool("IsOpen") == false);

            boutons_combat.SetActive(true);
            boutons_combat.transform.GetChild(0).gameObject.GetComponent<Button>().Select();
        }

        void StartDialogueCombat()
        {
            dialogueCombat.getDialogue().AddSentence("Un " + pokemon.getNom() + " sauvage veut se battre");
            dialogueCombat.getDialogue().AddSentence("Il a " + pokemon.getPv() + " PV");

            StartCoroutine(DialogueCombatStart());

            compteur = pokemonJoueurSelectionner.getPvRestant();
            compteurAdversaire = pokemon.getPvRestant();
        }

        // Start is called before the first frame update
        void Start()
        {
            // Initialisation des attaques, des pokémon, des starters, des objets
            Thread threadInitialisationAttaque = new Thread(jeu.initialisationAttaques);
            threadInitialisationAttaque.Start();
            Thread threadInitialisationPokemon = new Thread(jeu.initialisationPokemon);
            threadInitialisationPokemon.Start();
            jeu.initialisationPokemonStarter();
            Thread threadInitialisationDresseur = new Thread(jeu.initialisationDresseurs);
            threadInitialisationDresseur.Start();
            Thread threadInitialisationObjet = new Thread(jeu.initialisationObjets);
            threadInitialisationObjet.Start();

            Joueur = PlayButton.Joueur;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            jeuInitialiser = true;

            // Assignage des GameObjects
            boutons_attaque = GameObject.Find("Canvas").transform.GetChild(1).gameObject.transform.GetChild(2).gameObject;
            boutons_combat = GameObject.Find("Canvas").transform.GetChild(1).gameObject.transform.GetChild(5).gameObject;
            scroll_objets = GameObject.Find("Canvas").transform.GetChild(1).gameObject.transform.GetChild(4).gameObject;
            listeObjetsContent = scroll_objets.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
            menu = GameObject.Find("Canvas").transform.GetChild(0).gameObject.transform.GetChild(4).gameObject;
            menuPokemonStatistiques = menu.transform.GetChild(1).gameObject;
            menuStart = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
            UICombat = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
            UIJoueur = GameObject.Find("Canvas").transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
            UIAdversaire = GameObject.Find("Canvas").transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;
            barViePokemonJoueur = UIJoueur.transform.GetChild(0).transform.GetChild(2).gameObject;
            barViePokemonJoueurImage = barViePokemonJoueur.GetComponent<Image>();
            barExperiencePokemonJoueur = UIJoueur.transform.GetChild(5).transform.GetChild(2).gameObject;
            barExperiencePokemonJoueurImage = barExperiencePokemonJoueur.GetComponent<Image>();
            barViePokemonAdversaire = UIAdversaire.transform.GetChild(0).transform.GetChild(2).gameObject;
            barViePokemonAdversaireImage = barViePokemonAdversaire.GetComponent<Image>();
            LabelNiveauPokemonJoueurUI = UIJoueur.transform.GetChild(3).gameObject.GetComponent<Text>();
            LabelPvPokemonAdversaireUI = UIAdversaire.transform.GetChild(2).gameObject.GetComponent<Text>();
            StatistiquesChangementNiveau = UIJoueur.transform.GetChild(6).gameObject;
            TableStarter = GameObject.Find("TableStarter");
            BoitePC = GameObject.Find("BoitePC");
            menuPC = GameObject.Find("Canvas").transform.GetChild(3).gameObject;
            PokeballBulbizarre = TableStarter.transform.GetChild(0).gameObject;
            PokeballSalameche = TableStarter.transform.GetChild(1).gameObject;
            PokeballCarapuce = TableStarter.transform.GetChild(2).gameObject;
            BoiteDialogue = GameObject.Find("Canvas").transform.GetChild(1).gameObject.transform.GetChild(3).gameObject;
            BoiteDialogueTexte = BoiteDialogue.transform.GetChild(0).gameObject.GetComponent<Text>();
            BoiteDialogueAnimator = BoiteDialogue.GetComponent<Animator>();
            DialogueManagerGameObject = GameObject.Find("DialogueManager");
            GameObjectJoueur = GameObject.Find("vThirdPersonCamera").transform.GetChild(1).gameObject.transform.GetChild(4).gameObject;
            GameObjectJoueurCamera = GameObject.Find("vThirdPersonController");
            cameraCombat = GameObject.Find("CameraCombat");
            GameObjectMusique = GameObject.Find("GameObjectMusique");
            sceneBuilder = GameObject.Find("SceneBuilder");
            dialogueCombat = sceneBuilder.GetComponent<DialogueTrigger>();
            TimelineAnimationLancerPokeball = GameObject.Find("Timeline").GetComponent<PlayableDirector>();
            TimelineCameraCombat = GameObject.Find("TimelineCamera").GetComponent<PlayableDirector>();

            TimelineAnimationLancerPokeball.Stop();
            TimelineCameraCombat.Stop();

            cameraCombat.SetActive(false);

            // Construction de l'interface du menu des pokémon
            for (int i = 0; i < 6; i++)
            {
                imagePokemonMenu[i] = new GameObject();
                imagePokemonMenu[i].name = "ImagePokemon";
                imagePokemonMenu[i].gameObject.transform.SetParent(menu.transform.GetChild(0).gameObject.transform.GetChild(i));
                imagePokemonMenu[i].gameObject.transform.localPosition = new Vector3(-220, 0, 0);
                imagePokemonMenuImage[i] = imagePokemonMenu[i].AddComponent<Image>();

              //  Button imagePokemonMenuButton = imagePokemonMenu[i].AddComponent<Button>();
              //  EventTrigger imagePokemonMenuBoutonTrigger  = imagePokemonMenu[i].AddComponent<EventTrigger>();
              //  imagePokemonMenuButton.onClick.AddListener(() => { btn_all_objets_click(boutonStatistiquesPokemonMenuBouton[i]); });

                imagePokemonMenuImage[i].color = new Color32(255, 255, 255, 0);
                imagePokemonMenuImage[i].transform.Rotate(0, 180, 0);
              //  imagePokemonMenuImage[i].AddListener(() => { btn_all_objets_click(boutonStatistiquesPokemonMenuBouton[i]); });

                int numeroPokemon = i + 1;
                positionPokemonMenu[i] = i;

                textNomPokemonMenu[i] = CreateText(menu.transform.GetChild(0).gameObject.transform.GetChild(i), "NomPokemon" + numeroPokemon, 300, 100, 113, 0, "", 43, 0, 0, Color.black);
                pvPokemonMenu[i] = CreateText(menu.transform.GetChild(0).gameObject.transform.GetChild(i), "PvPokemon", 200, 100, 320, 0, "", 43, 0, 0, Color.black);
                pvPokemonMenuTexte[i] = pvPokemonMenu[i].GetComponent<Text>();
                textNomPokemonMenuBouton[i] = textNomPokemonMenu[i].AddComponent<Button>();
                //  textNomPokemonMenuBouton[i].onClick.AddListener(() => { btn_ouvrir_menu_statistiques_click(positionPokemonMenu[numeroPokemon - 1]); });
                textNomPokemonMenuBouton[i].onClick.AddListener(() => { btn_ouvrir_menu_options_pokemon_click(positionPokemonMenu[numeroPokemon - 1]); });

                boutonStatistiquesPokemonMenu[i] = CreateButton(menu.transform.GetChild(0).gameObject.transform.GetChild(i), "BoutonStatisiques", 220, 90, "Statistiques", 38);
                boutonStatistiquesPokemonMenu[i].SetActive(false);
                boutonStatistiquesPokemonMenuBouton[i] = boutonStatistiquesPokemonMenu[i].GetComponent<Button>();

                boutonChoisirPokemonMenu[i] = CreateButton(menu.transform.GetChild(0).gameObject.transform.GetChild(i), "BoutonChoixPokemon", 220, 90, "Choisir", 38, 500, 0);
                boutonChoisirPokemonMenu[i].SetActive(false);
                boutonChoisirPokemonMenuBouton[i] = boutonChoisirPokemonMenu[i].GetComponent<Button>();
                boutonChoisirPokemonMenuBouton[i].onClick.AddListener(() => { changementPokemon_click(positionPokemonMenu[numeroPokemon - 1]); });

                pokemonMenu[i] = menu.transform.GetChild(0).gameObject.transform.GetChild(i).gameObject;
                pokemonMenu[i].SetActive(false);

                // boutonStatistiquesPokemonMenuBouton[i].onClick.AddListener(() => { btn_all_objets_click(boutonStatistiquesPokemonMenuBouton[i]); });
            }

            LabelPvChangementNiveau = StatistiquesChangementNiveau.transform.GetChild(0).gameObject.GetComponent<Text>();
            LabelAttaqueChangementNiveau = StatistiquesChangementNiveau.transform.GetChild(1).gameObject.GetComponent<Text>();
            LabelDefenseChangementNiveau = StatistiquesChangementNiveau.transform.GetChild(2).gameObject.GetComponent<Text>();
            LabelVitesseChangementNiveau = StatistiquesChangementNiveau.transform.GetChild(3).gameObject.GetComponent<Text>();
            LabelAttaqueSpecialeChangementNiveau = StatistiquesChangementNiveau.transform.GetChild(4).gameObject.GetComponent<Text>();
            LabelDefenseSpecialeChangementNiveau = StatistiquesChangementNiveau.transform.GetChild(5).gameObject.GetComponent<Text>();

            ObjetProcheGameObject = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
            LabelObjetProche = ObjetProcheGameObject.GetComponent<Text>();

            Chemin = new DirectoryInfo("Assets/Resources/Musics");
            FichiersMusiqueCombat = Chemin.GetFiles("*.mp3");

            // BoiteDialogue.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => DialogueManagerGameObject.GetComponent<DialogueManager>().DisplayNextSentence(DialogueManagerGameObject.GetComponent<DialogueTrigger>().getDialogue()));

            Joueur.setObjetsSacOffline(jeu);


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
                    }
                }
                menuPC.SetActive(true);
            } 
            

            if (Input.GetKeyDown("c"))
            {
                Vector3 destinationPositionPokeball = new Vector3(destinationPokeball.transform.position.x, destinationPokeball.transform.position.y, destinationPokeball.transform.position.z - 0.5f);
                GameObject pokeball = (GameObject) Instantiate(Resources.Load("Models/Pokeballs/Pokeball"), destinationPositionPokeball, destinationPokeball.rotation, GameObjectJoueur.transform);
                Animator animatorPokeball = pokeball.GetComponent<Animator>();
                SignalReceiver signalPokeball = pokeball.GetComponent<SignalReceiver>();

                int idPokedex = pokemonJoueurSelectionner.getNoIdPokedex();
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

            if (Input.GetKeyDown("y"))
            {
                Vector3 destinationPositionPokeball = new Vector3(destinationPokeball.position.x, destinationPokeball.position.y, destinationPokeball.position.z - 0.5f);
                GameObject pokeball = Instantiate(pokeballInstance, destinationPositionPokeball, destinationPokeball.rotation);
                Animator animatorPokeball = pokeball.GetComponent<Animator>();

                int idPokedex = pokemonJoueurSelectionner.getNoIdPokedex();
                animatorPokeball.SetInteger("NumeroPokedexPokemon", idPokedex);

                animator.SetTrigger("trThrowBall");
                animator.Play("Red throw pokeball");
                animatorPokeball.Play("Ball throw");
            }

            if (Input.GetKeyDown("n"))
            {
                // Déclenchement d'un combat
                DeclenchementCombat();
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

        IEnumerator timerBarreJoueurStart()
        {
            while (true)
            {
                timerBarrePokemonJoueur();
                yield return new WaitForSeconds(0.1f);
            }
        }

        IEnumerator timerBarreAdversaireStart()
        {
            while (true)
            {
                timerBarrePokemonAdversaire();
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

        IEnumerator WaitBeforeMessageDisapear()
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

                LabelPvChangementNiveau.text = "PV :                           " + pokemonJoueurSelectionner.getPv();
                LabelAttaqueChangementNiveau.text = "Attaque :                   " + pokemonJoueurSelectionner.getStatistiquesAttaque();
                LabelDefenseChangementNiveau.text = "Défense :                  " + pokemonJoueurSelectionner.getStatistiquesDefense();
                LabelVitesseChangementNiveau.text = "Vitesse :                   " + pokemonJoueurSelectionner.getStatistiquesVitesse();
                LabelAttaqueSpecialeChangementNiveau.text = "Attaque Spéciale :    " + pokemonJoueurSelectionner.getStatistiquesAttaqueSpeciale();
                LabelDefenseSpecialeChangementNiveau.text = "Défense Spéciale :   " + pokemonJoueurSelectionner.getStatistiquesDefenseSpeciale();
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

                    string pokemonEnvoi = Joueur.attraperPokemon(pokemon);

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

                        int idPokedexImage = Joueur.getPokemonEquipe()[Joueur.getPokemonEquipe().Count - 1].getNoIdPokedex();                    
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
                    nombreMouvementsBall = 0;
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
                    rafraichirBarreViePokemonJoueur();
            }
        }

        /// <summary>
        /// Cette méthode gère la barre d'expérience du pokémon en combat
        private void timerBarreExperiencePokemonJoueur()
        {
            int pourcentageBarreExperience = (100 * (pokemonJoueurSelectionner.getExperience() - pokemonJoueurSelectionner.getExperiencePokemonReturn())) / (pokemonJoueurSelectionner.getExperiencePokemonProchainNiveau() - pokemonJoueurSelectionner.getExperiencePokemonReturn());

            if (compteurExperience <= pourcentageBarreExperience)
            {

                barExperiencePokemonJoueurImage.fillAmount = (float)(((100 * (pokemonJoueurSelectionner.getExperience() - pokemonJoueurSelectionner.getExperiencePokemonReturn())) / (pokemonJoueurSelectionner.getExperiencePokemonProchainNiveau() - pokemonJoueurSelectionner.getExperiencePokemonReturn())) - (pourcentageBarreExperience - compteurExperience)) / 100;

                compteurExperience += (pokemonJoueurSelectionner.getExperiencePokemonProchainNiveau() - pokemonJoueurSelectionner.getExperiencePokemonReturn()) / (pokemonJoueurSelectionner.getExperiencePokemonProchainNiveau() - pokemonJoueurSelectionner.getExperiencePokemonReturn());

                // Si le pourcentage d'expérience est supérieur à 100, la taille de la barre, alors cela veut dire que le pokémon à gagné un niveau ainsi que des statistiques en plus
                if (compteurExperience >= 100)
                {
                    compteurExperience = 0;

                    int nbViePokemonAvant = pokemonJoueurSelectionner.getPv();
                    int statistiquesAttaquePokemonAvant = pokemonJoueurSelectionner.getStatistiquesAttaque();
                    int statistiquesDefensePokemonAvant = pokemonJoueurSelectionner.getStatistiquesDefense();
                    int statistiquesVitessePokemonAvant = pokemonJoueurSelectionner.getStatistiquesVitesse();
                    int statistiquesAttaqueSpecialePokemonAvant = pokemonJoueurSelectionner.getStatistiquesAttaqueSpeciale();
                    int statistiquesDefenseSpecialePokemonAvant = pokemonJoueurSelectionner.getStatistiquesDefenseSpeciale();

                    pokemonJoueurSelectionner.setNiveau(pokemonJoueurSelectionner.getNiveau() + 1);

                    int nbviePokemonApres = pokemonJoueurSelectionner.getPv();
                    int statistiquesAttaquePokemonApres = pokemonJoueurSelectionner.getStatistiquesAttaque();
                    int statistiquesDefensePokemonApres = pokemonJoueurSelectionner.getStatistiquesDefense();
                    int statistiquesVitessePokemonApres = pokemonJoueurSelectionner.getStatistiquesVitesse();
                    int statistiquesAttaqueSpecialePokemonApres = pokemonJoueurSelectionner.getStatistiquesAttaqueSpeciale();
                    int statistiquesDefenseSpecialePokemonApres = pokemonJoueurSelectionner.getStatistiquesDefenseSpeciale();

                    int nbPvGagner = nbviePokemonApres - nbViePokemonAvant;
                    int statistiquesAttaqueGagner = statistiquesAttaquePokemonApres - statistiquesAttaquePokemonAvant;
                    int statistiquesDefenseGagner = statistiquesDefensePokemonApres - statistiquesDefensePokemonAvant;
                    int statistiquesVitesseGagner = statistiquesVitessePokemonApres - statistiquesVitessePokemonAvant;
                    int statistiquesAttaqueSpecialeGagner = statistiquesAttaqueSpecialePokemonApres - statistiquesAttaqueSpecialePokemonAvant;
                    int statistiquesDefenseSpecialeGagner = statistiquesDefenseSpecialePokemonApres - statistiquesDefenseSpecialePokemonAvant;

                    pokemonJoueurSelectionner.setPvRestant(pokemonJoueurSelectionner.getPvRestant() + nbPvGagner);
                    LabelNiveauPokemonJoueurUI.text = "N. " + pokemonJoueurSelectionner.getNiveau();

                    LabelPvChangementNiveau.text = "PV :                           + " + nbPvGagner;
                    LabelAttaqueChangementNiveau.text = "Attaque :                   + " + statistiquesAttaqueGagner;
                    LabelDefenseChangementNiveau.text = "Défense :                  + " + statistiquesDefenseGagner;
                    LabelVitesseChangementNiveau.text = "Vitesse :                   + " + statistiquesVitesseGagner;
                    LabelAttaqueSpecialeChangementNiveau.text = "Attaque Spéciale :    + " + statistiquesAttaqueSpecialeGagner;
                    LabelDefenseSpecialeChangementNiveau.text = "Défense Spéciale :   + " + statistiquesDefenseSpecialeGagner;

                    for (int i = 0; i <= Joueur.getPokemonEquipe().Count - 1; i++)
                    {
                        if (pokemonJoueurSelectionner == Joueur.getPokemonEquipe()[i])
                        {
                          //  label_pv_pokemon[i].Text = pokemonJoueurSelectionner.getPvRestant().ToString() + " / " + pokemonJoueurSelectionner.getPv().ToString() + " PV";
                        }
                    }
                    LabelPvPokemonJoueurUI.text = pokemonJoueurSelectionner.getPvRestant().ToString() + " / " + pokemonJoueurSelectionner.getPv().ToString() + " PV";

                    dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " passe au niveau " + pokemonJoueurSelectionner.getNiveau());
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
            //    timerBarreExperiencePokemonJoueur.Stop();

              //  if (groupBoxNiveauSuperieurStatistiques.Visible == false)
               // {
                 //   combat_btn.Enabled = true;

                  //  combat_btn.Focus();
               // }
            }

            // gBarreExperience.Dispose();



        }

        /// <summary>
        /// Cette méthode gère la vie et la barre du pokémon du joueur
        private void timerBarrePokemonJoueur()
        {

            if (compteur >= pokemonJoueurSelectionner.getPvRestant() && compteur >= 0 && compteur <= pokemonJoueurSelectionner.getPv() && gagnePvPokemonJoueur == false || compteur <= pokemonJoueurSelectionner.getPvRestant() && compteur <= pokemonJoueurSelectionner.getPv() && gagnePvPokemonJoueur == true)
            {

                if (compteur >= pokemonJoueurSelectionner.getPv() / 2)
                {
                    barViePokemonJoueurImage.color = new Color32(81, 209, 39, 255);
                    barViePokemonJoueurImage.fillAmount = (float)(pokemonJoueurSelectionner.getPv() - (pokemonJoueurSelectionner.getPv() - compteur)) / pokemonJoueurSelectionner.getPv() + 0.01f;
                    LabelPvPokemonJoueurUI.text = compteur + " / " + pokemonJoueurSelectionner.getPv() + " PV";

                }
                else if (compteur < pokemonJoueurSelectionner.getPv() / 2 && compteur >= pokemonJoueurSelectionner.getPv() / 5)
                {
                    barViePokemonJoueurImage.color = Color.yellow;
                    barViePokemonJoueurImage.fillAmount = (float)(pokemonJoueurSelectionner.getPv() - (pokemonJoueurSelectionner.getPv() - compteur)) / pokemonJoueurSelectionner.getPv();
                    LabelPvPokemonJoueurUI.text = compteur + " / " + pokemonJoueurSelectionner.getPv() + " PV";
                }
                else if (compteur < pokemonJoueurSelectionner.getPv() / 5 && compteur > 0)
                {
                    barViePokemonJoueurImage.color = Color.red;
                    barViePokemonJoueurImage.fillAmount = (float)(pokemonJoueurSelectionner.getPv() - (pokemonJoueurSelectionner.getPv() - compteur)) / pokemonJoueurSelectionner.getPv();
                    LabelPvPokemonJoueurUI.text = compteur + " / " + pokemonJoueurSelectionner.getPv() + " PV";
                }

                else
                {


                    if (pokemonJoueurSelectionner.getPv() - (pokemonJoueurSelectionner.getPv() - compteur) > 0)
                    {
                        barViePokemonJoueurImage.fillAmount = (float)(pokemonJoueurSelectionner.getPv() - (pokemonJoueurSelectionner.getPv() - compteur)) / pokemonJoueurSelectionner.getPv();
                        LabelPvPokemonJoueurUI.text = compteur + " / " + pokemonJoueurSelectionner.getPv() + " PV";


                    }
                    else
                    {
                        barViePokemonJoueurImage.fillAmount = 0f;
                        LabelPvPokemonJoueurUI.text = "K.O.";

                        dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " est K.O.");
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
                    if (pokemonJoueurSelectionner.getStatutPokemon() == "Brulure")
                    {
                        nbDegats = pokemonJoueurSelectionner.getPv() / 16;
                        dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " brule");
                        dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " perd " + nbDegats + " pv");
                        StartCoroutine(DialogueCombat());
                    }
                    else if (pokemonJoueurSelectionner.getStatutPokemon() == "Empoisonnement normal")
                    {
                        nbDegats = pokemonJoueurSelectionner.getPv() / 8;
                        dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " souffre du poison");
                        dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " perd " + nbDegats + " pv");
                        StartCoroutine(DialogueCombat());
                    }
                    else if (pokemonJoueurSelectionner.getStatutPokemon() == "Empoisonnement grave")
                    {
                        nbDegats = (pokemonJoueurSelectionner.getPv() / 16) * (nombreTourStatut - 1);
                        dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " souffre du poison");
                        dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " perd " + nbDegats + " pv");
                        StartCoroutine(DialogueCombat());
                    }
                }

                if (gagnePvPokemonJoueur == true)
                {
                    compteur = pokemonJoueurSelectionner.getPvRestant();
                    gagnePvPokemonJoueur = false;

                    rafraichirBarreViePokemonJoueur();
                }

                else if (pokemonJoueurAttaquePremier == false && nombreMouvementsBall < 0 && pokemonJoueurSelectionner.getPvRestant() > 0 && changement_pokemon == false && statutPokemonPerdPvJoueur == 0)
                {
                    rafraichirBarreViePokemonAdversaire();
                }

                if (pokemonJoueurAttaquePremier == false && pokemon.getPvRestant() <= 0)
                {

                }
                else if ((pokemonJoueurAttaquePremier == true && pokemon.getPvRestant() > 0 && statutPokemonPerdPvAdversaire == 0 && pokemonJoueurSelectionner.getPvRestant() > 0 && (pokemon.getStatutPokemon() == "Normal" || pokemon.getStatutPokemon() == "Paralysie" || pokemon.getStatutPokemon() == "Gelé" || pokemon.getStatutPokemon() == "Sommeil")) || (pokemonJoueurAttaquePremier == false && nombreMouvementsBall >= 0 && nombreMouvementsBall < 4 && pokemonJoueurSelectionner.getPvRestant() > 0 && (pokemon.getStatutPokemon() == "Normal" || pokemon.getStatutPokemon() == "Paralysie" || pokemon.getStatutPokemon() == "Gelé" || pokemon.getStatutPokemon() == "Sommeil")))
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

                else if (pokemon.getPvRestant() > 0 && pokemonJoueurSelectionner.getPvRestant() <= 0)
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
                else if ((pokemon.getStatutPokemon() == "Normal" || pokemon.getStatutPokemon() == "Paralysie" || pokemon.getStatutPokemon() == "Gelé" || pokemon.getStatutPokemon() == "Sommeil") && (pokemonJoueurSelectionner.getStatutPokemon() == "Brulure" || pokemonJoueurSelectionner.getStatutPokemon() == "Empoisonnement normal" || pokemonJoueurSelectionner.getStatutPokemon() == "Empoisonnement grave") && statutPokemonPerdPvAdversaire == 0 && statutPokemonPerdPvJoueur == 0 && pokemonJoueurAttaquePremier == true && pokemon.getPvRestant() > 0)
                {
                    compteur = pokemonJoueurSelectionner.getPvRestant();
                    if (pokemonJoueurSelectionner.getStatutPokemon() == "Brulure")
                    {
                        pokemonJoueurSelectionner.setPvRestant(pokemonJoueurSelectionner.getPvRestant() - (pokemonJoueurSelectionner.getPv() / 16));
                    }
                    else if (pokemonJoueurSelectionner.getStatutPokemon() == "Empoisonnement normal")
                    {
                        pokemonJoueurSelectionner.setPvRestant(pokemonJoueurSelectionner.getPvRestant() - (pokemonJoueurSelectionner.getPv() / 8));
                    }
                    else if (pokemonJoueurSelectionner.getStatutPokemon() == "Empoisonnement grave")
                    {
                        pokemonJoueurSelectionner.setPvRestant(pokemonJoueurSelectionner.getPvRestant() - ((pokemonJoueurSelectionner.getPv() / 16) * nombreTourStatut));
                        if (nombreTourStatut < 16)
                        {
                            nombreTourStatut++;
                        }
                    }
                    statutPokemonPerdPvJoueur = 1;
                    rafraichirBarreViePokemonJoueur();
                }

                else if (statutPokemonPerdPvJoueur == 2 && (pokemonJoueurSelectionner.getStatutPokemon() == "Brulure" || pokemonJoueurSelectionner.getStatutPokemon() == "Empoisonnement normal" || pokemonJoueurSelectionner.getStatutPokemon() == "Empoisonnement grave"))
                {
                    statutPokemonPerdPvJoueur = 0;                  

                    if (pokemonJoueurSelectionner.getPvRestant() > 0 && pokemon.getPvRestant() > 0)
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
                    if ((pokemonJoueurSelectionner.getStatutPokemon() != "Paralysie" && pokemonJoueurSelectionner.getStatutPokemon() != "Gelé" && pokemonJoueurSelectionner.getStatutPokemon() != "Sommeil") || (pokemonJoueurSelectionner.getStatutPokemon() == "Paralysie" && reussiteAttaqueParalyse == true) || (pokemonJoueurSelectionner.getStatutPokemon() == "Gelé" && reussiteAttaqueGel == true))
                    {
                        dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " a fait " + nbDegats + " dégâts ");
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
                        pokemonJoueurSelectionner.setEvPv(pokemonJoueurSelectionner.getEvPv() + pokemon.getGainEvPv());
                    }
                    if (pokemon.getGainEvAttaque() > 0)
                    {
                        pokemonJoueurSelectionner.setEvAttaque(pokemonJoueurSelectionner.getEvAttaque() + pokemon.getGainEvAttaque());
                    }
                    if (pokemon.getGainEvDefense() > 0)
                    {
                        pokemonJoueurSelectionner.setEvDefense(pokemonJoueurSelectionner.getEvDefense() + pokemon.getGainEvDefense());
                    }
                    if (pokemon.getGainEvVitesse() > 0)
                    {
                        pokemonJoueurSelectionner.setEvVitesse(pokemonJoueurSelectionner.getEvVitesse() + pokemon.getGainEvVitesse());
                    }
                    if (pokemon.getGainEvAttaqueSpeciale() > 0)
                    {
                        pokemonJoueurSelectionner.setEvAttaqueSpeciale(pokemonJoueurSelectionner.getEvAttaqueSpeciale() + pokemon.getGainEvAttaqueSpeciale());
                    }
                    if (pokemon.getGainEvDefenseSpeciale() > 0)
                    {
                        pokemonJoueurSelectionner.setEvDefenseSpeciale(pokemonJoueurSelectionner.getEvDefenseSpeciale() + pokemon.getGainEvDefenseSpeciale());
                    }

                    compteurExperience = (100 * (pokemonJoueurSelectionner.getExperience() - pokemonJoueurSelectionner.getExperiencePokemonReturn())) / (pokemonJoueurSelectionner.getExperiencePokemonProchainNiveau() - pokemonJoueurSelectionner.getExperiencePokemonReturn());
                    double experienceGagner = pokemonJoueurSelectionner.gainExperiencePokemonBattu(pokemon);

                    dialogueCombat.getDialogue().AddSentence(pokemonJoueurSelectionner.getNom() + " a obtenu " + experienceGagner + " points d'expérience");
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
                else if (pokemonJoueurAttaquePremier == false && pokemon.getPvRestant() > 0 && statutPokemonPerdPvAdversaire == 0 && (pokemon.getStatutPokemon() == "Normal" || pokemon.getStatutPokemon() == "Paralysie" || pokemon.getStatutPokemon() == "Gelé" || pokemon.getStatutPokemon() == "Sommeil") && (pokemonJoueurSelectionner.getStatutPokemon() == "Normal" || pokemonJoueurSelectionner.getStatutPokemon() == "Paralysie" || pokemonJoueurSelectionner.getStatutPokemon() == "Gelé" || pokemonJoueurSelectionner.getStatutPokemon() == "Sommeil"))
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
                else if (pokemonJoueurAttaquePremier == false && statutPokemonPerdPvAdversaire == 0 && (pokemon.getStatutPokemon() == "Normal" || pokemon.getStatutPokemon() == "Paralysie" || pokemon.getStatutPokemon() == "Gelé" || pokemon.getStatutPokemon() == "Sommeil") && (pokemonJoueurSelectionner.getStatutPokemon() == "Brulure" || pokemonJoueurSelectionner.getStatutPokemon() == "Empoisonnement normal" || pokemonJoueurSelectionner.getStatutPokemon() == "Empoisonnement grave") && pokemon.getPvRestant() > 0)
                {
                    compteur = pokemonJoueurSelectionner.getPvRestant();
                    if (pokemonJoueurSelectionner.getStatutPokemon() == "Brulure")
                    {
                        pokemonJoueurSelectionner.setPvRestant(pokemonJoueurSelectionner.getPvRestant() - (pokemonJoueurSelectionner.getPv() / 16));
                    }
                    else if (pokemonJoueurSelectionner.getStatutPokemon() == "Empoisonnement normal")
                    {
                        pokemonJoueurSelectionner.setPvRestant(pokemonJoueurSelectionner.getPvRestant() - (pokemonJoueurSelectionner.getPv() / 8));
                    }
                    else if (pokemonJoueurSelectionner.getStatutPokemon() == "Empoisonnement grave")
                    {
                        pokemonJoueurSelectionner.setPvRestant(pokemonJoueurSelectionner.getPvRestant() - ((pokemonJoueurSelectionner.getPv() / 16) * nombreTourStatut));
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
                        if (pokemonJoueurSelectionner.getStatutPokemon() == "Normal" || pokemonJoueurSelectionner.getStatutPokemon() == "Paralysie" || pokemonJoueurSelectionner.getStatutPokemon() == "Gelé" || pokemonJoueurSelectionner.getStatutPokemon() == "Sommeil")
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
                            compteur = pokemonJoueurSelectionner.getPvRestant();
                            if (pokemonJoueurSelectionner.getStatutPokemon() == "Brulure")
                            {
                                pokemonJoueurSelectionner.setPvRestant(pokemonJoueurSelectionner.getPvRestant() - (pokemonJoueurSelectionner.getPv() / 16));
                            }
                            else if (pokemonJoueurSelectionner.getStatutPokemon() == "Empoisonnement normal")
                            {
                                pokemonJoueurSelectionner.setPvRestant(pokemonJoueurSelectionner.getPvRestant() - (pokemonJoueurSelectionner.getPv() / 8));
                            }
                            else if (pokemonJoueurSelectionner.getStatutPokemon() == "Empoisonnement grave")
                            {
                                pokemonJoueurSelectionner.setPvRestant(pokemonJoueurSelectionner.getPvRestant() - ((pokemonJoueurSelectionner.getPv() / 16) * nombreTourStatut));
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

                if (pokemonJoueurAttaquePremier == false && statutPokemonPerdPvJoueur == 1 && (pokemonJoueurSelectionner.getStatutPokemon() == "Brulure" || pokemonJoueurSelectionner.getStatutPokemon() == "Empoisonnement normal" || pokemonJoueurSelectionner.getStatutPokemon() == "Empoisonnement grave") && pokemonJoueurSelectionner.getPvRestant() > 0)
                {
                    compteur = pokemonJoueurSelectionner.getPvRestant();

                    if (pokemonJoueurSelectionner.getStatutPokemon() == "Brulure")
                    {
                        pokemonJoueurSelectionner.setPvRestant(pokemonJoueurSelectionner.getPvRestant() - (pokemonJoueurSelectionner.getPv() / 16));
                    }
                    else if (pokemonJoueurSelectionner.getStatutPokemon() == "Empoisonnement normal")
                    {
                        pokemonJoueurSelectionner.setPvRestant(pokemonJoueurSelectionner.getPvRestant() - (pokemonJoueurSelectionner.getPv() / 8));
                    }
                    else if (pokemonJoueurSelectionner.getStatutPokemon() == "Empoisonnement grave")
                    {
                        pokemonJoueurSelectionner.setPvRestant(pokemonJoueurSelectionner.getPvRestant() - ((pokemonJoueurSelectionner.getPv() / 16) * nombreTourStatut));
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
