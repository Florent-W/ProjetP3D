using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VilleMapInteraction : MonoBehaviour
{
    [SerializeField]
    private ProjetP3DScene1.main main;

    // Start is called before the first frame update
    void Start()
    {

    }

    void test() { 
}


    // Update is called once per frame
    void Update()
    {
        /*
        Ray ray = this.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        var layerMask = 1 << 17;

        RaycastHit[] hitInfo = Physics.RaycastAll(ray.origin, ray.direction, 500, layerMask);
        foreach (RaycastHit h in hitInfo)
        {
            Debug.Log(h.collider.name);
        }
        */
        /*
        if (Input.GetMouseButton(0))
        {
            // Ray ray = this.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            GameObject curseurGameObject = main.mapGameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
            curseurGameObject.transform.position = Input.mousePosition; // Si l'utilisateur a utilisé la souris pour se déplacer, on déplace le curseur là ou est la souris

            Ray ray = this.GetComponent<Camera>().ScreenPointToRay(curseurGameObject.transform.position); // Si le curseur est sur l'icone de ville, on peut le téléporter

            var layerMask = 1 << 17;

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 900, layerMask)) // Si il y a bien eu les conditions requises, on peut téléporter le joueur
            {
                curseurGameObject.GetComponent<Image>().color = Color.green;

                Debug.Log(hit.collider.transform.position);
                main.JoueurManager.JoueursGameObject[0].transform.position = hit.collider.gameObject.transform.position;
            }
        }
        */
        /*
        if (Input.GetMouseButton(0))
        {
            GameObject curseurGameObject = main.mapGameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
            curseurGameObject.transform.position = Input.mousePosition; // Si l'utilisateur a utilisé la souris pour se déplacer, on déplace le curseur là ou est la souris
        }
            Ray ray = this.GetComponent<Camera>().ScreenPointToRay(curseurGameObject.transform.position);

            var layerMask = 1 << 17;

            Debug.Log(this.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition));

            RaycastHit2D hit = Physics2D.Raycast(this.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log(hit.transform.position);
            }
        }
        */
    }
}
