using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarteScript : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraMap;
    [SerializeField]
    private ProjetP3DScene1.main main;
    private GameObject joueurGameObject;
    [SerializeField]
    private GameObject mapIcone;
    [SerializeField]
    private GameObject positionPersonnage;

    // Start is called before the first frame update
    void Start()
    {
        joueurGameObject = main.JoueurManager.GetComponent<JoueurManagerScript>().JoueursGameObject[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf == true && joueurGameObject != null) // Si la map est active, on autorise le mouvement sur la map
        {
            Vector2 valeurBouger = joueurGameObject.GetComponent<PlayerInput>().actions["BougerVersHautBas"].ReadValue<Vector2>();
            Vector2 stickDroitZoom = joueurGameObject.GetComponent<PlayerInput>().actions["Zoom"].ReadValue<Vector2>();
            Vector2 scrollZoom = Input.mouseScrollDelta;

            // Debug.Log(cameraMap.transform.position.x);

            // Debug.Log(mapIcone.transform.position.x);

            if (Input.GetMouseButton(0))
            {
                // GameObject curseurGameObject = positionPersonnage;
                // curseurGameObject.transform.position = Input.mousePosition;
                Ray ray = cameraMap.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition); // Si l'utilisateur a utilisé la souris pour se déplacer, on déplace le curseur là ou est la souris
                RaycastHit hit;
                var layerMask = 1 << 10;

                if (Physics.Raycast(ray, out hit, 900, ~layerMask))
                {
                    positionPersonnage.transform.position = hit.point;
                }
            }

                if (valeurBouger.y > 0) // Si stick vers le haut, la caméra va vers le haut
            {
                cameraMap.transform.Translate(new Vector3(0, valeurBouger.y * 53 * Time.deltaTime, 0));
                positionPersonnage.transform.Translate(new Vector3(0, valeurBouger.y * 53 * Time.deltaTime, 0));
            }
            if (valeurBouger.y < 0) // Si stick vers le bas, la caméra va vers le bas
            {
                cameraMap.transform.Translate(new Vector3(0, valeurBouger.y * 53 * Time.deltaTime, 0));
                positionPersonnage.transform.Translate(new Vector3(0, valeurBouger.y * 53 * Time.deltaTime, 0));
            }
            if (valeurBouger.x > 0)
            {
                cameraMap.transform.Translate(new Vector3(valeurBouger.x * 53 * Time.deltaTime, 0, 0));
                positionPersonnage.transform.Translate(new Vector3(valeurBouger.x * 53 * Time.deltaTime, 0, 0));
            }
            if (valeurBouger.x < 0)
            {
                cameraMap.transform.Translate(new Vector3(valeurBouger.x * 53 * Time.deltaTime, 0, 0));
                positionPersonnage.transform.Translate(new Vector3(valeurBouger.x * 53 * Time.deltaTime, 0, 0));
            }
            if (scrollZoom.y > 0 || stickDroitZoom.y > 0)
            {
                cameraMap.transform.Translate(new Vector3(0, 0, 1 * 353 * Time.deltaTime));
            }
            if (scrollZoom.y < 0 || stickDroitZoom.y < 0)
            {
                cameraMap.transform.Translate(new Vector3(0, 0, 1 * -353 * Time.deltaTime));
            }
        }
    }
}
