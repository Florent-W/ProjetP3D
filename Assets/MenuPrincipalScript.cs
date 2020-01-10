using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPrincipalScript : MonoBehaviour
{
    public GameObject boutonNouvellePartie;

    // Start is called before the first frame update
    void Start()
    {
        boutonNouvellePartie.GetComponent<Button>().Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
