using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class Personnage
{
    public GameObject personnageGameObject;
    public Sprite personnageSprite;
    public Sprite personnageSpriteHaut;
    public Sprite personnageSpriteDroite;
    public Sprite personnageSpriteGauche;

    public AudioClip personnageMusique;
    public List<AudioClip> listePersonnageDialogues;
}

// Script qui va permettre de changer de personnage dans l'écran nouvelle partie
public class PersonnageSelectionScript : MonoBehaviour
{
    public List<Personnage> listePersonnages = new List<Personnage>();
    public List<Personnage> listePersonnagesDresseurs = new List<Personnage>();
    public List<Personnage> listePersonnagesHeros = new List<Personnage>();
    public List<Personnage> listePersonnagesSprites = new List<Personnage>();
    public List<Personnage> listePersonnagesNintendo = new List<Personnage>();
    public List<Personnage> listePersonnagesFinalFantasy = new List<Personnage>();
    public int numeroActuelListePersonnages = 0, numeroActuelListePersonnagesDresseurs = 0, numeroActuelListePersonnagesHeros = 0, numeroActuelListePersonnagesSprites = 0, numeroActuelListePersonnagesNintendo = 0, numeroActuelListePersonnagesFinalFantasy = 0;
    public GameObject personnageMenu;
    public Dropdown choixClassePersonnage;
    [SerializeField]
    private GameObject personnageSpriteGameObject;
    public string listePersonnageActuel;
    [SerializeField]
    private PersonnagesDonnees personnagesDonnees;
    private bool coroutineChargementTourne;

    // Start is called before the first frame update
    void Start()
    {
        personnageMenu = this.transform.GetChild(0).gameObject;
        // On charge les personnages
        listePersonnagesDresseurs = personnagesDonnees.listePersonnagesDresseurs;
        listePersonnagesHeros = personnagesDonnees.listePersonnagesHeros;
        listePersonnagesSprites = personnagesDonnees.listePersonnagesSprites;
        listePersonnagesNintendo = personnagesDonnees.listePersonnagesNintendo;
        listePersonnagesFinalFantasy = personnagesDonnees.listePersonnagesFinalFantasy;

        listePersonnages = listePersonnagesDresseurs;
        listePersonnageActuel = "Dresseurs";

        StartCoroutine(chargerListePersonnageSelectionSprite("Dresseurs", listePersonnagesDresseurs, "3D")); // On assigne les dresseurs
    }

