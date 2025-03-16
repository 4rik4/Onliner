using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomsController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _roomButtonPrefab;
    [SerializeField] private Transform _roomListTransform;
    private List<RoomInfo> _roomList = new List<RoomInfo>();

    private void RefreshRooms()
    {
        foreach (Transform child in _roomListTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (RoomInfo room in _roomList)
        {
            GameObject button = Instantiate(_roomButtonPrefab, _roomListTransform);
            button.GetComponent<RoomButton>().SetUp(room);
        }

        Debug.Log("Refreshed rooms!");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        _roomList = new List<RoomInfo>();

        foreach (RoomInfo room in roomList)
        {
            if (room.RemovedFromList == false)
                _roomList.Add(room);
        }

        Debug.Log("Room list updated");
        RefreshRooms();
    }
}
