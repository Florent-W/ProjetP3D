using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Script qui va changer les sprites d'un personnage en fonction  de a
public class SpriteDeplacement3D : MonoBehaviour
{
    private string direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
      //  if (this.gameObject.activeSelf == true && joueurGameObject != null) // Si la map est active, on autorise le mouvement sur la map
      //  {
            Vector2 valeurBouger = this.gameObject.transform.parent.gameObject.transform.parent.GetComponentInParent<PlayerInput>().actions["BougerVersHautBas"].ReadValue<Vector2>();
            SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

            if (valeurBouger.y > 0 && direction != "Haut") // Si stick vers le haut et on regarde si on est pas déjà en train de déplacer en haut le personnage, le sprite va vers le haut
            {
                if (DonneesChargement.nomSpriteHaut != null) // On ne change pas de sprite si ce sprite n'a pas été mis dans l'éditeur
                {
                    spriteRenderer.sprite = Resources.Load<Sprite>("Images/PersonnagesSprites/" + DonneesChargement.nomSpriteHaut);
                }
                spriteRenderer.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0); // On change l'angle pour qu'on le voit
                direction = "Haut";
            }
            if (valeurBouger.y < 0 && direction != "Bas") // Si stick vers le bas, la sprite va vers le bas
            {
                spriteRenderer.sprite = Resources.Load<Sprite>("Images/PersonnagesSprites/" + DonneesChargement.nomSprite);
                spriteRenderer.gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0); // On change l'angle pour qu'on le voit
                direction = "Bas";
            }
            if (valeurBouger.x > 0 && direction != "Droite")
            {
                if (DonneesChargement.nomSpriteDroite != null) // On ne change pas de sprite si ce sprite n'a pas été mis dans l'éditeur
                {
                    spriteRenderer.sprite = Resources.Load<Sprite>("Images/PersonnagesSprites/" + DonneesChargement.nomSpriteDroite);
                }
                spriteRenderer.gameObject.transform.localRotation = Quaternion.Euler(0, 270, 0); // On change l'angle pour qu'on le voit
                direction = "Droite";
            }
            if (valeurBouger.x < 0 && direction != "Gauche")
            {
                if (DonneesChargement.nomSpriteGauche != null) // On ne change pas de sprite si ce sprite n'a pas été mis dans l'éditeur
                {
                    spriteRenderer.sprite = Resources.Load<Sprite>("Images/PersonnagesSprites/" + DonneesChargement.nomSpriteGauche);
                }
                spriteRenderer.gameObject.transform.localRotation = Quaternion.Euler(0, 90, 0); // On change l'angle pour qu'on le voit
                direction = "Gauche";
        }
    }
  //  }
}
