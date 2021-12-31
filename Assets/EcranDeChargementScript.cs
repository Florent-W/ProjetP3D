using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Va controler la barre de chargement
public class EcranDeChargementScript : MonoBehaviour
{
    [SerializeField]
    private Slider sliderChargement;
    AsyncOperation async;
    private int nombrePointTexteChargement;

    [SerializeField]
    private GameObject sceneChangeHolder;
    [SerializeField]
    private GameObject menuNouvellePartie;
    [SerializeField]
    private GameObject menuChargerPartie;
    public string sceneToLoad;
    public InputField champNomJoueur;
    public static ClassLibrary.Dresseur Joueur = new ClassLibrary.Dresseur();
    private bool estEnTrainDeCharger;
    public string emplacementNumeroSauvegardeCharger;

    [SerializeField]
    private List<Sprite> backgroundEcranChargement = new List<Sprite>();

    public void BarreChargement()
    {
        this.gameObject.SetActive(true);
        this.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = backgroundEcranChargement[UnityEngine.Random.Range(0, backgroundEcranChargement.Count)]; // Choisit une image de chargement random
        StartCoroutine(BarreChargementCoroutine());
    }

    IEnumerator ModifierPourcentageChargement()
    {
        while (async.isDone != true)
        {
            this.gameObject.transform.GetChild(3).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = (sliderChargement.value * 100).ToString("F0") + " %";
            yield return null;
        }
    }

    IEnumerator ModifierTexteChargement()
    {
        while (async.isDone != true)
        {
            nombrePointTexteChargement += 1;
            if (nombrePointTexteChargement > 3)
            {
                nombrePointTexteChargement = 0;
            }
            this.gameObject.transform.GetChild(1).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Chargement" + string.Concat(Enumerable.Repeat(".", nombrePointTexteChargement));
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator BarreChargementCoroutine()
    {
        if (estEnTrainDeCharger == false)
        {
            async = SceneManager.LoadSceneAsync(DonneesChargement.nomSceneSuivante); // Controle si la scène est chargé
            async.allowSceneActivation = false; // La scène ne changera pas tout de suite
            estEnTrainDeCharger = true;
        }

        if (sceneChangeHolder != null)
        {
            if (sceneChangeHolder.GetComponent<ActiverBoutonNouvellePartie>().MenuActuel == "MenuNouvellePartie")
            {
                menuNouvellePartie.SetActive(false);
                if (champNomJoueur.text != "")
                {
                    Joueur.setNomPersonnage(champNomJoueur.text);
                }
                else
                {
                    Joueur.setNomPersonnage("Joueur");
                }
            }
            else if (sceneChangeHolder.GetComponent<ActiverBoutonNouvellePartie>().MenuActuel == "MenuChargerPartie")
            {
                menuChargerPartie.SetActive(false);

                string cheminFichier = Application.streamingAssetsPath + "/Joueur_" + emplacementNumeroSauvegardeCharger + ".xml";
                DataContractSerializer serializer = new DataContractSerializer(typeof(ClassLibrary.Dresseur));
                FileStream fs = new FileStream(cheminFichier, FileMode.Open);

                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());

                try
                {
                     Joueur = (ClassLibrary.Dresseur)serializer.ReadObject(reader);
                    // Joueur = (ClassLibrary.Dresseur)serializer.ReadObject(reader);


                    // rafraichirApresChargementSauvegarde();

                }
                catch
                {
                    //  MessageBox.Show("Impossible de deserialiser : " + erreur);
                }
                reader.Close();
            }
        }
        StartCoroutine(ModifierPourcentageChargement());
        StartCoroutine(ModifierTexteChargement());

        while (async.isDone == false) // Tant que la barre avance et que la scène n'est pas chargé, on continue
        {
            sliderChargement.value = Mathf.Clamp01(async.progress / 0.9f); // Changement de la valeur de la barre de chargement
            if (async.progress == 0.9f)
            {
                yield return new WaitForSeconds(5f);
                sliderChargement.value = 1f;
                async.allowSceneActivation = true;
                estEnTrainDeCharger = false;
            }
            yield return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = backgroundEcranChargement[UnityEngine.Random.Range(0, backgroundEcranChargement.Count)]; // Choisit une image de chargement random
        StartCoroutine(BarreChargementCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
