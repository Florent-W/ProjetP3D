using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Génération de mesh
public static class MeshGenerator
{
    // Générer mesh du terrain à partir de la HeightMap ainsi qu'avec la longueur et la hauteur de la map et la multiplicateur de hauteur pour ajouter plus de relief ainsi qu'une courbe qui va définir à quelle hauteur on peut appliquer le multiplicateur, le niveau de detail de la map ainsi qu'une option pour le flatshading
    public static MeshData GenerateTerrainMesh(float[,] heightmap, MeshSettings meshSettings, int levelOfDetail)
    {
        int skipIncrement = (levelOfDetail == 0) ? 1 : levelOfDetail * 2; // Pour simplifier le niveau de detail car Unity possède 12 niveau de détails mais pour aller plus vite on va aller jusqu'à 6 et multiplier par 2, on vérifie avec une condition que le niveau de détail n'est pas égal à 0, sinon il sera à 1
        int numVertsPerLine = meshSettings.numVertsPerLine; // Nombre de vertices

        Vector2 topLeft = new Vector2(-1, 1) * meshSettings.meshWorldSize / 2f; // Vertex tout à gauche en longueur et hauteur

        MeshData meshData = new MeshData(numVertsPerLine, skipIncrement, meshSettings.useFlatShading); // Initialisation des données du mesh avec la taille et la hauteur de la HeightMap et les vertices

        int[,] vertexIndicesMap = new int[numVertsPerLine, numVertsPerLine]; // Indices des vertex contenant un tableau allant jusqu'au bordure des map
        int meshVertexIndex = 0;
        int outOfMeshVertexIndex = -1;

        // Parcours de la hauteur et de la longueur de la heightmap avec prise en compte du niveau de détails qui multiplie par le nombre de fois en plus le niveau de détails
        for (int y = 0; y < numVertsPerLine; y++)
        {
            for (int x = 0; x < numVertsPerLine; x++)
            {
                bool isOutOfMeshVertex = y == 0 || y == numVertsPerLine - 1 || x == 0 || x == numVertsPerLine - 1; // On teste si le vertex sur lequel on est en dehors de la taille de la map et donc dans la bordure
                bool isSkippedVertex = x > 2 && x < numVertsPerLine - 3 && y > 2 && y < numVertsPerLine - 3 && ((x-2)%skipIncrement != 0 || (y-2)%skipIncrement != 0); // Est ce que l'on passe le vertex car le vertex fait partie de la bordure

                if (isOutOfMeshVertex) // Si c'est un vertex de la bordure
                {
                    vertexIndicesMap[x, y] = outOfMeshVertexIndex; // On l'ajoute dans le tableau d'indice des vertex
                    outOfMeshVertexIndex--;
                }
                else if(!isSkippedVertex) // Si ce n'esrt pas dans la bordure et si ce n'est pas un main vertex, alors c'est dans le mesh de la map
                {
                    vertexIndicesMap[x, y] = meshVertexIndex;
                    meshVertexIndex++;
                }
            }
        }

                // Parcours de la hauteur et de la longueur de la heightmap avec prise en compte du niveau de détails qui multiplie par le nombre de fois en plus le niveau de détails
                for (int y = 0; y < numVertsPerLine; y++)
        {
            for (int x = 0; x < numVertsPerLine; x++)
            {
                bool isSkippedVertex = x > 2 && x < numVertsPerLine - 3 && y > 2 && y < numVertsPerLine - 3 && ((x - 2) % skipIncrement != 0 || (y - 2) % skipIncrement != 0); // Est ce que l'on passe le vertex car le vertex fait partie de la bordure

                if (!isSkippedVertex)
                {
                    bool isOutOfMeshVertex = y == 0 || y == numVertsPerLine - 1 || x == 0 || x == numVertsPerLine - 1; // On teste si le vertex sur lequel on est en dehors de la taille de la map et donc dans la bordure
                    bool isMeshEdgeVertex = (y == 1 || y == numVertsPerLine - 2 || x == 1 || x == numVertsPerLine - 2) && !isOutOfMeshVertex; // Vertex sur les bords
                    bool isMainVertex = (x - 2) % skipIncrement == 0 && (y - 2) % skipIncrement == 0 && !isOutOfMeshVertex && !isMeshEdgeVertex; // main vertex
                    bool isEdgeConnectionVertex = (y == 2 || y == numVertsPerLine - 3 || x == 2 || x == numVertsPerLine - 3) && !isOutOfMeshVertex && !isMeshEdgeVertex && !isMainVertex; // Vertex de connection des bords

                    int vertexIndex = vertexIndicesMap[x, y]; // L'élément ou l'on est dans les vertex
                    Vector2 percent = new Vector2(x-1, y-1) / (numVertsPerLine - 3); // Stocke les uvMap du vertex avec un pourcentage, entre 0 et 1
                    Vector2 vertexPosition2D = topLeft + new Vector2(percent.x, -percent.y) * meshSettings.meshWorldSize; // Position du vertex
                    float height = heightmap[x, y]; // Hauteur

                    if(isEdgeConnectionVertex) // Si le vertex est un vertex de connexion
                    {
                        bool isVertical = x == 2 || x == numVertsPerLine - 3;
                        int dstToMainVertexA = ((isVertical)?y - 2:x-2) % skipIncrement; // Distance au VertexA
                        int dstToMainVertexB = skipIncrement - dstToMainVertexA; // Distance au VertexB
                        float dstPercentFromAToB = dstToMainVertexA / (float)skipIncrement; // Distance pourcentage entre A et B

                        float heightMainVertexA = heightmap[(isVertical)? x : x - dstToMainVertexA, (isVertical) ? y - dstToMainVertexA:y]; // Hauteur du vertex A
                        float heightMainVertexB = heightmap[(isVertical) ? x : x + dstToMainVertexB, (isVertical) ? y + dstToMainVertexB : y]; // Hauteur du vertex B

                        height = heightMainVertexA * (1 - dstPercentFromAToB) + heightMainVertexB * dstPercentFromAToB;
                    }

                    meshData.AddVertex(new Vector3(vertexPosition2D.x, height, vertexPosition2D.y), percent, vertexIndex); // On ajoute les vertex

                    bool createTriangle = x < numVertsPerLine - 1 && y < numVertsPerLine - 1 && (!isEdgeConnectionVertex || (x != 2 && y != 2));

                    // Permet d'ajouter les triangles que si les vertex ne sont pas sur les côtés, en haut et en bas
                    if (createTriangle)
                    {
                        int currentIncrement = (isMainVertex && x != numVertsPerLine - 3 && y != numVertsPerLine - 3) ? skipIncrement : 1;

                        int a = vertexIndicesMap[x, y]; // Les différents points du carré du vertex, deux triangles assemblés
                        int b = vertexIndicesMap[x + currentIncrement, y];
                        int c = vertexIndicesMap[x, y + currentIncrement];
                        int d = vertexIndicesMap[x + currentIncrement, y + currentIncrement];

                        meshData.AddTriangle(a, d, c); // Ajoute le triangle dans le meshData avec les trois sommets, premier triangle, il y a besoin de deux triangles pour le carré du pixel
                        meshData.AddTriangle(d, a, b); // Ajoute le triangle dans le meshData avec les trois sommets, deuxième triangle
                    }
                }
            }
        }
        meshData.ProcessMesh(); // On bake les normals du mesh là en les calculants pour dépenser moins de ressource car generateterrainmesh est en dehors du thread principal

        return meshData;
    }
}

