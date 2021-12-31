using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

// Menu de selection de personnage pour changer de personnages ou pour faire apparaitre des alliés qui suivront
public class PersonnagesSelection : MonoBehaviour
{
    public List<Personnage> listePersonnages = new List<Personnage>();
    public List<Personnage> listePersonnagesDresseurs = new List<Personnage>();
    public List<Personnage> listePersonnagesHeros = new List<Personnage>();
    public List<Personnage> listePersonnagesSprites = new List<Personnage>();
    public List<Personnage> listePersonnagesNintendo = new List<Personnage>();
    public List<Personnage> listePersonnagesFinalFantasy = new List<Personnage>();

    public List<GameObject> listePersonnagesAllier = new List<GameObject>(); // Liste des alliés déjà spawn

    [SerializeField]
    private PersonnagesDonnees personnagesDonnees;

    [SerializeField]
    private GameObject volumeGameObject;
    [SerializeField]
    private GameObject personnagePrefab;
    [SerializeField]
    private GameObject vThirdPersonCameraPrefab;
    [SerializeField]
    private Toggle toggleHD2D;

    [SerializeField]
    private Toggle toggleVoix;
    [SerializeField]
    private Toggle toggleSpawnAllier;

    private bool spawnAllier = false; // Si à false, le menu de selection sert à changer de personnage, à true, il fait spawn des alliés

    public GameObject personnagesSlot;
    public GameObject boutonPrefab;
    public ProjetP3DScene1.main main;

    private GameObject personnage;

    private GameObject allierGameObject;

    // Start is called before the first frame update
    void Start()
    {
        listePersonnagesDresseurs = personnagesDonnees.listePersonnagesDresseurs; // Initialisation
        listePersonnagesHeros = personnagesDonnees.listePersonnagesHeros;
        listePersonnagesSprites = personnagesDonnees.listePersonnagesSprites;
        listePersonnagesNintendo = personnagesDonnees.listePersonnagesNintendo;
        listePersonnagesFinalFantasy = personnagesDonnees.listePersonnagesFinalFantasy;

        // On charge chaque liste de personnages
        chargerListePersonnages(listePersonnagesDresseurs, "Dresseurs");
        chargerListePersonnages(listePersonnagesHeros, "Super Heros");
        chargerListePersonnages(listePersonnagesSprites, "Sprites");
        chargerListePersonnages(listePersonnagesNintendo, "Nintendo");
        chargerListePersonnages(listePersonnagesFinalFantasy, "Final Fantasy");

        chargerNavigationBoutonPersonnages();

        // On active le toggle du HD 2D si c'est un sprite au début
        if(DonneesChargement.dimensionPersonnage == "2D")
        {
            toggleHD2D.isOn = true;
        }
        // On active le toggle des voix si c'est activé au début
        if (DonneesChargement.voixAleatoireActive == true)
        {
            toggleVoix.isOn = true;
        }
        // On active le toggle des voix si c'est activé au début
        if (DonneesChargement.voixAleatoireActive == true)
        {
            toggleVoix.isOn = true;
        }

        main.canvasGameObject[0].transform.GetChild(12).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<Button>().Select(); // On selectionne le bon bouton
    }

    // Va permettre de charger la liste des personnages dans le menu à partir d'une liste de personnages indépendantes
    private void chargerListePersonnages(List<Personnage> listePersonnages, string nomCategorie)
    {
        // Parcours de la liste des personnages
        for (int i = 0; i < listePersonnages.Count; i++)
        {
            GameObject boutonPersonnages = Instantiate(boutonPrefab, personnagesSlot.transform); // On met un bouton par personnages
            if (listePersonnages[i].personnageGameObject != null) // Si ce n'est pas un sprite, on prend le nom du gameobject
            {
                boutonPersonnages.name = listePersonnages[i].personnageGameObject.name;
                boutonPersonnages.transform.GetChild(1).gameObject.GetComponent<Text>().text = listePersonnages[i].personnageGameObject.name;
            }
            else
            {
                boutonPersonnages.name = listePersonnages[i].personnageSprite.name;
                boutonPersonnages.transform.GetChild(1).gameObject.GetComponent<Text>().text = listePersonnages[i].personnageSprite.name;
            }
            boutonPersonnages.transform.GetChild(2).gameObject.GetComponent<Text>().text = nomCategorie;
            boutonPersonnages.transform.GetChild(3).gameObject.GetComponent<Text>().text = i.ToString();
            boutonPersonnages.transform.GetChild(4).gameObject.GetComponent<Image>().sprite = listePersonnages[i].personnageSprite;
        }
    }

