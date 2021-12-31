using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPokemon : MonoBehaviour
{
    public Terrain terrain;
    public int NombreGameObject = 12;

    public float itemXSpread = 20;
    public float itemYSpread = 0;
    public float itemZSpread = 20;

    private ClassLibrary.Pokemon pokemon = new ClassLibrary.Pokemon();
    bool spawnPokemonEffectuer = false;
    GameObject SceneBuilder;

    void SpawnGameObject()
    {

        Vector3 randPosition = new Vector3(Random.Range(-itemXSpread, itemXSpread), Random.Range(-itemYSpread, itemYSpread), Random.Range(-itemZSpread, itemZSpread)) + transform.position;
        float positionY = terrain.SampleHeight(randPosition);
        Vector3 positionPokemon = new Vector3(randPosition.x, positionY, randPosition.z);

        pokemon = pokemon.setRandomPokemonSeed(SceneBuilder.GetComponent<ProjetP3DScene1.main>().jeu);
        GameObject InstanceGameObject = (GameObject)Instantiate(Resources.Load("Models/Pokemon/" + pokemon.getNoIdPokedex()), positionPokemon, Quaternion.identity);
        InstanceGameObject.name = InstanceGameObject.name.Replace("(Clone)", "");
        InstanceGameObject.GetComponent<StatistiquesPokemon>().SetPokemon(pokemon); // Statistiques du pokemon
        // GameObject textPokemon3D = SceneBuilder.GetComponent<ProjetP3DScene1.main>().gameObject.GetComponent<CreerComposantScript>().CreateText3D(InstanceGameObject.transform, "NomPokemon3D", 250, 250, 0, 5, InstanceGameObject.GetComponent<StatistiquesPokemon>().GetPokemon().getNom(), 12, Color.cyan, SceneBuilder.GetComponent<ProjetP3DScene1.main>().controllerJoueurs[0]);
        GameObject barreViePokemon3D = SceneBuilder.GetComponent<ProjetP3DScene1.main>().gameObject.GetComponent<CreerComposantScript>().CreateBarre3D(InstanceGameObject.transform, "UIPokemon3D", 250, 250, 0, (float)2.8, SceneBuilder.GetComponent<ProjetP3DScene1.main>().uiPokemonGameObject, SceneBuilder.GetComponent<ProjetP3DScene1.main>().controllerJoueurs[0]);
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneBuilder = GameObject.Find("SceneBuilder");
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneBuilder.GetComponent<ProjetP3DScene1.main>().jeuInitialiser == true && spawnPokemonEffectuer == false)
        {
            for (int i = 0; i < NombreGameObject; i++)
            {
                SpawnGameObject();
            }
            spawnPokemonEffectuer = true;
        }
    }
}
