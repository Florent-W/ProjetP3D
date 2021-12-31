using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Generation/Mesh Settings")]
public class MeshSettings : UpdatableData
{
    public const int numSupportedLODs = 5; // Nombre de niveau de d�tail support�
    public const int numSupportedChunkSizes = 9; // Nombre de diff�rents tailles de chunk support�
    public const int numSupportedFlatshadedChunkSizes = 3; // Nombre de diff�rents tailles de chunk avec flatshading support�
    public static readonly int[] supportedChunkSizes = { 48, 72, 96, 120, 144, 168, 192, 216, 240 }; // Taille des chunks support�s

    public float meshScale = 2.5f; // L'�chelle du mesh de la map
    public bool useFlatShading;

    [Range(0, numSupportedChunkSizes - 1)]
    public int chunkSizeIndex;
    [Range(0, numSupportedFlatshadedChunkSizes - 1)]
    public int flatshadedChunkSizeIndex;
    public GameObject waterPrefab;
    public GameObject tree;
    public GameObject tree2;
    public GameObject tree3;
    public GameObject grass;
    public GameObject nenuphar;
    public GameObject rocks;
    public GameObject maison;
    public GameObject maison2;
    public GameObject lampe;
    public GameObject fleur;
    public GameObject fleur2;
    public GameObject grassGenerator;
    public GameObject volumeUnderwater;
    public PersonnagesDonnees personnagesDonnees;

    // Retourne la taille des chunks, nombre de vertices par lignes de mesh rendu au plus haut niveau de d�tail, inclut les deux extra vertices qui sont exclus du mesh final mais qui sont utilis� pour calculer les normales
    public int numVertsPerLine
    {
        get
        {
            return supportedChunkSizes[(useFlatShading) ? flatshadedChunkSizeIndex : chunkSizeIndex] + 5; // On regarde si on veut la taille des chunks avec flatshading ou non
        }
    }

    // Retourne la taille du mesh de la map avec l'�checelle
    public float meshWorldSize
    {
        get
        {
            return (numVertsPerLine - 3) * meshScale;
        }
    }
}
