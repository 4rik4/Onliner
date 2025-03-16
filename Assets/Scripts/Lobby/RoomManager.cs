using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _playerNamePrefab;
    [SerializeField] private Transform _playerListTransform;

    [SerializeField] private TextMeshProUGUI _roomNameText;
    [SerializeField] private GameObject _startGameButton;

    private List<Player> _players;

    public override void OnEnable()
    {
        base.OnEnable();
        _roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        _players = new List<Player>(PhotonNetwork.CurrentRoom.Players.Values);
        RefreshPlayers();

        if (PhotonNetwork.IsMasterClient)
            _startGameButton.SetActive(true);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void RefreshPlayers()
    {
        foreach (Transform child in _playerListTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (Player player in _players)
        {
            GameObject playerText = Instantiate(_playerNamePrefab, _playerListTransform);
            playerText.GetComponent<TextMeshProUGUI>().text = player.NickName;
        }

        Debug.Log("Refreshed players!");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined");
        _players.Add(newPlayer);
        RefreshPlayers();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + " left");
        _players.Remove(otherPlayer);
        RefreshPlayers();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.IsMasterClient)
            _startGameButton.SetActive(true);
    }
}
