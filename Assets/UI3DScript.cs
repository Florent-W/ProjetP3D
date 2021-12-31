using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI3DScript : MonoBehaviour
{
    public GameObject cameraCombat;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void createUI3D()
    {
     //   text3D =  this.gameObject.GetComponent<ProjetP3DScene1.main>().CreateText3D(CanvasWorldSpace.transform, "NomPokemon3D", 250, 250, Bulbizarre.transform.position.x, Bulbizarre.transform.position.y, Bulbizarre.transform.position.z, "bbbbbbbb", 14, 14, 14, Color.black);
    }

    public void createUI()
    {
      //  text.transform.position = Bulbizarre.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.gameObject.transform.LookAt(cameraCombat.GetComponent<Camera>().transform);
    }
}
