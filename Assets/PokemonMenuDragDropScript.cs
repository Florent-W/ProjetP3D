using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PokemonMenuDragDropScript : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        this.gameObject.transform.position = Input.mousePosition;
    }
}
