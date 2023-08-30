using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    private GameObject spawnedPlayer;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        spawnedPlayer = PhotonNetwork.Instantiate(playerPrefab.name, transform.position, transform.rotation);

    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayer);
    }
}
