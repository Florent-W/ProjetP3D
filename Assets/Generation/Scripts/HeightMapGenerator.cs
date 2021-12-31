using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Génére la HeightMap
public static class HeightMapGenerator
{
    // Les differentes options de la HeightMap avec sa génération
    public static HeightMap GenerateHeightMap(int width, int height, HeightMapSettings settings, Vector2 sampleCentre)
    {
        float[,] values = Noise.GenerateNoiseMap(width, height, settings.noiseSettings, sampleCentre); // On génére d'abord les valeurs de la noisemap

        AnimationCurve heightCurve_threadsafe = new AnimationCurve(settings.heightCurve.keys); // Courbe de la heightmap

        float minValue = float.MaxValue;
        float maxValue = float.MinValue;

        // On parcours la heightmap
        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                values[i, j] *= settings.heightCurve.Evaluate(values[i, j]) * settings.heightMultiplier; // Les valeurs de la heightmap pour chaque pixel

                // On regarde la valeur maximale et minimale
                if (values[i, j] > maxValue)
                {
                    maxValue = values[i, j];
                }
                if (values[i, j] < minValue)
                {
                    minValue = values[i, j];
                }
            }
        }

        return new HeightMap(values, minValue, maxValue);
    }
}

// Informations de la map
public struct HeightMap
{
    public readonly float[,] values;
    public readonly float minValues; // Valeur minimum de la heightmap
    public readonly float maxValues; // Valeur maximum de la heightmap

    // Assigne les valeurs aux maps
    public HeightMap(float[,] values, float minValue, float maxValue)
    {
        this.values = values;
        this.minValues = minValue;
        this.maxValues = maxValue;
    }
}