// Informations mesh
public class MeshData
{
    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs; // uvMap pour pouvoir appliquer une texture à la map
    Vector3[] bakedNormals;

    Vector3[] outOfMeshVertices; // Les vertices de la bordure, plus loin que le mesh de la map
    int[] outOfMeshTriangles; // Les triangles de la bordure

    int triangleIndex;
    int outOfMeshTriangleIndex;

    bool useFlatShading; // Utilise l'option flatShading pour voir plus les polygones

    // Définit les informations avec les vertices, vertex et triangles, uvMap avec les différentes formules pour les retrouver
    public MeshData(int numVertsPerLine, int skipIncrement, bool useFlatShading)
    {
        this.useFlatShading = useFlatShading;

        int numMeshEdgeVertices = (numVertsPerLine - 2) * 4 - 4; // Nombre de vertices proches du contour
        int numEdgeConnectionVertices = (skipIncrement - 1) * (numVertsPerLine - 5) / skipIncrement * 4; // Nombre de vertices de connection
        int numMainVerticesPerLine = (numVertsPerLine - 5) / skipIncrement + 1; // Nombre de vertices par ligne
        int numMainVertices = numMainVerticesPerLine * numMainVerticesPerLine; // Nombre de vertices

        vertices = new Vector3[numMeshEdgeVertices + numEdgeConnectionVertices + numMainVertices]; // Tous les vertices
        uvs = new Vector2[vertices.Length];

        int numMeshEdgeTriangles = 8 * (numVertsPerLine - 4); // Nombre de triangle au bord du mesh
        int numMainTriangles = (numMainVerticesPerLine-1) * (numMainVerticesPerLine - 1) * 2; // Nombre de triangle
        triangles = new int[(numMeshEdgeTriangles + numMainTriangles) * 3]; // tous les triangles

        outOfMeshVertices = new Vector3[numVertsPerLine * 4 - 4]; // On définit les vertices des bordures
        outOfMeshTriangles = new int[24 * (numVertsPerLine - 2)]; // Et les triangles de bordures
    }

