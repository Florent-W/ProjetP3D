using UnityEngine;
using UnityEngine.EventSystems;
// using Doozy.Engine.UI;
using System.Collections;

namespace Invector.CharacterController
{
    public abstract class vThirdPersonAnimator : vThirdPersonMotor
    {
        // public GameObject objectToActivate;
        // public GameObject pokeball;
        public Animator pokeballAnimator;
        public GameObject pokeballInstance;
        public Transform destinationPokeball;

        private IEnumerator WaitForAnimation(Animation animation)
        {
            do
            {
                yield return null;
            } while (animation.isPlaying);
        }

        public void lancerPokeball()
        {
            Vector3 destinationPositionPokeball = new Vector3(destinationPokeball.position.x, destinationPokeball.position.y, destinationPokeball.position.z - 0.5f);
            GameObject pokeball = Instantiate(pokeballInstance, destinationPositionPokeball, destinationPokeball.rotation);
            Animator animatorPokeball = pokeball.GetComponent<Animator>();
            animatorPokeball.applyRootMotion = true;

            if (Input.GetKeyDown(KeyCode.L))
            {
                Destroy(pokeball);
            }

            animator.SetTrigger("trThrowBall");
            animator.Play("Red throw pokeball");
            animatorPokeball.Play("Ball throw");
        }

        public virtual void UpdateAnimator()
        {
            if (animator == null || !animator.enabled) return;
            
            animator.SetBool("IsStrafing", isStrafing);
            animator.SetBool("IsGrounded", isGrounded);
            animator.SetFloat("GroundDistance", groundDistance);

            if (!isGrounded && !animator.GetBool("enTrainDeVoler"))
                animator.SetFloat("VerticalVelocity", verticalVelocity);

            if (isStrafing)
            {
                // strafe movement get the input 1 or -1
                animator.SetFloat("InputHorizontal", direction, 0.1f, Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                Vector3 destinationPositionPokeball = new Vector3(destinationPokeball.position.x, destinationPokeball.position.y, destinationPokeball.position.z - 0.5f);
                GameObject pokeball = Instantiate(pokeballInstance, destinationPositionPokeball, destinationPokeball.rotation);
               // pokeball.transform.parent = this.transform.GetChild(1).transform.GetChild(4);

              //  pokeball.transform.position = destination.position;
              //  pokeball.transform.position = destination.TransformPoint(destination.position);
               // pokeball.transform.localScale = new Vector3(1, 1, 1);
                Animator animatorPokeball = pokeball.GetComponent<Animator>();
                animatorPokeball.applyRootMotion = true;
                // animatorPokeball.runtimeAnimatorController = Resources.Load("Pokeball throw controller") as RuntimeAnimatorController;
             
                // pokeballRigibody.useGravity = true;
                // pokeballInstance.AddComponent<CapsuleCollider>();

                if (Input.GetKeyDown(KeyCode.L))
                {
                    Destroy(pokeball);
                }

                animator.SetTrigger("trThrowBall");
                // objectToActivate.SetActive(true);
                animator.Play("Red throw pokeball");
              //  pokeballAnimator.Play("Pokeball throw ");
                animatorPokeball.Play("Ball throw");
            }
          

            /*
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
            {
                Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("Red throw pokeball"));
            }
            */

            // fre movement get the input 0 to 1
            animator.SetFloat("InputVertical", speed, 0.1f, Time.deltaTime);
        }


        public void OnAnimatorMove()
        {
            // we implement this function to override the default root motion.
            // this allows us to modify the positional speed before it's applied.
            if (isGrounded)
            {
                transform.rotation = animator.rootRotation;

                var speedDir = Mathf.Abs(direction) + Mathf.Abs(speed);
                speedDir = Mathf.Clamp(speedDir, 0, 1);
                var strafeSpeed = (isSprinting ? 1.5f : 1f) * Mathf.Clamp(speedDir, 0f, 1f);
                
                // strafe extra speed
                if (isStrafing)
                {
                    if (strafeSpeed <= 0.5f)
                        ControlSpeed(strafeWalkSpeed);
                    else if (strafeSpeed > 0.5f && strafeSpeed <= 1f)
                        ControlSpeed(strafeRunningSpeed);
                    else
                        ControlSpeed(strafeSprintSpeed);
                }
                else if (!isStrafing)
                {
                    // free extra speed                
                    if (speed <= 0.5f)
                        ControlSpeed(freeWalkSpeed);
                    else if (speed > 0.5 && speed <= 1f)
                        ControlSpeed(freeRunningSpeed);
                    else
                        ControlSpeed(freeSprintSpeed);
                }
            }
        }
    }
}