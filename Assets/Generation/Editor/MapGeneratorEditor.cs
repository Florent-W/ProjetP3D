using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Permet de générer la map dans l'éditeur sans appuyer sur start
[CustomEditor (typeof(MapPreview))]
public class MapPreviewEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapPreview mapPreview = (MapPreview)target; // Objet mapPreview actuellement inspecté dans editeur
        if (DrawDefaultInspector()) // Si une valeur est changé dans l'inspecteur
        { 
            if(mapPreview.autoUpdate) // Pour la mise à jour automatique de la map si la case d'updade coché
            {
                mapPreview.DrawMapInEditor(); // Génération de la map
            }
        }

        // Génération boutons
        if(GUILayout.Button("Generate"))
        {
            mapPreview.DrawMapInEditor(); // Si appui sur bouton, on active la méthode qui génere la map
        }
    }
}
