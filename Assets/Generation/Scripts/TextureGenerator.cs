using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generation de la texture de la colorMap ou noiseMap, HeightMap
public static class TextureGenerator
{
    public static Texture2D TextureFromColourMap(Color[] colourMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height); // Initialisation de la texture
        texture.filterMode = FilterMode.Point; // Applique un filtre à la texture
        texture.wrapMode = TextureWrapMode.Clamp; // Change le WrapMode
        texture.SetPixels(colourMap); // On applique tous les pixels de la colourMap à la texture
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(HeightMap heightMap)
    {
        int width = heightMap.values.GetLength(0); // Longueur map
        int height = heightMap.values.GetLength(1); // Hauteur map

        Color[] colourMap = new Color[width * height]; // On créer un tableau de taille suffisante pour contenir toute les couleurs de la HeightMap

        // Parcours du tableau et attribution d'une couleur entre le noir et le blanc pour chaque partie du tableau
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, Mathf.InverseLerp(heightMap.minValues, heightMap.maxValues, heightMap.values[x, y])); // Couleur entre le noir et le blanc interpolé par chaque pixel de noiseMap
            }
        }

        // On applique tous les pixels de la HeightMap à la texture
        return TextureFromColourMap(colourMap, width, height);
    }
}
