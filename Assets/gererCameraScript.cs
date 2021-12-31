using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gererCameraScript : MonoBehaviour
{
    public GameObject CMVCam1;
    public GameObject CMVCam2;
    public GameObject CMVCam3;
    public GameObject CMVCam4;

    // Start is called before the first frame update
    void Start()
    {
        CMVCam1 = GameObject.Find("CM vcam1");
        CMVCam2 = GameObject.Find("CM vcam2");
        CMVCam3 = GameObject.Find("CM vcam3");
        CMVCam4 = GameObject.Find("CM vcam4");

        CMVCam1.SetActive(false);
        CMVCam2.SetActive(false);
        CMVCam3.SetActive(false);
        CMVCam4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
