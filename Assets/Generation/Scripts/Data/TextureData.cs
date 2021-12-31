using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Les param�tres des textures de la map
[CreateAssetMenu(menuName = "Generation/Texture Data")]
public class TextureData : UpdatableData
{
    const int textureSize = 512; // Taille des textures
    const TextureFormat textureFormat = TextureFormat.RGB565; // Format des textures

    public Layer[] layers; // Les layers des textures
    /* public Color[] baseColors; // Couleurs par d�faut des textures
    [Range(0,1)]
    public float[] baseStartHeights; // Hauteur par d�faut des textures
    [Range(0,1)]
    public float[] baseBlends; // Blend par d�faut des textures */

    float savedMinHeight; // Si il y a un probl�me, on prend les derni�res valeurs indiqu�s
    float savedMaxHeight;

    // On appliquera la texture au material du gameobject
    public void ApplyToMaterial(Material material)
    {
        material.SetInt("layerCount", layers.Length); // Nombre de layer
        material.SetColorArray("baseColours", layers.Select(x => x.tint).ToArray()); // On met les couleurs par d�faut sur le material
        material.SetFloatArray("baseStartHeights", layers.Select(x => x.startHeight).ToArray()); // On met les hauteurs par d�faut des textures
        material.SetFloatArray("baseBlends", layers.Select(x => x.blendStrenght).ToArray()); // On met les blends par d�faut des textures
        material.SetFloatArray("baseCoulourStrength", layers.Select(x => x.tintStrenght).ToArray()); // On met la force de la teinte blends par d�faut des textures
        material.SetFloatArray("baseTextureScales", layers.Select(x => x.textureScale).ToArray()); // On met l'�chelle des textures par d�faut des textures
        Texture2DArray texturesArray = GenerateTextureArray(layers.Select(x => x.texture).ToArray()); // On cr�er un tableau de texture
        material.SetTexture("baseTextures", texturesArray); // On applique une texture

        UpdateMeshHeights(material, savedMinHeight, savedMaxHeight);
    }

    // Met � jour la hauteur d'un mesh avec la hauteur minimum et maximum
    public void UpdateMeshHeights(Material material, float minHeight, float maxHeight)
    {
        savedMinHeight = minHeight;
        savedMaxHeight = maxHeight;

        material.SetFloat("minHeight", minHeight); // On ajoute les variables
        material.SetFloat("maxHeight", maxHeight);
    }

    // G�n�re un tableau de texture
    Texture2DArray GenerateTextureArray(Texture2D[] textures)
    {
        Texture2DArray textureArray = new Texture2DArray(textureSize, textureSize, textures.Length, textureFormat, true); // Initialisation de la textures
        for(int i = 0; i < textures.Length; i++)
        {
            textureArray.SetPixels(textures[i].GetPixels(), i); // On ajoute les pixels des textures dans le tableau
        }
        textureArray.Apply();
        return textureArray;
    }

    // Layer des textures
    [System.Serializable]
    public class Layer
    {
        public Texture2D texture;
        public Color tint; // La teinte
        [Range(0, 1)]
        public float tintStrenght; // Force de la teinte
        [Range(0, 1)]
        public float startHeight; // Commencement de la hauteur du layer
        [Range(0, 1)]
        public float blendStrenght; // Force des blend
        public float textureScale; // Force de la texture
    }
}
