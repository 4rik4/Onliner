using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class NicknameInputController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_InputField _nicknameInputField;

    public void OnPointerClick(PointerEventData eventData)
    {
        string nickname = _nicknameInputField.text;

        if (string.IsNullOrWhiteSpace(nickname))
            nickname = "Player" + Random.Range(10000, 99999);

        PhotonNetwork.NickName = nickname;
        MenuController.menu.OpenWindow(MenuController.WindowType.LobbyMenu);
    }
}
