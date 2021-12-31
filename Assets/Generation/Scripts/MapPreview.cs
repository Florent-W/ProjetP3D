using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Affichage de la map (Recup�re le tableau avec le perlin noise et le transforme en texture pour le terrain)
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

    // Dessine la map dans l'�diteur
    public void DrawMapInEditor()
    {
        // On applique le material
        textureData.ApplyToMaterial(terrainMaterial);
        textureData.UpdateMeshHeights(terrainMaterial, heightMapSettings.minHeight, heightMapSettings.maxHeight); // On met � jour la hauteur des mesh
        HeightMap HeightMap = HeightMapGenerator.GenerateHeightMap(meshSettings.numVertsPerLine, meshSettings.numVertsPerLine, heightMapSettings, Vector2.zero); // On g�n�re les informations de la map

        // D�pend du mode, on dessine selon si on a choisi la noiseMap ou la colorMap ou le mesh ou si on veut la fallofMap, la m�me chose pour la couleur
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

    // On dessine la texture de la map � l'aide d'une texture2D g�n�rer
    public void DrawTexture(Texture2D texture)
    {
        // Permet de mettre la texture sur le terrain sans avoir � d�mmarer l'application et donc on pourra voir les changements dans l'�diteur
        textureRender.sharedMaterial.mainTexture = texture;
        // Application de la taille de la texture sur la textureRenderer pour que le rendu est la bonne taille
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height) / 10f;
        // Active la texture object
        textureRender.gameObject.SetActive(true);
        // Desactive le meshFilter
        meshFilter.gameObject.SetActive(false);
    }

    // On dessine le mesh � partir d'un meshData
    public void DrawMesh(MeshData meshData)
    {
        meshFilter.sharedMesh = meshData.CreateMesh(); // On cr�er le mesh en tant que mesh partag�
        // D�sactive la texture object
        textureRender.gameObject.SetActive(false);
        // Active le meshFilter
        meshFilter.gameObject.SetActive(true);
    }

    // Quand les valeurs dans l'�diteur sont chang�s
    void OnValuesUpdated()
    {
        if (!Application.isPlaying) // SI le jeu n'est pas lanc�, on pourra dessin� la map dans l'�diteur et mettre � jour ses valeurs
        {
            DrawMapInEditor();
        }
    }

    // Quand les valeurs des param�tres des textures sont chang�s
    void OnTextureValuesUpdated()
    {
        textureData.ApplyToMaterial(terrainMaterial); // On applique la texture au mat�rial du terrain
    }

    // Lorsqu'une variable est chang�, on g�n�re la fallofmap
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
