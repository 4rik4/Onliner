using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PlayerManager : MonoBehaviour
{
    private PhotonView _view;
    private GameObject _playerObject;

    void Start()
    {
        _view = GetComponent<PhotonView>();
        CreatePlayerController();
    }

    private void CreatePlayerController()
    {
        if (_view.IsMine)
        {
            _playerObject = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        }
    }

    private void RespawnPlayer(float health)
    {
        PhotonNetwork.Destroy(_playerObject);
        CreatePlayerController();
    }

    private void CheckPlayerHealth(float health)
    {
        if (health <= 0)
        {
            RespawnPlayer(health);
        }
    }
}
