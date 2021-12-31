using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PersonnageControleScript : MonoBehaviour
{
    private string moveInputAxis = "Vertical";
    private string moveTurnAxis = "Horizontal";

    public float rotationRate = 360;

    public float moveSpeed = 10;
    private ControleJoueur controls;

    private Rigidbody rb;

    public float moveAxis, turnAxis;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        controls = new ControleJoueur();
        controls.Joueur.BougerVersHautBas.performed += ctx => GetAxis();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Bouger()
    {
        Debug.Log("test" + controls.Joueur.BougerVersHautBas.ReadValue<Vector2>());
    }

    // Update is called once per frame
    void Update()
    {
       // Vector2 mouvementVector2 = controls.Joueur.BougerVersHautBas.ReadValue<Vector2>();
      //  Vector3 mouvement = new Vector3();
      //  mouvement.x = mouvementVector2.x;
      //  mouvement.z = mouvementVector2.y;

      //  float mouvement = controls.Joueur.BougerVersHautBas.ReadValue<float>();

      //  Debug.Log(mouvement.ToString());

     //   float moveAxis = controls.Joueur.BougerVersHautBas.ReadValue<float>();
        float moveAxis = Input.GetAxis(moveInputAxis);
        float turnAxis = Input.GetAxis(moveTurnAxis);

        ApplyInput(moveAxis, turnAxis);
    }

    void GetAxis()
    {
      //  Vector2 moveAxis = controls.Joueur.BougerVersHautBas.ReadValue<Vector2>();
        float moveAxis = Input.GetAxis(moveInputAxis);
        float turnAxis = Input.GetAxis(moveTurnAxis);

       // Debug.Log(moveAxis + "" + controls.Joueur.BougerVersHautBas.ReadValue<Vector2>());
    }

    private void ApplyInput(float moveInput, float turnInput)
    {
        Move(moveInput);
        Turn(turnInput);
    }

    private void Move(float input)
    {
        // transform.Translate(Vector3.forward * input * moveSpeed);
        rb.AddForce(transform.forward * input * moveSpeed, ForceMode.Force);
    }

    private void Turn(float input)
    {
        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
    }
}
