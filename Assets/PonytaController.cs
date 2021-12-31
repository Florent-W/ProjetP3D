using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PonytaController : MonoBehaviour
{
    public float speed = 10;
    public int speedCameraHorizontal = 2;
    public int speedCameraVertical = 2;
    public GameObject VthirdPersonController, vthirdPersonCamera, vCamMonture;
    private bool estSurSol;

    public Rigidbody rb;
    private float yaw, pitch;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Terrain" || collision.gameObject.name == "Terrain Chunk")
        {
            estSurSol = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        // Mouvement Camera manette
        float rotateHorizontal = vthirdPersonCamera.GetComponent<PlayerInput>().actions["ControleCamera"].ReadValue<Vector2>().x;
        float rotateVertical = vthirdPersonCamera.GetComponent<PlayerInput>().actions["ControleCamera"].ReadValue<Vector2>().y;

        // Mouvement Camera souris
        float MouvementCameraSourisY = vthirdPersonCamera.GetComponent<PlayerInput>().actions["ControleCameraSourisY"].ReadValue<float>();
        float MouvementCameraSourisX = vthirdPersonCamera.GetComponent<PlayerInput>().actions["ControleCameraSourisX"].ReadValue<float>();

        yaw += speedCameraHorizontal * rotateHorizontal;
        pitch -= speedCameraVertical * rotateVertical;

        yaw += speedCameraHorizontal * MouvementCameraSourisX;
        pitch -= speedCameraVertical * MouvementCameraSourisY;

        // yaw = Mathf.Clamp(yaw, 150f, 200f);
        // pitch = Mathf.Clamp(pitch, -30f, 50f);

        // Vector3 mouvement = new Vector3(-hAxis, 0, -vAxis) * speed * Time.deltaTime; // Mouvement
        // transform.Translate(transform.position + mouvement);
        // transform.position += hAxis * Vector3.right;

        // transform.Rotate(Vector3.up * hAxis * 90 * Time.deltaTime);
        Vector2 valeurBouger = vthirdPersonCamera.GetComponent<PlayerInput>().actions["BougerVersHautBas"].ReadValue<Vector2>();
        float valeurSaut = vthirdPersonCamera.GetComponent<PlayerInput>().actions["Saut"].ReadValue<float>();

            if (valeurBouger.y > 0 && valeurSaut == 0 && estSurSol == true)
            {
                rb.velocity = transform.forward * speed;
            }
            if (valeurBouger.y < 0 && estSurSol == true)
            {
                rb.velocity = -transform.forward * speed;
            }
            if (valeurBouger.x > 0)
            {
                transform.Rotate(new Vector3(0, 20, 0) * Time.deltaTime * speed);
            }
            if (valeurBouger.x < 0)
            {
                transform.Rotate(new Vector3(0, -20, 0) * Time.deltaTime * speed);
            }
        
        if(valeurSaut > 0 && estSurSol == true)
        {
            estSurSol = false;
            rb.AddForce(new Vector3(0, 80, 0), ForceMode.Impulse);
        }

        VthirdPersonController.transform.eulerAngles = new Vector3(pitch, yaw, 0f); // Mouvement cam
        Cinemachine.CinemachineFreeLook vCamMontureCamera = vCamMonture.GetComponent<Cinemachine.CinemachineFreeLook>();

        vCamMontureCamera.m_XAxis.Value += rotateHorizontal * 8f;
        vCamMontureCamera.m_YAxis.Value += rotateVertical * 0.1f;

        vCamMontureCamera.m_XAxis.Value += MouvementCameraSourisX;
    }
}
