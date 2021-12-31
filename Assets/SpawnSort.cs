using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script qui va permettre de faire apparaitre les sortilèges
public class SpawnSort : MonoBehaviour
{
    public GameObject rangedSpellPrefab;
    public GameObject selectedUnit;
    [SerializeField]
    private vThirdPersonCamera vThirdPersonCamera;
    private Transform cible;

    // Start is called before the first frame update
    void Start()
    {
        vThirdPersonCamera = GameObject.Find("vThirdPersonController").GetComponent<vThirdPersonCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(selectedUnit != null) // Si la cible existe et que la distance est suffisante, on fait pivoter la caméra pour voir la cible
        {
           // cible = selectedUnit.transform.Find("Cible");
           // cible.RotateAround(selectedUnit.transform.position, Vector3.up, 20 * Time.deltaTime);
          // cible.LookAt(vThirdPersonCamera.transform);

            float distance = Vector3.Distance(selectedUnit.transform.position, vThirdPersonCamera.transform.position);
            if (distance < 21)
            {
                vThirdPersonCamera.transform.LookAt(new Vector3(selectedUnit.transform.position.x, selectedUnit.transform.position.y + 1, selectedUnit.transform.position.z)); // Pour que la caméra regarde la cible
            }
            else // Sinon on enlève la cible
            {
                RemoveTarget();
            }
        }

        if(Input.GetKeyDown("2"))
        {
            RangedSpell();
        }

        if(Input.GetMouseButtonDown(0))
        {
            SelectClosestTarget();
        }
        if (Input.GetMouseButtonDown(1))
        {
            RemoveTarget();
        }
    }

    public void RangedSpell()
    {
        Vector3 postionSort = new Vector3(this.transform.position.x, this.transform.position.y + 5, this.transform.position.z);
        GameObject sort = Instantiate(rangedSpellPrefab, postionSort, Quaternion.identity); // On spawn le sort

        sort.transform.GetComponent<RangedSpell>().Target = selectedUnit;
    }

    // On enlève la cible
    public void RemoveTarget()
    {
        if (selectedUnit != null)
        {
            cible = selectedUnit.transform.Find("Cible");
            if (cible != null)
            { // On enlève l'ancienne cible 
                Destroy(cible.gameObject);
            }
            selectedUnit = null; // On remet à zéro la cible selectionnée        
        }

    }

    public void SelectTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 10000))
        {
            if (hit.transform.gameObject.transform.parent.gameObject.tag == "PokemonAdverse") // On prend le pokémon adverse le plus proche
            {
                selectedUnit = hit.transform.gameObject;
            }
        }
    }

    public void SelectClosestTarget()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("PokemonAdverse"); // On cherche l'ennemi le plus proche
        int numeroClosest = -1; // -1 pas assigné
        int i = 0; // Nombre d'itération
        float distance = 441;
        Vector3 position = transform.position;
        foreach(GameObject go in gos) // On cherche le gameobject le plus proche
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance < distance)
            {
                numeroClosest = i;
                distance = curDistance;
            }
            i++;
        }
        if (numeroClosest >= 0) // On regarde que ça a bien été assigné
        {
            if (selectedUnit != gos[numeroClosest]) { // On regarde si ce n'est pas le même 
                if (selectedUnit != null)
                {
                    cible = selectedUnit.transform.Find("Cible");
                    if (cible != null)
                    { // On enlève l'ancienne cible 
                        Destroy(cible.gameObject);
                    }
                }

                selectedUnit = gos[numeroClosest];

                GameObject spriteGameObject = new GameObject("Cible");
                spriteGameObject.transform.parent = selectedUnit.transform;
                spriteGameObject.transform.localPosition = new Vector3(0, 3.5f, 0);
                spriteGameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
                spriteGameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                SpriteRenderer spriteRenderer = spriteGameObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = Resources.Load<Sprite>("Images/Menu/Cible_Icone"); // Ajout d'une cible pour montrer l'ennemi
                                                                                     // vThirdPersonCamera.SetTarget(selectedUnit.transform);
              // vThirdPersonCamera.transform.LookAt(selectedUnit.transform); // Pour que la caméra regarde la cible
            }
        }
    }
}
