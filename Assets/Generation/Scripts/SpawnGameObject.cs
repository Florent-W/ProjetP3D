using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnGameObject : MonoBehaviour
{
    public List<GameObject> gameObjectSpawn = new List<GameObject>();
    public int NombreGameObject = 32;

    public int itemXSpread = 90;
    public float itemYSpread = 0;
    public int itemZSpread = 120;

    bool spawnPokemonEffectuer = false;

    public void spawnGameObject(int numeroListeGameObject, int numeroChunk, float[,] hauteur)
    {
        for (int i = 0; i < gameObjectSpawn.Count - 1; i++) // Parcours de tous les objets
        {
            for (int j = 0; j < NombreGameObject; j++)
            {
                Vector3 positionChunk = new Vector3(this.gameObject.transform.GetChild(numeroChunk).gameObject.transform.position.x, 0, this.gameObject.transform.GetChild(numeroChunk).gameObject.transform.position.z);
                // int randPositionX = Random.Range(-itemXSpread, itemXSpread);
                // int randPositionZ = Random.Range(-itemZSpread, itemZSpread);

                int randPositionX = Random.Range(0, hauteur.GetLength(0));
                int randPositionZ = Random.Range(0, hauteur.GetLength(1));

                Vector3 randPosition = new Vector3(randPositionX, hauteur[randPositionX, randPositionZ], randPositionZ) + positionChunk;
                //  float positionY = this.gameObject.GetComponent<Terrain>().SampleHeight(randPosition);

                // Vector3 positionRandomChunk = this.gameObject.GetComponent<MeshCollider>().transform.position; // Position des collision
                // Vector3 positionChunk = new Vector3(randPosition.x, 1, randPosition.z);

                // pokemon = pokemon.setRandomPokemonSeed(SceneBuilder.GetComponent<ProjetP3DScene1.main>().jeu);
                // Debug.Log(this.gameObject.transform.GetChild(numeroChunk).gameObject.GetComponent<TerrainChunk>().heightMap); // Hauteur au point
                GameObject InstanceGameObject = (GameObject)Instantiate(gameObjectSpawn[i], randPosition, Quaternion.identity, this.gameObject.transform.GetChild(numeroChunk));
                InstanceGameObject.name = InstanceGameObject.name.Replace("(Clone)", "");
                // InstanceGameObject.GetComponent<StatistiquesPokemon>().SetPokemon(pokemon); // Statistiques du pokemon
                // GameObject textPokemon3D = SceneBuilder.GetComponent<ProjetP3DScene1.main>().gameObject.GetComponent<CreerComposantScript>().CreateText3D(InstanceGameObject.transform, "NomPokemon3D", 250, 250, 0, 5, InstanceGameObject.GetComponent<StatistiquesPokemon>().GetPokemon().getNom(), 12, Color.cyan, SceneBuilder.GetComponent<ProjetP3DScene1.main>().controllerJoueurs[0]);
                //  GameObject barreViePokemon3D = SceneBuilder.GetComponent<ProjetP3DScene1.main>().gameObject.GetComponent<CreerComposantScript>().CreateBarre3D(InstanceGameObject.transform, "UIPokemon3D", 250, 250, 0, (float)2.8, SceneBuilder.GetComponent<ProjetP3DScene1.main>().uiPokemonGameObject, SceneBuilder.GetComponent<ProjetP3DScene1.main>().controllerJoueurs[0]);
            }
        }
    }

    public void spawnGameObjectTest(int numVertsPerLine, int numeroChunk, float[,] hauteur)
    {
        for (int i = 0; i < numVertsPerLine; i++) // Parcours de tous les objets
        {
            for (int j = 0; j < numVertsPerLine; j++)
            {
                Vector3 positionChunk = new Vector3(this.gameObject.transform.GetChild(numeroChunk).gameObject.transform.position.x, 0, this.gameObject.transform.GetChild(numeroChunk).gameObject.transform.position.z);
                // int randPositionX = Random.Range(-itemXSpread, itemXSpread);
                // int randPositionZ = Random.Range(-itemZSpread, itemZSpread);

                int randPositionX = Random.Range(0, numVertsPerLine);
                int randPositionZ = Random.Range(0, numVertsPerLine);

                Vector3 randPosition = new Vector3(randPositionX, hauteur[randPositionX, randPositionZ], randPositionZ) + positionChunk;
                //  float positionY = this.gameObject.GetComponent<Terrain>().SampleHeight(randPosition);

                // Vector3 positionRandomChunk = this.gameObject.GetComponent<MeshCollider>().transform.position; // Position des collision
                // Vector3 positionChunk = new Vector3(randPosition.x, 1, randPosition.z);

                // pokemon = pokemon.setRandomPokemonSeed(SceneBuilder.GetComponent<ProjetP3DScene1.main>().jeu);
                // Debug.Log(this.gameObject.transform.GetChild(numeroChunk).gameObject.GetComponent<TerrainChunk>().heightMap); // Hauteur au point
                GameObject InstanceGameObject = (GameObject)Instantiate(gameObjectSpawn[0], randPosition, Quaternion.identity, this.gameObject.transform.GetChild(numeroChunk));
                InstanceGameObject.name = InstanceGameObject.name.Replace("(Clone)", "");
                // InstanceGameObject.GetComponent<StatistiquesPokemon>().SetPokemon(pokemon); // Statistiques du pokemon
                // GameObject textPokemon3D = SceneBuilder.GetComponent<ProjetP3DScene1.main>().gameObject.GetComponent<CreerComposantScript>().CreateText3D(InstanceGameObject.transform, "NomPokemon3D", 250, 250, 0, 5, InstanceGameObject.GetComponent<StatistiquesPokemon>().GetPokemon().getNom(), 12, Color.cyan, SceneBuilder.GetComponent<ProjetP3DScene1.main>().controllerJoueurs[0]);
                //  GameObject barreViePokemon3D = SceneBuilder.GetComponent<ProjetP3DScene1.main>().gameObject.GetComponent<CreerComposantScript>().CreateBarre3D(InstanceGameObject.transform, "UIPokemon3D", 250, 250, 0, (float)2.8, SceneBuilder.GetComponent<ProjetP3DScene1.main>().uiPokemonGameObject, SceneBuilder.GetComponent<ProjetP3DScene1.main>().controllerJoueurs[0]);
            }
        }
    }
    /*
    // On créer d'abord le personnage par défaut
    public GameObject instantiatePersonnagePrefab(GameObject prefabGameObject, Vector3 navMeshHitPosition, Quaternion quaternionIdentity, GameObject parent)
    {
        GameObject gameObject = GameObject.Instantiate(Resources.Load("Models/Personnages/Prefab/vThirdPersonCamera Prefab") as GameObject, navMeshHitPosition, quaternionIdentity, parent.transform); 
        return gameObject;
    }

    // Pour instantier un gameobject avec un delay dans un autre gameobject
    public GameObject instantiatePersonnage(GameObject prefabGameObject, GameObject personnageGameObject, Vector3 navMeshHitPosition, Quaternion quaternionIdentity)
    {
       GameObject gameObject = GameObject.Instantiate(personnageGameObject, navMeshHitPosition, Quaternion.identity, prefabGameObject.transform.GetChild(1).gameObject.transform); // On met le personnage
       return gameObject;
    }

    public IEnumerator instantiatePersonnageDelayCoroutine(GameObject prefabGameObject, Vector3 navMeshHitPosition, )
    {
        instantiatePersonnagePrefab(prefabGameObject, navMeshHitPosition, quaternionIdentity, parentPersonnagePrefab);
    }
    */
    public void SpawnDresseur(NavMeshSurface navMeshSurface, Transform parent, Transform positionPersonnage, MeshSettings meshSettings) {
        StartCoroutine(SpawnDresseurCoroutine(navMeshSurface, parent, positionPersonnage, meshSettings));
    }

   public IEnumerator SpawnDresseurCoroutine(NavMeshSurface navMeshSurface, Transform parent, Transform positionPersonnage, MeshSettings meshSettings)
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

        yield return null;
     // Debug.Log("personnagePrefab : " + personnagePrefab.transform.GetChild(1).name + " nomPersonnageRandom : " + nomPersonnageRandom + " hit : " + hit.position + " Quaternion : " + Quaternion.identity + Resources.Load("Models/Personnages/Pokemon/" + nomPersonnageRandom + "/" + nomPersonnageRandom).name);
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

        navMeshAgent.radius = 0.5f;
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
        NavMeshPokemonDestroy navMeshPokemonDestroyScript = gameObject.AddComponent<NavMeshPokemonDestroy>();
        navMeshPokemonDestroyScript.mapGenerator = parent.parent.gameObject;
        //  GameObject barreViePokemon3D = SceneBuilder.GetComponent<ProjetP3DScene1.main>().gameObject.GetComponent<CreerComposantScript>().CreateBarre3D(pokemonGameObject.transform, "UIPokemon3D", 250, 250, 0, (float)2.8, SceneBuilder.GetComponent<ProjetP3DScene1.main>().uiPokemonGameObject, SceneBuilder.GetComponent<ProjetP3DScene1.main>().controllerJoueurs[0]);
        // Debug.Log("Nom : " + pokemon.getNom() + " PV Restant : " + pokemon.getPvRestant() + " Id : " + pokemon.getIdPokedex());
    }

    public void spawnGameObjectPosition(int x, float y, int z)
    {
                Vector3 randPosition = new Vector3(x, y, z);
                //  float positionY = this.gameObject.GetComponent<Terrain>().SampleHeight(randPosition);

                // Vector3 positionRandomChunk = this.gameObject.GetComponent<MeshCollider>().transform.position; // Position des collision
                // Vector3 positionChunk = new Vector3(randPosition.x, 1, randPosition.z);

                // pokemon = pokemon.setRandomPokemonSeed(SceneBuilder.GetComponent<ProjetP3DScene1.main>().jeu);
                // Debug.Log(this.gameObject.transform.GetChild(numeroChunk).gameObject.GetComponent<TerrainChunk>().heightMap); // Hauteur au point
                GameObject InstanceGameObject = (GameObject)Instantiate(gameObjectSpawn[0], randPosition, Quaternion.identity);
                InstanceGameObject.name = InstanceGameObject.name.Replace("(Clone)", "");
                // InstanceGameObject.GetComponent<StatistiquesPokemon>().SetPokemon(pokemon); // Statistiques du pokemon
                // GameObject textPokemon3D = SceneBuilder.GetComponent<ProjetP3DScene1.main>().gameObject.GetComponent<CreerComposantScript>().CreateText3D(InstanceGameObject.transform, "NomPokemon3D", 250, 250, 0, 5, InstanceGameObject.GetComponent<StatistiquesPokemon>().GetPokemon().getNom(), 12, Color.cyan, SceneBuilder.GetComponent<ProjetP3DScene1.main>().controllerJoueurs[0]);
                //  GameObject barreViePokemon3D = SceneBuilder.GetComponent<ProjetP3DScene1.main>().gameObject.GetComponent<CreerComposantScript>().CreateBarre3D(InstanceGameObject.transform, "UIPokemon3D", 250, 250, 0, (float)2.8, SceneBuilder.GetComponent<ProjetP3DScene1.main>().uiPokemonGameObject, SceneBuilder.GetComponent<ProjetP3DScene1.main>().controllerJoueurs[0]);
            }
/*
    public void spawn(MeshSettings meshSettings, float[,] heightmap)
    {
        int numVertsPerLine = meshSettings.numVertsPerLine; // Nombre de vertices

        // Parcours de la hauteur et de la longueur de la heightmap avec prise en compte du niveau de détails qui multiplie par le nombre de fois en plus le niveau de détails
        for (int y = 0; y < numVertsPerLine; y++)
        {
            for (int x = 0; x < numVertsPerLine; x++)
            {
                if (y == 23 && x == 12)
                {
                   // Debug.Log(height);
                    GameObject plane = Resources.Load<GameObject>("Assets / Standard Assets / Environment / SpeedTree / Broadleaf / Broadleaf_Desktop");
                    plane.transform.position = new Vector3(y, heightmap[y, x], x);
                }
            }
        }
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        // gameObjectSpawn = GameObject.Find("Palm_Desktop");
    }

    // Update is called once per frame
    /*
    void Update()
    {
        if (spawnPokemonEffectuer == false)
        {
            for (int i = 0; i < NombreGameObject; i++)
            {
               // spawnGameObject();
            }
            spawnPokemonEffectuer = true;
        }
    } */
}
