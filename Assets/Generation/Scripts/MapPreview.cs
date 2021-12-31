using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Affichage de la map (Recupère le tableau avec le perlin noise et le transforme en texture pour le terrain)
public class MapPreview : MonoBehaviour
{
    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public enum DrawMode { NoiseMap, Mesh, FalloffMap }
    public DrawMode drawMode;

    public MeshSettings meshSettings;
    public HeightMapSettings heightMapSettings;
    public TextureData textureData;

    public Material terrainMaterial;

    [Range(0, MeshSettings.numSupportedLODs - 1)]
    public int editorPreviewLOD;

    public bool autoUpdate;

    // Dessine la map dans l'éditeur
    public void DrawMapInEditor()
    {
        // On applique le material
        textureData.ApplyToMaterial(terrainMaterial);
        textureData.UpdateMeshHeights(terrainMaterial, heightMapSettings.minHeight, heightMapSettings.maxHeight); // On met à jour la hauteur des mesh
        HeightMap HeightMap = HeightMapGenerator.GenerateHeightMap(meshSettings.numVertsPerLine, meshSettings.numVertsPerLine, heightMapSettings, Vector2.zero); // On génère les informations de la map

        // Dépend du mode, on dessine selon si on a choisi la noiseMap ou la colorMap ou le mesh ou si on veut la fallofMap, la même chose pour la couleur
        if (drawMode == DrawMode.NoiseMap)
        {
            DrawTexture(TextureGenerator.TextureFromHeightMap(HeightMap)); // Envoi de la texture de la noiseMap dans l'affichage
        }
        else if (drawMode == DrawMode.Mesh)
        {
            DrawMesh(MeshGenerator.GenerateTerrainMesh(HeightMap.values, meshSettings, editorPreviewLOD)); // Envoi du mesh dans l'affichage
        }
        else if (drawMode == DrawMode.FalloffMap)
        {
            DrawTexture(TextureGenerator.TextureFromHeightMap(new HeightMap(FallofGenerator.GenerateFalloffMap(meshSettings.numVertsPerLine), 0, 1))); // Envoi de la texture map avec le fallof, bordure dans l'affichage
        }
    }

    // On dessine la texture de la map à l'aide d'une texture2D générer
    public void DrawTexture(Texture2D texture)
    {
        // Permet de mettre la texture sur le terrain sans avoir à démmarer l'application et donc on pourra voir les changements dans l'éditeur
        textureRender.sharedMaterial.mainTexture = texture;
        // Application de la taille de la texture sur la textureRenderer pour que le rendu est la bonne taille
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height) / 10f;
        // Active la texture object
        textureRender.gameObject.SetActive(true);
        // Desactive le meshFilter
        meshFilter.gameObject.SetActive(false);
    }

    // On dessine le mesh à partir d'un meshData
    public void DrawMesh(MeshData meshData)
    {
        meshFilter.sharedMesh = meshData.CreateMesh(); // On créer le mesh en tant que mesh partagé
        // Désactive la texture object
        textureRender.gameObject.SetActive(false);
        // Active le meshFilter
        meshFilter.gameObject.SetActive(true);
    }

    // Quand les valeurs dans l'éditeur sont changés
    void OnValuesUpdated()
    {
        if (!Application.isPlaying) // SI le jeu n'est pas lancé, on pourra dessiné la map dans l'éditeur et mettre à jour ses valeurs
        {
            DrawMapInEditor();
        }
    }

    // Quand les valeurs des paramètres des textures sont changés
    void OnTextureValuesUpdated()
    {
        textureData.ApplyToMaterial(terrainMaterial); // On applique la texture au matérial du terrain
    }

    // Lorsqu'une variable est changé, on génère la fallofmap
    void OnValidate()
    {
        if (meshSettings != null) // Si il y a un asset terrain, on met les valeurs
        {
            meshSettings.OnValuesUpdated -= OnValuesUpdated;
            meshSettings.OnValuesUpdated += OnValuesUpdated; // On s'abonne
        }
        if (heightMapSettings != null) // Si il y a un asset noise, on met les valeurs
        {
            heightMapSettings.OnValuesUpdated -= OnValuesUpdated;
            heightMapSettings.OnValuesUpdated += OnValuesUpdated;
        }
        if (heightMapSettings != null) // Si il y a un asset texture, on met les valeurs
        {
            textureData.OnValuesUpdated -= OnTextureValuesUpdated;
            textureData.OnValuesUpdated += OnTextureValuesUpdated;
        }
    }
}
