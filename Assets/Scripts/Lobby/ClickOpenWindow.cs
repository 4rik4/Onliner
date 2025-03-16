using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOpenWindow : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private MenuController.WindowType windowType;

    public void OnPointerClick(PointerEventData eventData)
    {
        MenuController.menu.OpenWindow(windowType);
    }
}
