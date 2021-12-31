using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet de mettre � jour automatiquement les donn�es des assets noise et terrain
public class UpdatableData : ScriptableObject
{
    public event System.Action OnValuesUpdated;
    public bool autoUpdate;

#if UNITY_EDITOR // Si on est dans l'�diteur, on lance le code

    // Si il y a une modification dans l'�diteur
    protected virtual void OnValidate()
    {
        if(autoUpdate) // Si on a activ� l'option de la mise � jour automatique, on peut regarder si les valeurs des assets se mettent � jour
        {
            UnityEditor.EditorApplication.update += NotifyOfUpdatedValues;
        }
    }


    // Dit si il faut mettre � jour les valeurs des assets
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
