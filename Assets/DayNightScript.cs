using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightScript : MonoBehaviour
{
    public Light Lumiere;
    public float Vitesse = 10f;
    [SerializeField]
    private TempsUI tempsUI;

    // Start is called before the first frame update
    void Start()
    {
        Lumiere = GetComponent<Light>();
        InvokeRepeating("rafraichitUneSeconde", 0f, 1f / 60); // Pour que les ombres se déplacent
    }

    void rafraichitUneSeconde()
    {
        Lumiere.transform.eulerAngles = new Vector3((((((float)tempsUI.heures * 60f * 60f) + ((float)tempsUI.minutes * 60f) + (float)tempsUI.secondes) / (24f * 60f * 60f)) * 360) - 110f, 0, 0); // Déplace la lumière
    }

    // Update is called once per frame
    void Update()
    {
       // Lumiere.transform.Rotate(Vector3.right * Vitesse * Time.deltaTime);
    }
}
