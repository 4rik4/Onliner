using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayersOnlineCounter : MonoBehaviour
{
    private int _playersOnline = 0;
    private TextMeshProUGUI _playersOnlineText;

    private IEnumerator RefreshPlayersOnline()
    {
        while (true)
        {
            _playersOnline = PhotonNetwork.CountOfPlayers;
            _playersOnlineText.text = "Players online: " + _playersOnline;

            yield return new WaitForSeconds(5.1f);
        }
    }

    private void OnEnable()
    {
        _playersOnlineText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(RefreshPlayersOnline());
    }
}
