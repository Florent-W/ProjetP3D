using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Donn�es des noises, avec les diff�rents param�tres, cela cr�er un asset dans le menu create et on peut modifier ses param�tres dans l'�diteur
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

#if UNITY_EDITOR // Si on est dans l'�diteur, on lance le code
    // Lorsqu'une variable est chang�, on v�rifie que la map � les valeurs minimum dans l'�diteur sinon, c'est remis � la valeur minimum, on met � jour la valeur de noise et terrain
    protected override void OnValidate()
    {
        noiseSettings.ValidateValues(); // On v�rifi� les valeurs
        base.OnValidate();
    }
#endif
}
