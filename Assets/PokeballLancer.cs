using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeballLancer : MonoBehaviour
{
    public GameObject Joueur;
    public GameObject pokeball;
    public GameObject VthirdPersonCamera;

    public void lancerPokeball()
    {
       // Transform destinationPokeball = Joueur.gameObject.transform;

        Vector3 destinationPositionPokeball = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.5f);
        Debug.Log(destinationPositionPokeball);
        GameObject pokeball1 = Instantiate(pokeball, destinationPositionPokeball, this.transform.rotation);
        pokeball1.transform.localScale = new Vector3(1, 1, 1);
        Animator animatorPokeball = pokeball1.GetComponent<Animator>();

        animatorPokeball.applyRootMotion = true;

        if (Input.GetKeyDown(KeyCode.L))
        {
            Destroy(pokeball1);
        }

        Animator animator = VthirdPersonCamera.GetComponent<Animator>();

        animator.SetTrigger("trThrowBall");
        animator.Play("Red throw pokeball");
        animatorPokeball.Play("Ball throw");
    }

    // Start is called before the first frame update
    void Start()
    {
        //   GameObject pokeballPrefab = (GameObject) Resources.Load("Models/Pokeballs/Pokeball");

        //   pokeball = Instantiate(pokeballPrefab);
        //   pokeball.transform.localScale = new Vector3(1, 1, 1);
        //    pokeball.transform.SetParent(this.gameObject.transform);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            lancerPokeball();
        }

    }
}
