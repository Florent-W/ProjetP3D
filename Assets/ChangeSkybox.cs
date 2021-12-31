using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkybox : MonoBehaviour
{
    [SerializeField]
    private Material skyboxJour, skyboxNuit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((this.gameObject.GetComponent<TempsUI>().heures >= 20 || this.gameObject.GetComponent<TempsUI>().heures < 8) && RenderSettings.skybox != skyboxNuit)
        {
            RenderSettings.skybox = skyboxNuit;
            RenderSettings.ambientLight = new Color(120 / 255f, 120 / 255f, 120 / 255f);
        }
        else if(this.gameObject.GetComponent<TempsUI>().heures >= 8 && this.gameObject.GetComponent<TempsUI>().heures < 20 && RenderSettings.skybox != skyboxJour)
        {
            RenderSettings.skybox = skyboxJour;
            RenderSettings.ambientLight = new Color(236 / 255f, 236 / 255f, 236 / 255f);
        }
    }
}
