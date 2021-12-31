using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet de mettre à jour automatiquement les données des assets noise et terrain
public class UpdatableData : ScriptableObject
{
    public event System.Action OnValuesUpdated;
    public bool autoUpdate;

#if UNITY_EDITOR // Si on est dans l'éditeur, on lance le code

    // Si il y a une modification dans l'éditeur
    protected virtual void OnValidate()
    {
        if(autoUpdate) // Si on a activé l'option de la mise à jour automatique, on peut regarder si les valeurs des assets se mettent à jour
        {
            UnityEditor.EditorApplication.update += NotifyOfUpdatedValues;
        }
    }


    // Dit si il faut mettre à jour les valeurs des assets
    public void NotifyOfUpdatedValues()
    {
        UnityEditor.EditorApplication.update -= NotifyOfUpdatedValues;
        if (OnValuesUpdated != null)
        {
            OnValuesUpdated(); 
        }
    }
#endif
}