    // Permet de charger la selection des personnages
    private IEnumerator chargerListePersonnageSelectionSprite(string nomListePersonnageActuel, List<Personnage> listePersonnages, string dimensionPersonnage)
    {
        coroutineChargementTourne = true;
        yield return null;

        // On met les sprites
        for (int i = 0; i < listePersonnages.Count; i++)
        {
            Instantiate(personnageSpriteGameObject, this.transform.GetChild(4).gameObject.transform); // On met le nombre de fois le nombre de sprite
                                                                                                                                                  // this.transform.GetChild(4).gameObject.transform.GetChild(i + 1).gameObject.name = listePersonnagesSprite[i].name;
            GameObject personnageSpriteBoucle = this.transform.GetChild(4).gameObject.transform.GetChild(i).gameObject; // Le personnage selectionné dans la boucle
            personnageSpriteBoucle.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = listePersonnages[i].personnageSprite;
            personnageSpriteBoucle.GetComponent<positionPersonnageMenu>().positionPersonnage = i; // On met la position du personnage
        }

        int nbPersonnageColonneBas = listePersonnages.Count / 2; // Nombre de colonne après le personnage pour trouver le personnage du bas ou du haut

        for (int i = 0; i < listePersonnages.Count; i++)
        {
            GameObject personnageSpriteBoucle = this.transform.GetChild(4).gameObject.transform.GetChild(i).gameObject; // Le personnage selectionné dans la boucle
            Button personnageSpriteBoucleBouton = personnageSpriteBoucle.GetComponent<Button>();
            personnageSpriteBoucleBouton.onClick.AddListener(changerDresseurMenu);

            Navigation nav = new Navigation();
            nav.mode = Navigation.Mode.Explicit; // On change le mode

            if (i >= 1) // On selectionne la navigation vers la gauche
            {
                nav.selectOnLeft = this.transform.GetChild(4).gameObject.transform.GetChild(i - 1).gameObject.GetComponent<Button>();
            }
            else if (i == 0) // Navigation vers retour pour le premier personnage
            {
                nav.selectOnLeft = this.transform.parent.gameObject.transform.GetChild(3).gameObject.GetComponent<Button>();
            }

            if (i < listePersonnages.Count - 1) // On selectionne la navigation vers la droite
            {
                nav.selectOnRight = this.transform.GetChild(4).gameObject.transform.GetChild(i + 1).gameObject.GetComponent<Button>();
            }

            if (i < nbPersonnageColonneBas) // On selectionne la navigation vers le bas pour les personnages de la ligne du haut, ils iront vers le bouton retour si on appuie sur haut
            {
                nav.selectOnUp = this.transform.parent.gameObject.transform.GetChild(3).gameObject.GetComponent<Button>(); // Bouton retour
                nav.selectOnDown = this.transform.GetChild(4).gameObject.transform.GetChild(i + nbPersonnageColonneBas).gameObject.GetComponent<Button>();
            }
            else if (i >= nbPersonnageColonneBas && i < listePersonnages.Count) // On selectionne la navigation vers le haut pour les personnages du bas, ils iront vers l'input si on appui en bas sauf pour le dernier personnage
            {
                nav.selectOnUp = this.transform.GetChild(4).gameObject.transform.GetChild(i - nbPersonnageColonneBas).gameObject.GetComponent<Button>();

                if (i < listePersonnagesDresseurs.Count - 1)
                {
                    nav.selectOnDown = this.transform.parent.gameObject.transform.GetChild(2).gameObject.GetComponent<InputField>(); // Input
                }
                else if (i == listePersonnagesDresseurs.Count - 1)
                {
                    nav.selectOnDown = this.transform.parent.gameObject.transform.GetChild(5).gameObject.GetComponent<Dropdown>(); // Input
                }
            }

            personnageSpriteBoucleBouton.navigation = nav;
        }

        placerPersonnageGameObjectSelectionMenu(listePersonnages, dimensionPersonnage);

        yield return null;

        // Création de la navigation pour le bouton retour
        Navigation navBtnRetour = new Navigation();
        navBtnRetour.mode = Navigation.Mode.Explicit; // On change le mode
        navBtnRetour.selectOnDown = this.transform.GetChild(4).gameObject.transform.GetChild(0).gameObject.GetComponent<Button>();
        navBtnRetour.selectOnRight = this.transform.parent.gameObject.transform.GetChild(5).gameObject.GetComponent<Dropdown>();

        this.transform.parent.gameObject.transform.GetChild(3).gameObject.GetComponent<Button>().navigation = navBtnRetour;
        coroutineChargementTourne = false;

        this.transform.GetChild(4).gameObject.transform.GetChild(numeroActuelListePersonnages).gameObject.GetComponent<Button>().Select(); // On selectionne le bon bouton
    }

