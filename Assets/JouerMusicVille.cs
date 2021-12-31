using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JouerMusicVille : MonoBehaviour
{
    // Va servir à lancer une musique quand on rentre dedans
    [SerializeField]
    private AudioClip musiqueVille;

    private void OnTriggerEnter(Collider other)
    {
        if (DonneesChargement.musiquePersonnage == null) // Si ce n'est pas un personnage qui a une musique, on pourra mettre de la musique
        {
            if (other.name == "vThirdPersonCamera" || other.name == "vThirdPersonCameraJoueur2") // Déclenchement de la musique si un des joueurs rentre dans une ville
            {
                AudioSource GameObjectMusiqueAudioSource = this.gameObject.transform.parent.gameObject.GetComponent<VillesScript>().gameObjectMusiqueAudioSource;

                if (GameObjectMusiqueAudioSource.isPlaying == true && GameObjectMusiqueAudioSource.clip != musiqueVille)
                { // Si une musique est lancé, on la coupe
                    GameObjectMusiqueAudioSource.Stop();
                }

                if (GameObjectMusiqueAudioSource.clip != musiqueVille)
                {
                    GameObjectMusiqueAudioSource.clip = musiqueVille;
                    GameObjectMusiqueAudioSource.Play();
                }
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
