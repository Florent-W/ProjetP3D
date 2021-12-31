using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

// Informations des chunks
public class TerrainChunk
{
    const float colliderGenerationDistanceThreshold = 5;
    public event System.Action<TerrainChunk, bool> onVisibilityChanged;
    public Vector2 coord;

    GameObject meshObject;
    Vector2 sampleCentre;
    public Bounds bounds;

    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
    MeshCollider meshCollider;

    LODInfo[] detailLevels;
    LODMesh[] lodMeshes;
    int colliderLODIndex;

    public HeightMap heightMap;
    bool heightMapReceived;
    int previousLODIndex = -1;
    bool hasSetCollider;
    float maxViewDst;

    HeightMapSettings heightMapSettings;
    MeshSettings meshSettings;
    Transform viewer;

    NavMeshSurface meshNavMesh;

    GameObject SceneBuilder;

    public TerrainChunk(Vector2 coord, HeightMapSettings heightMapSettings, MeshSettings meshSettings, LODInfo[] detailLevels, int colliderLODIndex, Transform parent, Transform viewer, Material material)
    {
        this.coord = coord;
        this.detailLevels = detailLevels;
        this.colliderLODIndex = colliderLODIndex;
        this.heightMapSettings = heightMapSettings;
        this.meshSettings = meshSettings;
        this.viewer = viewer;

        sampleCentre = coord * meshSettings.meshWorldSize / meshSettings.meshScale;
        Vector2 position = coord * meshSettings.meshWorldSize;
        bounds = new Bounds(position, Vector2.one * meshSettings.meshWorldSize);

        meshObject = new GameObject("Terrain Chunk");
        meshObject.layer = 14;
        meshRenderer = meshObject.AddComponent<MeshRenderer>();
        meshFilter = meshObject.AddComponent<MeshFilter>();
        meshCollider = meshObject.AddComponent<MeshCollider>();
        meshRenderer.material = material;
       // GameObject meshUnderwaterProfile = GameObject.Instantiate(meshSettings.volumeUnderwater, meshObject.transform);
      // BoxCollider meshUnderwaterCollider = meshUnderwaterProfile.AddComponent<BoxCollider>();
       // meshUnderwaterCollider.isTrigger = true;
       // meshUnderwaterCollider.center = new Vector3(0, -5f, 0);
     //   meshUnderwaterCollider.size = new Vector3(708, 6f, 500);
       // PersonnageDansEau personnageDansEau = meshUnderwaterProfile.AddComponent<PersonnageDansEau>();

        meshObject.transform.position = new Vector3(position.x, 0, position.y);
        meshObject.transform.parent = parent;
        SetVisible(false);

        lodMeshes = new LODMesh[detailLevels.Length];
        for (int i = 0; i < detailLevels.Length; i++)
        {
            lodMeshes[i] = new LODMesh(detailLevels[i].lod);
            lodMeshes[i].updateCallback += UpdateTerrainChunk;
            if (i == colliderLODIndex)
            {
                lodMeshes[i].updateCallback += UpdateCollisionMesh;
            }
        }

        maxViewDst = detailLevels[detailLevels.Length - 1].visibleDstThreshold; // Distance maximum ou les chunks seront chargés selon le niveau de détail
    }

    // Chargement d'un chunk
    public void Load()
    {
        ThreadedDataRequester.RequestData(() => HeightMapGenerator.GenerateHeightMap(meshSettings.numVertsPerLine, meshSettings.numVertsPerLine, heightMapSettings, sampleCentre), OnHeightMapReceived); // On récupère les données de la heightmap
    }

    // Quand les données de la map sont reçu, on met à jour les chunks
    void OnHeightMapReceived(object heightMapObject)
    {
        this.heightMap = (HeightMap)heightMapObject;
        heightMapReceived = true;

        UpdateTerrainChunk();
    }

    // Position du personnage sur la map
    Vector2 viewerPosition
    {
        get
        {
            return new Vector2(viewer.position.x, viewer.position.z);
        }
    }