    // Va permettre de changer le personnage indiquer dans le menu principal
    public void changerDresseurMenu()
    {
        int numeroPersonnageSelectionMenu = EventSystem.current.currentSelectedGameObject.GetComponent<positionPersonnageMenu>().positionPersonnage; // Le personnage cliqué

        if (numeroActuelListePersonnages != numeroPersonnageSelectionMenu)
        { // Si on est pas sur le même, on peut continuer

            Destroy(this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(4).gameObject); // On détruit le personnage actuel

            numeroActuelListePersonnages = numeroPersonnageSelectionMenu; // On assigne le numéro du personnage selectionné

            if (listePersonnageActuel == "Dresseurs")
            {
                GameObject personnage = Instantiate(listePersonnagesDresseurs[numeroPersonnageSelectionMenu].personnageGameObject, this.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform); // Et on remplace par le personnage précédent
                personnage.name = listePersonnagesDresseurs[numeroPersonnageSelectionMenu].personnageGameObject.name;
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().avatar = listePersonnagesDresseurs[numeroPersonnageSelectionMenu].personnageGameObject.GetComponent<Animator>().avatar; // On assigne l'avatar
                personnageMenu.GetComponent<Animator>().Rebind();
            }
            else if(listePersonnageActuel == "Super Heros")
            {
                GameObject personnage = Instantiate(listePersonnagesHeros[numeroPersonnageSelectionMenu].personnageGameObject, this.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform); // Et on remplace par le personnage précédent
                personnage.name = listePersonnagesHeros[numeroPersonnageSelectionMenu].personnageGameObject.name;
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().avatar = listePersonnagesHeros[numeroPersonnageSelectionMenu].personnageGameObject.GetComponent<Animator>().avatar; // On assigne l'avatar
                personnageMenu.GetComponent<Animator>().Rebind();
            }
            else if (listePersonnageActuel == "Sprite")
            {
                GameObject personnage = new GameObject("Personnage");
                personnage.name = listePersonnagesSprites[numeroActuelListePersonnages].personnageSprite.name;
                personnage.transform.parent = this.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform;
                personnage.transform.localScale = new Vector3(8, 8, 1);
                personnage.transform.localPosition = new Vector3(-11, 104, 98);
                SpriteRenderer sprite = personnage.AddComponent<SpriteRenderer>();
                sprite.sprite = listePersonnagesSprites[numeroActuelListePersonnages].personnageSprite;
                sprite.material = Resources.Load<Material>("Materials/HD 2D"); // On charge le bon material
            }
            else if (listePersonnageActuel == "Nintendo")
            {
                GameObject personnage = Instantiate(listePersonnagesNintendo[numeroPersonnageSelectionMenu].personnageGameObject, this.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform); // Et on remplace par le personnage précédent
                personnage.name = listePersonnagesNintendo[numeroPersonnageSelectionMenu].personnageGameObject.name;
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().avatar = listePersonnagesNintendo[numeroPersonnageSelectionMenu].personnageGameObject.GetComponent<Animator>().avatar; // On assigne l'avatar
                personnageMenu.GetComponent<Animator>().Rebind();
            }
            else if (listePersonnageActuel == "Final Fantasy")
            {
                GameObject personnage = Instantiate(listePersonnagesFinalFantasy[numeroPersonnageSelectionMenu].personnageGameObject, this.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform); // Et on remplace par le personnage précédent
                personnage.name = listePersonnagesFinalFantasy[numeroPersonnageSelectionMenu].personnageGameObject.name;
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().avatar = listePersonnagesFinalFantasy[numeroPersonnageSelectionMenu].personnageGameObject.GetComponent<Animator>().avatar; // On assigne l'avatar
                personnageMenu.GetComponent<Animator>().Rebind();
            }
        }
    }

