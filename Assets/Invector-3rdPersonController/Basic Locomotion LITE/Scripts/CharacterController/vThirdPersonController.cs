using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Invector.CharacterController
{
    public class vThirdPersonController : vThirdPersonAnimator
    {
        protected virtual void Start()
        {
            if (SceneManager.GetActiveScene().name != "SceneMenu")
            {
                Cursor.visible = false;
            }
            /*
#if !UNITY_EDITOR
                Cursor.visible = false;
#endif
*/
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Collider")
            {
                animator.SetBool("dansEau", true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.name == "Collider")
            {
                animator.GetComponent<Animator>().SetBool("dansEau", false);
            }
        }

        public virtual void Sprint(bool value)
        {                                   
            isSprinting = value;            
        }

        public virtual void Strafe()
        {
            if (locomotionType == LocomotionType.OnlyFree) return;
            isStrafing = !isStrafing;
        }

        public virtual void Jump()
        {
            // conditions to do this action
            bool jumpConditions = isGrounded && !isJumping;
            // return if jumpCondigions is false
            if (!jumpConditions) return;
            // trigger jump behaviour
            jumpCounter = jumpTimer;            
            isJumping = true;
            // trigger jump animations            
            if (_rigidbody.velocity.magnitude < 1)
                animator.CrossFadeInFixedTime("Jump", 0.1f);
            else
                animator.CrossFadeInFixedTime("JumpMove", 0.2f);
        }

        public virtual void Fly()
        {
            // Toggle flying
            if (isFlying)
            {
                isFlying = false;
                animator.SetBool("enTrainDeVoler", isFlying);
                _rigidbody.useGravity = true;
                return;
            }

            if (animator.IsInTransition(0))
                return;

            // know if has enough stamina to make this action
            // bool staminaConditions = currentStamina > flyStaminaMove;

            // conditions to do this action
            bool flyConditions = !isGrounded;

            /*
            // return if flyConditions is false
            if (!flyConditions)
            return;
            */

            // trigger fly behaviour
            isFlying = true;
            isJumping = false;

            // trigger fly animations
            animator.SetBool("enTrainDeVoler", isFlying);
        }

        public virtual void RotateWithAnotherTransform(Transform referenceTransform)
        {
            var newRotation = new Vector3(transform.eulerAngles.x, referenceTransform.eulerAngles.y, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(newRotation), strafeRotationSpeed * Time.fixedDeltaTime);
            targetRotation = transform.rotation;
        }
    }
}