    public void SpawnAssetsOnChunkVerts(Vector3 pos, Vector2 centre, float assetOffset, Vector2 offsetMultiplier, GameObject treePrefab, Transform treeParent, float hauteurMinimum, float hauteurMaximum)
    {
        if (pos.y >= hauteurMinimum && pos.y <= hauteurMaximum)
        {
            if (offsetMultiplier.x == 0 && offsetMultiplier.y != 0)
            {
                GameObject tree0Y = Object.Instantiate(treePrefab, new Vector3(pos.x, pos.y, pos.z + assetOffset * offsetMultiplier.y), Quaternion.identity);
               // tree0Y.name = "Tree_at_X=0";
                tree0Y.transform.parent = treeParent.transform;
            }
            if (offsetMultiplier.x != 0 && offsetMultiplier.y == 0)
            {
                GameObject treeX0 = Object.Instantiate(treePrefab, new Vector3(pos.x + assetOffset * offsetMultiplier.x, pos.y, pos.z), Quaternion.identity);
                // treeX0.name = "Tree_at_Y=0";
                treeX0.transform.parent = treeParent.transform;
            }
            if (offsetMultiplier.x == 0 && offsetMultiplier.y == 0)
            {
                GameObject tree00 = Object.Instantiate(treePrefab, new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
               // tree00.name = "Tree_at_XY=0";
                tree00.transform.parent = treeParent.transform;
            }
            if (offsetMultiplier.x != 0 && offsetMultiplier.y != 0)
            {
                GameObject treeXY = Object.Instantiate(treePrefab, new Vector3(pos.x + assetOffset * offsetMultiplier.x, pos.y, pos.z + assetOffset * offsetMultiplier.y), Quaternion.identity);
              // treeXY.name = "Tree_value";
                treeXY.transform.parent = treeParent.transform;
            }
        }
    }

    public void SpawnTreeOnChunkVerts(Vector3 pos, Vector2 centre, float assetOffset, Vector2 offsetMultiplier, GameObject treePrefab, Transform treeParent)
    {
        if (pos.y >= 2.45 && pos.y <= 2.5f)
        {
            if (offsetMultiplier.x == 0 && offsetMultiplier.y != 0)
            {
                GameObject tree0Y = Object.Instantiate(treePrefab, new Vector3(pos.x, pos.y, pos.z + assetOffset * offsetMultiplier.y), Quaternion.identity);
                tree0Y.name = "Tree_at_X=0";
                tree0Y.transform.parent = treeParent.transform;
            }
            if (offsetMultiplier.x != 0 && offsetMultiplier.y == 0)
            {
                GameObject treeX0 = Object.Instantiate(treePrefab, new Vector3(pos.x + assetOffset * offsetMultiplier.x, pos.y, pos.z), Quaternion.identity);
                treeX0.name = "Tree_at_Y=0";
                treeX0.transform.parent = treeParent.transform;
            }
            if (offsetMultiplier.x == 0 && offsetMultiplier.y == 0)
            {
                GameObject tree00 = Object.Instantiate(treePrefab, new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
                tree00.name = "Tree_at_XY=0";
                tree00.transform.parent = treeParent.transform;
            }
            if (offsetMultiplier.x != 0 && offsetMultiplier.y != 0)
            {
                GameObject treeXY = Object.Instantiate(treePrefab, new Vector3(pos.x + assetOffset * offsetMultiplier.x, pos.y, pos.z + assetOffset * offsetMultiplier.y), Quaternion.identity);
                treeXY.name = "Tree_value";
                treeXY.transform.parent = treeParent.transform;
            }
        }
    }

    // Spawn asset sur les vertices
    public void SpawnWaterOnChunkVerts(Vector3 pos, Vector2 centre, float assetOffset, Vector2 offsetMultiplier, GameObject waterPrefab, Transform treeParent)
    {
        if (pos.y >= 0 && pos.y <= 5f)
        {
            if (offsetMultiplier.x == 0 && offsetMultiplier.y != 0)
            {
                GameObject tree0Y = Object.Instantiate(waterPrefab, new Vector3(pos.x, pos.y + 1, pos.z + assetOffset * offsetMultiplier.y), Quaternion.identity);
                tree0Y.name = "Eau";
                tree0Y.transform.parent = treeParent.transform;
                tree0Y.transform.localPosition = new Vector3(-53, 3, -8);
                tree0Y.transform.localScale = new Vector3(7, 1, 6);
               /* Volume volumeUnderwater = tree0Y.transform.GetChild(0).gameObject.AddComponent<Volume>();
                volumeUnderwater.isGlobal = false;
                volumeUnderwater.profile = meshSettings.volumeUnderwater.GetComponent<Volume>().profile; */
            }
            if (offsetMultiplier.x != 0 && offsetMultiplier.y == 0)
            {
                GameObject treeX0 = Object.Instantiate(waterPrefab, new Vector3(pos.x + assetOffset * offsetMultiplier.x, pos.y + 1, pos.z), Quaternion.identity);
                treeX0.name = "Eau";
                treeX0.transform.parent = treeParent.transform;
                treeX0.transform.localPosition = new Vector3(-53, 3, -8);
                treeX0.transform.localScale = new Vector3(7, 1, 6);
               /* Volume volumeUnderwater = treeX0.transform.GetChild(0).gameObject.AddComponent<Volume>();
                volumeUnderwater.isGlobal = false;
                volumeUnderwater.profile = meshSettings.volumeUnderwater.GetComponent<Volume>().profile; */
            }
            if (offsetMultiplier.x == 0 && offsetMultiplier.y == 0)
            {
                GameObject tree00 = Object.Instantiate(waterPrefab, new Vector3(pos.x, pos.y + 1, pos.z), Quaternion.identity);
                tree00.name = "Eau";
                tree00.transform.parent = treeParent.transform;
                tree00.transform.localPosition = new Vector3(-53, 3, -8);
                tree00.transform.localScale = new Vector3(7, 1, 6);
               /* Volume volumeUnderwater = tree00.transform.GetChild(0).gameObject.AddComponent<Volume>();
                volumeUnderwater.isGlobal = false;
                volumeUnderwater.profile = meshSettings.volumeUnderwater.GetComponent<Volume>().profile; */
            }
            if (offsetMultiplier.x != 0 && offsetMultiplier.y != 0)
            {
                GameObject treeXY = Object.Instantiate(waterPrefab, new Vector3(pos.x + assetOffset * offsetMultiplier.x, pos.y + 1, pos.z + assetOffset * offsetMultiplier.y), Quaternion.identity);
                treeXY.name = "Eau";
                treeXY.transform.parent = treeParent.transform;
                treeXY.transform.localPosition = new Vector3(-53, 3, -8);
                treeXY.transform.localScale = new Vector3(7, 1, 6);
              /*  Volume volumeUnderwater = treeXY.transform.GetChild(0).gameObject.AddComponent<Volume>();
                volumeUnderwater.isGlobal = false;
                volumeUnderwater.profile = meshSettings.volumeUnderwater.GetComponent<Volume>().profile; */
            }
        }
    }

    public void SpawnPokemonOnChunkVerts(Vector3 pos, Vector2 centre, float assetOffset, Vector2 offsetMultiplier, GameObject treePrefab, Transform treeParent)
    {
        ClassLibrary.Pokemon pokemon = new ClassLibrary.Pokemon(); // Classe pokémon

        // pokemon = pokemon.setRandomPokemonSeed(SceneBuilder.GetComponent<ProjetP3DScene1.main>().jeu);

        if (pos.y >= 2 && pos.y <= 2.5f)
        {
            if (offsetMultiplier.x == 0 && offsetMultiplier.y != 0)
            {
                GameObject tree0Y = Object.Instantiate(Resources.Load("Models/Pokemon/" + 1) as GameObject, new Vector3(pos.x, pos.y, pos.z + assetOffset * offsetMultiplier.y), Quaternion.identity);
                tree0Y.name = "Pokemon_at_X=0";
                tree0Y.name = tree0Y.name.Replace("(Clone)", "");
                tree0Y.transform.parent = treeParent.transform;
                tree0Y.tag = "PokemonAdverse";
            
               // InstanceGameObject.GetComponent<StatistiquesPokemon>().SetPokemon(pokemon);
            }
            if (offsetMultiplier.x != 0 && offsetMultiplier.y == 0)
            {
                GameObject treeX0 = Object.Instantiate(Resources.Load("Models/Pokemon/" + 1) as GameObject, new Vector3(pos.x + assetOffset * offsetMultiplier.x, pos.y, pos.z), Quaternion.identity);
                treeX0.name = "Pokemon_at_Y=0";
                treeX0.transform.parent = treeParent.transform;
                treeX0.tag = "PokemonAdverse";
            }
            if (offsetMultiplier.x == 0 && offsetMultiplier.y == 0)
            {
                GameObject tree00 = Object.Instantiate(Resources.Load("Models/Pokemon/" + 1) as GameObject, new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
                tree00.name = "Pokemon_at_XY=0";
                tree00.transform.parent = treeParent.transform;
                tree00.tag = "PokemonAdverse";
            }
            if (offsetMultiplier.x != 0 && offsetMultiplier.y != 0)
            {
                GameObject treeXY = Object.Instantiate(Resources.Load("Models/Pokemon/" + 1) as GameObject, new Vector3(pos.x + assetOffset * offsetMultiplier.x, pos.y, pos.z + assetOffset * offsetMultiplier.y), Quaternion.identity);
                treeXY.name = "Pokemon_value";
                treeXY.transform.parent = treeParent.transform;
                treeXY.tag = "PokemonAdverse";
            }

        }
    }

    public void SpawnPokemon(NavMeshSurface navMeshSurface, Transform parent, Transform positionPersonnage)
    {
        ClassLibrary.Pokemon pokemon = new ClassLibrary.Pokemon(); // Classe pokémon

        pokemon = pokemon.setRandomPokemonSeed(SceneBuilder.GetComponent<ProjetP3DScene1.main>().jeu);

        Vector3 positionHerbeCollider = positionPersonnage.position; // On récupère la position de l'herbe
                                                                     // Vector3 sizeHerbeCollider = navMeshSurface.gameObject.GetComponent<ChunkBaker>().NavMeshSize; // On récupère la taille de l'herbe
        Vector3 sizeHerbeCollider = new Vector3(150, 0, 150);

        float itemXSpread = sizeHerbeCollider.x / 2;
        float itemYSpread = 5;
        float itemZSpread = sizeHerbeCollider.z / 2;
        // Vector3 positionPokemon = new Vector3(randPosition.x, randPosition.y, randPosition.z);

        int nbPokemon = 3; // Nombre de pokémon à faire spawn
        for (int i = 0; i < nbPokemon; i++)
        {
            Vector3 randPosition = new Vector3(Random.Range(-itemXSpread, itemXSpread), Random.Range(-itemYSpread, itemYSpread), Random.Range(-itemZSpread, itemZSpread)) + positionHerbeCollider;
            NavMeshHit hit;

            NavMesh.SamplePosition(randPosition, out hit, 500, NavMesh.AllAreas);

            GameObject pokemonGameObject = Object.Instantiate(Resources.Load("Models/Pokemon/" + pokemon.getNoIdPokedex()) as GameObject, hit.position, Quaternion.identity, parent.transform);
            pokemonGameObject.name = pokemon.getNom();
            pokemonGameObject.tag = "PokemonAdverse";

            StatistiquesPokemon statistiquesPokemon = pokemonGameObject.AddComponent<StatistiquesPokemon>();
            statistiquesPokemon.SetPokemon(pokemon); // Statistiques du pokemon

            NavMeshAgent navMeshAgent = pokemonGameObject.AddComponent<NavMeshAgent>();

            if (pokemon.getType() == "Vol") { // Si c'est un pokémon qui peut voler, on le met plus haut
                navMeshAgent.baseOffset = 2.1f;
            }
            else
            {
                navMeshAgent.baseOffset = 0;
            }


            PatrouilleMouvement patrouilleMouvementScript = pokemonGameObject.AddComponent<PatrouilleMouvement>();

            // Ajoute les points de direction vers lesquels les personnages vont aller
            GameObject point = new GameObject("Point");
            point.transform.parent = pokemonGameObject.transform;
            point.transform.localPosition = new Vector3(2.88f, 1.68f, 6.41f);
            GameObject point2 = new GameObject("Point2");
            point2.transform.parent = pokemonGameObject.transform;
            point2.transform.localPosition = new Vector3(2.88f, 1.68f, -6.41f);

            patrouilleMouvementScript.points.Add(point.transform); // Ajoute les points de direction vers lesquels les personnages vont aller
            patrouilleMouvementScript.points.Add(point2.transform);

            NavMeshPokemonDestroy navMeshPokemonDestroyScript = pokemonGameObject.AddComponent<NavMeshPokemonDestroy>();
            navMeshPokemonDestroyScript.mapGenerator = meshObject.transform.parent.gameObject;
            GameObject barreViePokemon3D = SceneBuilder.GetComponent<ProjetP3DScene1.main>().gameObject.GetComponent<CreerComposantScript>().CreateBarre3D(pokemonGameObject.transform, "UIPokemon3D", 250, 250, 0, (float)2.8, SceneBuilder.GetComponent<ProjetP3DScene1.main>().uiPokemonGameObject, SceneBuilder.GetComponent<ProjetP3DScene1.main>().controllerJoueurs[0]);
            Debug.Log("Nom : " + pokemon.getNom() + " PV Restant : " + pokemon.getPvRestant() + " Id : " + pokemon.getIdPokedex());
        }
    }

    public void SpawnDresseur(NavMeshSurface navMeshSurface, Transform parent, Transform positionPersonnage)
    {
        // ClassLibrary.Pokemon pokemon = new ClassLibrary.Pokemon(); // Classe pokémon

        // pokemon = pokemon.setRandomPokemonSeed(SceneBuilder.GetComponent<ProjetP3DScene1.main>().jeu);

        Vector3 positionHerbeCollider = positionPersonnage.position; // On récupère la position de l'herbe
                                                                     // Vector3 sizeHerbeCollider = navMeshSurface.gameObject.GetComponent<ChunkBaker>().NavMeshSize; // On récupère la taille de l'herbe
        Vector3 sizeHerbeCollider = new Vector3(150, 0, 150);

        float itemXSpread = sizeHerbeCollider.x / 2;
        float itemYSpread = 5;
        float itemZSpread = sizeHerbeCollider.z / 2;
        // Vector3 positionPokemon = new Vector3(randPosition.x, randPosition.y, randPosition.z);

        Vector3 randPosition = new Vector3(Random.Range(-itemXSpread, itemXSpread), Random.Range(-itemYSpread, itemYSpread), Random.Range(-itemZSpread, itemZSpread)) + positionHerbeCollider;
        NavMeshHit hit;

        NavMesh.SamplePosition(randPosition, out hit, 500, NavMesh.AllAreas);

        int numeroPersonnageRandom = Random.Range(0, meshSettings.personnagesDonnees.listePersonnagesDresseurs.Count - 1); // On fait spawn un personnage random parmi les personnages
        string nomPersonnageRandom = meshSettings.personnagesDonnees.listePersonnagesDresseurs[numeroPersonnageRandom].personnageGameObject.name;

        GameObject personnagePrefab = GameObject.Instantiate(Resources.Load("Models/Personnages/Prefab/vThirdPersonCamera Prefab") as GameObject, hit.position, Quaternion.identity, parent.transform); // On créer d'abord le personnage par défaut

       // Debug.Log("personnagePrefab : " + personnagePrefab.transform.GetChild(1).name + " nomPersonnageRandom : " + nomPersonnageRandom + " hit : " + hit.position + " Quaternion : " + Quaternion.identity + Resources.Load<GameObject>("Models/Personnages/Pokemon/" + nomPersonnageRandom + "/" + nomPersonnageRandom).name);
        GameObject gameObject = GameObject.Instantiate(Resources.Load<GameObject>("Models/Personnages/Pokemon/" + nomPersonnageRandom + "/" + nomPersonnageRandom), hit.position, Quaternion.identity, personnagePrefab.transform.GetChild(1).gameObject.transform); // Puis on met le personnage
       gameObject.transform.localScale = new Vector3(0.014f, 0.014f, 0.014f);
        gameObject.transform.localPosition = new Vector3(0, 0, 0);

        int motsAEnlevernbCaracteres = "(Clone)".Length;
        gameObject.name = gameObject.name.Substring(0, gameObject.name.Length - motsAEnlevernbCaracteres); // On enlève une partie du nom pour que le nom du dresseur soit bien trouvé
        personnagePrefab.name = gameObject.name;

        personnagePrefab.GetComponent<Animator>().avatar = gameObject.GetComponent<Animator>().avatar;

        personnagePrefab.AddComponent<CombatDresseurScript>(); // On ajoute le script pour dire que c'est un dresseur
        NavMeshAgent navMeshAgent = personnagePrefab.AddComponent<NavMeshAgent>(); // On ajoute l'agent
        PatrouilleMouvement patrouilleMouvementScript = personnagePrefab.AddComponent<PatrouilleMouvement>(); // On ajoute le script permettant au personnage de savoir ou aller

        personnagePrefab.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        navMeshAgent.radius = 5f;
        navMeshAgent.height = 2f;
        navMeshAgent.avoidancePriority = Random.Range(5, 32);
        navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
        navMeshAgent.baseOffset = -0.09f;

        // patrouilleMouvementScript.points.Add(gameObject.transform); // Ajoute les points de direction vers lesquels les personnages vont aller
        GameObject point = new GameObject("Point");
        point.transform.parent = personnagePrefab.transform;
        point.transform.localPosition = new Vector3(150, 1, 0);
        GameObject point2 = new GameObject("Point2");
        point2.transform.parent = personnagePrefab.transform;
        point2.transform.localPosition = new Vector3(-150, 1, 0);

        patrouilleMouvementScript.points.Add(point.transform); // Ajoute les points de direction vers lesquels les personnages vont aller
        patrouilleMouvementScript.points.Add(point2.transform);

        // gameObject.tag = "PokemonAdverse";
        // gameObject.GetComponent<StatistiquesPokemon>().SetPokemon(pokemon); // Statistiques du pokemon
        // Debug.Log(parent.gameObject.transform.parent.gameObject.name);
       // NavMeshPokemonDestroy navMeshPokemonDestroyScript = personnagePrefab.AddComponent<NavMeshPokemonDestroy>();
       // navMeshPokemonDestroyScript.mapGenerator = parent.gameObject.transform.parent.gameObject;
        //  GameObject barreViePokemon3D = SceneBuilder.GetComponent<ProjetP3DScene1.main>().gameObject.GetComponent<CreerComposantScript>().CreateBarre3D(pokemonGameObject.transform, "UIPokemon3D", 250, 250, 0, (float)2.8, SceneBuilder.GetComponent<ProjetP3DScene1.main>().uiPokemonGameObject, SceneBuilder.GetComponent<ProjetP3DScene1.main>().controllerJoueurs[0]);
        // Debug.Log("Nom : " + pokemon.getNom() + " PV Restant : " + pokemon.getPvRestant() + " Id : " + pokemon.getIdPokedex()); */
    }

    public void SpawnTree(GameObject treePrefab, Transform parent, Transform positionPersonnage)
    {
        // ClassLibrary.Pokemon pokemon = new ClassLibrary.Pokemon(); // Classe pokémon

        // pokemon = pokemon.setRandomPokemonSeed(SceneBuilder.GetComponent<ProjetP3DScene1.main>().jeu);

        Vector3 positionHerbeCollider = positionPersonnage.position; // On récupère la position de l'herbe
                                                                     // Vector3 sizeHerbeCollider = navMeshSurface.gameObject.GetComponent<ChunkBaker>().NavMeshSize; // On récupère la taille de l'herbe
        Vector3 sizeHerbeCollider = new Vector3(positionPersonnage.transform.position.x, 0, positionPersonnage.transform.position.z);

        float itemXSpread = sizeHerbeCollider.x / 2;
        float itemYSpread = 5;
        float itemZSpread = sizeHerbeCollider.z / 2;
        // Vector3 positionPokemon = new Vector3(randPosition.x, randPosition.y, randPosition.z);

        for (int i = 0; i < 14; i++)
        {
            Vector3 randPosition = new Vector3(Random.Range(-itemXSpread, itemXSpread), Random.Range(-itemYSpread, itemYSpread), Random.Range(-itemZSpread, itemZSpread)) + positionHerbeCollider;
            NavMeshHit hit;

            NavMesh.SamplePosition(randPosition, out hit, 300, NavMesh.AllAreas);

            GameObject tree = GameObject.Instantiate(treePrefab, hit.position, Quaternion.identity, parent.transform);
        }
    }


    // Pour faire apparaitre de l'herbe sur les chunks
    void SpawnGrassOnChunk()
    {
        GrassRenderer grassRenderer = meshObject.AddComponent<GrassRenderer>(); // Au chargement du chunk, on ajoute le script pour spawn des objets

        grassRenderer.grassMesh = meshSettings.grassGenerator.GetComponent<GrassRenderer>().grassMesh;
        grassRenderer.material = meshSettings.grassGenerator.GetComponent<GrassRenderer>().material;
        grassRenderer.grassNumber = meshSettings.grassGenerator.GetComponent<GrassRenderer>().grassNumber;
        grassRenderer.size = new Vector2(300, 300);
    }

    void SpawnWaterOnChunk()
    {
        GrassRenderer grassRenderer = meshObject.AddComponent<GrassRenderer>(); // Au chargement du chunk, on ajoute le script pour spawn des objets

        grassRenderer.grassMesh = meshSettings.waterPrefab.GetComponent<StylizedWater2.WaterObject>().meshFilter.sharedMesh;
        grassRenderer.material = meshSettings.waterPrefab.GetComponent<StylizedWater2.WaterObject>().material;
        grassRenderer.grassNumber = 3;
        grassRenderer.size = new Vector2(721, 78);
        grassRenderer.startHeight = 9;
    }


    // Pour placer des objets dans les chunks
    public void PlaceObjectsOnChunks()
    {
        HashSet<Transform> alreadyGeneratedObjectAtThisChunkTransform = new HashSet<Transform>(); // Pour vérifier si l'objet à bien été placé

        Transform t = this.meshObject.transform; // Transform du chunk

            if (!alreadyGeneratedObjectAtThisChunkTransform.Contains(t))
            {
                SpawnGrassOnChunk();
                // SpawnWaterOnChunk();
                alreadyGeneratedObjectAtThisChunkTransform.Add(t);
            }
    }

    // Met à jour les chunks
    public void UpdateTerrainChunk()
    {
        if (SceneBuilder == null)
        {
            SceneBuilder = GameObject.Find("SceneBuilder");
            meshSettings.lampe.GetComponent<AllumerLampadaires>().tempsUIScript = SceneBuilder.GetComponent<ProjetP3DScene1.main>().canvasGameObject[0].transform.GetChild(8).gameObject.GetComponent<TempsUI>();
        }

        if (heightMapReceived)
        {
            float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));

            bool wasVisible = IsVisible();
            bool visible = viewerDstFromNearestEdge <= maxViewDst;

            if (visible)
            {
                int lodIndex = 0;

                for (int i = 0; i < detailLevels.Length - 1; i++)
                {
                    if (viewerDstFromNearestEdge > detailLevels[i].visibleDstThreshold)
                    {
                        lodIndex = i + 1;
                    }
                    else
                    {
                        break;
                    }
                }

                if (lodIndex != previousLODIndex)
                {
                    LODMesh lodMesh = lodMeshes[lodIndex];
                    if (lodMesh.hasMesh)
                    {
                        previousLODIndex = lodIndex;
                        meshFilter.mesh = lodMesh.mesh;

                      // PlaceObjectsOnChunks();
                        bool dejaPlace = false;
                        SpawnPokemon(meshObject.transform.parent.GetComponent<NavMeshSurface>(), meshObject.transform, viewer);
                        SpawnDresseur(meshObject.transform.parent.GetComponent<NavMeshSurface>(), meshObject.transform, viewer);
                      //  SpawnTree(meshSettings.tree2, meshObject.transform, viewer);
                        // int pokemonPlacer = 0;
                        // Asset placement
                        foreach (Vector3 vertex in meshFilter.mesh.vertices)
                        {              
                            SpawnAssetsOnChunkVerts(vertex, sampleCentre, meshSettings.meshWorldSize, coord, meshSettings.tree, meshFilter.transform, 2.45f, 2.5f);
                            SpawnAssetsOnChunkVerts(vertex, sampleCentre, meshSettings.meshWorldSize, coord, meshSettings.tree2, meshFilter.transform, 5.05f, 5.07f);
                            SpawnAssetsOnChunkVerts(vertex, sampleCentre, meshSettings.meshWorldSize, coord, meshSettings.tree2, meshFilter.transform, 9.05f, 9.07f);
                            SpawnAssetsOnChunkVerts(vertex, sampleCentre, meshSettings.meshWorldSize, coord, meshSettings.tree3, meshFilter.transform, 29.05f, 29.2f);
                            SpawnAssetsOnChunkVerts(vertex, sampleCentre, meshSettings.meshWorldSize, coord, meshSettings.tree3, meshFilter.transform, 31.05f, 31.2f);
                             SpawnAssetsOnChunkVerts(vertex, sampleCentre, meshSettings.meshWorldSize, coord, meshSettings.grass, meshFilter.transform, 5f, 7f);
                            // Vector3 positionNenuphar = new Vector3(vertex.x, vertex.y + 3, vertex.z);
                            // SpawnAssetsOnChunkVerts(positionNenuphar, sampleCentre, meshSettings.meshWorldSize, coord, meshSettings.nenuphar, meshFilter.transform, 1f, 1.75f);
                            SpawnAssetsOnChunkVerts(vertex, sampleCentre, meshSettings.meshWorldSize, coord, meshSettings.rocks, meshFilter.transform, 7f, 8f);
                            SpawnAssetsOnChunkVerts(vertex, sampleCentre, meshSettings.meshWorldSize, coord, meshSettings.rocks, meshFilter.transform, 11f, 12f);
                            SpawnAssetsOnChunkVerts(vertex, sampleCentre, meshSettings.meshWorldSize, coord, meshSettings.grass, meshFilter.transform, 14f, 20f);
                            SpawnAssetsOnChunkVerts(vertex, sampleCentre, meshSettings.meshWorldSize, coord, meshSettings.rocks, meshFilter.transform, 21f, 22f);
                            SpawnAssetsOnChunkVerts(vertex, sampleCentre, meshSettings.meshWorldSize, coord, meshSettings.maison, meshFilter.transform, 14f, 14.02f);
                            SpawnAssetsOnChunkVerts(vertex, sampleCentre, meshSettings.meshWorldSize, coord, meshSettings.maison2, meshFilter.transform, 14.03f, 14.05f);
                            SpawnAssetsOnChunkVerts(vertex, sampleCentre, meshSettings.meshWorldSize, coord, meshSettings.lampe, meshFilter.transform, 11f, 11.05f);
                            SpawnAssetsOnChunkVerts(vertex, sampleCentre, meshSettings.meshWorldSize, coord, meshSettings.fleur, meshFilter.transform, 6f, 7f);
                            SpawnAssetsOnChunkVerts(vertex, sampleCentre, meshSettings.meshWorldSize, coord, meshSettings.fleur2, meshFilter.transform, 14f, 15f);

                            /*  if (pokemonPlacer < 30)
                             {
                                 SpawnPokemonOnChunkVerts(vertex, sampleCentre, meshSettings.meshWorldSize, coord, meshSettings.tree, meshFilter.transform);
                                 pokemonPlacer++;
                             } */
                            //  
                            if (dejaPlace == false)
                            {
                                SpawnWaterOnChunkVerts(vertex, sampleCentre, meshSettings.meshWorldSize, coord, meshSettings.waterPrefab, meshFilter.transform);
                                dejaPlace = true;
                            }
                            // AssetPlacement.SpawnAssetsTwoOnChunkVerts(vertex, sampleCentre, meshSettings.meshWorldSize, coord, meshSettings.treeTwo, meshFilter.transform);
                            /*
                                                        if(vertex.y > 0 && vertex.y && !alreadyGeneratedObjectAtThisVertexCoords.Contains(new Vector2(vertex.x, vertex.y)))
                                                        {
                                                            AssetPlacement.SpawnAssetsOnChunksVerts(vertex, sampleCentre, meshSettings.meshWorldSize, coord, meshSettings.reed, meshFilter.transform);
                                                            alreadyGeneratedObjectAtThisVertexCoords.Add(new Vector2(vertex.x, vertex.y);
                                                        } */
                        }
                    }
                    else if (!lodMesh.hasRequestedMesh)
                    {
                        lodMesh.RequestMesh(heightMap, meshSettings); // On demande le mesh
                    }
                }


            }

            // Si la visibilité a changé, on change l'état du chunk
            if (wasVisible != visible)
            {
                SetVisible(visible);
                if (onVisibilityChanged != null)
                {
                    onVisibilityChanged(this, visible);
                }
            }
        }
    }

    public void UpdateCollisionMesh()
    {
        if (!hasSetCollider)
        {
            float sqrDstFromViewerToEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));

            if (sqrDstFromViewerToEdge < detailLevels[colliderLODIndex].sqrVisibleDstThreshold)
            {
                if (!lodMeshes[colliderLODIndex].hasRequestedMesh)
                {
                    lodMeshes[colliderLODIndex].RequestMesh(heightMap, meshSettings); // On demande le mesh
                }
            }

            if (sqrDstFromViewerToEdge < colliderGenerationDistanceThreshold * colliderGenerationDistanceThreshold)
            {
                if (lodMeshes[colliderLODIndex].hasMesh)
                {
                    /*
                    if (colliderLODIndex == 0)
                    {
                        meshNavMesh.BuildNavMesh();
                    }
                    */
                    meshCollider.sharedMesh = lodMeshes[colliderLODIndex].mesh;
                    hasSetCollider = true;
                }
            }
        }
    }

    public void SetVisible(bool visible)
    {
        if(visible == true)
        {
            // spawnGameObjectScript = meshObject.transform.parent.GetComponent<SpawnGameObject>();
            // GameObject InstanceGameObject = (GameObject)Instantiate(spawnGameObjectScript.gameObjectSpawn[0], new Vector3(0, this.heightMap.values[0, 1], 1) + meshObject.transform.position, Quaternion.identity, meshObject.transform);
            //  spawnGameObjectScript.spawnGameObject(1, meshObject.gameObject.transform.GetSiblingIndex(), this.heightMap.values);
            // spawnGameObjectScript.spawnGameObjectTest(meshSettings.numVertsPerLine, 1, this.heightMap.values);

            // Debug.Log(meshSettings.chunkSizeIndex);
            // meshObject.AddComponent<spawnGameObject>(); // Au chargement du chunk, on ajoute le script pour spawn des objets
        }
        meshObject.SetActive(visible);
        // spawnGameObjectScript.terrain = meshObject.GetComponent<Terrain>();
    }

    public bool IsVisible()
    {
        return meshObject.activeSelf;
    }

}

class LODMesh
{

    public Mesh mesh;
    public bool hasRequestedMesh;
    public bool hasMesh;
    int lod;
    public event System.Action updateCallback;

    public LODMesh(int lod)
    {
        this.lod = lod;
    }

    void OnMeshDataReceived(object meshDataObject)
    {
        mesh = ((MeshData)meshDataObject).CreateMesh();
        hasMesh = true;

        updateCallback();
    }

    // On demande un mesh et on génére un
    public void RequestMesh(HeightMap heightMap, MeshSettings meshSettings)
    {
        hasRequestedMesh = true;
        ThreadedDataRequester.RequestData(() => MeshGenerator.GenerateTerrainMesh(heightMap.values, meshSettings, lod), OnMeshDataReceived);
    }

}
