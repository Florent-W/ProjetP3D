using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class SauvegardeXml : MonoBehaviour
{
    public Text LabelNomPersonnage, LabelNombrePokemon;
    public Button BoutonConfirmerChargement;
    public ClassLibrary.Personnage Joueur = new ClassLibrary.Personnage();

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
            LabelNomPersonnage.text = Joueur.getNom();
            LabelNombrePokemon.text = Joueur.getPokemonEquipe().Count.ToString();
            LabelNomPersonnage.gameObject.SetActive(true);
            LabelNombrePokemon.gameObject.SetActive(true);
            BoutonConfirmerChargement.interactable = true;

            // rafraichirApresChargementSauvegarde();

        }
        catch
        {
            //  MessageBox.Show("Impossible de deserialiser : " + erreur);
        }
        reader.Close();


    }
}
