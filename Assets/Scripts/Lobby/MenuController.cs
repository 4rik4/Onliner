using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController menu;
    [SerializeField] private List<Window> windows;

    public enum WindowType
    {
        Load = 0,
        LobbyMenu = 1,
        CreateRoom = 2,
        Room = 3,
        ConnectToRoom = 4,
        ChooseNickname = 5,
        Quit = 6,
    }

    public void OpenWindow(WindowType windowType)
    {
        foreach (Window window in windows)
        {
            if (windowType == WindowType.Quit)
            {
                Application.Quit();
                return;
            }

            if (window.WindowType == windowType)
                window.gameObject.SetActive(true);
            else
                window.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        menu = this;
    }
}
