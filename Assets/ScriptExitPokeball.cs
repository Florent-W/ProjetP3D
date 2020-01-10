using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptExitPokeball : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {      
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.49 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.59 && animator.gameObject.GetComponent<Rigidbody>() == null)
        {
            CapsuleCollider gameObjectCapsuleCollider = animator.gameObject.GetComponent<CapsuleCollider>();
            gameObjectCapsuleCollider.enabled = true;
            gameObjectCapsuleCollider.radius = 0;
            gameObjectCapsuleCollider.height = 0.2f;
            animator.gameObject.AddComponent<Rigidbody>();
            Rigidbody gameObjectRigibody = animator.gameObject.GetComponent<Rigidbody>();
            gameObjectRigibody.mass = 5;
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject pokemonJoueurGameObject = (GameObject) Instantiate(Resources.Load("Models/Pokemon/" + animator.GetInteger("NumeroPokedexPokemon")), animator.gameObject.transform.position, animator.gameObject.transform.rotation);
        pokemonJoueurGameObject.name = "PokemonJoueur";

        /*
        Vector3 positionPokemonAdverse = new Vector3(animator.gameObject.transform.position.x + 8, animator.gameObject.transform.position.y, animator.gameObject.transform.position.z);
        Quaternion rotationPokemonAdverse = Quaternion.Euler(pokemon.transform.rotation.x, pokemon.transform.rotation.y + 180, pokemon.transform.rotation.z);

        GameObject pokemonAdverseGameObject = (GameObject)Instantiate(Resources.Load("Models/Pokemon/" + animator.GetInteger("NumeroPokedexPokemonAdverse")), positionPokemonAdverse, rotationPokemonAdverse);
        pokemonAdverseGameObject.transform.Rotate(0f, 180f, 0f); */

        /*
       CapsuleCollider gameObjectCapsuleCollider = animator.gameObject.GetComponent<CapsuleCollider>();
       gameObjectCapsuleCollider.enabled = true;
      //  animator.gameObject.AddComponent<Rigidbody>();
      //  Rigidbody gameObjectRigibody = animator.gameObject.GetComponent<Rigidbody>();
       // gameObjectRigibody.useGravity = false;       
       */
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
