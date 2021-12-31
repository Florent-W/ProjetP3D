using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise
{
    public enum NormalizeMode { Local, Global } // Mode pour normalisé

    // Generer noisemap (couleur entre le noir et le blanc) 2D avec la taille de la map, la hauteur et l'échelle du noise et quelques autres variables pour la génération
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, NoiseSettings settings, Vector2 sampleCentre)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        // Initialisation de la rng avec le seed
        System.Random prng = new System.Random(settings.seed);
        Vector2[] octaveOffsets = new Vector2[settings.octaves]; // Génération décalés avec des offsets permettant d'influer sur la génération des octaves pour avoir un terrain plus naturel

        float maxPossibleHeight = 0; // Hauteur possible maximale
        float amplitude = 1;
        float frequency = 1;

        for (int i = 0; i < settings.octaves; i++) // Parcours des octaves
        {
            float offsetX = prng.Next(-100000, 100000) + settings.offset.x + sampleCentre.x; // Nombre aléatoire pour les offsets entre les deux valeurs
            float offsetY = prng.Next(-100000, 100000) - settings.offset.y - sampleCentre.y; // Nombre aléatoire pour les offsets entre les deux valeurs
            octaveOffsets[i] = new Vector2(offsetX, offsetY); // Attribution des offsets

            maxPossibleHeight += amplitude; // L'amplitude est ajouté pour tous les octaves pour obtenir la hauteur maximale
            amplitude *= settings.persitance; // Persistance entre 0 et 1, il faut faire en sorte que plus le nombre d'octave est elevé, moins il y a de détails
        }

        if (settings.scale <= 0)
        {
            settings.scale = 0.0001f; // Il ne faut pas que le scale soit à 0
        }

        // Deux variable qui vont servir à dire à quelle valeur va être entre la noiseMap
        float maxLocalNoiseHeight = float.MinValue;
        float minLocalNoiseHeight = float.MaxValue;

        // Deux variable qui vont servir à faire un zoom si on modifie le noise scale
        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        // Generation du noise
        for(int y = 0; y < mapHeight; y++) // Hauteur
        {
            for(int x = 0; x < mapWidth; x++) // Longueur
            {
                amplitude = 1;
                frequency = 1;
                float noiseHeight = 0; // Permet de garder la dernière valeur attribué à noiseHeight

                for (int i = 0; i < settings.octaves; i++) // Nombre Octaves
                {
                    float sampleX = (x - halfWidth + octaveOffsets[i].x) / settings.scale * frequency; // Pour ne pas avoir toujours la même longueur, les offsets serviront à décaler un peu les valeurs de la noiseMap
                    float sampleY = (y - halfHeight + octaveOffsets[i].y) / settings.scale * frequency; // Plus la fréquence sera élevé, plus la hauteur de la map va changer rapidement

                    // Perlin noise
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1; // Entre -1 et 1
                    // Attribuer valeur perlin noise * amplitude à chaque octave
                    noiseHeight += perlinValue * amplitude;
                    // On va changer l'amplitude à chaque octave (un octave par noiseMap)
                    amplitude *= settings.persitance; // Persistance entre 0 et 1, il faut faire en sorte que plus le nombre d'octave est elevé, moins il y a de détails
                    frequency *= settings.lacunarity; // A chaque octave supplémentaire, la fréquence va augmenter, lacunarity toujours supérieur à 1
                }

                // Si la noiseHeight est supérieur au chiffre indiqué, on la fait redescendre et on met la valeur maximum autorisé
                if(noiseHeight > maxLocalNoiseHeight)
                {
                    maxLocalNoiseHeight = noiseHeight;
                }
                // Si la noiseHeight est inférieur au chiffre indiqué, on la fait monter et on met la valeur minimum autorisé
                if (noiseHeight < minLocalNoiseHeight)
                {
                    minLocalNoiseHeight = noiseHeight;
                }

                // Envoi de la valeur perlinNoise dans NoiseMap
                noiseMap[x, y] = noiseHeight;

                if (settings.normalizeMode == NormalizeMode.Global) // Que si c'est en global
                {
                    float normalizedHeight = (noiseMap[x, y] + 1) / (maxPossibleHeight / 0.9f); // Hauteur normalisé
                    noiseMap[x, y] = Mathf.Clamp(normalizedHeight, 0, int.MaxValue); // On met la hauteur normalisé pour la noiseMap pour réduire les écarts entre les chunks et on la clamp pour retourner un nombre entre 0 et la valeur max
                }
            }
        }
        if (settings.normalizeMode == NormalizeMode.Local) { // Si alignement pour terrain local
            // On normalise les valeurs de la noiseMap avec les valeurs maximum et minimum
            for (int y = 0; y < mapHeight; y++) // Hauteur
            {
                for (int x = 0; x < mapWidth; x++) // Longueur
                {
                    // Normalisation de la noiseMap entre 0 et 1 relative au minimum et au maximum 
                    noiseMap[x, y] = Mathf.InverseLerp(minLocalNoiseHeight, maxLocalNoiseHeight, noiseMap[x, y]);
                }
            }
        }
                // Et on retourne la noiseMap
                return noiseMap;
    }
}

// Options du noise
[System.Serializable]
public class NoiseSettings
{
    public Noise.NormalizeMode normalizeMode;

    public float scale = 50;

    public int octaves = 6;
    [Range(0, 1)]
    public float persitance = 0.6f;
    public float lacunarity = 2;

    public int seed;
    public Vector2 offset;

    // Vérifie si les valeurs sont bien dans la bonne ranges
    public void ValidateValues()
    {
        scale = Mathf.Max(scale, 0.01f); // Si l'échelle est en dessous de 0.01, elle sera ramené à ce nombre
        octaves = Mathf.Max(octaves, 1); // Si les octaves sont en dessous de 1, elles seront ramenés à ce nombre
        lacunarity = Mathf.Max(lacunarity, 1f); // Si la lacunarité est en dessous de 1, elle sera ramené à ce nombre
        persitance = Mathf.Clamp01(persitance); // Si la persistance est en dessous de 1, elle sera ramené à ce nombre
    }
}
