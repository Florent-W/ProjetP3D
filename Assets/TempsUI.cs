using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempsUI : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI temps;
    public int timer, secondes, minutes, heures;

    // Start is called before the first frame update
    void Start()
    {
        // temps.text = System.DateTime.Now.Hour.ToString("d2") + ":" + System.DateTime.Now.Minute.ToString("d2");
        temps.text = heures.ToString("d2") + ":" + minutes.ToString("d2");
        InvokeRepeating("ajouteUneSeconde", 0f, 1f / 60);
       // InvokeRepeating("ajouteUneMinute", 1f, 1f);
        // dayNightScript.Lumiere.transform.eulerAngles = new Vector3(Vector3.right * ((float) heures * (float) minutes) * (360f / (24f * 60f)), 0, 0);        
        // dayNightScript.Lumiere.transform.eulerAngles = Vector3.right * ((float)heures * (float)minutes) * (360f / (24f * 60f));
    }
    /*
    void ajouteUneSeconde()
    {
        secondes += 1;
        if(secondes >= 60)
        {
            secondes = 0;
        }
    }
    */

    void ajouteUneSeconde()
    {
        secondes += 1;
        if (secondes >= 60)
        {
            secondes = 0;

            minutes += 1;
            // dayNightScript.Lumiere.transform.Rotate(Vector3.right * (360f / (24f * 60f))); // Avance la lumière
            // dayNightScript.Lumiere.transform.Rotate(Vector3.right * (1f / 1440f) * 360f);

            if (minutes >= 60)
            {
                minutes = 0;
                heures += 1;

                if (heures >= 24)
                {
                    heures = 0;
                }

            }
            temps.text = heures.ToString("d2") + ":" + minutes.ToString("d2");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // dayNightScript.Lumiere.transform.eulerAngles = new Vector3(((1 * ((float)heures / 24 * (float)minutes / 60) * 360f)), 0, 0);
    }
}
