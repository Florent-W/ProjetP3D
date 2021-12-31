using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MinimapIconeMouvementJoueurScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Joueur;
    Quaternion rotation;

    private void Start()
    {
        rotation = this.gameObject.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 valeurBouger = this.gameObject.transform.parent.gameObject.GetComponent<PlayerInput>().actions["BougerVersHautBas"].ReadValue<Vector2>(); // Valeur du stick gauche
        string nomSpriteIcone = this.gameObject.GetComponent<SpriteRenderer>().sprite.name; // On compare le nom du sprite pour voir si il y a besoin de le remplacer
        int directionAnimation = this.gameObject.GetComponent<Animator>().GetInteger("Direction"); // Direction ou va le personnage

            // On change l'icone du personnage selon la direction du stick
            // this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Minimap/Red up");
            if (directionAnimation != 1 && valeurBouger.y > 0 && valeurBouger.x == 0) // Si le personnage bouge vers le haut
            {
                if (this.gameObject.GetComponent<Animator>().enabled == false)
                {
                    this.gameObject.GetComponent<Animator>().enabled = true;
                }
                this.gameObject.GetComponent<Animator>().SetInteger("Direction", 1);
            }
            else if(directionAnimation != 2 && valeurBouger.y < 0 && valeurBouger.x == 0)
            {
                if (this.gameObject.GetComponent<Animator>().enabled == false)
                {
                    this.gameObject.GetComponent<Animator>().enabled = true;
                }
                this.gameObject.GetComponent<Animator>().SetInteger("Direction", 2);
            }
            else if (directionAnimation != 3 && valeurBouger.y == 0 && valeurBouger.x < 0)
            {
                if (this.gameObject.GetComponent<Animator>().enabled == false)
                {
                    this.gameObject.GetComponent<Animator>().enabled = true;
                }
                this.gameObject.GetComponent<Animator>().SetInteger("Direction", 3);
            }
            else if (directionAnimation != 4 && valeurBouger.y == 0 && valeurBouger.x > 0)
            {
                if (this.gameObject.GetComponent<Animator>().enabled == false)
                {
                    this.gameObject.GetComponent<Animator>().enabled = true;
                }
                this.gameObject.GetComponent<Animator>().SetInteger("Direction", 4);
            }
            else if(directionAnimation != 0 && valeurBouger.y == 0 && valeurBouger.x == 0)
            {
                this.gameObject.GetComponent<Animator>().SetInteger("Direction", 0);
                this.gameObject.GetComponent<Animator>().enabled = false;

                if (directionAnimation == 1)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Minimap/Red up");
                }
                else if (directionAnimation == 2)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Minimap/Red down");
                }
                else if (directionAnimation == 3)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Minimap/Red left");
                }
                else if(directionAnimation == 4)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Minimap/Red right");
                }

               // this.gameObject.GetComponent<Animator>().enabled = true;
        }
        /*
        if (valeurBouger.y < 0 && valeurBouger.x == 0 && nomSpriteIcone != "Red down")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Minimap/Red down");
        }
        if (valeurBouger.x > 0 && valeurBouger.y == 0 && nomSpriteIcone != "Red right")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Minimap/Red right");
        }
        if (valeurBouger.x < 0 && valeurBouger.y == 0 && nomSpriteIcone != "Red left")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Minimap/Red left");
        }
        */
        this.gameObject.transform.localRotation = rotation;
        // this.gameObject.transform.eulerAngles = new Vector3(this.gameObject.transform.eulerAngles.x, Joueur.transform.eulerAngles.y, this.gameObject.transform.eulerAngles.z);
    }
}
