using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Options dans l'éditeur pour mettre à jours certains paramètres des assets tels que noise et terrain data
[CustomEditor(typeof(UpdatableData), true)]
public class UpdatableDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UpdatableData data = (UpdatableData)target;

        if(GUILayout.Button("Update")) // Si on appui sur le bouton update, on met à jour les valeurs des assets pour réfléter les changements sur la map
        {
            data.NotifyOfUpdatedValues();
            EditorUtility.SetDirty(target); /// Notifi si quelque chose à été changé
        }
    }
}
