using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using UnityEngine;

public class SauvegardeChargementClickScript : MonoBehaviour
{
    public void Sauvegarde_Click()
    {
        // SaveFileDialog saveFileDialog = new SaveFileDialog();

        // if (saveFileDialog.ShowDialog() == DialogResult.OK)
        // {
        // var cheminFichier = saveFileDialog.FileName;
        string cheminFichier = Application.streamingAssetsPath + "/Joueur.xml";
        XmlWriter writer = XmlWriter.Create(cheminFichier);

        DataContractSerializer serializer = new DataContractSerializer(typeof(ClassLibrary.Dresseur));

        try
        {
            serializer.WriteObject(writer, this.gameObject.GetComponent<ProjetP3DScene1.main>().JoueurManager.Joueurs[0]);
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
        DataContractSerializer serializer = new DataContractSerializer(typeof(ClassLibrary.Dresseur));
        FileStream fs = new FileStream(cheminFichier, FileMode.Open);

        XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());

        try
        {
            //   nombrePokemonAvantChargementSauvegarde = Joueur.getPokemonEquipe().Count;
            this.gameObject.GetComponent<ProjetP3DScene1.main>().JoueurManager.Joueurs[0] = (ClassLibrary.Dresseur)serializer.ReadObject(reader);
            // rafraichirApresChargementSauvegarde();

        }
        catch
        {
            //  MessageBox.Show("Impossible de deserialiser : " + erreur);
        }
        reader.Close();


    }
}
