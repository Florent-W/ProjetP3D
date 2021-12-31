using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class SauvegardeXml : MonoBehaviour
{
    public GameObject sauvegardeGameObject;
    public Button BoutonConfirmerChargement;
    public ClassLibrary.Dresseur Joueur = new ClassLibrary.Dresseur();

    public void Sauvegarde_Click()
    {
        // SaveFileDialog saveFileDialog = new SaveFileDialog();

        // if (saveFileDialog.ShowDialog() == DialogResult.OK)
        // {
        // var cheminFichier = saveFileDialog.FileName;
        var xmlWriterOptions = new XmlWriterSettings();
        xmlWriterOptions.Indent = true;
        xmlWriterOptions.NewLineOnAttributes = true;

            string cheminFichier = Application.streamingAssetsPath + "/Joueur.xml";
            XmlWriter writer = XmlWriter.Create(cheminFichier, xmlWriterOptions);

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

        for (int i = 1; i <= 3; i++) // On met toutes les sauvegardes
        {
            string cheminFichier = Application.streamingAssetsPath + "/Joueur_" + i + ".xml";
            DataContractSerializer serializer = new DataContractSerializer(typeof(ClassLibrary.Dresseur));
            FileStream fs = new FileStream(cheminFichier, FileMode.Open);

            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());

            try
            {
                //   nombrePokemonAvantChargementSauvegarde = Joueur.getPokemonEquipe().Count;
                Joueur = (ClassLibrary.Dresseur)serializer.ReadObject(reader);

                GameObject sauvegarde = null;
                if (i > 1) { // On met les emplacements des autres sauvegardes
                   sauvegarde = Instantiate(sauvegardeGameObject, sauvegardeGameObject.transform.parent);
                }
                else if(i == 1)
                {
                    sauvegarde = sauvegardeGameObject;
                }
                sauvegarde.transform.parent.gameObject.transform.GetChild(i - 1).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Dresseur : " + Joueur.getNom();
                sauvegarde.transform.parent.gameObject.transform.GetChild(i - 1).gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Pokédex : " + Joueur.getPokemonEquipe().Count.ToString();
                sauvegarde.transform.parent.gameObject.transform.GetChild(i - 1).gameObject.name = i.ToString();
                sauvegarde.transform.parent.gameObject.transform.GetChild(i - 1).gameObject.SetActive(true);
               // LabelNombrePokemon.gameObject.SetActive(true);
               /*
                if (i == 1) // On met le bouton de confirmation
                {
                  // BoutonConfirmerChargement.interactable = true;
                }
                */
                // rafraichirApresChargementSauvegarde();

            }
            catch(Exception erreur)
            {
                Debug.Log("Impossible de charger." + erreur);
            }
            reader.Close();


        }
    }
}
