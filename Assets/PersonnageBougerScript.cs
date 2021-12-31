using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PersonnageBougerScript : MonoBehaviour
{
    public float speed = 10;
    public int speedCameraHorizontal = 2;
    public int speedCameraVertical = 2;
    private bool estSurSol;
    private Rigidbody rb;
    private float yaw, pitch;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Terrain")
        {
            estSurSol = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 valeurBouger = this.GetComponent<PlayerInput>().actions["BougerVersHautBas"].ReadValue<Vector2>();
        float valeurSaut = this.GetComponent<PlayerInput>().actions["Saut"].ReadValue<float>();

        if (valeurBouger.x > 0 && valeurSaut == 0)
        {
            rb.velocity = transform.forward * speed;
        }
        if (valeurBouger.x < 0)
        {
            rb.velocity = -transform.forward * speed;
        }
        /*
        if (valeurBouger.x > 0)
        {
            rb.velocity = -transform.right * speed;
            // transform.Rotate(new Vector3(0, 20, 0) * Time.deltaTime * speed);
        }
        if (valeurBouger.x < 0)
        {
            rb.velocity = transform.right * speed;
            // transform.Rotate(new Vector3(0, -20, 0) * Time.deltaTime * speed);
        }
        */

        if (valeurSaut > 0 && estSurSol == true)
        {
            estSurSol = false;
            rb.AddForce(new Vector3(0, 80, 0), ForceMode.Impulse);
        }
    }
}