    public void chargerNavigationBoutonPersonnages()
    {
        int nombrePersonnages = personnagesSlot.transform.childCount; // Un de plus car il y a le bouton prefab
        int nombrePersonnagesParLigne = personnagesSlot.GetComponent<GridLayoutGroup>().constraintCount; // Pour avoir le nombre de personnage par ligne, on prend le nombre dans la grille
        int nombrePersonnagesDerniereLigne = (nombrePersonnages - 1) % nombrePersonnagesParLigne; // Pour avoir le nombre de personnage sur la dernière ligne

        // Parcours de la liste des personnages pour les navigations des boutons
        for (int i = 1; i < nombrePersonnages; i++)
        {
                Navigation nav = new Navigation();
                nav.mode = Navigation.Mode.Explicit; // On change le mode

                if (i >= 2) // On selectionne la navigation vers la gauche
                {
                    nav.selectOnLeft = personnagesSlot.transform.GetChild(i - 1).gameObject.GetComponent<Button>();
                }

                if (i < nombrePersonnages - 1) // On selectionne la navigation vers la droite
                {
                    nav.selectOnRight = personnagesSlot.transform.GetChild(i + 1).gameObject.GetComponent<Button>();
                }

                if (i > nombrePersonnagesParLigne) // On selectionne la navigation vers le haut pour les personnages qui ne sont pas sur la première ligne
                {
                    nav.selectOnUp = personnagesSlot.transform.GetChild(i - nombrePersonnagesParLigne).gameObject.GetComponent<Button>();
                }
                else // Si c'est les personnages sur la première ligne, aller vers le haut amène vers la dernière ligne
                {
                    if (i + nombrePersonnages - 1 - nombrePersonnagesDerniereLigne <= nombrePersonnages - 1)
                    {
                        nav.selectOnUp = personnagesSlot.transform.GetChild(i + nombrePersonnages - 1 - nombrePersonnagesDerniereLigne).gameObject.GetComponent<Button>();
                    }
                    else // Sauf si il n'y a pas de personnage sur la dernière ligne sur la même colonne que là ou on était et on ira à l'avant dernière ligne
                    {
                      // nav.selectOnUp = personnagesSlot.transform.GetChild(i + nombrePersonnages - 2 - nombrePersonnagesDerniereLigne - nombrePersonnagesDerniereLigne).gameObject.GetComponent<Button>();
                    }
                }


            if (i < (nombrePersonnages) - (nombrePersonnagesParLigne))
            // if (i < (nombrePersonnages - 1) - nombrePersonnagesDerniereLigne) // Calcul pour ne pas que la ligne tout en bas soit prise en compte, on mettra la navigation vers le bas pour les autres boutons si ils ont un boutons en bas
            {
                    nav.selectOnDown = personnagesSlot.transform.GetChild(i + nombrePersonnagesParLigne).gameObject.GetComponent<Button>();
                }
                else // Pour les personnages du bas, appuyer en bas fait revenir en haut
                {
                    if (i > nombrePersonnages - 1 - nombrePersonnagesDerniereLigne)
                    { 
                        nav.selectOnDown = personnagesSlot.transform.GetChild(i - (nombrePersonnages - 1 - nombrePersonnagesDerniereLigne)).gameObject.GetComponent<Button>();
                    }
                    else // Sauf si il n'y a pas de personnage sur la dernière ligne, dans ce cas, on prend une case avant et on l'a fait revenir en haut
                    {
                      // nav.selectOnDown = personnagesSlot.transform.GetChild(i - (nombrePersonnages - 1 - nombrePersonnagesDerniereLigne) + nombrePersonnagesDerniereLigne + 1).gameObject.GetComponent<Button>();
                    }
                }

                personnagesSlot.transform.GetChild(i).gameObject.GetComponent<Button>().navigation = nav;
                personnagesSlot.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    // Changement de musique après avoir selectionné un autre personnage
    public void changerMusique(List<Personnage> listePersonnages, int numeroPersonnage)
    {
        AudioClip personnageMusique = listePersonnages[numeroPersonnage].personnageMusique;
        AudioSource GameObjectMusiqueAudioSource = main.GameObjectMusique.GetComponent<AudioSource>();

        if (personnageMusique != null) // Si le personnage selectionné à une musique
        {
            GameObjectMusiqueAudioSource.clip = personnageMusique;
            GameObjectMusiqueAudioSource.Play(); // On lance la musique
        }
        else
        {
            if (GameObjectMusiqueAudioSource.isPlaying) // On arrete la musique si il n'y a pas de musique associée
            {
                GameObjectMusiqueAudioSource.Stop();
                GameObjectMusiqueAudioSource.clip = null;
            }
        }
    }

    // Checkbox qui va servir à changer le profil de HD2D à 3D ou le contraire
    public void checkboxChargerProfileVolume()
    {
        if(toggleHD2D.isOn) // Si le toggle est activé, on met le profil HD2D
        {
            chargerProfileHD2D();
        }
        else
        {
            chargerProfileDefault();
        }
    }

    // Checkbox qui va servir à savoir si on active les voix
    public void checkboxActiverVoix()
    {
        if (toggleVoix.isOn) // Si le toggle est activé, on met les voix
        {
            DonneesChargement.voixAleatoireActive = true;
        }
        else
        {
            DonneesChargement.voixAleatoireActive = false;
        }
    }

    // Checkbox qui va servir à savoir si on veut le menu pour changer de personnage ou pour faire spawn allier
    public void checkboxChangerMenuPersonnages()
    {
        if (toggleSpawnAllier.isOn) // Si le toggle est activé, on change le menu pour faire spawn des alliés
        {
            spawnAllier = true;
        }
        else
        {
            spawnAllier = false;
        }
    }

    // Charge le profile volume HD2D et bloque la caméra sur le joueur
    private void chargerProfileHD2D()
    {
        volumeGameObject.GetComponent<Volume>().profile = Resources.Load<VolumeProfile>("Profiles/HD 2D");

        // On lock la caméra pour qu'elle suive bien le joueur
        vThirdPersonCamera camera = main.controllerJoueurs[0].GetComponent<vThirdPersonCamera>();
        camera.lockCamera = true;
        camera.defaultDistance = 14;
        camera.height = 3;
    }

    // Charge le profile volume 3D et remet la caméra par défaut
    private void chargerProfileDefault()
    {
        // On remet le volume par défaut
        volumeGameObject.GetComponent<Volume>().profile = Resources.Load<VolumeProfile>("Profiles/Global Volume Profile");

        // On remet la caméra par défaut
        vThirdPersonCamera camera = main.controllerJoueurs[0].GetComponent<vThirdPersonCamera>();
        camera.lockCamera = false;
        camera.defaultDistance = 2.5f;
        camera.height = 1.4f;
    }

    // Chargement des sprites pour le mode HD 2D car cela met du temps à instancier le personnage par défaut
    private IEnumerator chargerSprite2D(int numeroPersonnage)
    {
        yield return new WaitForSeconds(0.1f);

        GameObject spriteGameObject = new GameObject("Sprite");
        spriteGameObject.transform.parent = main.JoueurManager.JoueursGameObject[0].transform.GetChild(1).gameObject.transform.GetChild(4).gameObject.transform;
        spriteGameObject.transform.localPosition = new Vector3(0, 41, 0);
        spriteGameObject.transform.localScale = new Vector3(5, 5, 5);
        SpriteRenderer spriteRenderer = spriteGameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Images/PersonnagesSprites/" + listePersonnagesSprites[numeroPersonnage].personnageSprite.name);
        spriteRenderer.material = Resources.Load<Material>("Materials/HD 2D"); // On charge le bon material
        spriteGameObject.AddComponent<ShadowedSprite>(); // On ajoute les ombres HD 2D

        spriteGameObject.AddComponent<SpriteDeplacement3D>(); // On ajoute le script de changement des sprites
    }

    // Chargement des sprites pour les alliés car cela met du temps à instancier le personnage par défaut
    private IEnumerator chargerSprite2DAllier(int numeroPersonnage, GameObject allierGameObject)
    {
        yield return new WaitForSeconds(0.1f);

        GameObject spriteGameObject = new GameObject("Sprite");
        spriteGameObject.transform.parent = allierGameObject.transform.GetChild(1).gameObject.transform;
        spriteGameObject.transform.localPosition = new Vector3(0, 41, 0);
       // spriteGameObject.transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 0));
        spriteGameObject.transform.localScale = new Vector3(5, 5, 5);
        SpriteRenderer spriteRenderer = spriteGameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Images/PersonnagesSprites/" + listePersonnagesSprites[numeroPersonnage].personnageSprite.name);
        spriteRenderer.material = Resources.Load<Material>("Materials/HD 2D"); // On charge le bon material
        spriteGameObject.AddComponent<ShadowedSprite>(); // On ajoute les ombres HD 2D

        SpriteGameObjectDeplacementScript spriteGameObjectRotationScript = spriteGameObject.AddComponent<SpriteGameObjectDeplacementScript>(); // On ajoute le script pour que le sprite regarde la caméra
        spriteGameObjectRotationScript.vThirdPersonCamera = main.controllerJoueurs[0]; // On ajoute la caméra

        GameObject minimapSpriteGameObject = allierGameObject.transform.GetChild(3).gameObject; // Puisque les sprites sont plus petit, il faut resize l'icone de la minimap du joueur
        minimapSpriteGameObject.transform.localPosition = new Vector3(-25, 160, 20);
        minimapSpriteGameObject.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
        minimapSpriteGameObject.transform.localScale = new Vector3(270, 270, 33);

        spriteGameObject.AddComponent<SpriteDeplacement3D>(); // On ajoute le script de changement des sprites
    }

    // Pour faire spawn un allier via le menu
    public void spawnAllierMenu(int numeroPersonnage, List<Personnage> listePersonnages, string dimensionPersonnage)
    {
        GameObject personnageActuel = main.JoueurManager.JoueursGameObject[0];

        Vector3 spawnPersonnageAllierPosition = new Vector3(personnageActuel.transform.position.x + 1, personnageActuel.transform.position.y, personnageActuel.transform.position.z); // Là ou vont spawn les personnages alliés
        GameObject allierGameObjectPrefab = Instantiate(vThirdPersonCameraPrefab, spawnPersonnageAllierPosition, personnageActuel.transform.rotation); // On spawn le prefab

        allierGameObjectPrefab.tag = "Allier";

        if (dimensionPersonnage == "3D") // Si c'est un personnage 3D, on fait spawn son gameobject sinon son sprite
        {
            allierGameObject = Instantiate(listePersonnages[numeroPersonnage].personnageGameObject, allierGameObjectPrefab.transform); // On spawn le personnage dans le prefab pour faire un personnage
            allierGameObjectPrefab.name = listePersonnages[numeroPersonnage].personnageGameObject.name;

            allierGameObject.GetComponent<Animator>().enabled = false;

            allierGameObject.transform.localScale = new Vector3(0.014f, 0.014f, 0.014f); // On met la bonne taille

            allierGameObjectPrefab.GetComponent<Animator>().avatar = allierGameObject.GetComponent<Animator>().avatar; // On met l'avatar
        }
        else if(dimensionPersonnage == "2D")
        {
            // allierGameObject = Instantiate(personnagePrefab, main.JoueurManager.JoueursGameObject[0].transform.GetChild(1).gameObject.transform); // Et on remplace par le personnage préfab
            allierGameObjectPrefab.name = listePersonnages[numeroPersonnage].personnageSprite.name;
            allierGameObjectPrefab.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f); // On met la bonne taille

            StartCoroutine(chargerSprite2DAllier(numeroPersonnage, allierGameObjectPrefab));
        }

        NavMeshAgent navMeshAgent = allierGameObjectPrefab.AddComponent<NavMeshAgent>(); // On ajoute l'agent
        PatrouilleMouvement patrouilleMouvementScript = allierGameObjectPrefab.AddComponent<PatrouilleMouvement>(); // On ajoute le script permettant au personnage de savoir ou aller
        patrouilleMouvementScript.pokemonAllier = true; // On dit que c'est un allier

        navMeshAgent.baseOffset = -0.1f;
        navMeshAgent.speed = 8f;

        patrouilleMouvementScript.points.Add(personnageActuel.transform);
        patrouilleMouvementScript.points.Add(allierGameObjectPrefab.transform);

        listePersonnagesAllier.Add(allierGameObjectPrefab); // On ajoute le personnage à la liste des alliers
    }

    public void changerPersonnage()
    {
        GameObject personnageActuel = main.JoueurManager.JoueursGameObject[0].transform.GetChild(1).gameObject.transform.GetChild(4).gameObject;
        GameObject boutonCliquerGameObject = EventSystem.current.currentSelectedGameObject;

        if (spawnAllier == false) // Si c'est false, on change de personnage, sinon on spawn un allié
        {
            if (personnageActuel.name != boutonCliquerGameObject.name) // Si le personnage actuel n'est pas le même que celui cliquer alors on pourra changer de personnage
            {
                Destroy(personnageActuel); // On enlève le personnage actuel

                string boutonCliquerPersonnageCategorie = boutonCliquerGameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text; // Categorie du personnage, servira à déterminer la liste

                int numeroPersonnage = int.Parse(boutonCliquerGameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text); // Numéro du personnage cliqué dans la liste

                if (DonneesChargement.dimensionPersonnage == "2D" && boutonCliquerPersonnageCategorie != "Sprites") // Si le personnage actuel est en 2D et qu'on veut mettre un personnage en 3D
                {
                    // On remet le volume par défaut
                    chargerProfileDefault();
                    toggleHD2D.isOn = false; // On remet le toggle à false vu qu'on désactive le mode HD2D
                }

                if (boutonCliquerPersonnageCategorie == "Dresseurs")
                {
                    personnage = Instantiate(listePersonnagesDresseurs[numeroPersonnage].personnageGameObject, main.JoueurManager.JoueursGameObject[0].transform.GetChild(1).gameObject.transform); // Et on remplace par le personnage
                    personnage.name = listePersonnagesDresseurs[numeroPersonnage].personnageGameObject.name; // On met le bon nom
                    changerMusique(listePersonnagesDresseurs, numeroPersonnage);
                    DonneesChargement.dimensionPersonnage = "3D";
                    DonneesChargement.listePersonnageDialogues = listePersonnagesDresseurs[numeroPersonnage].listePersonnageDialogues;
                }
                else if (boutonCliquerPersonnageCategorie == "Super Heros")
                {
                    personnage = Instantiate(listePersonnagesHeros[numeroPersonnage].personnageGameObject, main.JoueurManager.JoueursGameObject[0].transform.GetChild(1).gameObject.transform); // Et on remplace par le personnage
                    personnage.name = listePersonnagesHeros[numeroPersonnage].personnageGameObject.name;
                    changerMusique(listePersonnagesHeros, numeroPersonnage);
                    DonneesChargement.dimensionPersonnage = "3D";
                    DonneesChargement.listePersonnageDialogues = listePersonnagesHeros[numeroPersonnage].listePersonnageDialogues;
                }
                else if (boutonCliquerPersonnageCategorie == "Sprites")
                {
                    personnage = Instantiate(personnagePrefab, main.JoueurManager.JoueursGameObject[0].transform.GetChild(1).gameObject.transform); // Et on remplace par le personnage préfab
                    personnage.name = listePersonnagesSprites[numeroPersonnage].personnageSprite.name;
                    changerMusique(listePersonnagesSprites, numeroPersonnage);
                    if (DonneesChargement.dimensionPersonnage != "2D") // On change le profil que si ce n'était pas un sprite, on ne va pas charger le profil si il est déjà mis
                    {
                        chargerProfileHD2D();
                        toggleHD2D.isOn = true; // On met le toggle à true
                    }

                    DonneesChargement.dimensionPersonnage = "2D";
                    DonneesChargement.listePersonnageDialogues = listePersonnagesSprites[numeroPersonnage].listePersonnageDialogues;
                    DonneesChargement.nomSprite = listePersonnagesSprites[numeroPersonnage].personnageSprite.name; // On met le nom des sprites
                    if (listePersonnagesSprites[numeroPersonnage].personnageSpriteHaut != null) // Si il y a un sprite mis dans l'éditeur pour cette position, on l'assigne
                    {
                        DonneesChargement.nomSpriteHaut = listePersonnagesSprites[numeroPersonnage].personnageSpriteHaut.name;
                    }
                    else // Sinon on met à null
                    {
                        DonneesChargement.nomSpriteHaut = null;
                    }

                    if (listePersonnagesSprites[numeroPersonnage].personnageSpriteDroite != null)
                    {
                        DonneesChargement.nomSpriteDroite = listePersonnagesSprites[numeroPersonnage].personnageSpriteDroite.name;
                    }
                    else
                    {
                        DonneesChargement.nomSpriteDroite = null;
                    }

                    if (listePersonnagesSprites[numeroPersonnage].personnageSpriteGauche != null)
                    {
                        DonneesChargement.nomSpriteGauche = listePersonnagesSprites[numeroPersonnage].personnageSpriteGauche.name; // On met le nom des sprites
                    }
                    else
                    {
                        DonneesChargement.nomSpriteGauche = null;
                    }

                    StartCoroutine(chargerSprite2D(numeroPersonnage)); // On charge le sprite après
                }
                else if (boutonCliquerPersonnageCategorie == "Nintendo")
                {
                    personnage = Instantiate(listePersonnagesNintendo[numeroPersonnage].personnageGameObject, main.JoueurManager.JoueursGameObject[0].transform.GetChild(1).gameObject.transform); // Et on remplace par le personnage
                    personnage.name = listePersonnagesNintendo[numeroPersonnage].personnageGameObject.name;
                    changerMusique(listePersonnagesNintendo, numeroPersonnage);
                    DonneesChargement.dimensionPersonnage = "3D";
                    DonneesChargement.listePersonnageDialogues = listePersonnagesNintendo[numeroPersonnage].listePersonnageDialogues;
                }
                else if (boutonCliquerPersonnageCategorie == "Final Fantasy")
                {
                    personnage = Instantiate(listePersonnagesFinalFantasy[numeroPersonnage].personnageGameObject, main.JoueurManager.JoueursGameObject[0].transform.GetChild(1).gameObject.transform); // Et on remplace par le personnage
                    personnage.name = listePersonnagesFinalFantasy[numeroPersonnage].personnageGameObject.name;
                    changerMusique(listePersonnagesFinalFantasy, numeroPersonnage);
                    DonneesChargement.dimensionPersonnage = "3D";
                    DonneesChargement.listePersonnageDialogues = listePersonnagesFinalFantasy[numeroPersonnage].listePersonnageDialogues;
                }

                // Va servir pour l'animation de lancer un pokémon
                GameObject destinationPokeballGameObject = Instantiate(personnagePrefab.transform.GetChild(0).gameObject, personnage.transform); // On met destination pokéball
                main.destinationPokeball = destinationPokeballGameObject.transform; // On l'assigne
                main.GameObjectJoueur = personnage; // On assigne le personnage

                if (DonneesChargement.dimensionPersonnage == "3D")
                {
                    DonneesChargement.nomGameObjectModel = personnage.name;
                    personnage.transform.localScale = new Vector3(0.014f, 0.014f, 0.014f);
                    main.JoueurManager.JoueursGameObject[0].GetComponent<Animator>().avatar = personnage.GetComponent<Animator>().avatar; // On met l'avatar
                }
                else if (DonneesChargement.dimensionPersonnage == "2D")
                {
                    DonneesChargement.nomGameObjectModel = personnage.name;
                    main.JoueurManager.JoueursGameObject[0].GetComponent<Animator>().avatar = null; // On met l'avatar
                }

                StartCoroutine(main.JoueurManager.JoueursGameObject[0].GetComponent<ChangementCaracteristiquesPersonnage>().miseAJourCaracteristiquesChangementPersonnage()); // On met à jour si besoin les caractéristiques du personnages
            }
        }

        else // Si on veut faire spawn un allier
        {
            string boutonCliquerPersonnageCategorie = boutonCliquerGameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text; // Categorie du personnage, servira à déterminer la liste
            int numeroPersonnage = int.Parse(boutonCliquerGameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text); // Numéro du personnage cliqué dans la liste

            if (boutonCliquerPersonnageCategorie == "Dresseurs")
            {
                spawnAllierMenu(numeroPersonnage, listePersonnagesDresseurs, "3D");
            }
            else if (boutonCliquerPersonnageCategorie == "Super Heros")
            {
                spawnAllierMenu(numeroPersonnage, listePersonnagesHeros, "3D");
            }
            else if (boutonCliquerPersonnageCategorie == "Sprites")
            {
                spawnAllierMenu(numeroPersonnage, listePersonnagesSprites, "2D");
            }
            else if (boutonCliquerPersonnageCategorie == "Nintendo")
            {
                spawnAllierMenu(numeroPersonnage, listePersonnagesNintendo, "3D");
            }
            else if (boutonCliquerPersonnageCategorie == "Final Fantasy")
            {
                spawnAllierMenu(numeroPersonnage, listePersonnagesFinalFantasy, "3D");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
