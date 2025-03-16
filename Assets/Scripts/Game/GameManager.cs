using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        PhotonNetwork.Instantiate("Player Manager", Vector3.zero, Quaternion.identity);
    }
}

public interface IDamageable
{
    public void TakeDamage(int damage);
}