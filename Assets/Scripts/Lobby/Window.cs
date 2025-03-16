using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    [field: SerializeField] public MenuController.WindowType WindowType { get; private set; }
}
