using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBall : MonoBehaviour
{
    public Transform destination;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void OnMouseUp()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = destination.position;
            this.transform.parent = GameObject.Find("Destination").transform;
        }
        else if ((Input.GetKeyDown(KeyCode.G)))
        {
            GetComponent<CapsuleCollider>().enabled = true;
            this.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
        }
        this.gameObject.SetActive(true);

    }
}