    // Instancie et place le personnage selon sa classe
    public void assignerPersonnageClasse(int numeroPersonnageSelectionMenu)
    {
        if (listePersonnageActuel == "Dresseurs")
        {
            GameObject personnage = Instantiate(listePersonnagesDresseurs[numeroPersonnageSelectionMenu].personnageGameObject, this.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform); // Et on remplace par le personnage précédent
            personnage.name = listePersonnagesDresseurs[numeroPersonnageSelectionMenu].personnageGameObject.name;
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().avatar = listePersonnagesDresseurs[numeroPersonnageSelectionMenu].personnageGameObject.GetComponent<Animator>().avatar; // On assigne l'avatar
            personnageMenu.GetComponent<Animator>().Rebind();
        }
        else if (listePersonnageActuel == "Super Heros")
        {
            GameObject personnage = Instantiate(listePersonnagesHeros[numeroPersonnageSelectionMenu].personnageGameObject, this.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform); // Et on remplace par le personnage précédent
            personnage.name = listePersonnagesHeros[numeroPersonnageSelectionMenu].personnageGameObject.name;
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().avatar = listePersonnagesHeros[numeroPersonnageSelectionMenu].personnageGameObject.GetComponent<Animator>().avatar; // On assigne l'avatar
            personnageMenu.GetComponent<Animator>().Rebind();
        }
        else if (listePersonnageActuel == "Sprite")
        {
            GameObject personnage = new GameObject("Personnage");
            personnage.name = listePersonnagesSprites[numeroActuelListePersonnages].personnageSprite.name;
            personnage.transform.parent = this.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform;
            personnage.transform.localScale = new Vector3(8, 8, 1);
            personnage.transform.localPosition = new Vector3(-11, 104, 98);
            SpriteRenderer sprite = personnage.AddComponent<SpriteRenderer>();
            sprite.sprite = listePersonnagesSprites[numeroActuelListePersonnages].personnageSprite;
            sprite.material = Resources.Load<Material>("Materials/HD 2D"); // On charge le bon material
        }
        else if (listePersonnageActuel == "Nintendo")
        {
            GameObject personnage = Instantiate(listePersonnagesNintendo[numeroPersonnageSelectionMenu].personnageGameObject, this.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform); // Et on remplace par le personnage précédent
            personnage.name = listePersonnagesNintendo[numeroPersonnageSelectionMenu].personnageGameObject.name;
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().avatar = listePersonnagesNintendo[numeroPersonnageSelectionMenu].personnageGameObject.GetComponent<Animator>().avatar; // On assigne l'avatar
            personnageMenu.GetComponent<Animator>().Rebind();
        }
        else if (listePersonnageActuel == "Final Fantasy")
        {
            GameObject personnage = Instantiate(listePersonnagesFinalFantasy[numeroPersonnageSelectionMenu].personnageGameObject, this.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform); // Et on remplace par le personnage précédent
            personnage.name = listePersonnagesFinalFantasy[numeroPersonnageSelectionMenu].personnageGameObject.name;
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().avatar = listePersonnagesFinalFantasy[numeroPersonnageSelectionMenu].personnageGameObject.GetComponent<Animator>().avatar; // On assigne l'avatar
            personnageMenu.GetComponent<Animator>().Rebind();
        }
    }

    public void BoutonPrecedent()
    {
        Destroy(this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(4).gameObject); // On détruit le personnage actuel

        // On met le bon numéro du personnage
        if (listePersonnageActuel == "Dresseurs")
        {
            if (numeroActuelListePersonnages != 0) // Le numero actuel est la position dans la liste des personnages, avec le bouton précédent, si on est à zéro, on ne peut pas reculer, alors on prend le maximum, sinon on fait -1
            {
                numeroActuelListePersonnages -= 1;
            }
            else
            {
                numeroActuelListePersonnages = listePersonnagesDresseurs.Count - 1;
            }
        }
        else if (listePersonnageActuel == "Super Heros")
        {
            if (numeroActuelListePersonnages != 0) // Le numero actuel est la position dans la liste des personnages, avec le bouton précédent, si on est à zéro, on ne peut pas reculer, alors on prend le maximum, sinon on fait -1
            {
                numeroActuelListePersonnages -= 1;
            }
            else
            {
                numeroActuelListePersonnages = listePersonnagesHeros.Count - 1;
            }
        }
        else if (listePersonnageActuel == "Sprite")
        {
            if (numeroActuelListePersonnages != 0) // Le numero actuel est la position dans la liste des personnages, avec le bouton précédent, si on est à zéro, on ne peut pas reculer, alors on prend le maximum, sinon on fait -1
            {
                numeroActuelListePersonnages -= 1;
            }
            else
            {
                numeroActuelListePersonnages = listePersonnagesSprites.Count - 1;
            }
        }
        else if (listePersonnageActuel == "Nintendo")
        {
            if (numeroActuelListePersonnages != 0) // Le numero actuel est la position dans la liste des personnages, avec le bouton précédent, si on est à zéro, on ne peut pas reculer, alors on prend le maximum, sinon on fait -1
            {
                numeroActuelListePersonnages -= 1;
            }
            else
            {
                numeroActuelListePersonnages = listePersonnagesNintendo.Count - 1;
            }
        }
        else if (listePersonnageActuel == "Final Fantasy")
        {
            if (numeroActuelListePersonnages != 0) // Le numero actuel est la position dans la liste des personnages, avec le bouton précédent, si on est à zéro, on ne peut pas reculer, alors on prend le maximum, sinon on fait -1
            {
                numeroActuelListePersonnages -= 1;
            }
            else
            {
                numeroActuelListePersonnages = listePersonnagesFinalFantasy.Count - 1;
            }
        }

        assignerPersonnageClasse(numeroActuelListePersonnages); 
    }

