using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] TeamPlayersManager gameManager;
    public GameObject playerPrefab;
    private GameObject spawnedPlayer;

    public float minX;
    public float maxX;

    public float minY;
    public float maxY;

    void Start()
    {
        Vector2 randomPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        spawnedPlayer = PhotonNetwork.Instantiate(playerPrefab.name, randomPos, Quaternion.identity);
        TeamPlayersManager.onlinePlayerCount += 1;
        if (TeamPlayersManager.onlinePlayerCount == 1)
        {
            gameManager.seekers1.Add(spawnedPlayer.GetComponent<PlayerLogicManager>());
        }
        else if (TeamPlayersManager.onlinePlayerCount == 2)
        {
            gameManager.seekers2.Add(spawnedPlayer.GetComponent<PlayerLogicManager>());
        }
    }
}