using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Va déplacer aléatoirement le pokemon sur l'écran de chargement
public class BougerPokemonChargement : MonoBehaviour
{
    [SerializeField]
    private GameObject imageChargementGameObject;
    private string directionPokemon = "haut";
    private bool coroutineLancer = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    private IEnumerator ChangerDirectionPokemon()
    {
        coroutineLancer = true;
        if(directionPokemon == "haut")
        {
            directionPokemon = "bas";
        }
        else if(directionPokemon == "bas")
        {
            directionPokemon = "haut";
        }
        yield return new WaitForSeconds(3f);

        coroutineLancer = false;
    }

    public void SupprimerPokemon()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 0.1f;

        if(coroutineLancer == false)
        {
            StartCoroutine(ChangerDirectionPokemon());
        }

        if(directionPokemon == "haut")
        {
            this.transform.position += Vector3.up * speed;
        }
        else if(directionPokemon == "bas")
        {
            this.transform.position += Vector3.down * speed;
        }

        /*
        if (this.gameObject.transform.position.y < imageChargementGameObject.transform.position.y)
        {
            this.transform.position += Vector3.up * speed;
        }
        else if(this.gameObject.transform.position.y >= imageChargementGameObject.transform.position.y)
        {
            this.transform.position -= Vector3.up * speed;
        }
        */
    }
}
