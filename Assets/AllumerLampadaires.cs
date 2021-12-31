using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet d'allumer les lampadaires la nuit
public class AllumerLampadaires : MonoBehaviour
{
    public TempsUI tempsUIScript;
    [SerializeField]
    private Light lumiere;

    // Start is called before the first frame update
    void Start()
    {
       // tempsUIScript = GameObject.Find("UItemps").GetComponent<TempsUI>();
    }

    // Update is called once per frame
    void Update()
    {
         // Si la lumière n'est pas active et que c'est la nuit, on active la lumière
         if ((tempsUIScript.heures >= 20 || tempsUIScript.heures < 8) && lumiere.gameObject.activeSelf == false)
         {
            lumiere.gameObject.SetActive(true);
         }
        else if(tempsUIScript.heures >= 8 && tempsUIScript.heures < 20 && lumiere.gameObject.activeSelf == true) // On la ferme si c'est le jour
        {
             lumiere.gameObject.SetActive(false);
        }
    }
}
