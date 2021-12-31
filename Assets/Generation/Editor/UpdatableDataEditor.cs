using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Options dans l'�diteur pour mettre � jours certains param�tres des assets tels que noise et terrain data
[CustomEditor(typeof(UpdatableData), true)]
public class UpdatableDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UpdatableData data = (UpdatableData)target;

        if(GUILayout.Button("Update")) // Si on appui sur le bouton update, on met � jour les valeurs des assets pour r�fl�ter les changements sur la map
        {
            data.NotifyOfUpdatedValues();
            EditorUtility.SetDirty(target); /// Notifi si quelque chose � �t� chang�
        }
    }
}
