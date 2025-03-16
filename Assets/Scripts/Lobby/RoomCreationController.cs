using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public class RoomCreationController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_InputField _inputField;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (string.IsNullOrWhiteSpace(_inputField.text))
            return;

        PhotonNetwork.CreateRoom(_inputField.text);
    }
}
