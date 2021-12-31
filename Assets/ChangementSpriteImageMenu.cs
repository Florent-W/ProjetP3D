using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangementSpriteImageMenu : MonoBehaviour
{
    private Animator imageMenuController;
    private float tempsPasserSprite;
    private int nombreBouclePasserSprite = 0;
    private float tempsBoucle = 0;
    [SerializeField]
    private List<Sprite> dresseurImage;
    [SerializeField]
    private List<Sprite> premierPokemonImage;
    [SerializeField]
    private List<Sprite> deuxiemePokemonImage;
    private int numeroEquipeActuel = 0;
    // Start is called before the first frame update
    void Start()
    {
        imageMenuController = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (imageMenuController.GetCurrentAnimatorStateInfo(0).IsName("DeplacementImageMenu")) // Si le menu est en train de se déplacer, à la fin de la boucle, on changera les sprite
        {
            if (tempsBoucle == 0)
            {
                tempsBoucle = imageMenuController.GetCurrentAnimatorStateInfo(0).length; // Temps d'une boucle
            }

            tempsPasserSprite += Time.deltaTime; // Temps depuis qu'on a changer les sprite


            if (tempsPasserSprite > tempsBoucle * nombreBouclePasserSprite) // Si on passe une boucle, on augmente le compteur
            {
                nombreBouclePasserSprite++;
            }

            if (nombreBouclePasserSprite == 3) // On change de sprite
            {
                numeroEquipeActuel++;
                if (numeroEquipeActuel >= dresseurImage.Count)
                {
                    numeroEquipeActuel = 0;
                }

                this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = dresseurImage[numeroEquipeActuel];
                this.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = premierPokemonImage[numeroEquipeActuel];
                this.gameObject.transform.GetChild(2).GetComponent<Image>().sprite = deuxiemePokemonImage[numeroEquipeActuel];
                nombreBouclePasserSprite = 0;
                tempsPasserSprite = 0;
            }

        }
    }
}