    public void BoutonSuivant()
    {
        Destroy(this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(4).gameObject); // On détruit le personnage actuel

        // On met le bon numéro du personnage
        if (listePersonnageActuel == "Dresseurs")
        {
            if (numeroActuelListePersonnages >= listePersonnagesDresseurs.Count - 1) // Le numero actuel est la position dans la liste des personnages, avec le bouton suivant, si on est au maximum, on ne peut pas avancer, alors on revient à 0
            {
                numeroActuelListePersonnages = 0;
            }
            else
            {
                numeroActuelListePersonnages = numeroActuelListePersonnages + 1;
            }
        }
        else if (listePersonnageActuel == "Super Heros")
        {
            if (numeroActuelListePersonnages >= listePersonnagesHeros.Count - 1) // Le numero actuel est la position dans la liste des personnages, avec le bouton suivant, si on est au maximum, on ne peut pas avancer, alors on revient à 0
            {
                numeroActuelListePersonnages = 0;
            }
            else
            {
                numeroActuelListePersonnages = numeroActuelListePersonnages + 1;
            }
        }
        else if (listePersonnageActuel == "Sprite")
        {
            if (numeroActuelListePersonnages >= listePersonnagesSprites.Count - 1) // Le numero actuel est la position dans la liste des personnages, avec le bouton suivant, si on est au maximum, on ne peut pas avancer, alors on revient à 0
            {
                numeroActuelListePersonnages = 0;
            }
            else
            {
                numeroActuelListePersonnages = numeroActuelListePersonnages + 1;
            }
        }
        if (listePersonnageActuel == "Nintendo")
        {
            if (numeroActuelListePersonnages >= listePersonnagesNintendo.Count - 1) // Le numero actuel est la position dans la liste des personnages, avec le bouton suivant, si on est au maximum, on ne peut pas avancer, alors on revient à 0
            {
                numeroActuelListePersonnages = 0;
            }
            else
            {
                numeroActuelListePersonnages = numeroActuelListePersonnages + 1;
            }
        }
        else if (listePersonnageActuel == "Final Fantasy")
        {
            if (numeroActuelListePersonnages >= listePersonnagesFinalFantasy.Count - 1) // Le numero actuel est la position dans la liste des personnages, avec le bouton suivant, si on est au maximum, on ne peut pas avancer, alors on revient à 0
            {
                numeroActuelListePersonnages = 0;
            }
            else
            {
                numeroActuelListePersonnages = numeroActuelListePersonnages + 1;
            }
        }

        assignerPersonnageClasse(numeroActuelListePersonnages); // Spawn le personnage selon sa classe
    }

    // Va placer le nouveau personnage selectionné dans le menu
    public void placerPersonnageGameObjectSelectionMenu(List<Personnage> listePersonnages, string dimensionPersonnage)
    {
        if (dimensionPersonnage == "3D")
        {
            GameObject personnage = Instantiate(listePersonnages[numeroActuelListePersonnages].personnageGameObject, this.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform);
            personnage.name = listePersonnages[numeroActuelListePersonnages].personnageGameObject.name;
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().avatar = listePersonnages[numeroActuelListePersonnages].personnageGameObject.GetComponent<Animator>().avatar; // On assigne l'avatar
            personnageMenu.GetComponent<Animator>().Rebind();
        }
        else if(dimensionPersonnage == "2D")
        {
            GameObject personnage = new GameObject("Personnage");
            personnage.name = listePersonnagesSprites[numeroActuelListePersonnages].personnageSprite.name;
            personnage.transform.parent = this.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform;
            // personnage.transform.localScale = new Vector3(8, 8, 1);
            personnage.transform.localScale = new Vector3(210, 210, 1);
            personnage.transform.localPosition = new Vector3(-11, 104, 98);
            SpriteRenderer sprite = personnage.AddComponent<SpriteRenderer>();
            sprite.sprite = listePersonnagesSprites[numeroActuelListePersonnages].personnageSprite;
            sprite.material = Resources.Load<Material>("Materials/HD 2D"); // On charge le bon material
        }
    }

