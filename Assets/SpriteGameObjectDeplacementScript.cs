using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteGameObjectDeplacementScript : MonoBehaviour
{
    public GameObject vThirdPersonCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        this.gameObject.transform.LookAt(vThirdPersonCamera.GetComponent<Camera>().transform); // Pour que le sprite soit toujours visible, on déplace sa rotation
        this.transform.rotation *= Quaternion.Euler(0, 180, 0);
    }
}
