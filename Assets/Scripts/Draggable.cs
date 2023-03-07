using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler
{
    /// <summary>
    /// Logic for when a sprite is being dragged
    /// </summary>
    /// <param name="eventData">Data related to the current event</param>
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
}
