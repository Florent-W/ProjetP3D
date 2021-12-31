using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Permet de g�n�rer la map dans l'�diteur sans appuyer sur start
[CustomEditor (typeof(MapPreview))]
public class MapPreviewEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapPreview mapPreview = (MapPreview)target; // Objet mapPreview actuellement inspect� dans editeur
        if (DrawDefaultInspector()) // Si une valeur est chang� dans l'inspecteur
        { 
            if(mapPreview.autoUpdate) // Pour la mise � jour automatique de la map si la case d'updade coch�
            {
                mapPreview.DrawMapInEditor(); // G�n�ration de la map
            }
        }

        // G�n�ration boutons
        if(GUILayout.Button("Generate"))
        {
            mapPreview.DrawMapInEditor(); // Si appui sur bouton, on active la m�thode qui g�nere la map
        }
    }
}
