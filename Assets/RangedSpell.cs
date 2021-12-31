using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSpell : MonoBehaviour
{
    public GameObject Target;
    private ProjetP3DScene1.main main;

    private void Start()
    {
        main = GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Target != null)
        {
            Vector3 targetPosition = new Vector3(Target.transform.position.x, Target.transform.position.y, Target.transform.position.z);
            this.transform.LookAt(targetPosition);

            float distance2 = Vector3.Distance(Target.transform.position, this.transform.position);

            if(distance2 > 2f)
            {
                transform.Translate(Vector3.forward * 30.0f * Time.deltaTime);
            }
            else
            {
                HitTarget();
            }
        }
    }

    void HitTarget() {
        Target.GetComponent<StatistiquesPokemon>().GetPokemon().attaqueAvecSort(main.JoueurManager.Joueurs[0].getListeSort()[0]);
        Debug.Log(Target.GetComponent<StatistiquesPokemon>().GetPokemon().getPvRestant());
        Destroy(this.gameObject);
    }
}