    // Ajoute un vertex soit aux bordures soit au vertices
    public void AddVertex(Vector3 vertexPosition, Vector2 uv, int vertexIndex)
    {
        // On vérifie si le vertex fait parti de la bordure
        if(vertexIndex < 0)
        {
            outOfMeshVertices[-vertexIndex - 1] = vertexPosition;
        }
        else
        {
            vertices[vertexIndex] = vertexPosition;
            uvs[vertexIndex] = new Vector2(0, vertexPosition.y);
        }
    }

    // Ajoute triangle à un array
    public void AddTriangle(int a, int b, int c)
    {
        // On vérifie pour chacun des vertex si ils font partie de la bordure
        if (a < 0 || b < 0 || c < 0)
        {
            // Si il y en a un moins un, c'est qu'il fait partie de la bordure
            outOfMeshTriangles[outOfMeshTriangleIndex] = a; // On place le premier sommet du triangle de la bordure dans l'array
            outOfMeshTriangles[outOfMeshTriangleIndex + 1] = b; // Le deuxième
            outOfMeshTriangles[outOfMeshTriangleIndex + 2] = c;
            outOfMeshTriangleIndex += 3; // Pour dire qu'on avance de 3 dans l'array triangle vu qu'on vient de mettre trois sommet dans l'array
        }
        else // Sinon les vertex font partie du mesh
        {
            triangles[triangleIndex] = a; // On place le premier sommet du triangle dans l'array
            triangles[triangleIndex + 1] = b; // Le deuxième
            triangles[triangleIndex + 2] = c;
            triangleIndex += 3; // Pour dire qu'on avance de 3 dans l'array triangle vu qu'on vient de mettre trois sommet dans l'array
        }
    }

