using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Données des noises, avec les différents paramètres, cela créer un asset dans le menu create et on peut modifier ses paramètres dans l'éditeur
[CreateAssetMenu(menuName = "Generation/Noise Data")]
public class HeightMapSettings : UpdatableData
{
    public NoiseSettings noiseSettings; // Option noise

    public bool useFalloff;

    public float heightMultiplier;
    public AnimationCurve heightCurve;

    public float minHeight // Retourne la hauteur minimum du terrain
    {
        get
        {
            return heightMultiplier * heightCurve.Evaluate(0);
        }
    }

    public float maxHeight // Retourne la hauteur maximum du terrain
    {
        get
        {
            return heightMultiplier * heightCurve.Evaluate(1);
        }
    }

#if UNITY_EDITOR // Si on est dans l'éditeur, on lance le code
    // Lorsqu'une variable est changé, on vérifie que la map à les valeurs minimum dans l'éditeur sinon, c'est remis à la valeur minimum, on met à jour la valeur de noise et terrain
    protected override void OnValidate()
    {
        noiseSettings.ValidateValues(); // On vérifié les valeurs
        base.OnValidate();
    }
#endif
}
