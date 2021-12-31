using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControleCamera : MonoBehaviour
{
    private vThirdPersonCamera vThirdPersonCamera;
    private GameObject vThirdPersonControllerGameObject;

    // Start is called before the first frame update
    void Start()
    {
        vThirdPersonCamera = this.gameObject.GetComponent<vThirdPersonCamera>(); // Initialisation
        vThirdPersonControllerGameObject = vThirdPersonCamera.target.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 scrollZoom = Input.mouseScrollDelta; // On regarde le curseur de la souris pour savoir si on va zoomer la caméra sur le personnage ou la mettre plus loin
        Vector2 stickDroitZoom = vThirdPersonControllerGameObject.GetComponent<PlayerInput>().actions["Zoom"].ReadValue<Vector2>(); // Pareil pour le stick droit

        if ((scrollZoom.y > 0 || stickDroitZoom.y > 0) && vThirdPersonCamera.defaultDistance > 2.1f)
        {
            vThirdPersonCamera.defaultDistance -= 1f; // On augmente
        }
        else if ((scrollZoom.y < 0 || stickDroitZoom.y < 0) && vThirdPersonCamera.defaultDistance < 20)
        {
            vThirdPersonCamera.defaultDistance += 1f; // On diminue
        }
    }
}
