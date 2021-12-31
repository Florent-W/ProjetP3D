using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bordure des chunks pour mettre de l'eau autour des chunks par exemple, on soustrait le falloff à la hauteur de la map pour créer une belle ile
public static class FallofGenerator
{
    // Regarde pour chaque coordonnées si x ou y est proche de la fallofmap
    public static float[,] GenerateFalloffMap(int size)
    {
        float[,] map = new float[size, size]; // La taille de la map sans falloff

        // Parcours de chaque coordonnées de la map
        for(int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                float x = i / (float)size * 2 - 1; // Valeur entre -1 et 1
                float y = j / (float)size * 2 - 1; // Valeur entre -1 et 1

                float value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y)); // Il faut savoir si x ou y est la plus proche de 1, 1 est le plus proche de la fallofmap, bordure chunk
                map[i, j] = Evaluate(value); // On indique au coordonnées de la map quelle valeur de x ou y est la plus proche de la fallofmap et on applique la courbe pour ne pas que la fallofMap soit toujours autour du centre et pour avoir une taille de fallofMap un peu moins grande
            }
        }
        return map;
    }

    // Va servir pour ne pas que la fallofMap soit toujours autour du centre
    static float Evaluate(float value)
    {
        float a = 3f;
        float b = 2.2f;

        // Formule mathématique pour que la courbe fallOfMap soit plus varié et pas toujours autours centre
        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
    }
}
