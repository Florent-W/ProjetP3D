using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SystemeSauvegarde
{
    public static void sauvegardeJoueur(ClassLibrary.Personnage Joueur)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string chemin = Application.persistentDataPath + "/Joueur.sav";
      //  Debug.Log(chemin);
        FileStream stream = new FileStream(chemin, FileMode.Create);

        JoueurDonnees donnees = new JoueurDonnees(Joueur);
        formatter.Serialize(stream, donnees);
        stream.Close();
    }

    public static JoueurDonnees chargementJoueur()
    {
        string chemin = Application.persistentDataPath + "/Joueur.sav";

        if (File.Exists(chemin))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(chemin, FileMode.Open))
            {

                JoueurDonnees donnees = formatter.Deserialize(stream) as JoueurDonnees;

                stream.Close();

                return donnees;
            }
        }
        else
        {
            return null;
        }
    }
}