    // Active une des listes de personnages après avoir choisi sa classe
    public void ActiverListeClassePersonnage(Dropdown dropdown)
    {
        if (dropdown.value == 0 && listePersonnageActuel != "Dresseurs")
        {
            listePersonnageActuel = "Dresseurs";
            numeroActuelListePersonnagesHeros = numeroActuelListePersonnages;
            numeroActuelListePersonnages = numeroActuelListePersonnagesDresseurs;
        }
        else if (dropdown.value == 1 && listePersonnageActuel != "Super Heros")
        {
            listePersonnageActuel = "Super Heros";
            numeroActuelListePersonnagesDresseurs = numeroActuelListePersonnages;
            numeroActuelListePersonnages = numeroActuelListePersonnagesHeros;
        }
        else if (dropdown.value == 2 && listePersonnageActuel != "Sprite")
        {
            listePersonnageActuel = "Sprite";
            numeroActuelListePersonnagesDresseurs = numeroActuelListePersonnages;
            numeroActuelListePersonnages = numeroActuelListePersonnagesHeros;
        }
        else if (dropdown.value == 3 && listePersonnageActuel != "Nintendo")
        {
            listePersonnageActuel = "Nintendo";
            numeroActuelListePersonnagesNintendo = numeroActuelListePersonnages;
            numeroActuelListePersonnages = numeroActuelListePersonnagesNintendo;
        }
        else if (dropdown.value == 4 && listePersonnageActuel != "Final Fantasy")
        {
            listePersonnageActuel = "Final Fantasy";
            numeroActuelListePersonnagesFinalFantasy = numeroActuelListePersonnages;
            numeroActuelListePersonnages = numeroActuelListePersonnagesFinalFantasy;
        }

        // On veut placer le nouveau menu de sprite de selection de personnage, d'abord, on enlève l'ancien
        foreach (Transform child in this.gameObject.transform.GetChild(4).gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        /*
        // On met les nouveaux sprites
        for (int i = 0; i < listePersonnages.Count; i++)
        {
            GameObject personnageSpriteMenuGameObject = Instantiate(personnageSpriteGameObject, this.transform.GetChild(4).gameObject.transform); // On met le nombre de fois le nombre de sprite
            Button boutonPersonnageSprite = personnageSpriteMenuGameObject.GetComponent<Button>();
            boutonPersonnageSprite.onClick.AddListener(changerDresseurMenu);

            personnageSpriteMenuGameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = listePersonnages[i].personnageSprite;
            personnageSpriteMenuGameObject.GetComponent<positionPersonnageMenu>().positionPersonnage = i; // On met la position du personnage
        }
        */
        Destroy(this.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(4).gameObject); // On détruit le précédent gameobject selectionné

        if (listePersonnageActuel == "Dresseurs") {
            StartCoroutine(chargerListePersonnageSelectionSprite("Dresseurs", listePersonnagesDresseurs, "3D"));
        }
        else if(listePersonnageActuel == "Super Heros")
        {
            StartCoroutine(chargerListePersonnageSelectionSprite("Super Heros", listePersonnagesHeros, "3D"));
        }
        else if (listePersonnageActuel == "Sprite")
        {
            StartCoroutine(chargerListePersonnageSelectionSprite("Sprite", listePersonnagesSprites, "2D"));
        }
        else if (listePersonnageActuel == "Nintendo")
        {
            StartCoroutine(chargerListePersonnageSelectionSprite("Nintendo", listePersonnagesNintendo, "3D"));
        }
        else if (listePersonnageActuel == "Final Fantasy")
        {
            StartCoroutine(chargerListePersonnageSelectionSprite("Final Fantasy", listePersonnagesFinalFantasy, "3D"));
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
