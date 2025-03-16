using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class ConnectionController : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void ConnectToRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnectedToMaster()
    {
        MenuController.menu.OpenWindow(MenuController.WindowType.Load);
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        if (string.IsNullOrEmpty(PhotonNetwork.NickName))
            MenuController.menu.OpenWindow(MenuController.WindowType.ChooseNickname);
        else
            MenuController.menu.OpenWindow(MenuController.WindowType.LobbyMenu);
        Debug.Log("Connected to lobby");
    }

    public override void OnJoinedRoom()
    {
        MenuController.menu.OpenWindow(MenuController.WindowType.Room);
    }

    public override void OnLeftRoom()
    {
        MenuController.menu.OpenWindow(MenuController.WindowType.LobbyMenu);
    }
}
