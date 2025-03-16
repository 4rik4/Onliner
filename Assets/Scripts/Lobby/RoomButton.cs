using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomButton : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI _buttonText;
    private string _buttonName;
    private RoomInfo _roomInfo;

    public void OnPointerClick(PointerEventData eventData)
    {
        PhotonNetwork.JoinRoom(_roomInfo.Name);
    }

    public void SetUp(RoomInfo roomInfo)
    {
        _buttonName = roomInfo.Name;
        _roomInfo = roomInfo;
    }

    void Start()
    {
        _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        _buttonText.text = _buttonName;
    }
}
