using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseFocus : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (EventSystem.current.currentSelectedGameObject)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

}