    // Notre propre méthode pour calculer les normals des mesh car les normales changent entre les chunks et donc non pas la même luminosité, on corrige ça avec la méthode 
    Vector3[] CalculateNormals()
    {
        Vector3[] vertexNormals = new Vector3[vertices.Length]; // Les vertex des différentes normals, positions
        int triangleCount = triangles.Length / 3; // Un triangle est composé de trois vertex

        // Parcours des triangles
        for (int i = 0; i < triangleCount; i++)
        {
            int normalTriangleIndex = i * 3;
            // On récupère les différents vertex qui compose le triangle travaillé
            int vertexIndexA = triangles[normalTriangleIndex];
            int vertexIndexB = triangles[normalTriangleIndex + 1];
            int vertexIndexC = triangles[normalTriangleIndex + 2];

            Vector3 triangleNormal = SurfaceNormalFromIndices(vertexIndexA, vertexIndexB, vertexIndexC); // On récupère la normal du triangle à partir des trois vertex
            vertexNormals[vertexIndexA] += triangleNormal; // On ajoute la normal du triangle pour chaque vertex
            vertexNormals[vertexIndexB] += triangleNormal;
            vertexNormals[vertexIndexC] += triangleNormal;
        }

        int borderTriangleCount = outOfMeshTriangles.Length / 3; // Un triangle est composé de trois vertex

        // Parcours des triangles de la bordure
        for (int i = 0; i < borderTriangleCount; i++)
        {
            int normalTriangleIndex = i * 3;
            // On récupère les différents vertex qui compose le triangle travaillé
            int vertexIndexA = outOfMeshTriangles[normalTriangleIndex];
            int vertexIndexB = outOfMeshTriangles[normalTriangleIndex + 1];
            int vertexIndexC = outOfMeshTriangles[normalTriangleIndex + 2];

            Vector3 triangleNormal = SurfaceNormalFromIndices(vertexIndexA, vertexIndexB, vertexIndexC); // On récupère la normal du triangle à partir des trois vertex

            // Il faut vérifier si les vertexIndex sont égal ou supérieur à 0
            if (vertexIndexA >= 0)
            {
                vertexNormals[vertexIndexA] += triangleNormal; // On ajoute la normal du triangle pour chaque vertex
            }
            if (vertexIndexB >= 0)
            {
                vertexNormals[vertexIndexB] += triangleNormal;
            }
            if (vertexIndexC >= 0)
            {
                vertexNormals[vertexIndexC] += triangleNormal;
            }
        }

        // On normalise chaque normal
        for (int i = 0; i < vertexNormals.Length; i++)
        {
            vertexNormals[i].Normalize();
        }

        return vertexNormals;
    }

    // Permet de récupérer la normal d'un triangle
    Vector3 SurfaceNormalFromIndices(int indexA, int indexB, int indexC)
    {
        Vector3 pointA = (indexA < 0)? outOfMeshVertices[-indexA-1] : vertices[indexA]; // On vérifie pour chacun des points si ils sont les index sont inférieur à zéro
        Vector3 pointB = (indexB < 0) ? outOfMeshVertices[-indexB - 1] : vertices[indexB];
        Vector3 pointC = (indexC < 0) ? outOfMeshVertices[-indexC - 1] : vertices[indexC];

        // Produit vectoriel
        Vector3 sideAB = pointB - pointA;
        Vector3 sideAC = pointC - pointA;
        return Vector3.Cross(sideAB, sideAC).normalized; // On retourne le produit vectoriel normalisé pour la normal
    }

    // Méthode pour préparer les mesh et lui passer quelques arguments
    public void ProcessMesh()
    {
        if (useFlatShading) // Si on active l'option, on utilise le flatshading
        {
            FlatShading();
        }
        else // Sinon on utilise la méthode normal
        {
            BakeNormals();
        }
    }

    // On bake les normals pour la lumière de la map
    void BakeNormals()
    {
        bakedNormals = CalculateNormals(); // On le fait en recalculant les normals du mesh pour ne pas avoir de problèmes de luminosités
    }

    // On créer le flashshading pour avoir des polygones plus apparent sur la map si on veut sur la map
    public void FlatShading()
    {
        Vector3[] flatShadedVertices = new Vector3[triangles.Length]; // Les vertices pour le flatshading
        Vector2[] flatShadedUvs = new Vector2[triangles.Length]; // Les uv pour le flatshading

        // Parcours des triangles
        for (int i = 0; i < triangles.Length; i++)
        {
            flatShadedVertices[i] = vertices[triangles[i]]; // On stocke les vertices des triangles;
            flatShadedUvs[i] = uvs[triangles[i]]; // On stocke les uv des triangles;
            triangles[i] = i; // On met à jour l'élément i
        }

        vertices = flatShadedVertices; // On passe les vertices et uv en flatshaded
        uvs = flatShadedUvs;
    }

    // Retourne le Mesh qu'on a créer
    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh(); // Initialisation du mesh
        // Assignation des variables du mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        if (useFlatShading)
        {
            mesh.RecalculateNormals(); // Si on utilise la flatshading, on doit recalculer les normals
        }
        else // Sinon on assigne directement les normals baked
        {
            // On assigne les normals du mesh pour ne pas avoir de problèmes de luminosités
            mesh.normals = bakedNormals;
        }
       
        return mesh;
    }
}
